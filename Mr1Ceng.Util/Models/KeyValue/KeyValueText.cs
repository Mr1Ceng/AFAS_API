using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class KeyValueText
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyValueText()
    {
        Key = "";
        Value = "";
        Text = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="text">文本</param>
    public KeyValueText(string key, string value, string text)
    {
        Key = key;
        Value = value;
        Text = text;
    }

    /// <summary>
    /// 键
    /// </summary>
    [Description("键")]
    public string Key { get; set; }

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

/// <summary>
/// 键值对
/// </summary>
public class KeyValueText<T>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyValueText()
    {
        Key = "";
        Text = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="text"></param>
    public KeyValueText(string key, T value, string text)
    {
        Key = key;
        Value = value;
        Text = text;
    }

    /// <summary>
    /// 键
    /// </summary>
    [Description("键")]
    public string Key { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    [Description("值")]
    public T? Value { get; set; }

    /// <summary>
    /// 文本
    /// </summary>
    [Description("文本")]
    public string Text { get; set; }
}
