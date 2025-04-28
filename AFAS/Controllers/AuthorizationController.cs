using AFAS.Authorization;
using AFAS.Authorization.Models;
using AFAS.Entity;
using AFAS.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using Newtonsoft.Json;
using System.Reflection;
using WingWell.WebApi.Platform;

namespace AFAS.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = WebApiConfig.Authorization)]
    [Route("[controller]/[action]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(ILogger<AuthorizationController> logger)
        {
            _logger = logger;
        }


        private readonly int AUTHORIZATION_REDIS_EXPIRE_SECONDS = ParaHelper.GetLoginExpires();


        /// <summary>
        /// 获取TerminalAuthorization
        /// </summary>
        /// <param name="terminalId"></param>
        /// <returns></returns>

        [HttpPost("{terminalId}")]
        public ResponseModel<KeyValue<TerminalData>> GetTerminalAuthorization(string terminalId)
        {
            //校对TerminalId
            var terminal = TerminalHelper.GetTerminalInfo(terminalId);
            if (terminal.TerminalKey == "")
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "未知的TerminalId");
            }

            try
            {
                //生成Authorization
                var authorization = WebApiAuthorization.GetString(terminal.TerminalKey, terminal.TerminalSecret);
                return new ResponseModel<KeyValue<TerminalData>>(new KeyValue<TerminalData>
                {
                    Key = authorization,
                    Value = new TerminalData
                    {
                        TerminalId = terminal.TerminalId,
                        TerminalName = terminal.TerminalName,
                        TerminalType = terminal.TerminalType,
                        SystemId = terminal.SystemId
                    }
                });
            }
            catch (Exception ex)
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "【生成TerminalAuthorization失败】" + ex.Message);
            }
        }


        /// <summary>
        /// 获取UserTokenAuthorization
        /// </summary>
        /// <param name="terminalId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{terminalId}/{userId}")]
        public async Task<ResponseModel<KeyValue<UserTokenData>>> GetUserTokenAuthorizationAsync(string terminalId,
            string userId)
        {
            //校对UserId
            var identity = UserIdentityHelper.GetUserIdentityByUserId(userId, true);

            try
            {
                var userToken = new UserTokenData();
                using (var context = new AfasContext())
                {
                    var bUserToken = await context.BUserTokens.Where(x => x.UserId == identity.UserId).FirstOrDefaultAsync();
                    if (bUserToken != null) {
                        userToken = JsonConvert.DeserializeObject<UserTokenData>(bUserToken.TokenData);
                        if (userToken != null)
                        {
                            //如果还剩一半的时间过期，续期
                            if (userToken.Token.ExpireTime - UnixTimeHelper.GetUnixSeconds() < AUTHORIZATION_REDIS_EXPIRE_SECONDS / 2)
                            {
                                userToken.Token.ExpireTime
                                    = UnixTimeHelper.GetUnixSeconds(DateTime.Now.AddSeconds(AUTHORIZATION_REDIS_EXPIRE_SECONDS));
                                bUserToken.TokenData = JsonConvert.SerializeObject(userToken);
                                bUserToken.LoginExpires = AUTHORIZATION_REDIS_EXPIRE_SECONDS;
                                context.BUserTokens.Update(bUserToken);
                            }
                        }
                        else
                        {
                            #region 构建 UserTokenData

                            userToken = new UserTokenData
                            {
                                User = identity,
                                Token = new Token
                                {
                                    Value = NewCode.Token,
                                    ExpireTime = UnixTimeHelper.GetUnixSeconds(
                                        DateTime.Now.AddSeconds(AUTHORIZATION_REDIS_EXPIRE_SECONDS))
                                }
                            };

                            #endregion

                            #region 保存用户登录身份到数据库

                            bUserToken = new BUserToken()
                            {
                                UserId = identity.UserId,
                                TokenData = JsonConvert.SerializeObject(userToken),
                                LoginExpires = AUTHORIZATION_REDIS_EXPIRE_SECONDS,
                                CreateStamp = DateHelper.GetDateString()
                            };
                            context.BUserTokens.Update(bUserToken);
                            #endregion
                        }
                    }
                    else
                    {
                        #region 构建 UserTokenData

                        userToken = new UserTokenData
                        {
                            User = identity,
                            Token = new Token
                            {
                                Value = NewCode.Token,
                                ExpireTime = UnixTimeHelper.GetUnixSeconds(
                                    DateTime.Now.AddSeconds(AUTHORIZATION_REDIS_EXPIRE_SECONDS))
                            }
                        };

                        #endregion

                        #region 保存用户登录身份到数据库

                        bUserToken = new BUserToken()
                        {
                            UserId = identity.UserId,
                            TokenData = JsonConvert.SerializeObject(userToken),
                            LoginExpires = AUTHORIZATION_REDIS_EXPIRE_SECONDS,
                            CreateStamp = DateHelper.GetDateString()
                        };

                        context.BUserTokens.Add(bUserToken);
                        #endregion
                    }
                    await context.SaveChangesAsync();
                    //生成Authorization
                    var terminal = TerminalHelper.GetTerminalInfo(terminalId);
                    var authorization = WebApiAuthorization.GetString(terminal.TerminalKey, terminal.TerminalSecret,
                        userToken.User.UserId, userToken.Token.Value);
                    return new ResponseModel<KeyValue<UserTokenData>>(new KeyValue<UserTokenData>
                    {
                        Key = authorization,
                        Value = userToken
                    });

                }
            }
            catch (Exception ex)
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(),"【生成UserTokenAuthorization失败】" + ex.Message);
            }
        }
    }
}
