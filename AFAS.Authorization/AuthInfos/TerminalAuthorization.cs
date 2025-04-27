using AFAS.Authorization.Models;

namespace AFAS.Authorization.AuthInfos;

/// <summary>
/// Terminal 请求头数据
/// </summary>
public class TerminalAuthorization
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="authInfo"></param>
    protected TerminalAuthorization(IAuthInfo authInfo)
    {
        auth = authInfo;
    }

    /// <summary>
    /// 请求对象
    /// </summary>
    private readonly IAuthInfo auth;

    /// <summary>
    /// 用户身份信息
    /// </summary>
    protected Identifier identifier => auth.Identifier;

    /// <summary>
    /// 终端信息
    /// </summary>
    protected TerminalData terminal => auth.Identifier.Terminal;

    /// <summary>
    /// 请求头数据
    /// </summary>
    protected ActionHeadData actionHead => auth.Identifier.ActionHead;

    /// <summary>
    /// 调试数据
    /// </summary>
    protected dynamic? debugData = new
    {
    };
}
