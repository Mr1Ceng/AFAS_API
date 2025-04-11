using System.Text.RegularExpressions;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Mr1Ceng.Util.Swagger.internals;

/// <summary>
/// 自定义操作处理器（使用正则表达式将路径斜杠后第一个字符转换为小写）
/// </summary>
internal partial class ConvertPathFirstCharToLowerProcessor : IOperationProcessor
{
    /// <summary>
    /// 执行操作
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public bool Process(OperationProcessorContext context)
    {
        // 使用正则表达式将路径斜杠后第一个字符转换为小写
        context.OperationDescription.Path = MyRegex().Replace(context.OperationDescription.Path, x => x.Value.ToLower());
        return true;
    }

    /// <summary>
    /// 正则表达式规则
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"(?<=/)([A-Z])")]
    private static partial Regex MyRegex();
}
