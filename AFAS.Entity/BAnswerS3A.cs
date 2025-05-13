using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS3A
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public int GridRow { get; set; } = 0;

    public int GridColumn { get; set; } = 0;

    public int Value { get; set; } = 0;
}
