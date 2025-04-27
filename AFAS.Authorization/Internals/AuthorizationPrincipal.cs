using System.Security.Claims;
using System.Security.Principal;

namespace AFAS.Authorization.Internals;

/// <summary>
/// 委托容器
/// </summary>
internal class AuthorizationPrincipal : ClaimsPrincipal
{
    private readonly AuthorizationIdentity _Identity;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="identity"></param>
    public AuthorizationPrincipal(AuthorizationIdentity identity)
    {
        _Identity = identity;
    }

    /// <summary>
    /// 认证信息
    /// </summary>
    public override IIdentity Identity => _Identity;
}
