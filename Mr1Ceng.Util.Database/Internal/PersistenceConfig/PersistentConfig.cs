namespace Mr1Ceng.Util.Database.Internal.PersistenceConfig;

/// <summary>
/// 持久层配置类
/// </summary>
/// <remarks>
/// 该类封装了设置、得到xml文件路径，以及取得数据库连接字符串的功能
/// </remarks>
internal class PersistentConfig : IPersistentConfig
{
    #region 构造函数

    /// <summary>
    /// 构造方法
    /// </summary>
    protected PersistentConfig()
    {
    }

    #endregion


    #region 变量、属性

    /// <summary>
    /// 持久层配置文件路径
    /// </summary>
    public string ApplicationContextFile { get; set; } = "";

    #endregion
}
