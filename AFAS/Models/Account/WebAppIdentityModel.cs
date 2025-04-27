using AFAS.Authorization.Models;

namespace AFAS.Models.Account;

/// <summary>
/// 首页用户身份模型
/// </summary>
public class WebAppIdentityModel
{
    /// <summary>
    /// 用户主键
    /// </summary>
    public string UserId => User.UserId;

    /// <summary>
    /// 用户身份信息
    /// </summary>
    public UserIdentity User { get; set; } = new();

    /// <summary>
    /// Token
    /// </summary>
    public Token Token { get; set; } = new();
}
