using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 排序
/// </summary>
public enum Sorter
{
    /// <summary>
    /// 正序
    /// </summary>
    [Description("正序")] ASC = 0,

    /// <summary>
    /// 倒序
    /// </summary>
    [Description("倒序")] DESC = 1
}
