using System.Security.Principal;

namespace AFAS.Authorization.Internals;

/// <summary>
/// 控制器请求头的身份认证
/// </summary>
internal class AuthorizationIdentity : IIdentity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="identifier"></param>
    public AuthorizationIdentity(Identifier identifier)
    {
        Identifier = identifier;
    }

    /// <summary>
    /// 身份信息
    /// </summary>
    public Identifier Identifier { get; }

    /// <summary>
    /// 认证类型
    /// </summary>
    public string AuthenticationType => Identifier.AuthType;

    /// <summary>
    /// 已认证
    /// </summary>
    public bool IsAuthenticated => Identifier != null;

    /// <summary>
    /// 认证身份
    /// </summary>
    public string Name => Identifier == null ? "" : Identifier.Terminal.TerminalId;
}
