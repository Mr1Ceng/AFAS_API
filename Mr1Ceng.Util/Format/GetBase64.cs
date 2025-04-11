using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// 获取Base64
/// </summary>
public class GetBase64
{
    #region FromString

    /// <summary>
    /// 将普通文本转换成Base64编码的文本
    /// </summary>
    /// <param name="obj">普通文本</param>
    /// <returns></returns>
    public static string FromString(string? obj) => FromString(obj, Encoding.UTF8);

    /// <summary>
    /// 将普通文本转换成Base64编码的文本
    /// </summary>
    /// <param name="obj">普通文本</param>
    /// <param name="encoding">编码方式</param>
    /// <returns></returns>
    public static string FromString(string? obj, Encoding encoding)
        => obj != null ? Convert.ToBase64String(encoding.GetBytes(obj)) : "";

    #endregion


    #region FromHex

    /// <summary>
    /// 将指定的16进制字符串转换为Base64编码的文本
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string FromHex(string? obj) => FromBytes(GetBuffer.FromHex(obj));

    #endregion


    #region FromBytes

    /// <summary>
    /// 将Byte[]转换成Base64编码文本
    /// </summary>
    /// <param name="buffer">Byte[]</param>
    /// <returns></returns>
    public static string FromBytes(byte[]? buffer)
    {
        if (buffer == null)
        {
            return "";
        }

        var base64ArraySize = (int)Math.Ceiling(buffer.Length / 3d) * 4;
        var charBuffer = new char[base64ArraySize];
        Convert.ToBase64CharArray(buffer, 0, buffer.Length, charBuffer, 0);
        string s = new(charBuffer);
        return s;
    }

    #endregion


    #region FromStream

    /// <summary>
    /// 将Stream转换成Base64编码文本
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static string FromStream(Stream? stream) => FromBytes(GetBuffer.FromStream(stream));

    #endregion


    #region FromFilePath

    /// <summary>
    /// 将本地文件转换成Base64编码文本
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string FromFilePath(string path) => FileHelper.ReadFileToBase64(path);

    #endregion
}
