using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Mr1Ceng.Util.Swagger.internals;

/// <summary>
/// 自定义文档处理器（枚举注释）
/// </summary>
public class EnumDescriptionDocumentProcessor : IDocumentProcessor
{
    /// <summary>
    /// 执行操作
    /// </summary>
    /// <param name="context"></param>
    public void Process(DocumentProcessorContext context)
    {
        // // 遍历所有的 schema
        // foreach (var schema in context.SchemaResolver.Schemas)
        // {
        //     // 判断当前 schema 是否是枚举
        //     if (schema.IsEnumeration)
        //     {
        //         // 为每个枚举值添加描述
        //         foreach (var enumValue in schema.Enumeration)
        //         {
        //             var description = GetEnumDescription(enumValue);
        //             if (description != null)
        //             {
        //                 schema.Description += $"{enumValue}: {description}\n";
        //             }
        //         }
        //     }
        // }
    }

    private string GetEnumDescription(object enumValue) =>

        // 这里可以通过反射或其他方式获取枚举值的描述
        // 返回对应的注释
        "这是枚举值的描述";
}
