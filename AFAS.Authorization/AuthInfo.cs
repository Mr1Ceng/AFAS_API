using AFAS.Authorization.Models;
using Microsoft.AspNetCore.Http;
using Mr1Ceng.Util;
using System.Reflection;

namespace AFAS.Authorization;

/// <summary>
/// 基础HttpService
/// </summary>
public class AuthInfo : IAuthInfo
{
    /// <summary>
    /// HttpContext
    /// </summary>
    private readonly HttpContext httpContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="accessor"></param>
    public AuthInfo(IHttpContextAccessor accessor)
    {
        httpContext = accessor.HttpContext;
    }

    /// <summary>
    /// 请求头数据
    /// </summary>
    public Identifier Identifier => AuthorizationHelper.GetIdentifier(httpContext);

    /// <summary>
    /// UserIdentity 身份数据
    /// </summary>
    public UserIdentity UserIdentity => Identifier.User;

    /// <summary>
    /// 用户Token身份信息
    /// </summary>
    public UserTokenData UserToken => Identifier.UserToken;

    /// <summary>
    /// 用户有效性核验
    /// </summary>
    public void AuthorizationUser()
    {
        if (Identifier.User.UserId == "")
        {
            throw ForbiddenException.Get(MethodBase.GetCurrentMethod(), "验证用户身份有效性失败");
        }
    }
}
