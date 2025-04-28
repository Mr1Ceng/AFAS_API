using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mr1Ceng.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace AFAS.Infrastructure;

/// <summary>
/// 异常返回拦截器
/// </summary>
public class ExceptionResponseFilter : ExceptionFilterAttribute
{
    /// <summary>
    /// 重写异常处理
    /// </summary>
    /// <param name="context"></param>
    public override void OnException(ExceptionContext? context)
    {
        if (context?.Exception == null)
        {
            return;
        }

        //异常处理
        if (context.Exception is not BusinessException businessException)
        {
            businessException = BusinessException.Get(context.Exception);
        }

        ResponseExceptionModel response = new()
        {
            ExceptionType = businessException.ExceptionType,
            Message = businessException.Message == ""
                ? ((Exception)businessException).Message
                : businessException.Message,
            Exception = businessException,
            DebugData = businessException.InfoList.FirstOrDefault()?.DebugData,
        };

        try
        {
            if (businessException.ExceptionType != ExceptionType.Message
                && businessException.ExceptionType != ExceptionType.Forbidden)
            {
                response.DebugCode = LogHelper.RecordException(businessException); //写异常日志
            }
        }
        catch
        {
            //异常日志写失败，返回-1，通常用来判断错误
            response.DebugCode = -1;
        }

        var contentResult = new ContentResult
        {
            StatusCode = (int)HttpStatusCode.OK,
            ContentType = "application/json; charset=utf-8"
        };
        var r = new ResponseModel<ResponseExceptionModel>(ResponseStatus.EXCEPTION)
        {
            Data = response
        };

        contentResult.Content = JsonConvert.SerializeObject(r, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        context.Result = contentResult;
        context.Result = new ContentResult
        {
            StatusCode = (int)HttpStatusCode.OK,
            ContentType = "application/json; charset=utf-8",
            Content = JsonConvert.SerializeObject(new ResponseModel<ResponseExceptionModel>(ResponseStatus.EXCEPTION)
            {
                Data = response
            }, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            })
        };

        base.OnException(context);
    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task OnExceptionAsync(ExceptionContext? context)
    {
        if (context?.Exception == null)
        {
            return base.OnExceptionAsync(context);
        }

        //异常处理
        if (context.Exception is not BusinessException businessException)
        {
            businessException = context.Exception.InnerException is not BusinessException exception
                ? BusinessException.Get(context.Exception)
                : exception;
        }

        ResponseExceptionModel response = new()
        {
            ExceptionType = businessException.ExceptionType,
            Message = businessException.Message == ""
                ? ((Exception)businessException).Message
                : businessException.Message,
            Exception = businessException,
            DebugData = businessException.InfoList.FirstOrDefault()?.DebugData,
        };

        try
        {
            if (businessException.ExceptionType != ExceptionType.Message
                && businessException.ExceptionType != ExceptionType.Forbidden)
            {
                //写异常日志
                response.DebugCode = LogHelper.RecordException(businessException);
            }
        }
        catch
        {
            //异常日志写失败，返回-1，通常用来判断错误
            response.DebugCode = -1;
        }

        var contentResult = new ContentResult
        {
            StatusCode = (int)HttpStatusCode.OK,
            ContentType = "application/json; charset=utf-8"
        };
        var r = new ResponseModel<ResponseExceptionModel>(ResponseStatus.EXCEPTION)
        {
            Data = response
        };

        contentResult.Content = JsonConvert.SerializeObject(r, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        context.Result = contentResult;
        context.Result = new ContentResult
        {
            StatusCode = (int)HttpStatusCode.OK,
            ContentType = "application/json; charset=utf-8",
            Content = JsonConvert.SerializeObject(new ResponseModel<ResponseExceptionModel>(ResponseStatus.EXCEPTION)
            {
                Data = response
            }, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            })
        };
        return Task.CompletedTask;
    }
}
