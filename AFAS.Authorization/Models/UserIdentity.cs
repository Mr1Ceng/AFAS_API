using AFAS.Authorization.Enums;
using AFAS.Entity;

namespace AFAS.Authorization.Models;

/// <summary>
/// 用户身份信息
/// </summary>
public class UserIdentity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public UserIdentity()
    {
        UserId = "";
        UserAccount = "";
        UserName = "";
        NickName = "";
        AvatarUrl = "";
        Gender = "";
        Mobile = "";
        IsDeveloper = false;
        IsStaff = false;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="user"></param>
    internal UserIdentity(BUser user)
    {
        UserId = user.UserId;
        UserAccount = user.Account;
        UserName = user.UserName;
        NickName = user.NickName;
        AvatarUrl = user.AvatarUrl;
        Gender = user.Gender;
        Mobile = user.Mobile;
        IsDeveloper = user.IsDeveloper;
        IsStaff = user.Role == RoleEnum.TEACHER.ToString();
    }

    /// <summary>
    /// 用户主键
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 用户账号
    /// </summary>
    public string UserAccount { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 用户头像
    /// </summary>
    public string AvatarUrl { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// 是否开发者
    /// </summary>
    public bool IsDeveloper { get; set; }

    /// <summary>
    /// 是否企微用户
    /// </summary>
    public bool IsStaff { get; set; }
}
