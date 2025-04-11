namespace Mr1Ceng.Util.Database.Internal.Enums;

/// <summary>
/// 操作类型(新增、获取、修改、删除、持久化、Query)
/// </summary>
internal enum ActionType
{
    /// <summary>
    /// </summary>
    NoAction,

    /// <summary>
    /// 获取对象
    /// </summary>
    RetrieveObject,

    /// <summary>
    /// 新增对象
    /// </summary>
    InsertObject,

    /// <summary>
    /// 修改对象
    /// </summary>
    UpdateObject,

    /// <summary>
    /// 删除对象
    /// </summary>
    DeleteObject,

    /// <summary>
    /// 批量获取
    /// </summary>
    RetrieveCriteria,

    /// <summary>
    /// 批量更新
    /// </summary>
    UpdateCriteria,

    /// <summary>
    /// 批量删除
    /// </summary>
    DeleteCriteria,

    /// <summary>
    /// 存储过程
    /// </summary>
    ProcessCriteria,

    /// <summary>
    /// 新增
    /// </summary>
    InsertCommand,

    /// <summary>
    /// 获取
    /// </summary>
    SelectCommand,

    /// <summary>
    /// 修改
    /// </summary>
    UpdateCommand,

    /// <summary>
    /// 删除
    /// </summary>
    DeleteCommand,

    /// <summary>
    /// 持久化
    /// </summary>
    PersistentCriteria,

    /// <summary>
    /// 查询
    /// </summary>
    QueryCommand
}
