using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BUser
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public int Age { get; set; }

    public string Password { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string? Role { get; set; }
}
