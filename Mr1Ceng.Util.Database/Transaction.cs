using Mr1Ceng.Util.Database.Internal.Core;
using Mr1Ceng.Util.Database.Internal.Provider;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// 事务处理类
/// </summary>
/// <remarks>
/// 这个类封装了支持持久机制的简单以及嵌套事务所需的行为
/// 包括跨数据库的事务处理
/// </remarks>
public class Transaction
{
    #region 变量

    /// <summary>
    /// 实体层关键类
    /// </summary>
    private readonly Broker broker = Broker.Instance;

    #endregion


    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public Transaction()
    {
    }

    #endregion


    #region 事务操作

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <param name="dataSourceNames">事务的目标数据源</param>
    /// <returns>成功返回True，失败返回false</returns>
    public bool BeginTransaction(List<string>? dataSourceNames = null) => broker.BeginTransaction(this, dataSourceNames);

    /// <summary>
    /// 回滚事务
    /// </summary>
    /// <returns>成功返回True，失败返回false</returns>
    public bool RollbackTransaction() => broker.RollbackTransaction(this);

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <returns>成功返回True，失败返回false</returns>
    public bool CommitTransaction() => broker.CommitTransaction(this);

    /// <summary>
    /// 取得事务的IPersistenceProvider
    /// </summary>
    /// <param name="dbName">数据源名称</param>
    /// <returns>rdb</returns>
    internal DatabaseProvider GetPersistenceProvider(string dbName)
        => broker.GetPersistenceProvider(this, dbName);

    #endregion


    #region 属性

    /// <summary>
    /// 数据源列表
    /// </summary>
    internal Dictionary<string, DatabaseProvider> Databases { get; } = new();

    #endregion
}
