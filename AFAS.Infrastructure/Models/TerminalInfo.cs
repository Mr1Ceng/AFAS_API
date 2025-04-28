namespace AFAS.Infrastructure.Models;

/// <summary>
/// 终端信息
/// </summary>
public class TerminalInfo : TerminalItem
{
    /// <summary>
    /// 系统代号
    /// </summary>
    public int SystemCode { get; set; } = 0;

    /// <summary>
    /// 系统名称
    /// </summary>
    public string SystemName { get; set; } = "";

    /// <summary>
    /// 系统类型
    /// </summary>
    public string SystemType { get; set; } = "";

    /// <summary>
    /// 授权Key
    /// </summary>
    public string TerminalKey { get; set; } = "";

    /// <summary>
    /// 授权Secret
    /// </summary>
    public string TerminalSecret { get; set; } = "";

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; } = "";
}
