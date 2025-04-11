namespace Mr1Ceng.Tools.ORMapping.V7.Common;

/// <summary>
/// CommonInfo 的摘要说明。
/// </summary>
public class CommonInfo
{
    /// <summary>
    /// ORMapping文件名
    /// </summary>
    public string ORMappingName { get; set; } = "";

    /// <summary>
    /// 输出文件夹
    /// </summary>
    public string OutputDir { get; set; } = "";

    /// <summary>
    /// 是否输出实体对象
    /// </summary>
    public bool EntityClassFiles { get; internal set; } = false;

    /// <summary>
    /// 是否输出实体对象
    /// </summary>
    public bool AppSettingsFiles { get; internal set; } = false;
}
