using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BQuestionnaire
{
    public string QuestionnaireId { get; set; } = null!;

    public string QuestionnaireName { get; set; } = null!;

    public string VersionName { get; set; } = null!;

    public string Remark { get; set; } = null!;
}
