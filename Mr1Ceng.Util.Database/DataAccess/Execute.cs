using System.Data.SqlClient;
using Mr1Ceng.Util.Database.Internal.Core;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// ExecuteSQL
/// </summary>
public partial class DataAccess
{
    #region 无事务

    /// <summary>
    /// 执行SQL语句，返回受影的记录数
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>int</returns>
    public static int Execute(string dbName, string strSql, int timeout = 0)
        => Execute(dbName, strSql, [], timeout);

    /// <summary>
    /// 执行SQL语句，返回受影的记录数
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="parameter">查询参数(单)</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns></returns>
    public static int Execute(string dbName, string strSql, Parameter parameter, int timeout = 0)
        => Execute(dbName, strSql, [parameter], timeout);

    /// <summary>
    /// 执行SQL语句，返回受影的记录数
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>int</returns>
    public static int Execute(string dbName, string strSql, List<Parameter> parameters, int timeout = 0)
    {
        var broker = Broker.Instance;
        var cmd = broker.GetCommand(dbName);
        cmd.CommandText = strSql;
        foreach (var parameter in parameters)
        {
            cmd.Parameters.Add(new SqlParameter($"@{parameter.Name}", parameter.Value));
        }
        if (timeout > 0)
        {
            cmd.CommandTimeout = timeout;
        }
        return broker.Execute(cmd, dbName);
    }

    #endregion

    #region 有事务

    /// <summary>
    /// 执行SQL语句，返回受影的记录数
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>int</returns>
    public static int Execute(string dbName, string strSql, Transaction transaction, int timeout = 0)
        => Execute(dbName, strSql, new List<Parameter>(), transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回受影的记录数
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="parameter">查询参数(单)</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns></returns>
    public static int Execute(string dbName,
        string strSql,
        Parameter parameter,
        Transaction transaction,
        int timeout = 0)
        => Execute(dbName, strSql, [parameter], transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回受影的记录数
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>int</returns>
    public static int Execute(string dbName,
        string strSql,
        List<Parameter> parameters,
        Transaction transaction,
        int timeout = 0)
    {
        var rdb = transaction.GetPersistenceProvider(dbName);
        var cmd = rdb.GetCommand();
        cmd.CommandText = strSql;
        foreach (var parameter in parameters)
        {
            cmd.Parameters.Add(new SqlParameter($"@{parameter.Name}", parameter.Value));
        }
        if (timeout > 0)
        {
            cmd.CommandTimeout = timeout;
        }
        return rdb.DoCommand(cmd);
    }

    #endregion
}
