using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS1A
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public string GridType { get; set; } = "";

    public int TimeConsume { get; set; }
}
