using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BDictionary
{
    public string DictionaryId { get; set; } = "";

    public string DictionaryName { get; set; } = "";

    public string Introduce { get; set; } = "";

    public int Sort { get; set; } = 0;

    public string Status { get; set; } = "";
}
