using Mr1Ceng.Util.Database.Internal.Core;
using Mr1Ceng.Util.Database.Internal.Provider;

namespace Mr1Ceng.Util.Database.Internal.PersistenceConfig;

/// <summary>
/// 配置信息接口类
/// </summary>
internal interface IContextManager
{
    #region 属性

    /// <summary>
    /// 数据库连接池
    /// </summary>
    Dictionary<string, DatabaseProvider> DatabasePool { get; }

    /// <summary>
    /// ApplicationConfig中数据库映射的集合
    /// </summary>
    Dictionary<string, DatabaseMap> DatabaseMaps { get; }

    /// <summary>
    /// ORMapping中类映射的集合
    /// </summary>
    Dictionary<string, ClassMap> ClassMaps { get; }

    #endregion


    #region 方法

    /// <summary>
    /// 加载配置文件
    /// </summary>
    void LoadConfig();

    #endregion
}
