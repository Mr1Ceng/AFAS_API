﻿using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BAnswerT3A
{
    public string AnswerId { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public bool QuestionType { get; set; }

    public int QuestionSort { get; set; }

    public int Level { get; set; }

    public string Value { get; set; } = null!;
}
