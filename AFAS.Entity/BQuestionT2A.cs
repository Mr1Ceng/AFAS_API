using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionT2A
{
    public string QuestionId { get; set; } = "";

    public int QuestionSort { get; set; }

    public string AnswerSort { get; set; } = "";

    public string Answer { get; set; } = "";

    public bool IsTrue { get; set; }
}
