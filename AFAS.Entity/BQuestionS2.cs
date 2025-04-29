using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionS2
{
    public string QuestionId { get; set; } = "";

    public int GridRow { get; set; }

    public int GridColumn { get; set; }

    public int GridValue { get; set; }

    public bool IsTrue { get; set; }
}
