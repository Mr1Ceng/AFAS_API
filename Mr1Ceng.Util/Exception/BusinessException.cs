using System.Reflection;
using Newtonsoft.Json;

namespace Mr1Ceng.Util;

/// <summary>
/// 返回的异常处理
/// </summary>
public class BusinessException : Exception
{
    #region 静态方法

    /// <summary>
    /// 获取异常
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static BusinessException Get(BusinessException ex) => ex;

    /// <summary>
    /// 获取异常
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static BusinessException Get(Exception ex)
    {
        if (ex is BusinessException bex)
        {
            return bex;
        }
        return new BusinessException(ex);
    }

    /// <summary>
    /// 获取异常
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="exceptionType">异常类型</param>
    /// <returns></returns>
    public static BusinessException Get(Exception ex, ExceptionType exceptionType)
    {
        if (ex is BusinessException bex)
        {
            if (bex.Editable)
            {
                if (bex.ExceptionType != ExceptionType.Message && bex.ExceptionType != ExceptionType.Forbidden
                    && bex.ExceptionType != ExceptionType.Intercept)
                {
                    bex.ExceptionType = exceptionType;
                }
            }
            return bex;
        }
        return new BusinessException(ex, exceptionType);
    }

    /// <summary>
    /// 获取异常
    /// </summary>
    /// <param name="exceptionType"></param>
    /// <returns></returns>
    public static BusinessException Get(ExceptionType exceptionType = ExceptionType.Exception) => new(exceptionType);

    /// <summary>
    /// 创建异常
    /// </summary>
    /// <param name="method"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public static BusinessException Get(MethodBase? method, string message = "", object? debugData = null)
        => new BusinessException().Add(method, message, debugData);

    /// <summary>
    /// 创建异常
    /// </summary>
    /// <param name="method"></param>
    /// <param name="infoType"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public static BusinessException Get(MethodBase? method, ExceptionInfoType infoType, string message = "", object? debugData = null)
        => new BusinessException().Add(method, infoType, message, debugData);

    /// <summary>
    /// 创建异常
    /// </summary>
    /// <param name="method"></param>
    /// <param name="exceptionType"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    internal static BusinessException Create(MethodBase? method, ExceptionType exceptionType, string message, object? debugData)
        => exceptionType switch
        {
            ExceptionType.Intercept => new BusinessException().AddIntercept(method, message, debugData),
            ExceptionType.Forbidden => new BusinessException().AddForbidden(method, message, debugData),
            ExceptionType.Message => new BusinessException().AddMessage(method, message, debugData),
            _ => new BusinessException().Add(method, message, debugData)
        };

    #endregion


    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="exceptionType">异常类型</param>
    private BusinessException(ExceptionType exceptionType = ExceptionType.Exception)
    {
        ExceptionType = exceptionType;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="ex"></param>
    private BusinessException(Exception ex) : base(ex.Message, ex)
    {
        ExceptionType = ExceptionType.Exception;
        BusinessMessage = ex.Message;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="exceptionType">异常类型</param>
    private BusinessException(Exception ex, ExceptionType exceptionType) : base(ex.Message, ex)
    {
        ExceptionType = exceptionType;
        BusinessMessage = ex.Message;
    }

    #endregion


    #region 输出类型转换

    #region AsMessage

    /// <summary>
    /// 作为消息提醒处理
    /// </summary>
    /// <returns></returns>
    public BusinessException AsMessage(string message = "")
    {
        ExceptionType = ExceptionType.Message;
        if (!string.IsNullOrWhiteSpace(message))
        {
            BusinessMessage = message;
        }
        return this;
    }

    /// <summary>
    /// 作为消息提醒处理
    /// </summary>
    /// <param name="method"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException AsMessage(MethodBase? method, string message = "", object? debugData = null)
    {
        ExceptionType = ExceptionType.Message;
        if (!string.IsNullOrWhiteSpace(message))
        {
            BusinessMessage = message;
        }
        InfoList.Add(new BusinessExceptionInfo(ExceptionType, message, debugData).SetExceptionMethod(method));
        return this;
    }

    #endregion

    #region AsForbidden

    /// <summary>
    /// 作为未被授权处理
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public BusinessException AsForbidden(string message = "")
    {
        ExceptionType = ExceptionType.Forbidden;
        if (!string.IsNullOrWhiteSpace(message))
        {
            BusinessMessage = message;
        }
        return this;
    }

    /// <summary>
    /// 作为未被授权处理
    /// </summary>
    /// <param name="method"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException AsForbidden(MethodBase? method, string message = "", object? debugData = null)
    {
        ExceptionType = ExceptionType.Forbidden;
        if (!string.IsNullOrWhiteSpace(message))
        {
            BusinessMessage = message;
        }
        InfoList.Add(new BusinessExceptionInfo(ExceptionType, message, debugData).SetExceptionMethod(method));
        return this;
    }

    #endregion

    #region AsIntercept

    /// <summary>
    /// 作为事件拦截处理
    /// </summary>
    /// <returns></returns>
    public BusinessException AsIntercept(string interceptCode)
    {
        ExceptionType = ExceptionType.Intercept;
        BusinessMessage = interceptCode;
        return this;
    }

    /// <summary>
    /// 作为事件拦截处理
    /// </summary>
    /// <param name="method"></param>
    /// <param name="interceptCode"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException AsIntercept(MethodBase? method, string interceptCode, object? debugData = null)
    {
        ExceptionType = ExceptionType.Forbidden;
        BusinessMessage = interceptCode;
        InfoList.Add(new BusinessExceptionInfo(ExceptionType, "", debugData).SetExceptionMethod(method));
        return this;
    }

    #endregion

    #region AsException

    /// <summary>
    /// 作为普通异常处理
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public BusinessException AsException(string message = "")
    {
        ExceptionType = ExceptionType.Exception;
        if (!string.IsNullOrWhiteSpace(message))
        {
            BusinessMessage = message;
        }
        return this;
    }

    #endregion

    #endregion


    #region 添加异常信息

    /// <summary>
    /// 添加异常信息
    /// </summary>
    /// <param name="method"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException Add(MethodBase? method, object debugData) => Add(method, "", debugData);

    /// <summary>
    /// 添加异常信息
    /// </summary>
    /// <param name="method"></param>
    /// <param name="infoType"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException Add(MethodBase? method, ExceptionInfoType infoType, object debugData)
        => Add(method, infoType, "", debugData);

    /// <summary>
    /// 添加异常信息
    /// </summary>
    /// <param name="method"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException Add(MethodBase? method, string message = "", object? debugData = null)
    {
        if (Editable)
        {
            if (ExceptionType is ExceptionType.Message or ExceptionType.Forbidden or ExceptionType.Intercept)
            {
                var info = new BusinessExceptionInfo(ExceptionType, message, debugData).SetExceptionMethod(method);
                InfoList.Add(info);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    BusinessMessage = message;
                }

                var info = new BusinessExceptionInfo(ExceptionType, message, debugData).SetExceptionMethod(method);
                InfoList.Add(info);
            }
        }
        return this;
    }

    /// <summary>
    /// 添加异常信息
    /// </summary>
    /// <param name="method"></param>
    /// <param name="infoType"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException Add(MethodBase? method, ExceptionInfoType infoType, string message = "", object? debugData = null)
    {
        if (Editable)
        {
            if (ExceptionType is ExceptionType.Message or ExceptionType.Forbidden or ExceptionType.Intercept)
            {
                var info = new BusinessExceptionInfo(infoType, message, debugData).SetExceptionMethod(method);
                InfoList.Add(info);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    BusinessMessage = message;
                }

                var info = new BusinessExceptionInfo(infoType, message, debugData).SetExceptionMethod(method);
                InfoList.Add(info);
            }
        }
        return this;
    }

    /// <summary>
    /// 添加消息信息（如果异常类型不为 Forbidden 或 Intercept，把异常类型转换为 Message）
    /// </summary>
    /// <param name="method"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException AddMessage(MethodBase? method, string message = "", object? debugData = null)
    {
        if (Editable)
        {
            if (ExceptionType is ExceptionType.Forbidden or ExceptionType.Intercept)
            {
                var info = new BusinessExceptionInfo(ExceptionType.Message, message, debugData).SetExceptionMethod(method);
                InfoList.Add(info);
            }
            else
            {
                ExceptionType = ExceptionType.Message;
                if (!string.IsNullOrWhiteSpace(message))
                {
                    BusinessMessage = message;
                }

                var info = new BusinessExceptionInfo(ExceptionType.Message, message, debugData).SetExceptionMethod(method);
                InfoList.Add(info);
            }
        }
        return this;
    }

    /// <summary>
    /// 添加未被授权信息（如果异常类型不为 Intercept，把异常类型转换为 Forbidden）
    /// </summary>
    /// <param name="method"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException AddForbidden(MethodBase? method, string message = "", object? debugData = null)
    {
        if (Editable)
        {
            if (ExceptionType != ExceptionType.Intercept)
            {
                ExceptionType = ExceptionType.Forbidden;
                if (!string.IsNullOrWhiteSpace(message))
                {
                    BusinessMessage = message;
                }
            }

            var info = new BusinessExceptionInfo(ExceptionType.Forbidden, message, debugData).SetExceptionMethod(method);
            InfoList.Add(info);
        }
        return this;
    }

    /// <summary>
    /// 添加事件拦截信息（把异常类型转换为 Intercept）
    /// </summary>
    /// <param name="method"></param>
    /// <param name="interceptCode"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public BusinessException AddIntercept(MethodBase? method, string interceptCode, object? debugData = null)
    {
        if (Editable)
        {
            ExceptionType = ExceptionType.Intercept;
            BusinessMessage = interceptCode;

            var info = new BusinessExceptionInfo(ExceptionType.Forbidden, interceptCode, debugData).SetExceptionMethod(method);
            InfoList.Add(info);
        }
        return this;
    }

    #endregion


    #region 属性

    /// <summary>
    /// 错误类型
    /// </summary>
    public ExceptionType ExceptionType { get; set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    public string BusinessMessage { get; set; } = "";

    /// <summary>
    /// 错误信息列表
    /// </summary>
    public List<BusinessExceptionInfo> InfoList { get; set; } = [];

    #endregion


    #region 异常状态

    /// <summary>
    /// 禁止异常再进行继续修改
    /// </summary>
    public BusinessException Readonly()
    {
        Editable = false;
        return this;
    }

    /// <summary>
    /// 是否可修改
    /// </summary>
    [JsonIgnore]
    private bool Editable { get; set; } = true;

    #endregion


    #region 日志状态

    /// <summary>
    /// 强制写日志
    /// </summary>
    public BusinessException ForceLog()
    {
        RecordLog = true;
        return this;
    }

    /// <summary>
    /// 忽略写日志
    /// </summary>
    public BusinessException IgnoreLog()
    {
        RecordLog = false;
        return this;
    }

    /// <summary>
    /// 是否写日志
    /// </summary>
    [JsonIgnore]
    public bool? RecordLog { get; set; }

    #endregion


    #region 关联请求

    /// <summary>
    /// 设置请求主键
    /// </summary>
    /// <param name="requestId"></param>
    public BusinessException SetRequestId(int requestId)
    {
        RequestId = requestId;
        return this;
    }

    /// <summary>
    /// 请求主键
    /// </summary>
    [JsonIgnore]
    public int RequestId { get; set; }

    #endregion


    #region 方法

    /// <summary>
    /// 转换为Json
    /// </summary>
    /// <returns></returns>
    public string ToJson()
    {
        try
        {
            return JsonHelper.Serialize(this);
        }
        catch
        {
            return "JSON序列化失败";
        }
    }

    #endregion
}
