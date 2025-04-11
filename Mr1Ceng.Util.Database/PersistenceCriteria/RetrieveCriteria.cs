using System.Data;
using System.Reflection;
using Mr1Ceng.Util.Database.Internal.Enums;
using Mr1Ceng.Util.Database.Internal.Models;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// RetrieveCriteria 类
/// </summary>
/// <remarks>
/// 这个类层次封装了根据指定条件进行【获取】操作的行为
/// </remarks>
public class RetrieveCriteria<T> : PersistentCriteria where T : PersistentObject, new()
{
    #region 构造函数

    /// <summary>
    /// 生成一个RetrieveCriteria实例
    /// </summary>
    public RetrieveCriteria() : base(CriteriaTypes.Retrieve, typeof(T))
    {
    }

    #endregion


    #region 属性

    /// <summary>
    /// 排序列表
    /// </summary>
    internal List<OrderBy> OrderByList { get; } = [];

    #endregion


    #region 方法

    /// <summary>
    /// 设实体属性排序信息
    /// </summary>
    /// <param name="attributeName">实体属性名</param>
    /// <param name="isAsc">FieldOrderBy</param>
    public void OrderBy(string attributeName, FieldOrderBy isAsc) => OrderByList.Add(new OrderBy(attributeName, isAsc));

    /// <summary>
    /// 设实体属性排序信息
    /// </summary>
    /// <param name="attributeName">实体属性名</param>
    public void OrderBy(string attributeName) => OrderBy(attributeName, FieldOrderBy.Ascending);

    #endregion


    #region 查询方法

    /// <summary>
    /// 根据查询条件返回一个结果集
    /// </summary>
    /// <returns>DataTable</returns>
    public DataTable GetDataTable()
    {
        var cmd = ThisClassMap.GetSelectSqlFor(this);
        return broker.ExecuteQuery(cmd, ThisClassMap.Database.DatabaseName);
    }

    /// <summary>
    /// 返回一个PersistenceCursor对象
    /// </summary>
    /// <returns></returns>
    public PersistenceCursor<T> GetCursor()
    {
        var ds = new DataSet();
        ds.Tables.Add(GetDataTable());
        return new PersistenceCursor<T>(ds);
    }

    /// <summary>
    /// 返回实体对象列表
    /// </summary>
    /// <returns>实体对象列表</returns>
    public List<T> GetCollection()
    {
        var list = new List<T>();
        var cursor = GetCursor();
        for (var i = 0; i < cursor.Count; i++)
        {
            var obj = cursor.NextObject();
            if (obj == null)
            {
                break;
            }
            list.Add(obj);
        }
        return list;
    }

    /// <summary>
    /// 返回实体对象列表
    /// </summary>
    /// <param name="start">从第几条开始读取</param>
    /// <returns>实体对象列表</returns>
    public List<T> GetCollection(int start)
    {
        var list = new List<T>();
        var cursor = GetCursor();
        cursor.MoveObject(start);
        for (var i = start; i < cursor.Count; i++)
        {
            var obj = cursor.NextObject();
            if (obj == null)
            {
                break;
            }
            list.Add(obj);
        }
        return list;
    }

    /// <summary>
    /// 返回实体对象列表
    /// </summary>
    /// <param name="start">从第几条开始读取</param>
    /// <param name="length">读取多少行</param>
    /// <returns>实体对象列表</returns>
    public List<T> GetCollection(int start, int length)
    {
        var list = new List<T>();
        var cursor = GetCursor();
        var pageSize = length < cursor.Count ? length : cursor.Count;
        cursor.MoveObject(start);
        for (var i = start; i < pageSize; i++)
        {
            var obj = cursor.NextObject();
            if (obj == null)
            {
                break;
            }
            list.Add(obj);
        }
        return list;
    }

    #endregion


    #region 查询方法(事务中)

    /// <summary>
    /// 根据查询条件返回一个结果集(事务中)
    /// </summary>
    /// <param name="ts">事务</param>
    /// <returns>DataTable</returns>
    public DataTable GetDataTable(Transaction ts)
    {
        var dbName = ThisClassMap.Database.DatabaseName;
        var rdb = ts.GetPersistenceProvider(dbName);
        var dt = rdb.AsDataTable(ThisClassMap.GetSelectSqlFor(this));
        return dt;
    }

    /// <summary>
    /// 返回一个PersistenceCursor对象(事务中)
    /// </summary>
    /// <param name="ts">事务</param>
    /// <returns></returns>
    public PersistenceCursor<T> GetCursor(Transaction ts)
    {
        var ds = new DataSet();
        ds.Tables.Add(GetDataTable(ts));
        return new PersistenceCursor<T>(ds);
    }

    /// <summary>
    /// 返回实体对象列表(事务中)
    /// </summary>
    /// <param name="ts">事务</param>
    /// <returns>实体对象列表</returns>
    public List<T> GetCollection(Transaction ts)
    {
        var list = new List<T>();
        var cursor = GetCursor(ts);
        for (var i = 0; i < cursor.Count; i++)
        {
            var obj = cursor.NextObject();
            if (obj == null)
            {
                break;
            }
            list.Add(obj);
        }
        return list;
    }

    /// <summary>
    /// 返回实体对象列表(事务中)
    /// </summary>
    /// <param name="ts">事务</param>
    /// <param name="start">从第几条开始读取</param>
    /// <returns>实体对象列表</returns>
    public List<T> GetCollection(Transaction ts, int start)
    {
        var list = new List<T>();
        var cursor = GetCursor(ts);
        cursor.MoveObject(start);
        for (var i = start; i < cursor.Count; i++)
        {
            var obj = cursor.NextObject();
            if (obj == null)
            {
                break;
            }
            list.Add(obj);
        }
        return list;
    }

    /// <summary>
    /// 返回实体对象列表(事务中)
    /// </summary>
    /// <param name="ts">事务</param>
    /// <param name="start">从第几条开始读取</param>
    /// <param name="length">读取多少行</param>
    /// <returns>实体对象列表</returns>
    public List<T> GetCollection(Transaction ts, int start, int length)
    {
        var list = new List<T>();
        var cursor = GetCursor(ts);
        var pageSize = length < cursor.Count ? length : cursor.Count;
        cursor.MoveObject(start);
        for (var i = start; i < pageSize; i++)
        {
            var obj = cursor.NextObject();
            if (obj == null)
            {
                break;
            }
            list.Add(obj);
        }
        return list;
    }

    #endregion


    #region 查询方法(取第一行)

    /// <summary>
    /// 获取第一个结果集
    /// </summary>
    /// <returns></returns>
    public T GetFirstObject()
    {
        var cmd = ThisClassMap.GetSelectSqlFor(this);
        var dt = broker.ExecuteQuery(cmd, ThisClassMap.Database.DatabaseName);

        var obj = new T();
        if (dt.Rows.Count > 0)
        {
            try
            {
                obj = broker.GetEntityObject<T>(dt.Rows[0]);
                obj.IsPersistent = true;
            }
            catch (Exception ex)
            {
                throw BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database,
                    $"获取实体对象失败,{typeof(T).Name}", new
                    {
                        cmd
                    });
            }
        }
        return obj;
    }

    #endregion
}
