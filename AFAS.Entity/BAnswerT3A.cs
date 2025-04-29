using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerT3A
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public bool QuestionType { get; set; }

    public int QuestionSort { get; set; }

    public int Level { get; set; }

    public string Value { get; set; } = "";
}
