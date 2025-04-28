using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class SService
{
    public string SystemId { get; set; } = null!;

    public string ServiceId { get; set; } = null!;

    public int ServiceCode { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ServiceType { get; set; } = null!;

    public string RootUrl { get; set; } = null!;

    public string VirtualPath { get; set; } = null!;

    public string CorsUrls { get; set; } = null!;

    public int Timeout { get; set; }

    public string Remark { get; set; } = null!;

    public DateTime CreateStamp { get; set; }

    public DateTime ModifyStamp { get; set; }
}
