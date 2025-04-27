using AFAS.Entity;
using AFAS.Infrastructure.Models;
using Mr1Ceng.Util;

namespace YaoZR.Service.Data.SystemBasic;

/// <summary>
/// 终端相关工具类
/// </summary>
public class TerminalHelper
{
    /// <summary>
    /// 获取终端列表
    /// </summary>
    /// <returns></returns>
    public static IList<TerminalItem> GetTerminalList()
    {
        var terminals = new List<STerminal>();
        using (var context = new AfasContext())
        {
            terminals = context.STerminals.OrderBy(x => x.TerminalCode).ToList();
        }
        return terminals.Select(x => new TerminalItem
        {
            SystemId = x.SystemId,
            TerminalId = x.TerminalId,
            TerminalCode = x.TerminalCode,
            TerminalName = x.TerminalName,
            TerminalType = x.TerminalType,
            IsSite = x.IsSite
        }).ToList();
    }

    /// <summary>
    /// 获取终端列表
    /// </summary>
    /// <returns></returns>
    public static IList<TerminalKeyItem> GetTerminalKeyList() => new s_TerminalManager().GetEntityObjects()
        .Cast<s_Terminal>()
        .OrderBy(x => x.TerminalCode)
        .Select(x => new TerminalKeyItem
        {
            SystemId = x.SystemId,
            TerminalId = x.TerminalId,
            TerminalCode = x.TerminalCode,
            TerminalName = x.TerminalName,
            TerminalType = x.TerminalType,
            IsSite = x.IsSite,
            TerminalKey = x.TerminalKey,
            TerminalSecret = x.TerminalSecret
        }).ToList();

    /// <summary>
    /// 获取终端信息
    /// </summary>
    /// <param name="terminalId"></param>
    /// <returns></returns>
    public static TerminalInfo GetTerminalInfo(string terminalId)
    {
        terminalId = GetString.FromObject(terminalId, 50);
        var terminal = new s_TerminalManager().GetEntityObject(terminalId);
        if (terminal.IsPersistent)
        {
            var system = new s_SystemManager().GetEntityObject(terminal.SystemId);
            return new TerminalInfo
            {
                SystemId = terminal.SystemId,
                SystemCode = system.SystemCode,
                SystemName = system.SystemName,
                SystemType = system.SystemType,
                TerminalId = terminal.TerminalId,
                TerminalCode = terminal.TerminalCode,
                TerminalName = terminal.TerminalName,
                TerminalType = terminal.TerminalType,
                TerminalKey = terminal.TerminalKey,
                TerminalSecret = terminal.TerminalSecret,
                IsSite = terminal.IsSite,
                Remark = terminal.Remark
            };
        }
        return new TerminalInfo
        {
            TerminalId = terminalId
        };
    }
}
