using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogApi
{
    public int LogId { get; set; } = 0;

    public string TimeStamp { get; set; } = "";

    public string TerminalId { get; set; } = "";

    public string AuthType { get; set; } = "";

    public string Token { get; set; } = "";

    public string SiteUrl { get; set; } = "";

    public string PhysicalPath { get; set; } = "";

    public string AbsoluteUri { get; set; } = "";

    public string IpAddress { get; set; } = "";

    public string UserLanguages { get; set; } = "";

    public string UserAgent { get; set; } = "";

    public long TerminalSpan { get; set; }

    public long TokenSpan { get; set; }

    public bool IsSuccess { get; set; }

    public string Exception { get; set; } = "";
}
