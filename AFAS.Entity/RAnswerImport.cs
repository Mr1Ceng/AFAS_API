using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class RAnswerImport
{
    public string ImportId { get; set; } = "";

    public string UserId { get; set; } = "";

    public string ImportStamp { get; set; } = "";

    public bool IsSuccess { get; set; }

    public string ImportResult { get; set; } = "";
}
