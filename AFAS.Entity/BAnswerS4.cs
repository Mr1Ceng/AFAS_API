using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS4
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int TimeConsume { get; set; } = 0;

    public string QuestionImage { get; set; } = null!;

    public string AnswerImage { get; set; } = null!;

    public int CrossNumber { get; set; } = 0;

    public int StandardScore { get; set; } = 0;

    public string Remark { get; set; } = null!;
}
