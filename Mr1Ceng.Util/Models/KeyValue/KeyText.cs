using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class KeyText
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyText()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="text">值</param>
    public KeyText(string key, string text)
    {
        Key = key;
        Text = text;
    }

    /// <summary>
    /// 键
    /// </summary>
    [Description("键")]
    public string Key { get; set; } = "";

    /// <summary>
    /// 文本
    /// </summary>
    [Description("文本")]
    public string Text { get; set; } = "";
}
