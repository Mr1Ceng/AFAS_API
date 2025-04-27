namespace AFAS.Authorization.Internals.Models;

/// <summary>
/// 请求的浏览器信息
/// </summary>
public class RequestAgent
{
    /// <summary>
    /// 客户端IP地址
    /// </summary>
    public string IpAddress { get; set; } = "";

    /// <summary>
    /// 请求的网络绝对路径
    /// </summary>
    public string AbsoluteUri { get; set; } = "";

    /// <summary>
    /// 客户端语言
    /// </summary>
    public string UserLanguages { get; set; } = "";

    /// <summary>
    /// 浏览器的UserAgent
    /// </summary>
    public string UserAgent { get; set; } = "";
}
