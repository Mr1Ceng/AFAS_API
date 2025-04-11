using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class KeySort
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeySort()
    {
        Key = "";
        Sort = 0;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="sort">显示顺序</param>
    public KeySort(string key, int sort)
    {
        Key = key;
        Sort = sort;
    }

    /// <summary>
    /// 键
    /// </summary>
    [Description("键")]
    public string Key { get; set; }

    /// <summary>
    /// 显示顺序
    /// </summary>
    [Description("显示顺序")]
    public int Sort { get; set; }
}
