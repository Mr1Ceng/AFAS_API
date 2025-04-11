using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 含路径的文件信息
/// </summary>
public class FileUrlInfo
{
    /// <summary>
    /// 文件名
    /// </summary>
    [Description("文件名")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Url地址
    /// </summary>
    [Description("Url地址")]
    public string Url { get; set; } = "";

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    [Description("文件大小（字节）")]
    public long Size { get; set; }
}
