using System.Data;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// ExecutePageQuery
/// </summary>
public partial class DataAccess
{
    #region 无事务

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        KeySorterValue sorter,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, [sorter], [], timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        KeySorterValue sorter,
        Parameter parameter,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, [sorter], [parameter], timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        KeySorterValue sorter,
        List<Parameter> parameters,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, [sorter], parameters, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        List<KeySorterValue> sorters,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, sorters, [], timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        List<KeySorterValue> sorters,
        Parameter parameter,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, sorters, [parameter], timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        List<KeySorterValue> sorters,
        List<Parameter> parameters,
        int timeout = 0)
        => ExecuteQuery(dbName, GetPageQuerySQL(baseSql, pageIndex, pageSize, sorters), parameters, timeout);

    #endregion

    #region 有事务

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        KeySorterValue sorter,
        Transaction transaction,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, [sorter], [], transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        KeySorterValue sorter,
        Parameter parameter,
        Transaction transaction,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, [sorter], [parameter], transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorter">排序条件</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        KeySorterValue sorter,
        List<Parameter> parameters,
        Transaction transaction,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, [sorter], parameters, transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        List<KeySorterValue> sorters,
        Transaction transaction,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, sorters, new List<Parameter>(), transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="parameter">查询参数</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        List<KeySorterValue> sorters,
        Parameter parameter,
        Transaction transaction,
        int timeout = 0)
        => ExecutePageQuery(dbName, baseSql, pageIndex, pageSize, sorters, [parameter], transaction, timeout);

    /// <summary>
    /// 执行SQL语句，返回DataTable对象
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorters">排序条件</param>
    /// <param name="parameters">查询参数</param>
    /// <param name="transaction">事务</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecutePageQuery(string dbName,
        string baseSql,
        int pageIndex,
        int pageSize,
        List<KeySorterValue> sorters,
        List<Parameter> parameters,
        Transaction transaction,
        int timeout = 0)
        => ExecuteQuery(dbName, GetPageQuerySQL(baseSql, pageIndex, pageSize, sorters), parameters, transaction,
            timeout);

    #endregion

    #region 加工分页脚本

    /// <summary>
    /// 基本方法，加工分页脚本
    /// </summary>
    /// <param name="baseSql">SQL语句</param>
    /// <param name="pageIndex">页码索引（从0开始，0是第一页）</param>
    /// <param name="pageSize">页面大小</param>
    /// <param name="sorters">排序条件</param>
    /// <returns></returns>
    public static string GetPageQuerySQL(string baseSql, int pageIndex, int pageSize, List<KeySorterValue> sorters)
    {
        return $@"
            WITH 
                BaseQuery AS (
                    {baseSql}
                ),
    
                OrderQuery AS (
                    SELECT *,
                        RowNumber = ROW_NUMBER() OVER (ORDER BY {string.Join(", ", sorters.Select(sorter => $"{sorter.Key} {sorter.Value}"))})
                    FROM BaseQuery
                )

            SELECT * FROM OrderQuery WHERE RowNumber BETWEEN {pageSize * pageIndex + 1} AND {pageSize * (pageIndex + 1)};
        ";
    }

    #endregion
}
