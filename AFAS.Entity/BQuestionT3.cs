using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionT3
{
    public string QuestionId { get; set; } = null!;

    public bool QuestionType { get; set; }

    public int QuestionSort { get; set; } = 0;

    public int Level { get; set; } = 0;

    public string QuestionQ { get; set; } = null!;

    public string QuestionA { get; set; } = null!;
}
