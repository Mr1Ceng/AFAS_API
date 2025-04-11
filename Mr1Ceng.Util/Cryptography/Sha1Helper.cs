using System.Security.Cryptography;
using System.Text;

namespace Mr1Ceng.Util.Cryptography;

/// <summary>
/// Sha1工具类
/// </summary>
public class Sha1Helper
{
    /// <summary>
    /// 将普通文本转换成Sha1编码的文本【全大写】
    /// </summary>
    /// <param name="encryptValue">要加密的字符串</param>
    /// <returns>加密后的Sha1字符串</returns>
    public static string Encrypt(string encryptValue) => Encrypt(Encoding.UTF8.GetBytes(encryptValue));

    /// <summary>
    /// 将普通文本转换成Sha1编码的文本【全大写】
    /// </summary>
    /// <param name="encryptValue">要加密的字符串</param>
    /// <param name="encoding">编码方式</param>
    /// <returns>加密后的Sha1字符串</returns>
    public static string Encrypt(string encryptValue, Encoding encoding) => Encrypt(encoding.GetBytes(encryptValue));

    /// <summary>
    /// 将Byte[]转换成Sha1编码文本【全大写】
    /// </summary>
    /// <param name="buffer">Byte[]</param>
    /// <returns>加密后的Sha1字符串</returns>
    public static string Encrypt(byte[] buffer)
        => BitConverter.ToString(SHA1.Create().ComputeHash(buffer)).Replace("-", "").ToUpper();
}
