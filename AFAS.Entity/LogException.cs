using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogException
{
    public int LogId { get; set; }

    public string TimeStamp { get; set; } = "";

    public string Token { get; set; } = "";

    public string PageKey { get; set; } = "";

    public string ActionKey { get; set; } = "";

    public string FunctionName { get; set; } = "";

    public int ExceptionCode { get; set; }

    public string ExceptionType { get; set; } = "";

    public string ExceptionName { get; set; } = "";

    public string Message { get; set; } = "";

    public string DebugData { get; set; } = "";

    public string Exception { get; set; } = "";

    public bool IsDispose { get; set; }

    public string DisposeMessage { get; set; } = "";

    public string DisposeUserId { get; set; } = "";

    public string DisposeStamp { get; set; } = "";
}
