namespace Mr1Ceng.Util.Database.Internal.Core;

/// <summary>
/// TableMap 实例信息
/// </summary>
internal sealed class TableMap
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="database"></param>
    internal TableMap(string tableName, DatabaseMap database)
    {
        TableName = tableName;
        Database = database;
    }

    #endregion


    #region 属性

    /// <summary>
    /// 数据库信息
    /// </summary>
    internal DatabaseMap Database { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    internal string TableName { get; set; }

    /// <summary>
    /// 数据列
    /// </summary>
    internal List<AttributeMap> Attributes { get; set; } = [];

    /// <summary>
    /// 主键列
    /// </summary>
    internal List<AttributeMap> PrimaryKeys { get; set; } = [];

    /// <summary>
    /// 自动增长列
    /// </summary>
    internal AttributeMap? AutoIdentityKey { get; set; }

    #endregion
}
