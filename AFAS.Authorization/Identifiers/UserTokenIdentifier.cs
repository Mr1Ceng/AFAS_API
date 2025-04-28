using AFAS.Authorization.Enums;
using Microsoft.AspNetCore.Http;

namespace AFAS.Authorization.Identifiers;

/// <summary>
/// 用户Token身份信息
/// </summary>
public class UserTokenIdentifier : Identifier
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">HTTP客户端</param>
    public UserTokenIdentifier(HttpContext context) : base(context, AttributeType.UserToken)
    {
    }
}
