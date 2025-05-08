using AFAS.Models.Account;

namespace AFAS.Business.Account;

/// <summary>
/// 【Terminal】药职人平台用户登录
/// </summary>
public interface IUserLogoutService
{
    #region 网页端退出登录

    /// <summary>
    /// 网页密码登录
    /// </summary>
    /// <param name="account"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task WebAppLogoutAsync();

    #endregion
}
