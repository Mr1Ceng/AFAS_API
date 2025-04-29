using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerT1A
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public int QuestionSort { get; set; }

    public string AnswerSort { get; set; } = "";
}
