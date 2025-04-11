using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 手机号码
/// </summary>
public class MobileModel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public MobileModel()
    {
        Mobile = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="mobile"></param>
    public MobileModel(string mobile)
    {
        Mobile = mobile;
    }

    /// <summary>
    /// 手机号码
    /// </summary>
    [Description("手机号码")]
    public string Mobile { get; set; }
}
