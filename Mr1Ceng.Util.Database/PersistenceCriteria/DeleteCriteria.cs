using Mr1Ceng.Util.Database.Internal.Enums;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// DeleteCriteria 类
/// </summary>
public class DeleteCriteria<T> : PersistentCriteria where T : PersistentObject
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public DeleteCriteria() : base(CriteriaTypes.Delete, typeof(T))
    {
    }

    #endregion


    #region 方法

    /// <summary>
    /// 执行数据更新操作
    /// </summary>
    /// <returns></returns>
    public int Execute()
    {
        var cmd = ThisClassMap.GetDeleteSqlFor(this);
        SqlString = cmd.CommandText;
        return broker.Execute(cmd, ThisClassMap.Database.DatabaseName);
    }

    /// <summary>
    /// 执行数据更新操作(有事务)
    /// </summary>
    /// <param name="ts"></param>
    /// <returns></returns>
    public int Execute(Transaction ts)
    {
        var rdb = ts.GetPersistenceProvider(ThisClassMap.Database.DatabaseName);
        var result = rdb.DoCommand(ThisClassMap.GetDeleteSqlFor(this));
        return result;
    }

    #endregion
}
