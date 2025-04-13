using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BQuestion
{
    public string QuestionId { get; set; } = null!;

    public string QuestionCode { get; set; } = null!;

    public string QuestionName { get; set; } = null!;

    public string QuestionnaireId { get; set; } = null!;

    public string Precautions { get; set; } = null!;

    public string Instruction { get; set; } = null!;

    public string Instruction2 { get; set; } = null!;

    public string Instruction3 { get; set; } = null!;

    public string Instruction4 { get; set; } = null!;
}
