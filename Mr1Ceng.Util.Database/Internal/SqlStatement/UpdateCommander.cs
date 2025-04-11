using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Mr1Ceng.Util.Database.Internal.Core;

namespace Mr1Ceng.Util.Database.Internal.SqlStatement;

/// <summary>
/// UpdateCommander 类
/// </summary>
/// <typeparam name="T">PersistentObject</typeparam>
/// <remarks>这个类层知道如何根据ClassMap对象构造Update语句</remarks>
internal class UpdateCommander<T> where T : PersistentObject
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="clsMap"></param>
    public UpdateCommander(ClassMap clsMap)
    {
        ThisClassMap = clsMap;
    }

    #endregion


    #region 属性

    /// <summary>
    /// ClassMap
    /// </summary>
    internal ClassMap ThisClassMap { get; }

    /// <summary>
    /// Sql语句
    /// </summary>
    internal string SqlString { get; set; } = "";

    #endregion


    #region 方法

    /// <summary>
    /// 生成实体对象对应的IDbCommand
    /// </summary>
    /// <returns>IDbCommand</returns>
    public IDbCommand BuildForObject(T obj)
    {
        if (ThisClassMap.PrimaryKeys.Count == 0)
        {
            throw new Exception($"没有主键：{ThisClassMap.Table.TableName}");
        }

        // 构造Update语句
        SqlString = $@"
            UPDATE [{ThisClassMap.Table.TableName}]
            SET 
                {string.Join(", ", ThisClassMap.Attributes.Where(x => !x.Column.IsAutoIdentityKey && !x.Column.IsPrimaryKey).Select(x => $"[{x.Column.ColumnName}] = @{x.Name}"))}
            WHERE {string.Join(" AND ", ThisClassMap.PrimaryKeys.Select(x => $"[{x.Column.ColumnName}] = @{x.Name}"))}
        ";

        // 命令
        var cmd = ThisClassMap.PersistenceProvider.GetCommand();
        cmd.CommandText = SqlString;
        foreach (var attributeMap in ThisClassMap.Attributes)
        {
            if (attributeMap.Column.IsAutoIdentityKey && !attributeMap.Column.IsPrimaryKey)
            {
                continue;
            }

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "@" + attributeMap.Name;
            parameter.DbType = attributeMap.Column.ColumnType;
            parameter.Value = obj.GetAttributeValue(attributeMap.Name) ?? DBNull.Value;
            cmd.Parameters.Add(parameter);
        }
        return cmd;
    }

    /// <summary>
    /// 生成PersistentCriteria对应的IDbCommand
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns>IDbCommand</returns>
    public IDbCommand BuildForCriteria(UpdateCriteria<T> criteria)
    {
        if (criteria.ForUpdateCollection.Count == 0)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Develop,
                $"没有要更新字段：{ThisClassMap.Table.TableName}", criteria);
        }

        #region 待更新的字段

        List<string> updateFields = [];
        List<UpdateParameterItem> updateParams = [];
        foreach (var item in criteria.ForUpdateCollection)
        {
            var paramName = $"@{item.Key.Name}_{NewCode.Mark}";
            updateFields.Add($"[{item.Key.Column.ColumnName}] = {paramName}");
            updateParams.Add(new UpdateParameterItem
            {
                Name = paramName,
                DbType = item.Key.Column.ColumnType,
                Value = item.Value
            });
        }

        #endregion

        // 构造Update语句
        SqlString = $@"
            UPDATE [{ThisClassMap.Table.TableName}]
            SET 
                {string.Join(", ", updateFields)}
        ";

        // 过滤
        if (criteria.Selections.Count > 0)
        {
            SqlString += $" WHERE {string.Join(" AND ", criteria.Selections)}";
        }

        // 命令
        var cmd = ThisClassMap.PersistenceProvider.GetCommand();
        cmd.CommandText = SqlString;

        // 赋值数据
        foreach (var item in updateParams)
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = item.Name;
            parameter.DbType = item.DbType;
            parameter.Value = item.Value ?? DBNull.Value;
            cmd.Parameters.Add(parameter);
        }

        // 查询数据
        if (criteria.Parameters.Count > 0)
        {
            foreach (var parameter in criteria.Parameters)
            {
                cmd.Parameters.Add(new SqlParameter($"@{parameter.Name}", parameter.Value));
            }
        }

        return cmd;
    }

    #endregion
}

internal class UpdateParameterItem
{
    public string Name { get; set; } = "";
    public DbType DbType { get; set; }
    public object? Value { get; set; }
}
