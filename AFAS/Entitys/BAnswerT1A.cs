﻿using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswerT1A
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public int QuestionSort { get; set; }

    public string AnswerSort { get; set; } = null!;
}
