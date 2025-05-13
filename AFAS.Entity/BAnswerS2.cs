using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS2
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public int TimeConsume { get; set; } = 0;

    public int MarkNumber { get; set; } = 0;

    public int ErrorNumber { get; set; } = 0;

    public decimal ErrorRate { get; set; }

    public int StandardScore { get; set; } = 0;

    public string Remark { get; set; } = "";
}
