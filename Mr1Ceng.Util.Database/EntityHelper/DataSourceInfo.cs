namespace Mr1Ceng.Util.Database.EntityHelper;

/// <summary>
/// 数据源配置信息
/// </summary>
internal class DataSourceInfo
{
    #region 属性

    /// <summary>
    /// 数据源名称
    /// </summary>
    internal string Name { get; set; } = "";

    /// <summary>
    /// 数据源地址
    /// </summary>
    internal string DataSource { get; set; } = "";

    /// <summary>
    /// 数据库名称
    /// </summary>
    internal string DatabaseName { get; set; } = "";

    /// <summary>
    /// 数据开账号
    /// </summary>
    internal string UserID { get; set; } = "";

    /// <summary>
    /// 数据库密码
    /// </summary>
    internal string Password { get; set; } = "";

    /// <summary>
    /// ORMapping文件名称
    /// </summary>
    internal string ORMappingName { get; set; } = "";

    #endregion
}
