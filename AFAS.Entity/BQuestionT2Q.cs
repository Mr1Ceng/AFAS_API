using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionT2Q
{
    public string QuestionId { get; set; } = "";

    public int QuestionSort { get; set; }

    public string QuestionQ1 { get; set; } = "";

    public string QuestionQ2 { get; set; } = "";
}
