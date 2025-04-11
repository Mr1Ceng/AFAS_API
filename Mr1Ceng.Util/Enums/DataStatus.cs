using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 数据类型
/// </summary>
public enum DataStatus
{
    /// <summary>
    /// 无效的
    /// </summary>
    [Description("无效的")] INACTIVE = 0,

    /// <summary>
    /// 有效的
    /// </summary>
    [Description("有效的")] ACTIVE = 1,

    /// <summary>
    /// 草稿
    /// </summary>
    [Description("草稿")] DRAFT = 2
}
