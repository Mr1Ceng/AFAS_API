using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class SSystem
{
    public string SystemId { get; set; } = "";

    public int SystemCode { get; set; } = 0;

    public string SystemName { get; set; } = "";

    public string SystemType { get; set; } = "";

    public string CreateStamp { get; set; } = "";

    public string ModifyStamp { get; set; } = "";
}
