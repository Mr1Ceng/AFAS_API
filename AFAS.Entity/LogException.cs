using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogException
{
    public int LogId { get; set; } = 0;

    public string TimeStamp { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string PageKey { get; set; } = null!;

    public string ActionKey { get; set; } = null!;

    public string FunctionName { get; set; } = null!;

    public int ExceptionCode { get; set; } = 0;

    public string ExceptionType { get; set; } = null!;

    public string ExceptionName { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string DebugData { get; set; } = null!;

    public string Exception { get; set; } = null!;

    public bool IsDispose { get; set; }

    public string DisposeMessage { get; set; } = null!;

    public string DisposeUserId { get; set; } = null!;

    public string DisposeStamp { get; set; } = null!;
}
