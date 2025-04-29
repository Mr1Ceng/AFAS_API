using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BAnswer
{
    public string AnswerId { get; set; } = "";

    public string QuestionnaireId { get; set; } = "";

    public string UserId { get; set; } = "";

    public string QuestionnaireDate { get; set; } = "";

    public string TeacherId { get; set; } = "";

    public string Status { get; set; } = "";

    public string RadarImage { get; set; } = "";

    public string Simage { get; set; } = "";

    public string Sresult { get; set; } = "";

    public string Timage { get; set; } = "";

    public string Tresult { get; set; } = "";

    public string Weak { get; set; } = "";

    public string Advantage { get; set; } = "";

    public string Remark { get; set; } = "";

    public string SuggestedCourse { get; set; } = "";

    public string LevelCode { get; set; } = "";
}
