using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionS1
{
    public string QuestionId { get; set; } = "";

    public string GridType { get; set; } = "";

    public int GridValue { get; set; } = 0;

    public int GridSort { get; set; } = 0;
}
