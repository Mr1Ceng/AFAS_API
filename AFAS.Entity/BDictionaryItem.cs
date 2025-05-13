using System;
using System.Collections.Generic;

namespace AFAS.Entity;

public partial class BDictionaryItem
{
    public string DictionaryId { get; set; } = "";

    public string ItemId { get; set; } = "";

    public string ItemName { get; set; } = "";

    public string ParentItemId { get; set; } = "";

    public string Field1 { get; set; } = "";

    public string Field2 { get; set; } = "";

    public string Field3 { get; set; } = "";

    public string Introduce { get; set; } = "";

    public int Sort { get; set; } = 0;

    public string Status { get; set; } = "";
}
