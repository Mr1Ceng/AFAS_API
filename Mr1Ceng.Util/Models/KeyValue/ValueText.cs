using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class ValueText
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ValueText()
    {
        Value = "";
        Text = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="text">值</param>
    public ValueText(string value, string text)
    {
        Value = value;
        Text = text;
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
}
