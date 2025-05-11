using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionT2
{
    public string QuestionId { get; set; } = null!;

    public string Question { get; set; } = null!;

    public int Number1 { get; set; } = 0;

    public int Number2 { get; set; } = 0;
}
