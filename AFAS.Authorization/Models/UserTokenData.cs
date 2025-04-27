namespace AFAS.Authorization.Models;

/// <summary>
/// 用户Token身份信息
/// </summary>
public class UserTokenData
{
    /// <summary>
    /// 用户身份信息
    /// </summary>
    public UserIdentity User { get; set; } = new();

    /// <summary>
    /// Token
    /// </summary>
    public Token Token { get; set; } = new();
}
