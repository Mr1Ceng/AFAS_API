using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// Url数据对象
/// </summary>
public class UrlModel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public UrlModel()
    {
        Url = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="url"></param>
    public UrlModel(string url)
    {
        Url = url;
    }

    /// <summary>
    /// Url地址
    /// </summary>
    [Description("Url地址")]
    public string Url { get; set; }
}
