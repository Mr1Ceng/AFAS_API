﻿using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BQuestionT3
{
    public string QuestionId { get; set; } = null!;

    public bool QuestionType { get; set; }

    public int QuestionSort { get; set; }

    public int Level { get; set; }

    public string QuestionQ { get; set; } = null!;

    public string QuestionA { get; set; } = null!;
}
