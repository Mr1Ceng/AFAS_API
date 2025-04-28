namespace AFAS.Infrastructure.Models;

/// <summary>
/// 终端信息（含Key、Secret）
/// </summary>
public class TerminalKeyItem : TerminalItem
{
    /// <summary>
    /// 授权Key
    /// </summary>
    public string TerminalKey { get; set; } = "";

    /// <summary>
    /// 授权Secret
    /// </summary>
    public string TerminalSecret { get; set; } = "";
}
