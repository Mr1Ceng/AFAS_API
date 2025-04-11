namespace Mr1Ceng.Util.Database;

/// <summary>
/// 系统业务基础类
/// </summary>
public abstract class ManagerBase
{
    /// <summary>
    /// SystemEnvironment
    /// </summary>
    public SystemEnvironment SystemEnvironment { get; } = SystemEnvironment.Instance;
}
