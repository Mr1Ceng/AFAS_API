using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogDebug
{
    public int LogId { get; set; } = 0;

    public string TimeStamp { get; set; } = "";

    public string Message { get; set; } = "";

    public string Remark { get; set; } = "";

    public string Content { get; set; } = "";

    public string Data { get; set; } = "";
}
