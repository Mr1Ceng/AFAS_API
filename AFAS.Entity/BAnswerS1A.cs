using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS1A
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public string GridType { get; set; } = null!;

    public int TimeConsume { get; set; } = 0;
}
