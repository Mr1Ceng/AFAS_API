using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class KeyValueSort
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyValueSort()
    {
        Key = "";
        Value = "";
        Sort = 0;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="sort">显示顺序</param>
    public KeyValueSort(string key, string value, int sort)
    {
        Key = key;
        Value = value;
        Sort = sort;
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
    /// 显示顺序
    /// </summary>
    [Description("显示顺序")]
    public int Sort { get; set; }
}

/// <summary>
/// 键值对
/// </summary>
public class KeyValueSort<T>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyValueSort()
    {
        Key = "";
        Sort = 0;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="sort"></param>
    public KeyValueSort(string key, T value, int sort)
    {
        Key = key;
        Value = value;
        Sort = sort;
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
    /// 显示顺序
    /// </summary>
    [Description("显示顺序")]
    public int Sort { get; set; }
}
