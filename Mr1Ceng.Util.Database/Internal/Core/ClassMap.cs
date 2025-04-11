using System.Data;
using System.Reflection;
using Mr1Ceng.Util.Database.Internal.Provider;
using Mr1Ceng.Util.Database.Internal.SqlStatement;

namespace Mr1Ceng.Util.Database.Internal.Core;

/// <summary>
/// ClassMap 信息
/// </summary>
/// <remarks>该类封装了映射类到关系数据库的行为</remarks>
internal sealed class ClassMap : PersistentObject
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tableMap"></param>
    /// <param name="className"></param>
    /// <param name="provider"></param>
    public ClassMap(DatabaseProvider provider, TableMap tableMap, string className)
    {
        PersistenceProvider = provider;
        Table = tableMap;
        ClassName = className;
    }

    #endregion


    #region 属性

    /// <summary>
    /// 类名
    /// </summary>
    public string ClassName { get; }

    /// <summary>
    /// 表
    /// </summary>
    public TableMap Table { get; private set; }

    /// <summary>
    /// Table所在的数据库信息
    /// </summary>
    public DatabaseMap Database => Table.Database;

    /// <summary>
    /// 数据列
    /// </summary>
    public List<AttributeMap> Attributes => Table.Attributes;

    /// <summary>
    /// 主键列
    /// </summary>
    public List<AttributeMap> PrimaryKeys => Table.PrimaryKeys;

    /// <summary>
    /// 自动增长列
    /// </summary>
    public AttributeMap? AutoIdentityKey => Table.AutoIdentityKey;

    /// <summary>
    /// 映射数据库
    /// </summary>
    public DatabaseProvider PersistenceProvider { get; }

    #endregion


    #region 方法

    /// <summary>
    /// 增加AttributeMap到ClassMap
    /// </summary>
    /// <param name="attribute"></param>
    public void AddAttributeMap(AttributeMap attribute)
    {
        if (Table.Database.DatabaseName == "")
        {
            Table = attribute.Column.Table;
        }

        Table.Attributes.Add(attribute);

        if (attribute.Column.IsPrimaryKey)
        {
            Table.PrimaryKeys.Add(attribute);
        }

        if (attribute.Column.IsAutoIdentityKey)
        {
            Table.AutoIdentityKey = attribute;
        }
    }

    /// <summary>
    /// 获取属性字段
    /// </summary>
    /// <param name="index">索引</param>
    /// <returns></returns>
    public AttributeMap GetAttributeMap(int index) => Attributes[index];

    /// <summary>
    /// 获取属性字段
    /// </summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public AttributeMap GetAttributeMap(string name)
    {
        var attribute = Table.Attributes.FirstOrDefault(x => x.Name == name);
        if (attribute == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Develop, $"属性字段不能为空:{name}");
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Develop, $"属性字段不能为空:{name}");
        }
        return attribute;
    }

    /// <summary>
    /// 获取主键字段
    /// </summary>
    /// <param name="index">关键字索引</param>
    /// <returns></returns>
    public AttributeMap GetKeyAttributeMap(int index) => PrimaryKeys[index];

    /// <summary>
    /// 获取属性总数
    /// </summary>
    /// <returns></returns>
    public int GetSize() => Attributes.Count;

    /// <summary>
    /// 获取主键总数
    /// </summary>
    /// <returns></returns>
    public int GetKeySize() => PrimaryKeys.Count;

    /// <summary>
    /// 获取实体对象的SELECT语句
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public IDbCommand GetSelectSqlFor<T>(T obj) where T : PersistentObject, new()
        => new SelectCommander<T>(this).BuildForObject(obj);

    /// <summary>
    /// 获取PersistentCriteria的SELECT语句
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public IDbCommand GetSelectSqlFor<T>(RetrieveCriteria<T> criteria) where T : PersistentObject, new()
        => new SelectCommander<T>(this).BuildForCriteria(criteria);

    /// <summary>
    /// 获取实体对象的INSERT语句
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public IDbCommand GetInsertSqlFor<T>(T obj) where T : PersistentObject
        => new InsertCommander<T>(this).BuildForObject(obj);

    /// <summary>
    /// 获取实体对象的UPDATE语句
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public IDbCommand GetUpdateSqlFor<T>(T obj) where T : PersistentObject
        => new UpdateCommander<T>(this).BuildForObject(obj);

    /// <summary>
    /// 获取PersistentCriteria的UPDATE语句
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public IDbCommand GetUpdateSqlFor<T>(UpdateCriteria<T> criteria) where T : PersistentObject
        => new UpdateCommander<T>(this).BuildForCriteria(criteria);

    /// <summary>
    /// 获取实体对象的DELETE语句
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public IDbCommand GetDeleteSqlFor<T>(T obj) where T : PersistentObject
        => new DeleteCommander<T>(this).BuildForObject(obj);

    /// <summary>
    /// 获取PersistentCriteria的DELETE语句
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public IDbCommand GetDeleteSqlFor<T>(DeleteCriteria<T> criteria) where T : PersistentObject
        => new DeleteCommander<T>(this).BuildForCriteria(criteria);

    /// <summary>
    /// 设置实体对象的数据
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="row"></param>
    public void SetObject<T>(T obj, DataRow row) where T : PersistentObject
    {
        foreach (var attributeMap in Attributes.Where(attributeMap => row[attributeMap.Name] != DBNull.Value))
        {
            obj.SetAttributeValue(attributeMap.Name, row[attributeMap.Name]);
        }
    }

    #endregion
}
