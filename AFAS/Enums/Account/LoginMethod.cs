using System.ComponentModel;

namespace AFAS.Enums.Account;

/// <summary>
/// 登录方式
/// </summary>
internal enum LoginMethod
{
    [Description("登录网页")] WEB,
    [Description("登录网页【超级密码】")] WEB_SUPER,
    [Description("登录网页【账号密码】")] WEB_PASSWORD,
    [Description("登录网页【短信验证码】")] WEB_MOBILE
}
