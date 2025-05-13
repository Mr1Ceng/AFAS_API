using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionS3
{
    public string QuestionId { get; set; } = "";

    public int GridRow { get; set; } = 0;

    public int GridColumn { get; set; } = 0;

    public int GridValue { get; set; } = 0;
}
