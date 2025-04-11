namespace Mr1Ceng.Util.Database.Internal.PersistenceConfig;

/// <summary>
/// 持久层配置接口
/// </summary>
/// <remarks>
/// 该类封装了设置、得到xml文件路径，以及取得数据库连接字符串的功能
/// </remarks>
internal interface IPersistentConfig
{
    #region 属性

    string ApplicationContextFile { get; }

    #endregion
}
