using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.AspNetCore;
using Mr1Ceng.Util.Swagger.internals;

namespace Mr1Ceng.Util.Swagger;

/// <summary>
/// Swagger配置类
/// </summary>
public class SwaggerHelper
{
    /// <summary>
    /// 配置API服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <param name="heads"></param>
    public static void Config(IServiceCollection services, SwaggerConfig config, List<string>? heads = null)
    {
        if (config.ShowSwagger == ShowSwagger.HIDE)
        {
            return;
        }

        foreach (var item in config.SwaggerGroups)
        {
            services.AddOpenApiDocument(settings =>
            {
                if (config.SwaggerGroups.Count > 1)
                {
                    settings.ApiGroupNames = [item.GroupId];
                }
                settings.DocumentName = item.GroupId;
                settings.Title = item.GroupName;
                settings.Version = DateHelper.GetDateString();

                settings.UseRouteNameAsOperationId = true;
                settings.UseHttpAttributeNameAsOperationId = true;
                settings.UseControllerSummaryAsTagDescription = true;
                settings.GenerateOriginalParameterNames = false;

                // 添加自定义路由处理器，使用正则表达式将路径斜杠后第一个字符转换为小写
                settings.OperationProcessors.Add(new ConvertPathFirstCharToLowerProcessor());

                // 添加枚举注释
                //config.DocumentProcessors.Add(new EnumDescriptionDocumentProcessor());

                if (config.ShowSwagger == ShowSwagger.LOCAL_DEBUG)
                {
                    // 添加请求头
                    settings.AddSecurity("Authorization", [], new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Name = "Authorization"
                    });

                    if (heads != null)
                    {
                        // 添加请求头
                        foreach (var head in heads)
                        {
                            settings.AddSecurity(head, [], new OpenApiSecurityScheme
                            {
                                Type = OpenApiSecuritySchemeType.ApiKey,
                                In = OpenApiSecurityApiKeyLocation.Header,
                                Name = head
                            });
                        }
                    }
                }
            });
        }
    }

    /// <summary>
    /// 应用API服务
    /// </summary>
    /// <param name="app"></param>
    /// <param name="config"></param>
    public static void Apply(IApplicationBuilder app, SwaggerConfig config)
    {
        if (config.ShowSwagger == ShowSwagger.HIDE)
        {
            return;
        }

        foreach (var item in config.SwaggerGroups)
        {
            app.UseOpenApi(settings =>
            {
                settings.DocumentName = item.GroupId;
                settings.Path = $"/{item.GroupId}/swagger.json";
            });
        }

        if (config.ShowSwagger != ShowSwagger.ONLY_JSON)
        {
            app.UseSwaggerUi(settings =>
            {
                settings.DocExpansion = "list"; //full全部展开
                settings.EnableTryItOut = config.ShowSwagger == ShowSwagger.LOCAL_DEBUG;

                foreach (var item in config.SwaggerGroups)
                {
                    var url = config.ShowSwagger == ShowSwagger.LOCAL_DEBUG
                        ? $"/{item.GroupId}/swagger.json"
                        : $"/{config.VirtualPath}/{item.GroupId}/swagger.json";

                    settings.SwaggerRoutes.Add(new SwaggerUiRoute(item.GroupId, url));
                }
            });
        }
    }
}
