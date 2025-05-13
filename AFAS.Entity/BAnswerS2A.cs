using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswerS2A
{
    public string AnswerId { get; set; } = "";

    public string QuestionId { get; set; } = "";

    public int GridRow { get; set; } = 0;

    public int GridColumn { get; set; } = 0;

    public bool Selected { get; set; }
}
