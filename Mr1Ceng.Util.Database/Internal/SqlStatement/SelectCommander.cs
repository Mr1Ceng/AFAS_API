using System.Data;
using System.Data.SqlClient;
using Mr1Ceng.Util.Database.Internal.Core;

namespace Mr1Ceng.Util.Database.Internal.SqlStatement;

/// <summary>
/// SelectCommander 类
/// </summary>
/// <typeparam name="T">PersistentObject</typeparam>
/// <remarks>这个类层知道如何根据ClassMap对象构造Select语句</remarks>
internal class SelectCommander<T> where T : PersistentObject, new()
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="clsMap"></param>
    public SelectCommander(ClassMap clsMap)
    {
        ThisClassMap = clsMap;

        //构造Select语句
        SqlString = $@"
            SELECT
                {string.Join(", ", clsMap.Attributes.Select(x => {
                    var columnName = x.Column.ColumnName;
                    var attrName = x.Name;
                    if (attrName != "" && attrName != columnName)
                    {
                        return $"[{columnName}] AS [{attrName}]";
                    }
                    return $"[{columnName}]";
                }))}
            FROM [{clsMap.Table.TableName}]
        ";
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
    internal string SqlString { get; set; }

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

        var fields = ThisClassMap.PrimaryKeys.Select(x => $"[{x.Column.ColumnName}] = @{x.Name}").ToList();
        SqlString += " WHERE" + string.Join(" AND ", fields);
        var cmd = ThisClassMap.PersistenceProvider.GetCommand();
        cmd.CommandText = SqlString;
        foreach (var attributeMap in ThisClassMap.PrimaryKeys)
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "@" + attributeMap.Name;
            parameter.DbType = attributeMap.Column.ColumnType;
            parameter.Value = obj.GetAttributeValue(attributeMap.Name);
            cmd.Parameters.Add(parameter);
        }
        return cmd;
    }

    /// <summary>
    /// 生成PersistentCriteria对应的IDbCommand
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns>IDbCommand</returns>
    public IDbCommand BuildForCriteria(RetrieveCriteria<T> criteria)
    {
        // 过滤
        if (criteria.Selections.Count > 0)
        {
            SqlString += $" WHERE {string.Join(" AND ", criteria.Selections)}";
        }

        // 排序
        var orderByList = criteria.OrderByList;
        if (orderByList.Count > 0)
        {
            var orderBys = orderByList.Select(x
                    => $"[{ThisClassMap.GetAttributeMap(x.AttributeName).Column.ColumnName}] {(x.IsAscend == FieldOrderBy.Ascending ? "ASC" : "DESC")}")
                .ToList();
            SqlString += " ORDER BY " + string.Join(", ", orderBys);
        }

        // 命令
        var cmd = ThisClassMap.PersistenceProvider.GetCommand();
        cmd.CommandText = SqlString;
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
