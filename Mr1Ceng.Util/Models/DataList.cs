using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 查询的数据模型
/// </summary>
/// <typeparam name="T"></typeparam>
public class DataList<T>
{
    /// <summary>
    /// 总记录数
    /// </summary>
    [Description("总记录数")]
    public int Count { get; set; }

    /// <summary>
    /// Data数组
    /// </summary>
    [Description("Data数组")]
    public List<T> Data { get; set; } = [];
}
