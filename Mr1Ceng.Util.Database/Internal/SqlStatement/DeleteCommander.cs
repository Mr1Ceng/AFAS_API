using System.Data;
using System.Data.SqlClient;
using Mr1Ceng.Util.Database.Internal.Core;

namespace Mr1Ceng.Util.Database.Internal.SqlStatement;

/// <summary>
/// DeleteCommander 类
/// </summary>
/// <remarks>
/// 这个类层知道如何根据ClassMap对象构造Delete语句
/// </remarks>
internal class DeleteCommander<T> where T : PersistentObject
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="clsMap"></param>
    public DeleteCommander(ClassMap clsMap)
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

        // 构造Delete语句
        SqlString = $@"
            DELETE FROM [{ThisClassMap.Table.TableName}]
            WHERE {string.Join(" AND ", ThisClassMap.PrimaryKeys.Select(x => $"[{x.Column.ColumnName}] = @{x.Name}"))}
        ";

        // 命令
        var cmd = ThisClassMap.PersistenceProvider.GetCommand();
        cmd.CommandText = SqlString;
        foreach (var attributeMap in ThisClassMap.PrimaryKeys)
        {
            var parameter = cmd.CreateParameter();
            parameter.DbType = attributeMap.Column.ColumnType;
            parameter.Value = obj.GetAttributeValue(attributeMap.Name);
            parameter.ParameterName = "@" + attributeMap.Name;
            cmd.Parameters.Add(parameter);
        }
        return cmd;
    }

    /// <summary>
    /// 生成PersistentCriteria对应的IDbCommand
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns>IDbCommand</returns>
    public IDbCommand BuildForCriteria(DeleteCriteria<T> criteria)
    {
        // 构造Delete语句
        SqlString = $"DELETE FROM [{ThisClassMap.Table.TableName}]";

        // 过滤
        if (criteria.Selections.Count > 0)
        {
            SqlString += $" WHERE {string.Join(" AND ", criteria.Selections)}";
        }

        // 命令
        var cmd = ThisClassMap.PersistenceProvider.GetCommand();
        cmd.CommandText = SqlString;

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
