using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS1
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public int OriginScore { get; set; } = 0;

    public int StandardScore { get; set; } = 0;

    public string Remark { get; set; } = "";
}
