using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BEvaluationStandard
{
    public string LevelCode { get; set; } = "";

    public string LevelName { get; set; } = "";

    public int S1 { get; set; } = 0;

    public int S2 { get; set; } = 0;

    public int S3 { get; set; } = 0;

    public int S4 { get; set; } = 0;

    public int S5 { get; set; } = 0;

    public int T1 { get; set; } = 0;

    public int T2 { get; set; } = 0;

    public int T3 { get; set; } = 0;
}
