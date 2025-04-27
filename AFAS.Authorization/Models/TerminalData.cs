namespace AFAS.Authorization.Models;

/// <summary>
/// 终端信息
/// </summary>
public class TerminalData
{
    /// <summary>
    /// 终端编码
    /// </summary>
    public string TerminalId { get; set; } = "";

    /// <summary>
    /// 终端名称
    /// </summary>
    public string TerminalName { get; set; } = "";

    /// <summary>
    /// 终端类型
    /// </summary>
    public string TerminalType { get; set; } = "";

    /// <summary>
    /// 系统代号
    /// </summary>
    public string SystemId { get; set; } = "";

    /// <summary>
    /// 客户端与服务器的时间间隔毫秒数
    /// </summary>
    internal long TimeSpan { get; set; }
}
