using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BQuestionS1
{
    public string QuestionId { get; set; } = null!;

    public string GridType { get; set; } = null!;

    public int GridValue { get; set; }

    public int GridSort { get; set; }
}
