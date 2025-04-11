using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// Url数据对象
/// </summary>
public class FileUrlModel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public FileUrlModel()
    {
        Name = "";
        Url = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="url"></param>
    public FileUrlModel(string name, string url)
    {
        Name = name;
        Url = url;
    }

    /// <summary>
    /// 文件名
    /// </summary>
    [Description("文件名")]
    public string Name { get; set; }

    /// <summary>
    /// Url地址
    /// </summary>
    [Description("Url地址")]
    public string Url { get; set; }
}
