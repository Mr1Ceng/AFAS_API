using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 异常返回对象
/// </summary>
public class ResponseExceptionModel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ResponseExceptionModel()
    {
        ExceptionType = ExceptionType.Success;
        Message = "";
    }

    /// <summary>
    /// 错误类型
    /// </summary>
    [Description("错误类型")]
    public ExceptionType ExceptionType { get; set; }

    /// <summary>
    /// 错误名称
    /// </summary>
    [Description("错误名称")]
    public string ExceptionName => EnumHelper.GetDescription(ExceptionType);

    /// <summary>
    /// 错误消息
    /// </summary>
    [Description("错误消息")]
    public string Message { get; set; }

    /// <summary>
    /// 错误数据
    /// </summary>
    [Description("错误数据")]
    public BusinessException? Exception { get; set; }

    /// <summary>
    /// 错误日志代码（异常日志的查询主键。如果为0，说明没有异常日志；如果为-1，说明有错误且异常日志写失败了。）
    /// </summary>
    [Description("错误日志代码（异常日志的查询主键。如果为0，说明没有异常日志；如果为-1，说明有错误且异常日志写失败了。）")]
    public int DebugCode { get; set; }

    /// <summary>
    /// 错误数据
    /// </summary>
    [Description("错误数据")]
    public object? DebugData { get; set; }
}
