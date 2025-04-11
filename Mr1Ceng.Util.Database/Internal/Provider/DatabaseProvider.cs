using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Mr1Ceng.Util.Database.Internal.Enums;

namespace Mr1Ceng.Util.Database.Internal.Provider;

/// <summary>
/// SQLServer 数据提供者
/// </summary>
/// <remarks>
/// 封装了对  SQLServer 操作的行为
/// </remarks>
internal class DatabaseProvider
{
    #region 变量定义

    /// <summary>
    /// 事务
    /// </summary>
    private IDbTransaction? DatabaseTransaction;

    #endregion


    #region 属性

    /// <summary>
    /// 数据源名
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    internal string ConnectionString
    {
        get;
    }

    /// <summary>
    /// 数据库连接
    /// </summary>
    private readonly IDbConnection DatabaseConnection;

    /// <summary>
    /// 是否处在事务中
    /// </summary>
    public bool IsInTransaction { get; private set; }

    /// <summary>
    /// </summary>
    public string QuotationMarksStart { get; set; } = "";

    /// <summary>
    /// </summary>
    public string QuotationMarksEnd { get; set; } = "";

    #endregion


    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="connectionString"></param>
    public DatabaseProvider(string name, string connectionString)
    {
        Name = name;
        ConnectionString = connectionString;
        DatabaseConnection = new SqlConnection(connectionString);
    }

    #endregion


    #region 数据库基本方法

    /// <summary>
    /// 获取Command
    /// </summary>
    /// <returns></returns>
    public IDbCommand GetCommand()
    {
        if (DatabaseConnection == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database, "数据库连接错误");
        }

        var cmd = DatabaseConnection.CreateCommand();
        if (IsInTransaction)
        {
            cmd.Transaction = DatabaseTransaction;
        }
        return cmd;
    }

    /// <summary>
    /// 打开数据库
    /// </summary>
    public void Open()
    {
        if (DatabaseConnection.State == ConnectionState.Closed)
        {
            DatabaseConnection.Open();
        }
    }

    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void Close()
    {
        if (DatabaseConnection.State == ConnectionState.Open)
        {
            DatabaseConnection.Close();
        }
        IsInTransaction = false;
    }

    /// <summary>
    /// 启动一个事务
    /// </summary>
    public void BeginTransaction()
    {
        if (DatabaseConnection == null || DatabaseConnection.State == ConnectionState.Closed)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database, "数据库连接错误");
        }

        //事务处理级别
        //ReadUncommitted:可以读未提交的数据，最低级别
        //可根据使用情况调整锁定级别 lwj 2012-01-29
        DatabaseTransaction = DatabaseConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

        IsInTransaction = true;
    }

    /// <summary>
    /// 提交一个事务
    /// </summary>
    public void CommitTransaction()
    {
        if (DatabaseTransaction != null)
        {
            DatabaseTransaction.Commit();
            IsInTransaction = false;
        }
        else
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database, "未启动事务");
        }
    }

    /// <summary>
    /// 回滚一个事务
    /// </summary>
    public void RollbackTransaction()
    {
        if (DatabaseTransaction != null)
        {
            DatabaseTransaction.Rollback();
            IsInTransaction = false;
            DatabaseTransaction = null;
        }
        else
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database, "未启动事务");
        }
    }

    /// <summary>
    /// 执行一个命令
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public int DoCommand(IDbCommand cmd)
    {
        if (IsInTransaction)
        {
            cmd.Transaction = DatabaseTransaction;
        }
        cmd.Connection = DatabaseConnection;

        //#if DEBUG
        //			System.Console.WriteLine(cmd.CommandText);
        //#endif
        var result = cmd.ExecuteNonQuery();
        return result;
    }

    /// <summary>
    /// 获取结果集
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public IDataReader GetDataReader(IDbCommand cmd)
    {
        cmd.Connection = DatabaseConnection;
        if (IsInTransaction)
        {
            cmd.Transaction = DatabaseTransaction;
        }

        //#if DEBUG
        //			System.Console.WriteLine(cmd.CommandText);
        //#endif
        var reader = cmd.ExecuteReader();
        return reader;
    }

    #endregion


    #region 重写抽象方法

    /// <summary>
    /// 数据库连接是否有效
    /// </summary>
    /// <param name="connectionString"></param>
    public void TryConnection(string connectionString)
    {
        try
        {
            DatabaseConnection.Open();
        }
        catch (SqlException ex)
        {
            throw BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database, "数据库连接失败");
        }
        finally
        {
            DatabaseConnection.Close();
        }
    }

    /// <summary>
    /// 克隆
    /// </summary>
    /// <returns></returns>
    public DatabaseProvider GetCopy() => new(Name, ConnectionString);


    /// <summary>
    /// 执行插入一条记录 适用于有 自动生成标识的列
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="identity"></param>
    /// <returns></returns>
    public int InsertRecord(IDbCommand cmd, out object identity)
    {
        cmd.Transaction = DatabaseTransaction;
        cmd.Connection = DatabaseConnection;
        var result = cmd.ExecuteNonQuery();

        cmd.CommandText = " SELECT @@IDENTITY ";
        identity = Convert.ToInt32(cmd.ExecuteScalar());

        return result;
    }

    /// <summary>
    /// 返回查询字段
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="attrName"></param>
    /// <returns></returns>
    public string GetSelectField(string columnName, string attrName = "") => "";

    /// <summary>
    /// 返回过滤字段
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="attrName"></param>
    /// <returns></returns>
    public string GetWhereParameter(string columnName, string attrName = "") => "";


    /// <summary>
    /// 返回一个SqlDataAdapter
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public IDataAdapter GetAdapter(IDbCommand cmd)
    {
        cmd.Connection = DatabaseConnection;
        return new SqlDataAdapter((SqlCommand)cmd);
    }

    /// <summary>
    /// 返回DataTable
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public DataTable AsDataTable(IDbCommand cmd)
    {
        cmd.Connection = DatabaseConnection;
        cmd.Transaction = DatabaseTransaction;
        var adapter = new SqlDataAdapter((SqlCommand)cmd);

        var dt = new DataTable();
        adapter.Fill(dt);

        return dt;
    }

    /// <summary>
    /// 返回DataTable的第一行
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public DataRow? GetDataRow(IDbCommand cmd)
    {
        cmd.Connection = DatabaseConnection;
        var adapter = new SqlDataAdapter((SqlCommand)cmd);

        var dt = new DataTable();
        adapter.Fill(dt);

        return dt.Rows.Count > 0 ? dt.Rows[0] : null;
    }

    /// <summary>
    /// 获取字段名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetName(string name) => QuotationMarksStart + name + QuotationMarksEnd;

    /// <summary>
    /// 获取数据表名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetTableName(string name) => name;

    #endregion


    #region 静态方法

    /// <summary>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static SqlValueType GetSqlValueType(DbType type)
    {
        return type switch
        {
            DbType.AnsiString => SqlValueType.NotSupport,
            DbType.Binary => SqlValueType.NotSupport,
            DbType.Byte => SqlValueType.Number,
            DbType.Boolean => SqlValueType.BoolToInt,
            DbType.Currency => SqlValueType.Number,
            DbType.Date => SqlValueType.SimpleQuotesString,
            DbType.DateTime => SqlValueType.SimpleQuotesString,
            DbType.Decimal => SqlValueType.Number,
            DbType.Double => SqlValueType.Number,
            DbType.Guid => SqlValueType.SimpleQuotesString,
            DbType.Int16 => SqlValueType.Number,
            DbType.Int32 => SqlValueType.Number,
            DbType.Int64 => SqlValueType.Number,
            DbType.Object => SqlValueType.NotSupport,
            DbType.SByte => SqlValueType.Number,
            DbType.Single => SqlValueType.Number,
            DbType.String => SqlValueType.String,
            DbType.Time => SqlValueType.SimpleQuotesString,
            DbType.UInt16 => SqlValueType.Number,
            DbType.UInt32 => SqlValueType.Number,
            DbType.UInt64 => SqlValueType.Number,
            DbType.VarNumeric => SqlValueType.Number,
            DbType.AnsiStringFixedLength => SqlValueType.String,
            DbType.StringFixedLength => SqlValueType.String,
            DbType.Xml => SqlValueType.String,
            DbType.DateTime2 => SqlValueType.SimpleQuotesString,
            DbType.DateTimeOffset => SqlValueType.NotSupport,
            _ => throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config, $"不支持的数据字段类型:{type}")
        };
    }

    #endregion
}
