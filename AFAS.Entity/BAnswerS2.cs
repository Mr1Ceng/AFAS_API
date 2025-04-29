using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS2
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public int TimeConsume { get; set; }

    public int MarkNumber { get; set; }

    public int ErrorNumber { get; set; }

    public decimal ErrorRate { get; set; }

    public int StandardScore { get; set; }

    public string Remark { get; set; } = "";
}
