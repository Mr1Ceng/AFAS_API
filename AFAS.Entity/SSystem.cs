using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class SSystem
{
    public string SystemId { get; set; } = null!;

    public int SystemCode { get; set; } = 0;

    public string SystemName { get; set; } = null!;

    public string SystemType { get; set; } = null!;

    public string CreateStamp { get; set; } = "";

    public string ModifyStamp { get; set; } = "";
}
