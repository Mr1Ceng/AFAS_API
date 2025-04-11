using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Mr1Ceng.Util;

namespace Mr1Ceng.Tools.ORMapping.V7.Common;

/// <summary>
/// SqlDBOperator 的摘要说明。
/// </summary>
public class SqlDBOperator : DBOperator
{
    private readonly SqlConnection conn;
    private readonly SqlCommand cmd;

    /// <summary>
    /// </summary>
    /// <param name="connString"></param>
    public SqlDBOperator(string connString)
    {
        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand
            {
                Connection = conn
            };
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), "获得数据库连接失败：" + connString, ex);
        }
    }

    public override ConnectionState State => conn.State;

    /// <summary>
    /// 打开同数据库的连接
    /// </summary>
    public override void Open()
    {
        if (conn.State.ToString().ToUpper() != "OPEN")
        {
            conn.Open();
        }
    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    public override void Close()
    {
        if (conn.State.ToString().ToUpper() == "OPEN")
        {
            conn.Close();
        }
        if (cmd != null)
        {
            cmd.Dispose();
        }
    }

    /// <summary>
    /// 活动数据库
    /// </summary>
    public override string CurDataBase => conn.Database;

    /// <summary>
    /// 执行SQL语句，返回DataSet
    /// </summary>
    /// <param name="queryString"></param>
    /// <param name="strTable"></param>
    /// <returns></returns>
    public override DataSet exeSqlForDataSet(string queryString, string strTable)
    {
        try
        {
            DataSet ds = new();
            SqlDataAdapter ad = new();
            cmd.CommandText = queryString;
            ad.SelectCommand = cmd;
            ad.Fill(ds, strTable);
            return ds;
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(),  $"执行SQL语句失败：{strTable}；{queryString}", ex);
        }
    }

    /// <summary>
    ///  获得列名集合
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public override DataSet GetColumn(string tablename)
    {
        try
        {
            DataSet ds = new();
            var array = new string[2, 2];
            array[0, 0] = "@table_name";
            array[0, 1] = tablename;
            ds = ExcuteSp("sp_Columns", array);

            DataSet ds2 = new();
            var strsql = $"select top 1 * from [{tablename}]";
            SqlDataAdapter da = new(strsql, conn);
            da.Fill(ds2);

            foreach (DataRow datarow in ds.Tables[0].Rows)
            {
                if (datarow["COLUMN_DEF"].ToString() != "")
                {
                    var strDEF = datarow["COLUMN_DEF"].ToString();
                    strDEF = strDEF.Replace("(", "");
                    strDEF = strDEF.Replace(")", "");
                    datarow["COLUMN_DEF"] = strDEF.Trim();
                }
                foreach (DataColumn datacolumn in ds2.Tables[0].Columns)
                {
                    if (datarow["COLUMN_NAME"].ToString() == datacolumn.ColumnName)
                    {
                        if (datarow["TYPE_NAME"].ToString().IndexOf("identity") > 0)
                        {
                            datarow["TYPE_NAME"] = datacolumn.DataType + ":identity";
                        }
                        else
                        {
                            datarow["TYPE_NAME"] = datacolumn.DataType.ToString();
                        }
                    }
                }
            }
            return ds;
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(),  "获得列名集合失败：" + tablename, ex);
        }
    }

    /// <summary>
    /// 获取主键名称
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public override DataSet GetPrimary(string tablename)
    {
        try
        {
            DataSet ds = new();
            var array = new string[2, 2];
            array[0, 0] = "@table_name";
            array[0, 1] = tablename;
            ds = ExcuteSp("sp_pKeys", array);
            return ds;
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(),  "获取主键名称失败：" + tablename, ex);
        }
    }

    /// <summary>
    /// 获取数据库的数据表
    /// </summary>
    /// <returns></returns>
    public override DataSet GetTables()
    {
        try
        {
            DataSet ds = new();
            var strSql = "	SELECT name AS TABLE_NAME, xtype AS TABLE_TYPE FROM sysobjects ";
            strSql += " WHERE (xtype = 'U' OR xtype = 'V') ORDER BY xtype, TABLE_NAME";
            return ds = exeSqlForDataSet(strSql, "sysobjects");
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(),  "获取数据库的数据表失败", ex);
        }
    }

    /// <summary>
    /// 获取Table类别
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns>U/V</returns>
    public override string GetTableType(string tableName)
    {
        try
        {
            var strSql = string.Format(@"SELECT xtype AS TABLE_TYPE FROM sysobjects WHERE name = '{0}' ", tableName);
            var ds = exeSqlForDataSet(strSql, "sysobjects");
            return ds == null || ds.Tables.Count == 0 ? "" : ds.Tables[0].Rows[0][0].ToString().Trim();
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(),  $"获取Table类别失败：{tableName}", ex);
        }
    }

    /// <summary>
    ///  获取Table备注
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public override string GetTableDescription(string tableName)
    {
        try
        {
            var strsql = $@"
                    SELECT c.value FROM sys.tables a
                    INNER JOIN sys.extended_properties c ON a.object_id = c.major_id AND c.minor_id = 0
                    WHERE a.name = '{tableName}'
                        and a.schema_id=(select schema_id from sys.schemas where name='dbo')
                ";
            var ds = exeSqlForDataSet(strsql, "sysobjects");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return GetString.FromObject(ds.Tables[0].Rows[0][0]);
            }
            return "";
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(),  $"获取Table备注失败：{tableName}", ex);
        }
    }

    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="sqName"></param>
    /// <param name="array"></param>
    /// <returns></returns>
    public override DataSet ExcuteSp(string sqName, string[,] array)
    {
        try
        {
            DataSet dset = new();
            SqlDataAdapter dp = new();

            SqlCommand cmmd = new();
            dp.SelectCommand = cmmd;
            if (conn.State.ToString() == "Closed")
            {
                Open();
            }

            dp.SelectCommand.Connection = conn;
            dp.SelectCommand.CommandType = CommandType.StoredProcedure;
            dp.SelectCommand.CommandText = sqName;
            if (array != null)
            {
                for (var i = 0; i <= array.GetUpperBound(0); i++)
                {
                    if (array[i, 0] != null)
                    {
                        var Parm = dp.SelectCommand.Parameters.Add(array[i, 0], SqlDbType.NVarChar);
                        Parm.Value = array[i, 1];
                    }
                }
            }
            dp.Fill(dset, "Default");
            return dset;
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(),  $"执行存储过程失败：{sqName}", ex);
        }
    }

    /// <summary>
    /// 取得数据库列表
    /// </summary>
    /// <returns></returns>
    public override string[] GetDatabases()
    {
        if (conn.Database == "master")
        {
            var ds = ExcuteSp("dbo.sp_MShasdbaccess", null);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            var sa = new string[ds.Tables[0].Rows.Count];
            var count = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                sa[count++] = row[0].ToString().Trim();
            }

            return sa;
        }
        return new[]
        {
            conn.Database
        };
    }

    /// <summary>
    ///  获取列名备注
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public override IList<KeyValueText> GetColumnRemarks(string tablename)
    {
        IList<KeyValueText> result = new List<KeyValueText>();
        try
        {
            var strsql = $@"
                    select a.name as table_name, b.name as column_name, c.value as remarks  
                    from sys.tables a left join sys.columns b on a.object_id=b.object_id 
                    left join sys.extended_properties c on a.object_id=c.major_id 
                    where a.name='{tablename}' and c.minor_id<>0 and b.column_id=c.minor_id 
                    and a.schema_id=(select schema_id from sys.schemas where name='dbo')
                ";
            DataSet ds = new();
            SqlDataAdapter da = new(strsql, conn);
            da.Fill(ds);

            foreach (DataRow datarow in ds.Tables[0].Rows)
            {
                result.Add(new KeyValueText
                {
                    Key = GetString.FromObject(datarow["table_name"]),
                    Value = GetString.FromObject(datarow["column_name"]),
                    Text = GetString.FromObject(datarow["remarks"])
                });
            }
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(),  "获取列名备注失败：" + tablename, ex);
        }

        return result;
    }
}
