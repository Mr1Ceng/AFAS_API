using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswer
{
    public string AnswerId { get; set; } = null!;

    public string QuestionnaireId { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
