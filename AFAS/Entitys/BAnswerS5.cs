using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswerS5
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int TimeConsume { get; set; }

    public string QuestionImage { get; set; } = null!;

    public string AnswerImage { get; set; } = null!;

    public int ShapeNumber { get; set; }

    public int ErrorNumber { get; set; }

    public int StandardScore { get; set; }

    public string Remark { get; set; } = null!;
}
