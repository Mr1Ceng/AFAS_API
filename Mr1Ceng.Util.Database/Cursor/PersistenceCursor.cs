using System.Data;
using System.Reflection;
using Mr1Ceng.Util.Database.Internal.Core;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// 对象游标的封装
/// </summary>
public class PersistenceCursor<T> where T : PersistentObject, new()
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="ds"></param>
    internal PersistenceCursor(DataSet ds)
    {
        results = ds;
        Count = ds.Tables[0].Rows.Count;
    }

    #endregion


    #region 方法

    /// <summary>
    /// 返回上一个实体对象*
    /// </summary>
    /// <returns>PersistentObject</returns>
    public T? PreviousObject()
    {
        try
        {
            var obj = broker.GetEntityObject<T>(results.Tables[0].Rows[index]);
            obj.IsPersistent = true;
            index--;
            return obj;
        }
        catch (IndexOutOfRangeException)
        {
            return null;
        }
    }

    /// <summary>
    /// 返回下一个实体对象*
    /// </summary>
    /// <returns>PersistentObject</returns>
    public T? NextObject()
    {
        try
        {
            var obj = broker.GetEntityObject<T>(results.Tables[0].Rows[index]);
            obj.IsPersistent = true;
            index++;
            return obj;
        }
        catch (IndexOutOfRangeException)
        {
            return null;
        }
    }

    /// <summary>
    /// 到指定对象*
    /// </summary>
    /// <returns>PersistentObject</returns>
    public T? MoveObject(int point)
    {
        if (point < 0 || point >= Count)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Database, "对象超过范围请大于0小于" + Count, new
            {
                point
            });
        }

        try
        {
            var obj = broker.GetEntityObject<T>(results.Tables[0].Rows[point]);
            obj.IsPersistent = true;
            index = point;
            return obj;
        }
        catch (IndexOutOfRangeException)
        {
            return null;
        }
    }

    /// <summary>
    /// 将指针移动到第一个实体对象处
    /// </summary>
    public void MoveFirst()
    {
        index = 0;
    }

    /// <summary>
    /// 将指针移动到最后一个实体对象处
    /// </summary>
    public void MoveLast()
    {
        index = Count > 0 ? Count - 1 : 0;
    }

    /// <summary>
    /// 是否还有对象
    /// </summary>
    /// <returns>True仍有对象，False无对象</returns>
    public bool HasObject() => index < Count;

    #endregion


    #region 属性

    /// <summary>
    /// 实体对象数
    /// </summary>
    public int Count { get; }

    #endregion


    #region 变量

    /// <summary>
    /// 实体层代理
    /// </summary>
    private readonly Broker broker = Broker.Instance;

    /// <summary>
    /// 数据池
    /// </summary>
    private readonly DataSet results;

    /// <summary>
    /// 游标索引
    /// </summary>
    private int index;

    #endregion
}
