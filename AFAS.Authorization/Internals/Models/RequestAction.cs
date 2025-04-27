namespace AFAS.Authorization.Internals.Models;

/// <summary>
/// 请求Action信息
/// </summary>
public class RequestAction
{
    /// <summary>
    /// 请求的API站点
    /// </summary>
    public string SiteUrl { get; set; } = "";

    /// <summary>
    /// 请求的API路径
    /// </summary>
    public string PhysicalPath { get; set; } = "";
}
