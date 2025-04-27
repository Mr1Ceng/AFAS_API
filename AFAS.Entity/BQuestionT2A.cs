using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionT2A
{
    public string QuestionId { get; set; } = null!;

    public int QuestionSort { get; set; }

    public string AnswerSort { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public bool IsTrue { get; set; }
}
