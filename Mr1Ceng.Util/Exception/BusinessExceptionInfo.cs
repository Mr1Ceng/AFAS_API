using System.Reflection;

namespace Mr1Ceng.Util;

/// <summary>
/// 异常信息
/// </summary>
public class BusinessExceptionInfo
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public BusinessExceptionInfo(ExceptionType exceptionType, string message = "", object? debugData = null)
    {
        InfoType = exceptionType.ToString();
        Message = message;
        DebugData = debugData;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public BusinessExceptionInfo(ExceptionInfoType infoType, string message = "", object? debugData = null)
    {
        InfoType = infoType.ToString();
        Message = message;
        DebugData = debugData;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public BusinessExceptionInfo(string infoType, string message = "", object? debugData = null)
    {
        InfoType = infoType;
        Message = message;
        DebugData = debugData;
    }

    /// <summary>
    /// 错误类型
    /// </summary>
    public string InfoType { get; set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 调试数据
    /// </summary>
    public object? DebugData { get; set; }

    /// <summary>
    /// 产生错误日志的方法名称
    /// </summary>
    public string FunctionName { get; private set; } = "";


    #region 内部方法

    /// <summary>
    /// 产生错误日志的方法
    /// </summary>
    internal BusinessExceptionInfo SetExceptionMethod(MethodBase? value)
    {
        if (value != null)
        {
            FunctionName = ReflectionHelper.GetFunctionName(value);
        }
        return this;
    }

    #endregion
}
