using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 方法运行状态
/// </summary>
public enum FunctionStatus
{
    /// <summary>
    /// 失败
    /// </summary>
    [Description("失败")] FAILURE,

    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")] SUCCESS
}
