namespace Mr1Ceng.Util.Database.Internal.Core;

/// <summary>
/// DatabaseMap 数据源实例信息
/// </summary>
internal sealed class DatabaseMap
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    internal DatabaseMap()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="databaseName"></param>
    internal DatabaseMap(string databaseName)
    {
        DatabaseName = databaseName;
    }

    #endregion


    #region 属性

    /// <summary>
    /// 数据库名
    /// </summary>
    internal string DatabaseName { get; } = "";

    #endregion
}
