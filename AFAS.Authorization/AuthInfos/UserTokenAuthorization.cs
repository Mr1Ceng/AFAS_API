using AFAS.Authorization.Models;

namespace AFAS.Authorization.AuthInfos;

/// <summary>
/// UserToken授权业务类
/// </summary>
public class UserTokenAuthorization
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="authInfo"></param>
    protected UserTokenAuthorization(IAuthInfo authInfo)
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
    /// UserIdentity 身份数据
    /// </summary>
    protected UserIdentity userIdentity => auth.UserIdentity;

    /// <summary>
    /// 用户Token身份信息
    /// </summary>
    protected UserTokenData userToken => auth.UserToken;
}
