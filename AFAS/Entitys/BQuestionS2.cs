using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BQuestionS2
{
    public string QuestionId { get; set; } = null!;

    public int GridRow { get; set; }

    public int GridColumn { get; set; }

    public int GridValue { get; set; }
}
