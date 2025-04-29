using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestion
{
    public string QuestionId { get; set; } = "";

    public string QuestionCode { get; set; } = "";

    public string QuestionName { get; set; } = "";

    public string QuestionnaireId { get; set; } = "";

    public string Precautions { get; set; } = "";

    public string Instruction { get; set; } = "";

    public string Instruction2 { get; set; } = "";

    public string Instruction3 { get; set; } = "";

    public string Instruction4 { get; set; } = "";
}
