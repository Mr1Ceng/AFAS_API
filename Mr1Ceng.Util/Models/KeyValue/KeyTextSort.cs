using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class KeyTextSort : KeyText
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyTextSort()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="text">值</param>
    /// <param name="sort">显示顺序</param>
    public KeyTextSort(string key, string text, int sort)
    {
        Key = key;
        Text = text;
        Sort = sort;
    }

    /// <summary>
    /// 显示顺序
    /// </summary>
    [Description("显示顺序")]
    public int Sort { get; set; }
}
