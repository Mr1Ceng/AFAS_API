using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogToken
{
    public int LogId { get; set; }

    public string TimeStamp { get; set; } = "";

    public string TerminalId { get; set; } = "";

    public string AuthType { get; set; } = "";

    public string Token { get; set; } = "";

    public string AppId { get; set; } = "";

    public string UserId { get; set; } = "";

    public string UserName { get; set; } = "";

    public string NickName { get; set; } = "";

    public string Mobile { get; set; } = "";

    public bool IsDeveloper { get; set; }

    public bool IsStaff { get; set; }

    public string TokenData { get; set; } = "";
}
