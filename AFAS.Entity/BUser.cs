using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BUser
{
    public string UserId { get; set; } = "";

    public string UserName { get; set; } = "";

    public string NickName { get; set; } = "";

    public string AvatarUrl { get; set; } = "";

    public string Gender { get; set; } = "";

    public int Age { get; set; }

    public string Password { get; set; } = "";

    public string Account { get; set; } = "";

    public string Mobile { get; set; } = "";

    public string Role { get; set; } = "";

    public bool IsDeveloper { get; set; }
}
