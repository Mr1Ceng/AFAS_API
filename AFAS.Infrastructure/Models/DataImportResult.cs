namespace AFAS.Infrastructure.Models;

/// <summary>
/// 数据导入结果
/// </summary>
public class DataImportResult
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 成功行数
    /// </summary>
    public int SuccessCount { get; set; }

    /// <summary>
    /// 错误行数
    /// </summary>
    public int ErrorCount { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public List<string> ErrorMessages { get; set; } = new();

    /// <summary>
    /// 文件路径
    /// </summary>
    public string OutputUrl { get; set; } = "";

    /// <summary>
    /// 导入结果
    /// </summary>
    public object Data { get; set; } = new();
}
