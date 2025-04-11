using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 排序字段
/// </summary>
public class KeySorterValue
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeySorterValue()
    {
        Key = "";
        Value = Sorter.ASC;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key"></param>
    public KeySorterValue(string key)
    {
        Key = key;
        Value = Sorter.ASC;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public KeySorterValue(string key, Sorter value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// 字段
    /// </summary>
    [Description("字段")]
    public string Key { get; set; }

    /// <summary>
    /// 文本
    /// </summary>
    [Description("排序")]
    public Sorter Value { get; set; }
}
