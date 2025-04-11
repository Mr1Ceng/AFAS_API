using Mr1Ceng.Util.Database.Internal.Core;
using Mr1Ceng.Util.Database.Internal.Enums;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// UpdateCriteria 类
/// </summary>
public class UpdateCriteria<T> : PersistentCriteria where T : PersistentObject
{
    #region 构造函数

    /// <summary>
    /// 生成一个RetrieveCriteria实例
    /// </summary>
    public UpdateCriteria() : base(CriteriaTypes.Update, typeof(T))
    {
    }

    #endregion


    #region 属性

    /// <summary>
    /// </summary>
    internal Dictionary<AttributeMap, object?> ForUpdateCollection { get; } = new();

    #endregion


    #region 方法

    /// <summary>
    /// 执行数据更新操作
    /// </summary>
    /// <returns>更新的行数</returns>
    public int Execute()
    {
        var cmd = ThisClassMap.GetUpdateSqlFor(this);
        return broker.Execute(cmd, ThisClassMap.Database.DatabaseName);
    }

    /// <summary>
    /// 执行数据更新操作(有事务)
    /// </summary>
    /// <param name="ts">事务</param>
    /// <returns>更新的行数</returns>
    public int Execute(Transaction ts)
    {
        var rdb = ts.GetPersistenceProvider(ThisClassMap.Database.DatabaseName);
        var result = rdb.DoCommand(ThisClassMap.GetUpdateSqlFor(this));
        return result;
    }

    /// <summary>
    /// 添加要更新的字段
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddAttributeForUpdate(string attributeName, object? attributeValue)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        ForUpdateCollection.Add(attributeMap, attributeValue);
    }

    #endregion
}
