using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 提交列表查询的数据模型
/// </summary>
public class ListQueryModel
{
    /// <summary>
    /// 排序字段
    /// </summary>
    [Description("排序字段")]
    public KeySorterValue? Sorter { get; set; }
}

/// <summary>
/// 提交列表查询的数据模型
/// </summary>
/// <typeparam name="T">查询字段</typeparam>
public class ListQueryModel<T>
{
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
/// 提交列表查询的数据模型
/// </summary>
/// <typeparam name="T">查询字段</typeparam>
/// <typeparam name="F">过滤字段</typeparam>
public class ListQueryModel<T, F>
{
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
