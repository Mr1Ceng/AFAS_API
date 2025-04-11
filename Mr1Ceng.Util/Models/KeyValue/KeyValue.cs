using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class KeyValue
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyValue()
    {
        Key = "";
        Value = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public KeyValue(string key, string value)
    {
        Key = key;
        Value = value;
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
}

/// <summary>
/// 键值对
/// </summary>
/// <typeparam name="T"></typeparam>
public class KeyValue<T>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyValue()
    {
        Key = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public KeyValue(string key, T value)
    {
        Key = key;
        Value = value;
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
}
