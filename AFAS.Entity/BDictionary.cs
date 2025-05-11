using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BDictionary
{
    public string DictionaryId { get; set; } = null!;

    public string DictionaryName { get; set; } = null!;

    public string Introduce { get; set; } = null!;

    public int Sort { get; set; } = 0;

    public string Status { get; set; } = null!;
}
