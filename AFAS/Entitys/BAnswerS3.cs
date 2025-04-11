using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswerS3
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int TimeConsume { get; set; }

    public int RightNumber { get; set; }

    public int ErrorNumber { get; set; }

    public int StandardScore { get; set; }

    public string Remark { get; set; } = null!;
}
