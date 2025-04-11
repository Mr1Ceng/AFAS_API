namespace Mr1Ceng.Util.Database.Internal.Enums;

/// <summary>
/// 数据类型
/// </summary>
internal enum SqlValueType
{
    /// <summary>
    /// 数字
    /// </summary>
    Number,

    /// <summary>
    /// 单引号字符串
    /// </summary>
    SimpleQuotesString,

    /// <summary>
    /// 字符串
    /// </summary>
    String,

    /// <summary>
    /// Bool转0、1
    /// </summary>
    BoolToInt,

    /// <summary>
    /// 不被支持的
    /// </summary>
    NotSupport
}
