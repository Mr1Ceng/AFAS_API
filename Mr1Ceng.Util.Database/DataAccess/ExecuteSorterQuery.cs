using System.Data;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// ExecuteSorterQuery
/// </summary>
public partial class DataAccess
{
    #region 无事务

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns></returns>
    public static DataTable ExecuteSorterQuery(string dbName, string strSql, KeySorterValue sorter, int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, [sorter], [], timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="parameter">查询参数[单]</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        KeySorterValue sorter,
        Parameter parameter,
        int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, [sorter], [parameter], timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        KeySorterValue sorter,
        List<Parameter> parameters,
        int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, [sorter], parameters, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        List<KeySorterValue> sorters,
        int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, sorters, new List<Parameter>(), timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="parameter">查询参数[单]</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        List<KeySorterValue> sorters,
        Parameter parameter,
        int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, sorters, [parameter], timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        List<KeySorterValue> sorters,
        List<Parameter> parameters,
        int timeout = 0)
        => ExecuteQuery(dbName, GetSorterQuerySQL(strSql, sorters), parameters, timeout);

    #endregion

    #region 有事务

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns></returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        KeySorterValue sorter,
        Transaction transaction,
        int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, [sorter], [], transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="parameter">查询参数[单]</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        KeySorterValue sorter,
        Parameter parameter,
        Transaction transaction,
        int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, [sorter], [parameter], transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        KeySorterValue sorter,
        List<Parameter> parameters,
        Transaction transaction,
        int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, [sorter], parameters, transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        List<KeySorterValue> sorters,
        Transaction transaction,
        int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, sorters, new List<Parameter>(), transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="parameter">查询参数[单]</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        List<KeySorterValue> sorters,
        Parameter parameter,
        Transaction transaction,
        int timeout = 0)
        => ExecuteSorterQuery(dbName, strSql, sorters, [parameter], transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteSorterQuery(string dbName,
        string strSql,
        List<KeySorterValue> sorters,
        List<Parameter> parameters,
        Transaction transaction,
        int timeout = 0)
        => ExecuteQuery(dbName, GetSorterQuerySQL(strSql, sorters), parameters, transaction, timeout);

    #endregion

    #region 私有方法

    /// <summary>
    /// 私有方法，加工排序页脚本
    /// </summary>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="sorters">排序条件</param>
    /// <returns></returns>
    private static string GetSorterQuerySQL(string baseSql, List<KeySorterValue> sorters)
        => sorters.Count == 0
            ? baseSql
            : $@"
SELECT * FROM (
    {baseSql}
) AS BaseQuery_SorterQuerySQL
ORDER BY {string.Join(", ", sorters.Select(sorter => $"{sorter.Key} {sorter.Value}"))}
            ";

    #endregion
}
