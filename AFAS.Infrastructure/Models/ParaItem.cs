namespace AFAS.Infrastructure.Models;

/// <summary>
/// 系统参数
/// </summary>
public class ParaItem
{
    /// <summary>
    /// 参数类型
    /// </summary>
    public string ParaType { get; set; } = "";

    /// <summary>
    /// 参数编码
    /// </summary>
    public string ParaId { get; set; } = "";

    /// <summary>
    /// 参数名称
    /// </summary>
    public string ParaName { get; set; } = "";

    /// <summary>
    /// 参数值
    /// </summary>
    public string ParaValue { get; set; } = "";

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; } = "";
}
