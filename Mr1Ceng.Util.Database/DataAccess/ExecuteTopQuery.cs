using System.Data;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// ExecuteQuery
/// </summary>
public partial class DataAccess
{
    #region 无事务

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="topCount">查询返回的行数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteTopQuery(string dbName, string strSql, int topCount = 0, int timeout = 0)
        => ExecuteQuery(dbName, GetTopQuerySQL(strSql, topCount), new List<Parameter>(), timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="parameter">查询参数(单)</param>
    /// <param name="topCount">查询返回的行数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteTopQuery(string dbName, string strSql, Parameter parameter, int topCount = 0, int timeout = 0)
        => ExecuteQuery(dbName, GetTopQuerySQL(strSql, topCount), [parameter], timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="strSql">SQL语句</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="topCount">查询返回的行数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteTopQuery(string dbName,
        string strSql,
        List<Parameter> parameters,
        int topCount = 0,
        int timeout = 0)
        => ExecuteQuery(dbName, GetTopQuerySQL(strSql, topCount), parameters, timeout);

    #endregion

    #region 私有方法

    /// <summary>
    /// 私有方法，加工查询脚本
    /// </summary>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="topCount">查询返回的行数</param>
    /// <returns></returns>
    private static string GetTopQuerySQL(string baseSql, int topCount)
        => topCount == 0
            ? baseSql
            : $"SELECT TOP {topCount} * FROM ({baseSql}) TopQuerySQL";

    #endregion
}
