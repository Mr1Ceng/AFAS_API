using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class SService
{
    public string SystemId { get; set; } = "";

    public string ServiceId { get; set; } = "";

    public int ServiceCode { get; set; }

    public string ServiceName { get; set; } = "";

    public string ServiceType { get; set; } = "";

    public string RootUrl { get; set; } = "";

    public string VirtualPath { get; set; } = "";

    public string CorsUrls { get; set; } = "";

    public int Timeout { get; set; }

    public string Remark { get; set; } = "";

    public string CreateStamp { get; set; } = "";

    public string ModifyStamp { get; set; } = "";
}
