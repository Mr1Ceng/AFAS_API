using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// 获取Buffer
/// </summary>
public class GetBuffer
{
    #region FromString

    /// <summary>
    /// 将String转换成Byte[]
    /// </summary>
    /// <param name="obj">Stream</param>
    /// <returns></returns>
    public static byte[] FromString(string? obj)
    {
        if (obj == null)
        {
            return Array.Empty<byte>();
        }

        return Encoding.UTF8.GetBytes(obj);
    }

    /// <summary>
    /// 将String转换成Byte[]
    /// </summary>
    /// <param name="obj">Stream</param>
    /// <param name="encoding">编码方式</param>
    /// <returns></returns>
    public static byte[] FromString(string? obj, Encoding encoding)
    {
        if (obj == null)
        {
            return Array.Empty<byte>();
        }

        return encoding.GetBytes(obj);
    }

    #endregion


    #region FromBase64

    /// <summary>
    /// 将Base64编码的文本转换成Byte[]
    /// </summary>
    /// <param name="obj">普通文本</param>
    /// <returns></returns>
    public static byte[] FromBase64(string? obj)
    {
        if (obj == null)
        {
            return Array.Empty<byte>();
        }
        return Convert.FromBase64String(obj);
    }

    #endregion


    #region FromImageBase64

    /// <summary>
    /// 将Base64编码的文本转换成MemoryStream
    /// </summary>
    /// <param name="base64"></param>
    /// <returns></returns>
    public static byte[] FromImageBase64(string? base64)
    {
        if (base64 == null)
        {
            return Array.Empty<byte>();
        }

        if (base64.Contains(','))
        {
            base64 = base64.Split(',')[1];
        }
        var dummyData = base64.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");

        return Convert.FromBase64String(dummyData);
    }

    #endregion


    #region FromHex

    /// <summary>
    /// 将指定的16进制字符串转换为byte数组
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static byte[] FromHex(string? obj)
    {
        if (obj == null)
        {
            return [];
        }

        var buffer = new byte[obj.Length / 2];
        for (var i = 0; i < obj.Length; i += 2)
        {
            buffer[i / 2] = Convert.ToByte(obj.Substring(i, 2), 16);
        }
        return buffer;
    }

    #endregion


    #region FromStream

    /// <summary>
    /// 将Stream转换成Byte[]
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static byte[] FromStream(Stream? stream)
    {
        if (stream == null)
        {
            return Array.Empty<byte>();
        }

        var streamLength = (int)stream.Length;
        var buffer = new byte[streamLength];
        var read = stream.Read(buffer, 0, streamLength);
        stream.Close();
        return buffer;
    }

    #endregion


    #region FromFilePath

    /// <summary>
    /// 将本地文件转换成Byte[]
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static byte[] FromFilePath(string? path) => FileHelper.ReadFileToBuffer(path);

    #endregion
}
