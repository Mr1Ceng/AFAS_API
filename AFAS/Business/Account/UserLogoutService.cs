using AFAS.Authorization;
using AFAS.Authorization.AuthInfos;
using AFAS.Entity;
using AFAS.Enums.Account;
using AFAS.Infrastructure;
using AFAS.Models.Account;
using Mr1Ceng.Util;
using Newtonsoft.Json;
using System.Reflection;

namespace AFAS.Business.Account;

/// <summary>
/// 【UserToken】用户登录
/// </summary>
public class UserLogoutService : UserTokenAuthorization, IUserLogoutService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="authInfo"></param>
    public UserLogoutService(IAuthInfo authInfo) : base(authInfo)
    {
    }
    #region 网页密码登录

    /// <summary>
    /// 网页退出登录
    /// </summary>
    /// <returns></returns>
    public async Task WebAppLogoutAsync()
    {
        await UserTokenHelper.RemoveUserTokenByUserIdAsync(userIdentity.UserId);
    }

    #endregion
}
