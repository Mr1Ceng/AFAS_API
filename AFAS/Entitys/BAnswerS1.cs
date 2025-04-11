using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswerS1
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int OriginScore { get; set; }

    public int StandardScore { get; set; }

    public string Remark { get; set; } = null!;
}
