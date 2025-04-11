using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 含备注字段的键值对
/// </summary>
public class KeyTextRemark : KeyText
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyTextRemark()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="text">值</param>
    public KeyTextRemark(string key, string text)
    {
        Key = key;
        Text = text;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="text">值</param>
    /// <param name="remark">备注</param>
    public KeyTextRemark(string key, string text, string remark)
    {
        Key = key;
        Text = text;
        Remark = remark;
    }

    /// <summary>
    /// 备注
    /// </summary>
    [Description("备注")]
    public string Remark { get; set; } = "";
}
