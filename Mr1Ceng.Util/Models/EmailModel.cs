using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 电子邮件
/// </summary>
public class EmailModel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public EmailModel()
    {
        Email = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="email"></param>
    public EmailModel(string email)
    {
        Email = email;
    }

    /// <summary>
    /// 邮件地址
    /// </summary>
    [Description("邮件地址")]
    public string Email { get; set; }
}
