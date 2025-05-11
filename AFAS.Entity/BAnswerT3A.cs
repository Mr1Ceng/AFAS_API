using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerT3A
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public bool QuestionType { get; set; }

    public int QuestionSort { get; set; } = 0;

    public int Level { get; set; } = 0;

    public string Value { get; set; } = "";
}
