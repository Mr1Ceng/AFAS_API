using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 数据分页查询
/// </summary>
public class PageQuery
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
    /// 查询文本
    /// </summary>
    [Description("查询文本")]
    public string Data { get; set; } = "";
}

/// <summary>
/// 数据查询对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageQuery<T>
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
    /// 查询数据模型
    /// </summary>
    [Description("查询数据模型")]
    public T? Data { get; set; }
}
