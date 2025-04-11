using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswerS2A
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public string GridRow { get; set; } = null!;

    public int MarkNumber { get; set; }

    public int ErrorNumber { get; set; }

    public int TimeConsume { get; set; }
}
