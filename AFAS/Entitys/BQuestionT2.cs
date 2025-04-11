using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BQuestionT2
{
    public string QuestionId { get; set; } = null!;

    public string Question { get; set; } = null!;

    public int Number1 { get; set; }

    public int Number2 { get; set; }
}
