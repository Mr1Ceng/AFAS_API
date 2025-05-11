using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerT1
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int Number1 { get; set; } = 0;

    public int Number2 { get; set; } = 0;

    public int Number3 { get; set; } = 0;

    public int ErrorNumber { get; set; } = 0;

    public int StandardScore { get; set; } = 0;

    public string Remark { get; set; } = null!;
}
