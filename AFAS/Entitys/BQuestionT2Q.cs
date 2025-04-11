using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BQuestionT2Q
{
    public string QuestionId { get; set; } = null!;

    public int QuestionSort { get; set; }

    public string QuestionQ1 { get; set; } = null!;

    public string QuestionQ2 { get; set; } = null!;
}
