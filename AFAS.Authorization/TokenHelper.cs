using AFAS.Authorization.Internals;
using AFAS.Authorization.Models;
using AFAS.Entity;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace AFAS.Authorization;

/// <summary>
/// RedisToken 工具类
/// </summary>
public class TokenHelper
{
    #region 根据Token获取TokenData

    /// <summary>
    /// 根据Token获取 UserTokenData
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<UserTokenData> GetUserTokenDataAsync(string userId, string token)
    {
        try
        {
            using (var context = new AfasContext())
            {
                var bUserToken = await context.BUserTokens.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (bUserToken != null)
                {
                    var userToken = JsonConvert.DeserializeObject<UserTokenData>(bUserToken.TokenData);
                    if (userToken != null && userToken.Token.Value == token)
                    {
                        //如果还剩一半的时间过期，续期
                        if (userToken.Token.ExpireTime - UnixTimeHelper.GetUnixSeconds() < AuthorizationParam.LoginExpires / 2)
                        {
                            userToken.Token.ExpireTime
                                = UnixTimeHelper.GetUnixSeconds(DateTime.Now.AddSeconds(AuthorizationParam.LoginExpires));
                            bUserToken.TokenData = JsonConvert.SerializeObject(userToken);
                            bUserToken.LoginExpires = AuthorizationParam.LoginExpires;
                            context.BUserTokens.Update(bUserToken);
                            await context.SaveChangesAsync();
                        }

                        return userToken;
                    }
                }
            }
            throw ForbiddenException.Get(MethodBase.GetCurrentMethod(),"Token失效");
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), "获取UserTokenData失败", ex);
        }
    }

    #endregion

    #region 操作Redis中的请求头数据

    /// <summary>
    /// 更新Redis中的用户身份信息
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static async Task RefreshRedisTokenAsync(string userId)
    {
        try
        {
            using (var context = new AfasContext())
            {
                var user = await context.BUsers.Where(x => x.UserId == GetString.FromObject(userId, 32)).FirstOrDefaultAsync();
                if (user ==null)
                {
                    return;
                }
                UserIdentity userIdentity = new(user);
                var expireTime = UnixTimeHelper.GetUnixSeconds(DateTime.Now.AddSeconds(AuthorizationParam.LoginExpires));
                var userToken = new UserTokenData()
                {
                    User = userIdentity,
                    Token = new Token()
                    {
                        ExpireTime = expireTime,
                        Value = NewCode.Token
                    },
                };
                var bUserToken = await context.BUserTokens.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (bUserToken != null)
                {
                    bUserToken.TokenData = JsonConvert.SerializeObject(userToken);
                    bUserToken.LoginExpires = AuthorizationParam.LoginExpires;
                    bUserToken.CreateStamp = DateHelper.GetDateString();
                    context.BUserTokens.Update(bUserToken);
                }
                else
                {
                    bUserToken = new BUserToken()
                    {
                        UserId = userIdentity.UserId,
                        TokenData = JsonConvert.SerializeObject(userToken),
                        LoginExpires = AuthorizationParam.LoginExpires,
                        CreateStamp = DateHelper.GetDateString()
                    };
                    context.BUserTokens.Add(bUserToken);
                }

                await context.SaveChangesAsync();
            }         
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), "更新用户身份信息失败", ex);
        }
    }

    /// <summary>
    /// 删除Redis中的用户身份信息
    /// </summary>
    /// <param name="userId"></param>
    public static async Task RemoveTokenAsync(string userId)
    {
        using (var context = new AfasContext())
        {
            var bUserToken = await context.BUserTokens.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (bUserToken != null)
            {
                context.BUserTokens.Remove(bUserToken);
                await context.SaveChangesAsync();
            }
        }
    }
    #endregion
}
