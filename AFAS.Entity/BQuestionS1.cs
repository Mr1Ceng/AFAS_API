using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionS1
{
    public string QuestionId { get; set; } = "";

    public string GridType { get; set; } = "";

    public int GridValue { get; set; }

    public int GridSort { get; set; }
}
