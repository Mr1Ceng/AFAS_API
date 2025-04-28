using AFAS.Infrastructure;
using Mr1Ceng.Util.Swagger;

namespace WingWell.WebApi.Platform;

/// <summary>
/// WebApi的分组名称
/// </summary>
internal class WebApiConfig
{
    internal static readonly List<string> BusinessAssemblyNames =
    [
        "AFAS",
    ];

    internal const string Account = "account";

    internal const string Authorization = "toolkit";

    internal const string Basic = "selector";

    internal const string DevTools = "data-tools";

    internal const string Questionnaire = "questionnaire";

    /// <summary>
    /// Swagger配置
    /// </summary>
    internal static SwaggerConfig SwaggerConfig => new()
    {
        VirtualPath = SystemConfig.VirtualPath,
        ShowSwagger = (ShowSwagger)SystemConfig.ShowSwagger,
        SwaggerGroups =
        [
            new SwaggerGroupItem(Account, "账号服务"),
            new SwaggerGroupItem(Authorization, "授权服务"),
            new SwaggerGroupItem(Basic, "基础服务"),
            new SwaggerGroupItem(DevTools, "开发工具"),
            new SwaggerGroupItem(Questionnaire, "测试服务"),
        ]
    };
}
