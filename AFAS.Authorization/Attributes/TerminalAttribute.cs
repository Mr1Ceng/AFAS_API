using AFAS.Authorization.Identifiers;
using AFAS.Authorization.Internals;
using AFAS.Authorization.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Mr1Ceng.Util;
using Newtonsoft.Json;
using System.Reflection;

namespace AFAS.Authorization.Attributes;

/// <summary>
/// Terminal认证方式
/// </summary>
public class TerminalAttribute : ActionFilterAttribute, IAllowAnonymousFilter
{
    /// <summary>
    /// 执行Terminal认证（不验证客户端时间）
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!AuthorizationHelper.AllowAnonymous(context))
        {
            #region 验证请求头的内容

            var headers = context.HttpContext.Request.Headers;

            var author = headers["Authorization"].ToString();
            if (author == "")
            {
                throw ForbiddenException.Get(MethodBase.GetCurrentMethod(), "没有在Request.Headers中获取到Authorization");
            }

            #endregion

            TerminalIdentifier identifier = new(context.HttpContext);
            try
            {
                #region 获取 ActionHeadData

                identifier.ActionHead = new ActionHeadData
                {
                };

                #endregion

                #region 认证 Authorization

                identifier.Terminal = AuthorizationHelper.AuthorizationTerminal(author);

                #endregion

                #region 输出 Identifier

                context.HttpContext.User = new AuthorizationPrincipal(new AuthorizationIdentity(identifier));

                #endregion
            }
            catch (BusinessException ex)
            {
                identifier.RecordAPI(JsonConvert.SerializeObject(ex)); // 写API异常日志
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "身份认证失败");
            }
            catch (Exception ex)
            {
                identifier.RecordAPI(JsonConvert.SerializeObject(ex)); // 写API异常日志
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "身份认证失败");
            }
            identifier.RecordAPI(); // 写API访问日志
        }

        OnActionExecuting(context);
        if (context.Result == null)
        {
            OnActionExecuted(await next());
        }
    }
}
