using AFAS.Models.Account;

namespace AFAS.Business.Account;

/// <summary>
/// 【Terminal】药职人平台用户登录
/// </summary>
public interface IUserLoginService
{
    #region 网页端登录

    /// <summary>
    /// 网页密码登录
    /// </summary>
    /// <param name="account"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<WebAppIdentityModel> WebAppLoginByPasswordAsync(string account, string password);

    #endregion

    #region 重置登录密码

    ///// <summary>
    ///// 发送重置密码的短信验证码
    ///// </summary>
    ///// <param name="mobile"></param>
    ///// <returns></returns>
    //public Task<string> SendWebAppResetPasswordVerifyCodeAsync(string mobile);

    ///// <summary>
    ///// 根据手机号重置密码
    ///// </summary>
    ///// <param name="data"></param>
    ///// <param name="newPassword"></param>
    ///// <returns></returns>
    //public Task WebAppResetPasswordByMobileAsync(SmsVerifyCode data, string newPassword);

    #endregion
}
