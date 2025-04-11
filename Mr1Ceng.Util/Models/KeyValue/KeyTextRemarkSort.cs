using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class KeyTextRemarkSort : KeyTextSort
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyTextRemarkSort()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="text">值</param>
    /// <param name="remark">备注</param>
    /// <param name="sort">显示顺序</param>
    public KeyTextRemarkSort(string key, string text, string remark, int sort)
    {
        Key = key;
        Text = text;
        Remark = remark;
        Sort = sort;
    }

    /// <summary>
    /// 备注
    /// </summary>
    [Description("备注")]
    public string Remark { get; set; } = "";
}
