using System.ComponentModel;

namespace AFAS.Authorization.Enums;

/// <summary>
/// 请求头类型
/// </summary>
public enum AttributeType
{
    /// <summary>
    /// Terminal认证方式
    /// </summary>
    [Description("Terminal认证方式")]
    Terminal,

    /// <summary>
    /// 用户令牌身份认证
    /// </summary>
    [Description("用户令牌身份认证")]
    UserToken
}
