using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class STerminal
{
    public string SystemId { get; set; } = "";

    public string TerminalId { get; set; } = "";

    public int TerminalCode { get; set; } = 0;

    public string TerminalName { get; set; } = "";

    public string TerminalType { get; set; } = "";

    public string TerminalKey { get; set; } = "";

    public string TerminalSecret { get; set; } = "";

    public bool IsSite { get; set; }

    public string Remark { get; set; } = "";

    public string CreateStamp { get; set; } = "";

    public string ModifyStamp { get; set; } = "";
}
