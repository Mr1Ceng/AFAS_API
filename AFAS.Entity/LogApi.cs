using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogApi
{
    public int LogId { get; set; }

    public string TimeStamp { get; set; } = null!;

    public string TerminalId { get; set; } = null!;

    public string AuthType { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string SiteUrl { get; set; } = null!;

    public string PhysicalPath { get; set; } = null!;

    public string AbsoluteUri { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public string UserLanguages { get; set; } = null!;

    public string UserAgent { get; set; } = null!;

    public long TerminalSpan { get; set; }

    public long TokenSpan { get; set; }

    public bool IsSuccess { get; set; }

    public string Exception { get; set; } = null!;
}
