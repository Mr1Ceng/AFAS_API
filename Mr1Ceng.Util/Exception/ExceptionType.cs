using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 错误类型
/// </summary>
public enum ExceptionType
{
    /// <summary>
    /// 日志记录
    /// </summary>
    [Description("日志记录")] Record = 0,

    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")] Success = 200,

    /// <summary>
    /// 常规错误
    /// </summary>
    [Description("常规错误")] Exception = 600,

    /// <summary>
    /// 消息提醒
    /// </summary>
    [Description("消息提醒")] Message = 700,

    /// <summary>
    /// 未被授权
    /// </summary>
    [Description("未被授权")] Forbidden = 403,

    /// <summary>
    /// 事件拦截
    /// </summary>
    [Description("事件拦截")] Intercept = 800
}
