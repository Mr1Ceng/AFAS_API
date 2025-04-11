using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class ValueTextSort
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ValueTextSort()
    {
        Value = "";
        Text = "";
        Sort = 0;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="text">值</param>
    /// <param name="sort">显示顺序</param>
    public ValueTextSort(string value, string text, int sort)
    {
        Value = value;
        Text = text;
        Sort = sort;
    }

    /// <summary>
    /// 值
    /// </summary>
    [Description("值")]
    public string Value { get; set; }

    /// <summary>
    /// 文本
    /// </summary>
    [Description("文本")]
    public string Text { get; set; }

    /// <summary>
    /// 显示顺序
    /// </summary>
    [Description("显示顺序")]
    public int Sort { get; set; }
}
