using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerT1
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public int Number1 { get; set; }

    public int Number2 { get; set; }

    public int Number3 { get; set; }

    public int ErrorNumber { get; set; }

    public int StandardScore { get; set; }

    public string Remark { get; set; } = "";
}
