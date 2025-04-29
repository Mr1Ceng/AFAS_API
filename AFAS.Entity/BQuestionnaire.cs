using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionnaire
{
    public string QuestionnaireId { get; set; } = "";

    public string QuestionnaireName { get; set; } = "";

    public string VersionName { get; set; } = "";

    public string Remark { get; set; } = "";
}
