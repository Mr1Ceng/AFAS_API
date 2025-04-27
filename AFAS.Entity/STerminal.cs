using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class STerminal
{
    public string SystemId { get; set; } = null!;

    public string TerminalId { get; set; } = null!;

    public int TerminalCode { get; set; }

    public string TerminalName { get; set; } = null!;

    public string TerminalType { get; set; } = null!;

    public string TerminalKey { get; set; } = null!;

    public string TerminalSecret { get; set; } = null!;

    public bool IsSite { get; set; }

    public string Remark { get; set; } = null!;

    public string CreateStamp { get; set; } = null!;

    public string ModifyStamp { get; set; } = null!;
}
