using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerT1A
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int QuestionSort { get; set; } = 0;

    public string AnswerSort { get; set; } = null!;
}
