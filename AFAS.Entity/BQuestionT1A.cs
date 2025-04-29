using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionT1A
{
    public string QuestionId { get; set; } = "";

    public int QuestionSort { get; set; }

    public string AnswerSort { get; set; } = "";

    public string Answer { get; set; } = "";

    public bool IsTrue { get; set; }
}
