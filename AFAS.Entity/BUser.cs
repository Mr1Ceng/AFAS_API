using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BUser
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string NickName { get; set; } = null!;

    public string AvatarUrl { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; } = 0;

    public string Password { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool IsDeveloper { get; set; }
}
