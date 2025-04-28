using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class SSystem
{
    public string SystemId { get; set; } = null!;

    public int SystemCode { get; set; }

    public string SystemName { get; set; } = null!;

    public string SystemType { get; set; } = null!;

    public DateTime CreateStamp { get; set; }

    public DateTime ModifyStamp { get; set; }
}
