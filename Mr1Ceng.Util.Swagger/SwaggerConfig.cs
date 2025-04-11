namespace Mr1Ceng.Util.Swagger;

/// <summary>
/// Swagger配置信息
/// </summary>
public class SwaggerConfig
{
    /// <summary>
    /// 是否显示Swagger
    /// </summary>
    public ShowSwagger ShowSwagger { get; set; }

    /// <summary>
    /// 二级路径
    /// </summary>
    public string VirtualPath { get; set; } = "";

    /// <summary>
    /// API分组
    /// </summary>
    public List<SwaggerGroupItem> SwaggerGroups { get; set; } = [];
}
