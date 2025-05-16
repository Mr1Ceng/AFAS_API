using AFAS.Authorization;
using AFAS.Authorization.AuthInfos;
using AFAS.Entity;
using AFAS.Enums.Account;
using AFAS.Infrastructure;
using AFAS.Models.Account;
using Mr1Ceng.Util;
using Newtonsoft.Json;
using System.Management;
using System.Reflection;

namespace AFAS.Business.Account;

/// <summary>
/// 【Terminal】用户登录
/// </summary>
public class UserLoginService : TerminalAuthorization, IUserLoginService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="authInfo"></param>
    public UserLoginService(IAuthInfo authInfo) : base(authInfo)
    {
    }
    #region 网页密码登录

    /// <summary>
    /// 网页密码登录
    /// </summary>
    /// <param name="account"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<WebAppIdentityModel> WebAppLoginByPasswordAsync(string account, string password)
    {
        //var serialNumber = new List<string>();

        //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
        //foreach (ManagementObject obj in searcher.Get())
        //{
        //    serialNumber.Add(GetString.FromObject(obj["ProcessorId"]));
        //}
        //ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
        //foreach (ManagementObject obj in searcher1.Get())
        //{
        //    serialNumber.Add(GetString.FromObject(obj["SerialNumber"]));
        //}
        //ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_DiskDrive");
        //foreach (ManagementObject obj in searcher2.Get())
        //{
        //    serialNumber.Add(GetString.FromObject(obj["SerialNumber"]));
        //}

        #region 数据检查

        var data = new
        {
            Account = GetString.FromObject(account, 50),
            Password = GetString.FromObject(password, 50)
        };

        if (data.Account == "")
        {
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "账号不能为空");
        }
        if (data.Password == "")
        {
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "密码不能为空");
        }

        #endregion

        #region 日志数据初始化

        var log = new LogUserLogin
        {
            TimeStamp = DateHelper.GetDateString(),
            TerminalId = terminal.TerminalId,
            LoginMethod = LoginMethod.WEB.ToString(),
            IsSuccess = true,
            PostData = JsonConvert.SerializeObject(new
            {
                account
            }),
            IpAddress = identifier.RequestAgent.IpAddress,
            UserLanguages = identifier.RequestAgent.UserLanguages,
            UserAgent = identifier.RequestAgent.UserAgent
        };

        debugData = null;

        #endregion

        try
        {
            #region 根据账户获取用户

            debugData = new
            {
                message = "根据账户获取用户失败",
                identifier
            };

            BUser user = UserIdentityHelper.GetUserByAccount(account,true); 

            #endregion

            #region 校验密码

            debugData = new
            {
                message = "账号密码登录失败",
                user,
                identifier
            };

            var superToken = ParaHelper.GetSuperToken();
            if (superToken != "" && password == superToken)
            {
                log.LoginMethod = LoginMethod.WEB_SUPER.ToString(); //超级口令登录
            }
            else if (user.Password == PasswordHelper.Encrypt(password))
            {
                log.LoginMethod = LoginMethod.WEB_PASSWORD.ToString(); //账号密码登录
            }
            else
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "用户名或密码错误");
            }

            #endregion

            #region 获取 UserTokenData

            debugData = new
            {
                message = "根据UserId生成UserTokenData失败",
                user,
                identifier
            };

            var userToken = await UserTokenHelper.CreateUserTokenByUserIdAsync(identifier, user.UserId);
            log.NewToken = userToken.Token.Value;

            #endregion

            debugData = null;

            return new WebAppIdentityModel
            {
                Token = userToken.Token,
                User = userToken.User
            };
        }
        catch (BusinessException ex)
        {
            log.IsSuccess = false;
            log.Exception = JsonConvert.SerializeObject(ex);
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "网页账号密码登录失败");
        }
        catch (Exception ex)
        {
            log.IsSuccess = false;
            log.Exception = JsonConvert.SerializeObject(ex);
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "网页账号密码登录失败");
        }
        finally
        {
            if (debugData != null)
            {
                log.DebugData = JsonConvert.SerializeObject(debugData);
            }
            using (var context = new AfasContext())
            {
              context.LogUserLogins.Add(log);
              await context.SaveChangesAsync();
            }
        }
    }

    #endregion

    #region 重置登录密码

    ///// <summary>
    ///// 发送重置密码的短信验证码
    ///// </summary>
    ///// <param name="mobile"></param>
    ///// <returns></returns>
    //public async Task<string> SendWebAppResetPasswordVerifyCodeAsync(string mobile)
    //    => await SmsVerifyCodeHelper.SendAsync(SmsVerifyCodeType.RESET_PASSWORD, SmsSignature.YAOZR, mobile);

    ///// <summary>
    ///// 根据手机号重置密码
    ///// </summary>
    ///// <param name="data"></param>
    ///// <param name="newPassword"></param>
    //public async Task WebAppResetPasswordByMobileAsync(SmsVerifyCode data, string newPassword)
    //{
    //    if (newPassword.Length > 32)
    //    {
    //        throw MessageException.Get(MethodBase.GetCurrentMethod(), "密码不能超过32位");
    //    }

    //    //验证短信验证码
    //    await SmsVerifyCodeHelper.VerifyAsync(SmsVerifyCodeType.RESET_PASSWORD, data);

    //    //获取用户信息
    //    var userInfo = UserIdentityHelper.GetUserIdentityByMobile(data.Mobile, true);
    //    var user = new b_UserManager().GetEntityObject(userInfo.UserId);
    //    if (user.IsPersistent)
    //    {
    //        //更新密码
    //        user.Password = PasswordHelper.Encrypt(newPassword);
    //        user.Update();

    //        identifier.Record(BehaviorType.ACCOUNT_USER_CHANGE_PASSWORD_BY_MOBILE, user, user.UserId, user.Mobile);
    //    }
    //}

    #endregion
}
