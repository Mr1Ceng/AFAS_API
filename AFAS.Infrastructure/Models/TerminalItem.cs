namespace AFAS.Infrastructure.Models;

/// <summary>
/// 终端信息
/// </summary>
public class TerminalItem
{
    /// <summary>
    /// 系统代号;s_System.SystemId
    /// </summary>
    public string SystemId { get; set; } = "";

    /// <summary>
    /// 终端代码
    /// </summary>
    public string TerminalId { get; set; } = "";

    /// <summary>
    /// 终端代号
    /// </summary>
    public int TerminalCode { get; set; } = 0;

    /// <summary>
    /// 终端名称
    /// </summary>
    public string TerminalName { get; set; } = "";

    /// <summary>
    /// 终端类型
    /// </summary>
    public string TerminalType { get; set; } = "";

    /// <summary>
    /// 是否存在页面管理
    /// </summary>
    public bool IsSite { get; set; } = false;
}
