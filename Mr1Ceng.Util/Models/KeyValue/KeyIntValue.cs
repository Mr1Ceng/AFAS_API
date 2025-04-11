using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class KeyIntValue
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyIntValue()
    {
        Key = "";
        Value = 0;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public KeyIntValue(string key, int value)
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
    public int Value { get; set; }
}
