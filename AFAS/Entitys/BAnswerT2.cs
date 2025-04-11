using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswerT2
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int Number1 { get; set; }

    public int Number2 { get; set; }

    public int StandardScore { get; set; }

    public string Remark { get; set; } = null!;
}
