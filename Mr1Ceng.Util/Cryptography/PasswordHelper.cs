namespace Mr1Ceng.Util;

/// <summary>
/// 密码工具类
/// </summary>
public class PasswordHelper
{
    #region Encrypt

    /// <summary>
    /// 生成密码的密文
    /// </summary>
    /// <param name="password">明文密码</param>
    /// <returns>加密后的密码</returns>
    public static string Encrypt(string password) => Encrypt(password, "WaPxY3");

    /// <summary>
    /// 生成密码的密文
    /// </summary>
    /// <param name="password">明文密码</param>
    /// <param name="seed">加密因子</param>
    /// <returns>加密后的密码</returns>
    public static string Encrypt(string password, string seed) => Sha256Helper.Encrypt($"{seed}_{password}");

    #endregion
}
