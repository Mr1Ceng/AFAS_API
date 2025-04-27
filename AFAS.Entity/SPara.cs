using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class SPara
{
    public string ParaId { get; set; } = null!;

    public string ParaName { get; set; } = null!;

    public string ParaValue { get; set; } = null!;

    public string ParaType { get; set; } = null!;

    public string Remark { get; set; } = null!;
}
