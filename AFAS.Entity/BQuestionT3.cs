using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionT3
{
    public string QuestionId { get; set; } = "";

    public bool QuestionType { get; set; }

    public int QuestionSort { get; set; }

    public int Level { get; set; }

    public string QuestionQ { get; set; } = "";

    public string QuestionA { get; set; } = "";
}
