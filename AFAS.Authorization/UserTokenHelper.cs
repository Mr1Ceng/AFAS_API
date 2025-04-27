using AFAS.Authorization.Internals;
using AFAS.Authorization.Models;
using AFAS.Entity;
using Mr1Ceng.Util;
using Newtonsoft.Json;
using System.Reflection;

namespace AFAS.Authorization;

/// <summary>
/// 用户 Token 工具类
/// </summary>
public class UserTokenHelper
{
    /// <summary>
    /// 根据 UserId 生成 UserTokenData
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static async Task<UserTokenData> CreateUserTokenByUserIdAsync(Identifier identifier, string userId)
    {
        #region 数据检查

        if (userId == "")
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), "UserId不存在");
        }

        #endregion

        #region 获取用户信息

        var identity = UserIdentityHelper.GetUserIdentityByUserId(userId);
        if (identity.UserId == "")
        {
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "获取用户信息失败");
        }

        #endregion

        #region 构建 UserTokenData

        UserTokenData data = new()
        {
            User = identity,
            Token = new Token
            {
                Value = NewCode.Token,
                ExpireTime = UnixTimeHelper.GetUnixSeconds(DateTime.Now.AddSeconds(AuthorizationParam.LoginExpires)),
                TimeSpan = identifier.Terminal.TimeSpan
            }
        };

        #endregion

        #region 保存用户登录身份到数据库

        using (var context = new AfasContext())
        {

            var userToken = context.BUserTokens.Where(x => x.UserId == data.User.UserId).FirstOrDefault();
            if (userToken != null)
            {
                userToken.TokenData = JsonConvert.SerializeObject(data);
                userToken.LoginExpires = AuthorizationParam.LoginExpires;
                userToken.CreateStamp = DateHelper.GetDateString();
                context.BUserTokens.Update(userToken);
            }
            else
            {
                userToken = new BUserToken() { UserId = data.User.UserId };
                userToken.TokenData = JsonConvert.SerializeObject(data);
                userToken.LoginExpires = AuthorizationParam.LoginExpires;
                userToken.CreateStamp = DateHelper.GetDateString();
                context.BUserTokens.Add(userToken);
            }
            await context.SaveChangesAsync();
        };

        #endregion

        #region 写Token日志

        try //安全代码，不抛异常
        {
            using (var context = new AfasContext())
            {
                context.LogTokens.Add(new LogToken
                {
                    TimeStamp = DateHelper.GetDateString(),
                    TerminalId = identifier.Terminal.TerminalId,
                    AuthType = identifier.AuthType,
                    Token = data.Token.Value,
                    UserId = data.User.UserId,
                    UserName = data.User.UserName,
                    NickName = data.User.NickName,
                    Mobile = data.User.Mobile,
                    IsDeveloper = data.User.IsDeveloper,
                    IsStaff = data.User.IsStaff,
                    TokenData = JsonConvert.SerializeObject(data)
                });
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {

        }

        #endregion

        identifier.UserToken = data;
        return data;
    }

    /// <summary>
    /// 根据Mobile生成UserTokenData
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static async Task<UserTokenData> CreateUserTokenByMobileAsync(Identifier identifier, string mobile)
    {

        #region 数据检查

        if (mobile == "")
        {
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "Mobile不存在");
        }

        #endregion

        #region 获取用户信息

        var identity = UserIdentityHelper.GetUserIdentityByMobile(mobile);
        if (identity.UserId == "")
        {
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "获取用户信息失败");
        }

        #endregion

        #region 构建 UserTokenData

        UserTokenData data = new()
        {
            User = identity,
            Token = new Token
            {
                Value = NewCode.Token,
                ExpireTime = UnixTimeHelper.GetUnixSeconds(DateTime.Now.AddSeconds(AuthorizationParam.LoginExpires)),
                TimeSpan = identifier.Terminal.TimeSpan
            }
        };

        #endregion

        #region 保存用户登录身份到数据库

        using (var context = new AfasContext())
        {

            var userToken = context.BUserTokens.Where(x => x.UserId == data.User.UserId).FirstOrDefault();
            if (userToken != null)
            {
                userToken.TokenData = JsonConvert.SerializeObject(data);
                userToken.LoginExpires = AuthorizationParam.LoginExpires;
                userToken.CreateStamp = DateHelper.GetDateString();
                context.BUserTokens.Update(userToken);
            }
            else
            {
                userToken = new BUserToken() { UserId = data.User.UserId };
                userToken.TokenData = JsonConvert.SerializeObject(data);
                userToken.LoginExpires = AuthorizationParam.LoginExpires;
                userToken.CreateStamp = DateHelper.GetDateString();
                context.BUserTokens.Add(userToken);
            }
            await context.SaveChangesAsync();
        };

        #endregion

        #region 写Token日志

        try //安全代码，不抛异常
        {
            using (var context = new AfasContext())
            {
                context.LogTokens.Add(new LogToken
                {
                    TimeStamp = DateHelper.GetDateString(),
                    TerminalId = identifier.Terminal.TerminalId,
                    AuthType = identifier.AuthType,
                    Token = data.Token.Value,
                    UserId = data.User.UserId,
                    UserName = data.User.UserName,
                    NickName = data.User.NickName,
                    Mobile = data.User.Mobile,
                    IsDeveloper = data.User.IsDeveloper,
                    IsStaff = data.User.IsStaff,
                    TokenData = JsonConvert.SerializeObject(data)
                });
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {

        }

        #endregion

        identifier.UserToken = data;
        return data;
    }
}

