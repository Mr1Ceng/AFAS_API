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
/// 用户令牌身份认证
/// </summary>
public class UserTokenAttribute : ActionFilterAttribute, IAllowAnonymousFilter
{
    /// <summary>
    /// 执行UserToken认证
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

            UserTokenIdentifier identifier = new(context.HttpContext);
            try
            {
                #region 获取 ActionHeadData

                identifier.ActionHead = new ActionHeadData
                {
                };

                #endregion

                #region 认证 Authorization

                identifier.Terminal = AuthorizationHelper.AuthorizationTerminal(author, identifier.ActionHead);

                if (identifier.ActionHead.KeyId == "")
                {
                    throw ForbiddenException.Get(MethodBase.GetCurrentMethod(), "UserId不存在");
                }
                if (identifier.ActionHead.KeyId.Length != 32)
                {
                    throw ForbiddenException.Get(MethodBase.GetCurrentMethod(), "UserId格式错误");
                }
                if (identifier.ActionHead.Token == "")
                {
                    throw ForbiddenException.Get(MethodBase.GetCurrentMethod(), "Token不存在");
                }
                if (identifier.ActionHead.Token.Length != 32)
                {
                    throw ForbiddenException.Get(MethodBase.GetCurrentMethod(), "Token格式错误");
                }

                #endregion

                #region 输出 Identifier
                identifier.UserToken
                    = await TokenHelper.GetUserTokenDataAsync(identifier.ActionHead.KeyId,
                        identifier.ActionHead.Token);
                //AuthorizationHelper.AuthorizationTimeStamp(identifier.Terminal.TimeSpan, identifier.UserToken.Token.TimeSpan); //验证客户端时间
                context.HttpContext.User = new AuthorizationPrincipal(new AuthorizationIdentity(identifier));

                #endregion
            }
            catch (Exception ex)
            {
                identifier.RecordAPI(JsonConvert.SerializeObject(ex)); // 写API异常日志
                throw BusinessException.Get(ex).AddMessage(MethodBase.GetCurrentMethod(), "身份认证失败");
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
