namespace AFAS.Infrastructure.Models;

/// <summary>
/// 系统字典选项
/// </summary>
public class DictionaryItem
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public DictionaryItem()
    {
        DictionaryId = "";
        ItemId = "";
        ItemName = "";
        ParentItemId = "";
        Field1 = "";
        Field2 = "";
        Field3 = "";
        Introduce = "";
        Sort = 0;
        Status = "";
    }

    /// <summary>
    /// 字典编码
    /// </summary>
    public string DictionaryId { get; set; }

    /// <summary>
    /// 条目名称
    /// </summary>
    public string ItemId { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// 父条目编码
    /// </summary>
    public string ParentItemId { get; set; }

    /// <summary>
    /// 拓展属性1
    /// </summary>
    public string Field1 { get; set; }

    /// <summary>
    /// 拓展属性2
    /// </summary>
    public string Field2 { get; set; }

    /// <summary>
    /// 拓展属性3
    /// </summary>
    public string Field3 { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Introduce { get; set; }

    /// <summary>
    /// 显示顺序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; }
}
