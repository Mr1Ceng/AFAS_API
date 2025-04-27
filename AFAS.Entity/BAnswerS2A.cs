using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS2A
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int GridRow { get; set; }

    public int GridColumn { get; set; }

    public bool Selected { get; set; }
}
