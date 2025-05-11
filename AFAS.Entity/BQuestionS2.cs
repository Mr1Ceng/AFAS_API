using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionS2
{
    public string QuestionId { get; set; } = null!;

    public int GridRow { get; set; } = 0;

    public int GridColumn { get; set; } = 0;

    public int GridValue { get; set; } = 0;

    public bool IsTrue { get; set; }
}
