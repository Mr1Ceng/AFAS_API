using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerT3
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int Level1 { get; set; } = 0;

    public int Level2 { get; set; } = 0;

    public int StandardScore { get; set; } = 0;

    public string Remark { get; set; } = null!;
}
