using AFAS.Authorization;
using AFAS.Authorization.AuthInfos;
using AFAS.Authorization.Enums;
using AFAS.Entity;
using Mr1Ceng.Util;
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

    #region 重置密码

    /// <summary>
    /// 根据用户Id重置密码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="newPassword"></param>
    public async Task WebAppResetPasswordAsync(string newPassword, string userId="")
    {
        if (newPassword.Length > 32)
        {
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "密码不能超过32位");
        }

        var changeUserId = userId == "" ? userIdentity.UserId : userId;
        //获取用户信息
        var user = UserIdentityHelper.GetUserByUserId(changeUserId, true);
        if (userId != "" && user.UserId != userIdentity.UserId && user.Role == RoleEnum.TEACHER.ToString())
        {
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "没有权限修改他人密码");
        }
        using (var context = new AfasContext())
        {
            //更新密码
            user.Password = PasswordHelper.Encrypt(newPassword);
            context.BUsers.Update(user);
            await context.SaveChangesAsync();
        }
        await UserTokenHelper.RemoveUserTokenByUserIdAsync(changeUserId);
    }


    #endregion
}
