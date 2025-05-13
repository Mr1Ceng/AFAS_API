using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS5
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public int TimeConsume { get; set; } = 0;

    public string QuestionImage { get; set; } = "";

    public string AnswerImage { get; set; } = "";

    public int ShapeNumber { get; set; } = 0;

    public int ErrorNumber { get; set; } = 0;

    public int StandardScore { get; set; } = 0;

    public string Remark { get; set; } = "";
}
