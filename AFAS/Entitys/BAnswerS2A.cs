using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswerS2A
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public string GridRow { get; set; } = null!;

    public int GridColumn { get; set; }

    public bool Selected { get; set; }
}
