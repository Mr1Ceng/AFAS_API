using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值对
/// </summary>
public class KeyUrl
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public KeyUrl()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="url">值</param>
    public KeyUrl(string key, string url)
    {
        Key = key;
        Url = url;
    }

    /// <summary>
    /// 键
    /// </summary>
    [Description("键")]
    public string Key { get; set; } = "";

    /// <summary>
    /// Url地址
    /// </summary>
    [Description("Url地址")]
    public string Url { get; set; } = "";
}
