namespace Mr1Ceng.Util.Database.Internal.Enums;

/// <summary>
/// 事务状态
/// </summary>
internal enum TransactionState
{
    /// <summary>
    /// 启动
    /// </summary>
    Started,

    /// <summary>
    /// 提交
    /// </summary>
    Commit,

    /// <summary>
    /// 回滚
    /// </summary>
    Rollback
}
