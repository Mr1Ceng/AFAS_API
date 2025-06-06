﻿using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class LogUserLogin
{
    public int LogId { get; set; } = 0;

    public string TimeStamp { get; set; } = "";

    public string TerminalId { get; set; } = "";

    public string AppId { get; set; } = "";

    public string LoginMethod { get; set; } = "";

    public string NewToken { get; set; } = "";

    public bool IsSuccess { get; set; }

    public string PostData { get; set; } = "";

    public string DebugData { get; set; } = "";

    public string Exception { get; set; } = "";

    public string IpAddress { get; set; } = "";

    public string UserLanguages { get; set; } = "";

    public string UserAgent { get; set; } = "";

    public string DeviceBrand { get; set; } = "";

    public string DeviceModel { get; set; } = "";

    public int BenchmarkLevel { get; set; } = 0;

    public string OsType { get; set; } = "";

    public string OsVersion { get; set; } = "";

    public double PixelRatio { get; set; }

    public int ScreenWidth { get; set; } = 0;

    public int ScreenHeight { get; set; } = 0;

    public int WindowWidth { get; set; } = 0;

    public int WindowHeight { get; set; } = 0;
}
