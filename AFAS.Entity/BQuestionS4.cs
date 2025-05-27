using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BQuestionS4
{
    public string QuestionId { get; set; } = "";

    public int Spacing { get; set; } = 0;

    public int Perturbation { get; set; } = 0;

    public int RingNumber { get; set; } = 0;
}
