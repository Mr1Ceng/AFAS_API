using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 多选键值对
/// </summary>
public class MultiValueText
{
    /// <summary>
    /// 值
    /// </summary>
    [Description("值")]
    public List<string> Values { get; set; } = [];

    /// <summary>
    /// 显示文本
    /// </summary>
    [Description("显示文本")]
    public string DisplayText { get; set; } = "";

    /// <summary>
    /// 其他文本
    /// </summary>
    [Description("其他文本")]
    public string OtherText { get; set; } = "";
}
