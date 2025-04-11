using System.Security.Cryptography;
using System.Text;

namespace Mr1Ceng.Util.Cryptography;

/// <summary>
/// MD5加密工具类
/// </summary>
public class Md5Helper
{
    /// <summary>
    /// 将文本转换成MD5编码【全大写】
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string GetMD5(string data) => GetMD5(data, Encoding.UTF8);

    /// <summary>
    /// 将文本转换成MD5编码【全大写】
    /// </summary>
    /// <param name="data"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string GetMD5(string data, Encoding encoding)
    {
        var hashBytes = MD5.Create().ComputeHash(encoding.GetBytes(data));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
    }

    /// <summary>
    /// 将二进制转换成MD5编码【全大写】
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string GetMD5(byte[] data)
    {
        var hashBytes = MD5.Create().ComputeHash(data);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
    }

    /// <summary>
    /// 将Stream转换成MD5编码【全大写】
    /// </summary>
    /// <param name="data"></param>
    /// <returns>加密后的Sha1字符串</returns>
    public static string GetMD5(Stream data)
    {
        var hashBytes = MD5.Create().ComputeHash(data);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
    }
}
