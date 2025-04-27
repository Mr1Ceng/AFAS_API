using AFAS.Authorization.Internals;
using AFAS.Authorization.Models;
using AFAS.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Mr1Ceng.Util;
using System.Reflection;

namespace AFAS.Authorization;

/// <summary>
/// 授权服务业务类
/// </summary>
public class AuthorizationHelper
{
    #region 内部方法

    /// <summary>
    /// 判断是否为忽略请求头的请求
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    internal static bool AllowAnonymous(ActionExecutingContext context)
    {
        var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;

        //若控制器设置[AllowAnonymous] 则为true
        //若控制器没有设置[AllowAnonymous] 则为false
        var allowAnyone =
            descriptor.ControllerTypeInfo.GetCustomAttributes(typeof(IAllowAnonymous), true).Any() ||
            descriptor.MethodInfo.GetCustomAttributes(typeof(IAllowAnonymous), true).Any();
        return allowAnyone;
    }

    /// <summary>
    /// 终端身份认证
    /// </summary>
    /// <param name="author"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    internal static TerminalData AuthorizationTerminal(string author, ActionHeadData? data = null)
    {
        var authorization = new WebApiAuthorization(author);
        if (data != null)
        {
            data.KeyId = authorization.KeyId;
            data.Token = authorization.Token;
        }

        #region 获取终端身份信息

        var terminals = new List<STerminal>();
        using (var context = new AfasContext())
        {
            terminals = context.STerminals.Where(x => x.TerminalKey == authorization.ProviderKey).ToList();
            if (terminals.Count == 0)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Forbidden, $"ProviderKey不存在:{authorization.ProviderKey}");
            }
        }
       

        #endregion

        var terminal = terminals.First();
        try
        {
            return new TerminalData
            {
                TerminalId = terminal.TerminalId,
                TerminalName = terminal.TerminalName,
                TerminalType = terminal.TerminalType,
                SystemId = terminal.SystemId,
                TimeSpan = authorization.CheckTerminal(terminal.TerminalSecret)
            };
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Forbidden, "身份认证失败", ex);
        }
    }

    ///// <summary>
    ///// 验证客户端时间
    ///// </summary>
    ///// <param name="terminalSpan">当前请求：客户端和服务器已知的时差毫秒数（可正可负可为零）</param>
    ///// <param name="tokenSpan">已知的：客户端和服务器已知的时差毫秒数（可正可负可为零）</param>
    //internal static void AuthorizationTimeStamp(double terminalSpan, double tokenSpan)
    //{
    //    if (SystemConfig.Timeout > 0 && Math.Abs(terminalSpan - tokenSpan) > SystemConfig.Timeout)
    //    {
    //        throw new ForbiddenException("Token超时");
    //    }
    //}

    #endregion


    #region 认证请求头

    /// <summary>
    /// 获取请求头数据
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static Identifier GetIdentifier(HttpContext httpContext)
    {
        var identity = httpContext.User.Identity;
        if (identity == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Forbidden, "没有获取到身份信息");
        }
        var identifier = ((AuthorizationIdentity)identity).Identifier;
        if (identifier == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Forbidden, "没有获取到身份信息");
        }
        return identifier;
    }

    /// <summary>
    /// 获取 UserIdentity 身份数据
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static UserIdentity GetUserIdentity(HttpContext httpContext) => GetIdentifier(httpContext).User;

    #endregion
}
