using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswerS3A
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int GridRow { get; set; }

    public int GridColumn { get; set; }

    public int Value { get; set; }
}
