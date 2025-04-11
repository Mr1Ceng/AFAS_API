using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// WebApi返回状态
/// </summary>
public enum ResponseStatus
{
    /// <summary>
    /// 异常
    /// </summary>
    [Description("异常")]
    EXCEPTION = 0,

    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    SUCCESS = 1
}
