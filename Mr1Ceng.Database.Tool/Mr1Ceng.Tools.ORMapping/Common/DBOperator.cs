using System.Data;
using Mr1Ceng.Util;

namespace Mr1Ceng.Tools.ORMapping.V7.Common;

/// <summary>
/// DBOperator 的摘要说明。
/// </summary>
public abstract class DBOperator
{
    public static DBOperator Instance(Configuration configuration)
    {
        var strConn = configuration.ConnectInfo.ConnectionString;
        return new SqlDBOperator(strConn);
    }

    public abstract ConnectionState State { get; }

    /// <summary>
    /// 打开同数据库的连接
    /// </summary>
    public abstract void Open();

    /// <summary>
    /// 关闭连接
    /// </summary>
    public abstract void Close();

    /// <summary>
    /// 活动数据库
    /// </summary>
    public abstract string CurDataBase { get; }

    /// <summary>
    /// 执行SQL语句，返回DataSet
    /// </summary>
    /// <param name="QueryString"></param>
    /// <param name="strTable"></param>
    /// <returns></returns>
    public abstract DataSet exeSqlForDataSet(string QueryString, string strTable);

    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="sqName"></param>
    /// <param name="array"></param>
    /// <returns></returns>
    public abstract DataSet ExcuteSp(string sqName, string[,] array);

    /// <summary>
    /// 获取数据库的数据表
    /// </summary>
    /// <returns></returns>
    public abstract DataSet GetTables();

    /// <summary>
    /// 获取Table类别
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public abstract string GetTableType(string tableName);

    /// <summary>
    ///  获取Table备注
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public abstract string GetTableDescription(string tableName);

    /// <summary>
    /// 获得列名集合
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public abstract DataSet GetColumn(string tableName);

    /// <summary>
    /// 获取主键名称
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public abstract DataSet GetPrimary(string tableName);

    /// <summary>
    /// 取得数据库列表
    /// </summary>
    /// <returns></returns>
    public abstract string[] GetDatabases();

    /// <summary>
    /// 获取列名备注
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public abstract IList<KeyValueText> GetColumnRemarks(string tableName);
}
