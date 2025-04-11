namespace Mr1Ceng.Util;

/// <summary>
/// 数据获取策略
/// </summary>
public enum DataFetchStrategy
{
    /// <summary>
    /// 只访问本地，不访问云端
    /// </summary>
    OnlyLocal = 0,

    /// <summary>
    /// 优先访问本地，若本地无数据再访问云端
    /// </summary>
    PreferLocal = 1,

    /// <summary>
    /// 只访问云端，不使用本地缓存或存储
    /// </summary>
    OnlyRemote = 2
}
