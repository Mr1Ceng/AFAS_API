using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogUserLogin
{
    public int LogId { get; set; } = 0;

    public string TimeStamp { get; set; } = "";

    public string TerminalId { get; set; } = null!;

    public string AppId { get; set; } = null!;

    public string LoginMethod { get; set; } = null!;

    public string NewToken { get; set; } = null!;

    public bool IsSuccess { get; set; }

    public string PostData { get; set; } = null!;

    public string DebugData { get; set; } = null!;

    public string Exception { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public string UserLanguages { get; set; } = null!;

    public string UserAgent { get; set; } = null!;

    public string DeviceBrand { get; set; } = null!;

    public string DeviceModel { get; set; } = null!;

    public int BenchmarkLevel { get; set; } = 0;

    public string OsType { get; set; } = null!;

    public string OsVersion { get; set; } = null!;

    public double PixelRatio { get; set; }

    public int ScreenWidth { get; set; } = 0;

    public int ScreenHeight { get; set; } = 0;

    public int WindowWidth { get; set; } = 0;

    public int WindowHeight { get; set; } = 0;
}
