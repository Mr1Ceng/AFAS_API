using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Mr1Ceng.Util.Database.Internal.PersistenceConfig;
using Mr1Ceng.Util.Database.Internal.Provider;

namespace Mr1Ceng.Util.Database.Internal.Core;

/// <summary>
/// 实体层的关键类
/// </summary>
/// <remarks>维护数据库持久机制的连接，并且处理对象应用程序与持久机制之间的通信</remarks>
internal sealed class Broker
{
    /// <summary>
    /// 得到Broker的实例
    /// </summary>
    public static readonly Broker Instance = new();

    #region 构造函数

    private Broker()
    {
        var config = new ContextManager();
        config.LoadConfig();
        m_DatabasePool = config.DatabasePool;
        m_ClassMaps = config.ClassMaps;
    }

    #endregion


    #region 变量

    /// <summary>
    /// 数据库连接池
    /// </summary>
    private readonly IDictionary<string, DatabaseProvider> m_DatabasePool;

    /// <summary>
    /// 实体类集合
    /// </summary>
    private readonly IDictionary<string, ClassMap> m_ClassMaps;

    #endregion


    #region 方法

    #region 获取数据源信息及相关操作

    /// <summary>
    /// 根据类名获得相应的ClassMap
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ClassMap GetClassMap(string name)
    {
        var cm = m_ClassMaps[name];
        if (cm == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config, "未找到名为[" + name + "]实体类相对应类影射信息");
        }
        return cm;
    }

    /// <summary>
    /// 返回一IDbCommand
    /// </summary>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public IDbCommand GetCommand(string dbName) => dbName == "" ? GetDatabase().GetCommand() : GetDatabase(dbName).GetCommand();

    /// <summary>
    /// 根据数据源返回 SQLServerProvider
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private DatabaseProvider GetDatabase(string name) => m_DatabasePool[name];

    private DatabaseProvider GetDatabase() => m_DatabasePool.First().Value;

    /// <summary>
    /// 返回实体对象所在数据库名
    /// </summary>
    /// <param name="obj">实体对象</param>
    /// <returns>数据源名</returns>
    public string GetDatabaseName(PersistentObject obj) => GetDatabaseName(obj.GetClassName());

    /// <summary>
    /// 返回实体对象所在数据库名
    /// </summary>
    /// <param name="className">实体对象类型名称</param>
    /// <returns>数据源名</returns>
    public string GetDatabaseName(string className) => GetClassMap(className).Database.DatabaseName;

    #endregion


    #region Execute

    /// <summary>
    /// 根据Command返回一个DataTable
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="rdbName"></param>
    /// <returns></returns>
    public DataTable ExecuteQuery(IDbCommand cmd, string rdbName)
    {
        var rdb = rdbName == "" ? GetDatabase().GetCopy() : GetDatabase(rdbName).GetCopy();
        try
        {
            rdb.Open();
            var dt = rdb.AsDataTable(cmd);
            return dt;
        }
        catch (Exception ex)
        {
            throw ErrorHandle(ex, null, cmd.CommandText);
        }
        finally
        {
            rdb.Close();
        }
    }

    /// <summary>
    /// 执行命令，Command处理
    /// </summary>
    /// <param name="cmd">IDbCommand对象</param>
    /// <param name="rdbName"></param>
    /// <returns>受影响的行数</returns>
    public int Execute(IDbCommand cmd, string rdbName)
    {
        var rdb = rdbName == "" ? GetDatabase().GetCopy() : GetDatabase(rdbName).GetCopy();
        rdb.Open();
        var intReturn = rdb.DoCommand(cmd);
        rdb.Close();
        return intReturn;
    }

    /// <summary>
    /// 将整个DataTable写入数据库表中
    /// </summary>
    /// <param name="dt">数据</param>
    /// <param name="tableName">目标表名</param>
    /// <param name="rdbName">数据源名</param>
    /// <param name="timeout">超时时间(以秒为单位)，为0则取默认值30秒</param>
    /// <remarks>lwj 2013-02-15 添加</remarks>
    public void SaveDataTable(DataTable dt, string tableName, string rdbName, int timeout = 0)
    {
        var rdb = rdbName == "" ? GetDatabase().GetCopy() : GetDatabase(rdbName).GetCopy();

        var sw = new Stopwatch();
        using SqlConnection conn = new(rdb.ConnectionString + ";Max Pool Size = 512;");
        SqlBulkCopy bulkCopy = new(conn)
        {
            DestinationTableName = tableName,
            BatchSize = dt.Rows.Count
        };
        if (timeout > 0)
        {
            bulkCopy.BulkCopyTimeout = timeout;
        }
        conn.Open();
        sw.Start();

        if (dt.Rows.Count != 0)
        {
            bulkCopy.WriteToServer(dt);
        }
        sw.Stop();
        bulkCopy.Close();
        conn.Close();
    }

    #endregion


    #region 返回一个实体对象

    /// <summary>
    /// 返回一个实体对象
    /// </summary>
    /// <param name="row"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetEntityObject<T>(DataRow row) where T : PersistentObject, new()
    {
        var obj = new T();
        m_ClassMaps[obj.GetClassName()].SetObject(obj, row);
        return obj;
    }

    #endregion


    #region 获取当前实体对象

    /// <summary>
    /// 获取当前实体对象
    /// </summary>
    /// <param name="obj">PersistentObject对象</param>
    /// <returns>成功 True,失败 False</returns>
    public bool RetrieveObject(PersistentObject obj)
    {
        var clsMap = GetClassMap(obj.GetClassName());
        var rdb = clsMap.PersistenceProvider.GetCopy();
        try
        {
            rdb.Open();
            return RetrieveObjectPrivate(obj, clsMap, rdb);
        }
        catch (Exception ex)
        {
            throw ErrorHandle(ex, obj);
        }
        finally
        {
            rdb.Close();
        }
    }

    /// <summary>
    /// 获取当前实体对象(事务中)
    /// </summary>
    /// <param name="obj">PersistentObject对象</param>
    /// <param name="transaction">事务</param>
    /// <returns>成功 True,失败 False</returns>
    public bool RetrieveObject(PersistentObject obj, Transaction transaction)
    {
        var clsMap = GetClassMap(obj.GetClassName());
        var rdb = transaction.GetPersistenceProvider(clsMap.Database.DatabaseName);
        return RetrieveObjectPrivate(obj, clsMap, rdb);
    }

    /// <summary>
    /// 获取EntityObject对象
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="clsMap"></param>
    /// <param name="rdb"></param>
    /// <returns></returns>
    private bool RetrieveObjectPrivate(PersistentObject obj, ClassMap clsMap, DatabaseProvider rdb)
    {
        var cmd = clsMap.GetSelectSqlFor(obj);
        var reader = rdb.GetDataReader(cmd);

        if (reader.Read())
        {
            SetObject(obj, reader, clsMap);
            reader.Close();
            obj.IsPersistent = true;
        }
        else
        {
            reader.Close();
            obj.IsPersistent = false;
        }

        return obj.IsPersistent;
    }

    /// <summary>
    /// 设置EntityObject对象
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="reader"></param>
    /// <param name="clsMap"></param>
    private void SetObject(PersistentObject obj, IDataReader reader, ClassMap clsMap)
    {
        for (var i = 0; i < clsMap.Attributes.Count; i++)
        {
            var attMap = clsMap.Attributes[i];
            var objTmp = reader[i];
            if (objTmp != DBNull.Value)
            {
                obj.SetAttributeValue(attMap.Name, objTmp);
            }
        }
    }

    #endregion


    #region 保存一个实体对象

    /// <summary>
    /// 保存一个实体对象
    /// </summary>
    /// <param name="obj">PersistentObject对象</param>
    public void SaveObject(PersistentObject obj)
    {
        var cm = GetClassMap(obj.GetClassName());
        var rdb = cm.PersistenceProvider.GetCopy();
        try
        {
            rdb.Open();
            SaveObjectPrivate(obj, cm, rdb);
        }
        catch (BusinessException ex)
        {
            throw ErrorHandle(ex, obj).Add(MethodBase.GetCurrentMethod());
        }
        catch (Exception ex)
        {
            throw ErrorHandle(ex, obj).Add(MethodBase.GetCurrentMethod());
        }
        finally
        {
            rdb.Close();
        }
    }

    /// <summary>
    /// 保存一个实体对象(在事务中)
    /// </summary>
    /// <param name="obj">PersistentObject对象</param>
    /// <param name="transaction">事务</param>
    public void SaveObject(PersistentObject obj, Transaction transaction)
    {
        var clsMap = GetClassMap(obj.GetClassName());
        var rdb = transaction.GetPersistenceProvider(clsMap.Database.DatabaseName);
        SaveObjectPrivate(obj, clsMap, rdb);
    }

    /// <summary>
    /// 保存EntityObject对象
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="cm"></param>
    /// <param name="rdb"></param>
    private void SaveObjectPrivate(PersistentObject obj, ClassMap cm, DatabaseProvider rdb)
    {
        IDbCommand cmd;
        if (obj.IsPersistent)
        {
            cmd = cm.GetUpdateSqlFor(obj);
            if (rdb.DoCommand(cmd) < 1)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
                    obj.GetClassName() + "对象更新失败，可能是数据已被删除");
            }
        }
        else
        {
            cmd = cm.GetInsertSqlFor(obj);
            if (cm.Table.AutoIdentityKey is null)
            {
                rdb.DoCommand(cmd);
            }
            else
            {
                rdb.InsertRecord(cmd, out var id);
                obj.SetAttributeValue(cm.Table.AutoIdentityKey.Name, id);
            }
        }
    }

    #endregion


    #region 删除一个实体对象

    /// <summary>
    /// 删除一个实体对象
    /// </summary>
    /// <param name="obj">PersistentObject对象</param>
    public void DeleteObject(PersistentObject obj)
    {
        var clsMap = GetClassMap(obj.GetClassName());
        clsMap.GetDeleteSqlFor(obj);
        var rdb = clsMap.PersistenceProvider.GetCopy();
        try
        {
            rdb.Open();
            if (rdb.DoCommand(clsMap.GetDeleteSqlFor(obj)) > 0)
            {
                obj.IsPersistent = false;
            }
        }
        catch (Exception ex)
        {
            throw ErrorHandle(ex, obj);
        }
        finally
        {
            rdb.Close();
        }
    }

    /// <summary>
    /// 删除一个实体对象(在事务中)
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="transaction">事务</param>
    public void DeleteObject(PersistentObject obj, Transaction transaction)
    {
        var clsMap = GetClassMap(obj.GetClassName());
        var rdb = transaction.GetPersistenceProvider(clsMap.Database.DatabaseName);
        if (rdb.DoCommand(clsMap.GetDeleteSqlFor(obj)) > 0)
        {
            obj.IsPersistent = false;
        }
    }

    #endregion


    #region 事务处理

    /// <summary>
    /// 取得事务的 SQLServerProvider
    /// </summary>
    /// <param name="transaction">事务</param>
    /// <param name="dbName">数据源名称</param>
    /// <returns>rdb</returns>
    internal DatabaseProvider GetPersistenceProvider(Transaction transaction, string dbName) => transaction.Databases[dbName];

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <param name="transaction">事务</param>
    /// <param name="dataSourceNames">事务的目标数据源</param>
    /// <returns>成功返回True，失败返回false</returns>
    public bool BeginTransaction(Transaction transaction, List<string>? dataSourceNames = null)
    {
        transaction.Databases.Clear();

        if (dataSourceNames == null)
        {
            var rdb = GetDatabase().GetCopy();
            transaction.Databases.Add(rdb.Name, rdb);
            rdb.Open();
            rdb.BeginTransaction();
        }
        else
        {
            foreach (var rdb in dataSourceNames.Select(dataSourceName => GetDatabase(dataSourceName).GetCopy()))
            {
                transaction.Databases.Add(rdb.Name, rdb);
                rdb.Open();
                rdb.BeginTransaction();
            }
        }

        return true;
    }

    /// <summary>
    /// 回滚事务
    /// </summary>
    /// <param name="transaction">事务</param>
    /// <returns>成功返回True，失败返回false</returns>
    public bool RollbackTransaction(Transaction transaction)
    {
        try
        {
            foreach (var rs in transaction.Databases.Values)
            {
                rs.RollbackTransaction();
            }
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database, "回滚事务失败");
        }
        return true;
    }

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="transaction">事务</param>
    /// <returns>成功返回True，失败返回false</returns>
    public bool CommitTransaction(Transaction transaction)
    {
        try
        {
            foreach (var rs in transaction.Databases.Values)
            {
                rs.CommitTransaction();
            }
        }
        catch (Exception ex)
        {
            foreach (var rs in transaction.Databases.Values)
            {
                rs.RollbackTransaction();
            }
            throw BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database, "提交事务失败");
        }
        finally
        {
            foreach (var rs in transaction.Databases.Values)
            {
                rs.Close();
            }
        }
        return true;
    }

    #endregion


    #region ErrorHandle

    /// <summary>
    /// 数据库操作错误解读
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="obj"></param>
    /// <param name="strMessage"></param>
    private static BusinessException ErrorHandle(Exception ex, PersistentObject? obj, string strMessage = "")
    {
        var className = "";
        if (obj != null)
        {
            className = obj.GetClassName();
        }

        if (ex is SqlException sqlErr)
        {
            int j;
            for (j = 0; j < sqlErr.Errors.Count; j++)
            {
                if (sqlErr.Errors[j].Number != 3621)
                {
                    break;
                }
            }
            throw sqlErr.Errors[j].Number switch
            {
                2627 => BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
                    className + " 对象或其关联对象的数据与已存在数据冲突。" + strMessage, obj),
                8152 => BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
                    className + " 数据超出数据库容纳范围。" + strMessage, obj),
                515 => BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
                    className + " 中某些属性不能为Null。" + strMessage, obj),
                0 => BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
                    className + " 某些属性数据类型于数据库数据类型不兼容。" + strMessage, obj),
                544 => BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
                    className + " 有自动编号属性，不能指定特定值。" + strMessage, obj),
                547 or 11040 => BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
                    "试图对 " + className + " 对象操作，由于约束机制，操作被终止。" + strMessage, obj),
                _ => BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
                    className + " " + sqlErr.Message + strMessage, obj)
            };
        }
        return BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
            className + "数据库操作异常。" + strMessage, obj);
    }

    #endregion

    #endregion
}
