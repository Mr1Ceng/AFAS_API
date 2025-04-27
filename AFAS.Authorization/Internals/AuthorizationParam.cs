using AFAS.Infrastructure;

namespace AFAS.Authorization.Internals;

/// <summary>
/// 全局参数
/// </summary>
internal class AuthorizationParam
{
    /// <summary>
    /// 登录有效时长（单位秒，默认值7200）
    /// </summary>
    internal static readonly int LoginExpires = ParaHelper.GetLoginExpires();

    /// <summary>
    /// 超级口令
    /// </summary>
    internal static readonly string SuperToken = ParaHelper.GetSuperToken();
}
