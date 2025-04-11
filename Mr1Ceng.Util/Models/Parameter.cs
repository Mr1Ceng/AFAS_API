using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 参数
/// </summary>
public class Parameter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public Parameter()
    {
        Name = "";
        Value = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public Parameter(string name, string value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// 参数名称
    /// </summary>
    [Description("参数名称")]
    public string Name { get; set; }

    /// <summary>
    /// 参数值
    /// </summary>
    [Description("参数值")]
    public string Value { get; set; }
}
