using System;
using System.Collections.Generic;

namespace AFAS.Entitys;

public partial class BDictionaryItem
{
    public string DictionaryId { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public string ParentItemId { get; set; } = null!;

    public string Field1 { get; set; } = null!;

    public string Field2 { get; set; } = null!;

    public string Field3 { get; set; } = null!;

    public string Introduce { get; set; } = null!;

    public int Sort { get; set; }

    public string Status { get; set; } = null!;
}
