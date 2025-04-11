using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BQuestionT1Q
{
    public string QuestionId { get; set; } = null!;

    public int QuestionSort { get; set; }

    public string QuestionQ { get; set; } = null!;
}
