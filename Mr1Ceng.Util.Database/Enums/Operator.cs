namespace Mr1Ceng.Util.Database;

/// <summary>
/// 关系操作表达式
/// </summary>
public enum Operator
{
    /// <summary>
    /// 等于
    /// </summary>
    Equal,

    /// <summary>
    /// 不等于
    /// </summary>
    NotEqual,

    /// <summary>
    /// 大于
    /// </summary>
    GreaterThan,

    /// <summary>
    /// 大于等于
    /// </summary>
    GreaterThanOrEqual,

    /// <summary>
    /// 小于
    /// </summary>
    LessThan,

    /// <summary>
    /// 小于等于
    /// </summary>
    LessThanOrEqual,

    /// <summary>
    /// 匹配
    /// </summary>
    Match,

    /// <summary>
    /// 不匹配
    /// </summary>
    NotMatch,

    /// <summary>
    /// 前端匹配
    /// </summary>
    MatchPrefix,

    /// <summary>
    /// 范围匹配(IN)
    /// </summary>
    IN,

    /// <summary>
    /// 范围匹配(BETWEEN)
    /// </summary>
    BETWEEN
}
