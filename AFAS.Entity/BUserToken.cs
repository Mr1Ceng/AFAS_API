﻿using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BUserToken
{
    public string UserId { get; set; } = "";

    public string TokenData { get; set; } = "";

    public int LoginExpires { get; set; } = 0;

    public string CreateStamp { get; set; } = "";
}
