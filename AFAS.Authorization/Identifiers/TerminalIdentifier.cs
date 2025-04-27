using AFAS.Authorization.Enums;
using Microsoft.AspNetCore.Http;

namespace AFAS.Authorization.Identifiers;

/// <summary>
/// 终端Terminal信息
/// </summary>
public class TerminalIdentifier : Identifier
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">HTTP客户端</param>
    public TerminalIdentifier(HttpContext context) : base(context, AttributeType.Terminal)
    {
    }
}
