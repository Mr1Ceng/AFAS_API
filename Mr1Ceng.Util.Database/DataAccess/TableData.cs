using System.Data;
using Mr1Ceng.Util.Database.Internal.Core;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// GetTableData
/// </summary>
public partial class DataAccess
{
    /// <summary>
    /// 获取表格全量数据
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="tableName">数据表名称</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>int</returns>
    public static DataTable GetTableData(string dbName, string tableName, int timeout = 0)
    {
        var broker = Broker.Instance;
        var cmd = broker.GetCommand(dbName);
        cmd.CommandText = "SELECT * FROM " + tableName;
        if (timeout > 0)
        {
            cmd.CommandTimeout = timeout;
        }
        return broker.ExecuteQuery(cmd, dbName);
    }

    /// <summary>
    /// 将整个DataTable写入数据库表中
    /// </summary>
    /// <param name="dbName">数据源名</param>
    /// <param name="tableName">数据表名称</param>
    /// <param name="dt">数据</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <returns>int</returns>
    public static void SaveTableData(string dbName, string tableName, DataTable dt, int timeout = 0)
        => Broker.Instance.SaveDataTable(dt, tableName, dbName, timeout);
}
