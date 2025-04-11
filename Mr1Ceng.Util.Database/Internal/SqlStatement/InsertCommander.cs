using System.Data;
using Mr1Ceng.Util.Database.Internal.Core;

namespace Mr1Ceng.Util.Database.Internal.SqlStatement;

/// <summary>
/// InsertCommander 类
/// </summary>
/// <remarks>
/// 这个类层知道如何根据ClassMap对象构造Insert语句
/// </remarks>
internal class InsertCommander<T> where T : PersistentObject
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="clsMap"></param>
    public InsertCommander(ClassMap clsMap)
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

        // 构造Insert语句
        SqlString = $@"
            INSERT INTO [{ThisClassMap.Table.TableName}]
            ({string.Join(", ", ThisClassMap.Attributes.Where(x => !x.Column.IsAutoIdentityKey).Select(x => $"[{x.Column.ColumnName}]"))})
            VALUES 
            ({string.Join(", ", ThisClassMap.Attributes.Where(x => !x.Column.IsAutoIdentityKey).Select(x => $"@{x.Name}"))})
        ";

        var cmd = ThisClassMap.PersistenceProvider.GetCommand();
        cmd.CommandText = SqlString;
        foreach (var attributeMap in ThisClassMap.Attributes)
        {
            if (attributeMap.Column.IsAutoIdentityKey)
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

    #endregion
}
