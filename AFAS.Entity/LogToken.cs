using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogToken
{
    public int LogId { get; set; }

    public string TimeStamp { get; set; } = null!;

    public string TerminalId { get; set; } = null!;

    public string AuthType { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string AppId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string NickName { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public bool IsDeveloper { get; set; }

    public bool IsStaff { get; set; }

    public string TokenData { get; set; } = null!;
}
