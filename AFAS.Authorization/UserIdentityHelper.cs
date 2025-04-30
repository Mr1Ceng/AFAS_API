using AFAS.Authorization.Models;
using AFAS.Entity;
using Mr1Ceng.Util;
using System.Data;
using System.Reflection;

namespace AFAS.Authorization;

/// <summary>
/// 用户身份工具类
/// </summary>
public class UserIdentityHelper
{
    #region 获取用户信息

    /// <summary>
    /// 根据UserId获取用户信息
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="throwException">如果取不到，是否抛异常</param>
    /// <returns></returns>
    public static UserIdentity GetUserIdentityByUserId(string userId, bool throwException = false)
        => new(GetUserByUserId(userId, throwException));

    /// <summary>
    /// 根据账号获取用户信息
    /// </summary>
    /// <param name="account"></param>
    /// <param name="throwException">如果取不到，是否抛异常</param>
    /// <returns></returns>
    public static UserIdentity GetUserIdentityByAccount(string account, bool throwException = false)
        => new(GetUserByAccount(account, throwException));

    /// <summary>
    /// 根据手机号获取用户信息
    /// </summary>
    /// <param name="mobile"></param>
    /// <param name="throwException">如果取不到，是否抛异常</param>
    /// <returns></returns>
    public static UserIdentity GetUserIdentityByMobile(string mobile, bool throwException = false)
        => new(GetUserByMobile(mobile, throwException));

    /// <summary>
    /// 根据账号获取用户ID
    /// </summary>
    /// <param name="account"></param>
    /// <param name="throwException">如果取不到，是否抛异常</param>
    /// <returns></returns>
    public static string GetUserIdByAccount(string account, bool throwException = false)
        => GetUserByAccount(account, throwException).UserId;

    /// <summary>
    /// 根据手机号获取用户ID
    /// </summary>
    /// <param name="mobile"></param>
    /// <param name="throwException">如果取不到，是否抛异常</param>
    /// <returns></returns>
    public static string GetUserIdByMobile(string mobile, bool throwException = false)
        => GetUserByMobile(mobile, throwException).UserId;

    /// <summary>
    /// 获取用户名称
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="throwException">如果取不到，是否抛异常</param>
    /// <returns></returns>
    public static string GetUserNameByUserId(string userId, bool throwException = false)
        => GetUserByUserId(userId, throwException).UserName;

    /// <summary>
    /// 获取用户的昵称
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="throwException">如果取不到，是否抛异常</param>
    /// <returns></returns>
    public static string GetNickNameByUserId(string userId, bool throwException = false)
        => GetUserByUserId(userId, throwException).NickName;

    /// <summary>
    /// 获取用户手机号
    /// </summary>
    /// <param name="account">账号（可以是：UserId、UserAccount中的任意一种类）</param>
    /// <param name="throwException">如果取不到，是否抛异常</param>
    /// <returns></returns>
    public static string GetUserMobileByAccount(string account, bool throwException = false)
        => GetUserByAccount(GetString.FromObject(account), throwException).Mobile;

    /// <summary>
    /// 获取角色关联的用户列表
    /// </summary>
    /// <param name="role"></param>
    /// <param name="maxCount"></param>
    /// <returns></returns>
    public static IList<UserIdentity> GetUserListByRoleId(string role, int maxCount = 50)
    {
        var strsql = $@"
            SELECT TOP {maxCount}
                UserId,
                Account AS UserAccount,
                UserName,
                NickName,
                AvatarUrl,
                Gender,
                Mobile,
                IsDeveloper,
                iif(Role='TEACHER',1,0 ) AS IsStaff
            FROM b_User
            WHERE Role = @Role
            ORDER BY b_User.UserName
        ";
        var dt = new DataTable();
        using (var context = new AfasContext())
        {
            dt = new EFCoreExtentions(context).ExecuteSqlQuery(strsql, new Parameter("Role", GetString.FromObject(role)));
        }

        return GetList.FromDataTable<UserIdentity>(dt);
    }

    /// <summary>
    /// 根据用户编码获取用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    public static BUser GetUserByUserId(string userId, bool throwException)
    {
        if (userId.ToUpper() == userId && userId.Length == 32)
        {
            var user = new BUser();
            using (var context = new AfasContext())
            {
                user = context.BUsers.Where(x => x.UserId == userId).FirstOrDefault();
            }
            if (user != null)
            {
                return user;
            }
        }
        if (throwException)
        {
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "用户不存在");
        }
        return new BUser();
    }

    /// <summary>
    /// 根据账号获取用户
    /// </summary>
    /// <param name="account"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    public static BUser GetUserByAccount(string account, bool throwException)
    {
        if (account.ToUpper() == account && account.Length == 32)
        {
            var user = new BUser();
            using (var context = new AfasContext())
            {
                user = context.BUsers.Where(x => x.UserId == account).FirstOrDefault();
            }
            if (user != null)
            {
                return user;
            }
        }

        IList<BUser> userList = new List<BUser>();
        using (var context = new AfasContext())
        {
            userList = context.BUsers.Where(x => x.Account == account).ToList();
        }
        if (userList.Count == 1)
        {
            return userList[0];
        }
        if (throwException)
        {
            if (userList.Count == 0)
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "用户不存在");
            }
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "该账号不唯一");
        }
        return new BUser();
    }

    /// <summary>
    /// 根据手机号获取用户
    /// </summary>
    /// <param name="mobile"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    public static BUser GetUserByMobile(string mobile, bool throwException)
    {
        if (mobile.ToUpper() == mobile && mobile.Length == 32)
        {
            var user = new BUser();
            using (var context = new AfasContext())
            {
                user = context.BUsers.Where(x => x.UserId == mobile).FirstOrDefault();
            }
            if (user != null)
            {
                return user;
            }
        }

        IList<BUser> userList = new List<BUser>();
        using (var context = new AfasContext())
        {
            userList = context.BUsers.Where(x => x.Mobile == mobile).ToList();
        }
        if (userList.Count == 1)
        {
            return userList[0];
        }
        if (throwException)
        {
            if (userList.Count == 0)
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "用户不存在");
            }
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "该账号不唯一");
        }
        return new BUser();
    }

    #endregion
}
