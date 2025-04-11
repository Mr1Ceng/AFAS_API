using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 提交表格查询的数据模型
/// </summary>
public class TableQueryModel
{
    /// <summary>
    /// 页码索引（从0开始，0是第一页）
    /// </summary>
    [Description("页码索引（从0开始，0是第一页）")]
    public int Index { get; set; }

    /// <summary>
    /// 页面大小（0，表示不分页）
    /// </summary>
    [Description("页面大小（0表示不分页）")]
    public int Size { get; set; }

    /// <summary>
    /// 排序字段
    /// </summary>
    [Description("排序字段")]
    public KeySorterValue? Sorter { get; set; }
}

/// <summary>
/// 提交表格查询的数据模型
/// </summary>
/// <typeparam name="T">查询字段</typeparam>
public class TableQueryModel<T>
{
    /// <summary>
    /// 页码索引（从0开始，0是第一页）
    /// </summary>
    [Description("页码索引（从0开始，0是第一页）")]
    public int Index { get; set; }

    /// <summary>
    /// 页面大小（0，表示不分页）
    /// </summary>
    [Description("页面大小（0表示不分页）")]
    public int Size { get; set; }

    /// <summary>
    /// 排序字段
    /// </summary>
    [Description("排序字段")]
    public KeySorterValue? Sorter { get; set; }

    /// <summary>
    /// 查询数据模型
    /// </summary>
    [Description("查询数据模型")]
    public T? Data { get; set; }
}

/// <summary>
/// 提交表格查询的数据模型
/// </summary>
/// <typeparam name="T">查询字段</typeparam>
/// <typeparam name="F">过滤字段</typeparam>
public class TableQueryModel<T, F>
{
    /// <summary>
    /// 页码索引（从0开始，0是第一页）
    /// </summary>
    [Description("页码索引（从0开始，0是第一页）")]
    public int Index { get; set; }

    /// <summary>
    /// 页面大小（0，表示不分页）
    /// </summary>
    [Description("页面大小（0表示不分页）")]
    public int Size { get; set; }

    /// <summary>
    /// 排序字段
    /// </summary>
    [Description("排序字段")]
    public KeySorterValue? Sorter { get; set; }

    /// <summary>
    /// 过滤数据模型
    /// </summary>
    [Description("过滤数据模型")]
    public F? Filter { get; set; }

    /// <summary>
    /// 查询数据模型
    /// </summary>
    [Description("查询数据模型")]
    public T? Data { get; set; }
}
