using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BEvaluationStandard
{
    public string LevelCode { get; set; } = null!;

    public string LevelName { get; set; } = null!;

    public int S1 { get; set; }

    public int S2 { get; set; }

    public int S3 { get; set; }

    public int S4 { get; set; }

    public int S5 { get; set; }

    public int T1 { get; set; }

    public int T2 { get; set; }

    public int T3 { get; set; }
}
