using AFAS.Authorization.Models;

namespace AFAS.Authorization;

/// <summary>
/// 基础HttpService
/// </summary>
public interface IAuthInfo
{
    /// <summary>
    /// 请求头数据
    /// </summary>
    Identifier Identifier { get; }

    /// <summary>
    /// UserIdentity 身份数据
    /// </summary>
    UserIdentity UserIdentity { get; }

    /// <summary>
    /// 用户Token身份信息
    /// </summary>
    UserTokenData UserToken { get; }

    /// <summary>
    /// 用户有效性核验
    /// </summary>
    void AuthorizationUser();
}
