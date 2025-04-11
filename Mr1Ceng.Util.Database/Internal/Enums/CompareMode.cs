namespace Mr1Ceng.Util.Database.Internal.Enums;

/// <summary>
/// 对比模式
/// </summary>
internal enum CompareMode
{
    /// <summary>
    /// 正常关系比较模式
    /// </summary>
    Compare,

    /// <summary>
    /// 截位比较模式
    /// </summary>
    CompareSubstring,

    /// <summary>
    /// 字段长度比较
    /// </summary>
    CompareFieldLen,

    /// <summary>
    /// 字段比较模式
    /// </summary>
    CompareField,

    /// <summary>
    /// 自定义比较（设定SQL语句）
    /// </summary>
    CompareCustomer
}
