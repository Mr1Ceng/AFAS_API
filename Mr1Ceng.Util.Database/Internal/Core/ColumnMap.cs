using System.Data;

namespace Mr1Ceng.Util.Database.Internal.Core;

/// <summary>
/// ColumnMap 实例信息
/// </summary>
internal sealed class ColumnMap
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="table"></param>
    internal ColumnMap(string columnName, TableMap table)
    {
        ColumnName = columnName;
        Table = table;
    }

    #endregion


    #region 属性

    /// <summary>
    /// </summary>
    internal TableMap Table { get; }

    /// <summary>
    /// 列名
    /// </summary>
    internal string ColumnName { get; }

    /// <summary>
    /// 数据库中的数据类型
    /// </summary>
    internal DbType ColumnType { get; set; }

    /// <summary>
    /// </summary>
    internal int ColumnSize { get; set; }

    /// <summary>
    /// 是否主键
    /// </summary>
    internal bool IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否自动增长列
    /// </summary>
    internal bool IsAutoIdentityKey { get; set; }

    #endregion


    #region 方法

    /// <summary>
    /// 获取全名
    /// </summary>
    /// <returns></returns>
    internal string GetFullyQualifiedName() => $"{Table.TableName}.{ColumnName}";

    /// <summary>
    /// 获取没有点的全名
    /// </summary>
    /// <returns></returns>
    internal string GetFullNameWithNoDot() => Table.TableName + ColumnName;

    #endregion
}
