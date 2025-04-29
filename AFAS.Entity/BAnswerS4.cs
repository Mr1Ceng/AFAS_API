using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS4
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public int TimeConsume { get; set; }

    public string QuestionImage { get; set; } = "";

    public string AnswerImage { get; set; } = "";

    public int CrossNumber { get; set; }

    public int StandardScore { get; set; }

    public string Remark { get; set; } = "";
}
