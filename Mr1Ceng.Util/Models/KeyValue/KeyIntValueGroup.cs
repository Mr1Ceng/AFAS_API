using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 键值组
/// </summary>
public class KeyIntValueGroup
{
    /// <summary>
    /// 组编码
    /// </summary>
    [Description("组编码")]
    public string GroupId { get; set; } = "";

    /// <summary>
    /// 组名称
    /// </summary>
    [Description("组名称")]
    public string GroupName { get; set; } = "";

    /// <summary>
    /// 键值清单
    /// </summary>
    [Description("键值清单")]
    public List<KeyIntValue> ItemList { get; set; } = [];
}
