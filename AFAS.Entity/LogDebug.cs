using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogDebug
{
    public int LogId { get; set; }

    public string TimeStamp { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Remark { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Data { get; set; } = null!;
}
