using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// 获取Stream
/// </summary>
public class GetStream
{
    #region FromString

    /// <summary>
    /// 将普通文本转换成MemoryStream
    /// </summary>
    /// <param name="obj">普通文本</param>
    /// <returns></returns>
    public static MemoryStream FromString(string? obj) => FromString(obj, Encoding.UTF8);

    /// <summary>
    /// 将普通文本转换成MemoryStream
    /// </summary>
    /// <param name="obj">普通文本</param>
    /// <param name="encoding">编码方式</param>
    /// <returns></returns>
    public static MemoryStream FromString(string? obj, Encoding encoding)
    {
        if (obj == null)
        {
            return new MemoryStream(Array.Empty<byte>());
        }
        return new MemoryStream(encoding.GetBytes(obj));
    }

    #endregion


    #region FromBytes

    /// <summary>
    /// 将Byte[]转换成MemoryStream
    /// </summary>
    /// <param name="buffer">Byte[]</param>
    /// <returns></returns>
    public static MemoryStream FromBytes(byte[]? buffer)
    {
        buffer ??= [];
        return new MemoryStream(buffer);
    }

    #endregion


    #region FromBase64

    /// <summary>
    /// 将Base64编码的文本转换成MemoryStream
    /// </summary>
    /// <param name="base64"></param>
    /// <param name="trimHead">是否要剔除文件头，默认不剔除</param>
    /// <returns></returns>
    public static MemoryStream FromBase64(string? base64, bool trimHead = false)
    {
        if (base64 == null)
        {
            return new MemoryStream([]);
        }

        if (trimHead)
        {
            var strs = base64.Split(',');
            if (strs.Length == 2)
            {
                base64 = strs[1];
            }
        }
        return new MemoryStream(GetBuffer.FromBase64(base64));
    }

    #endregion


    #region FromImageBase64

    /// <summary>
    /// 将Base64编码的文本转换成MemoryStream
    /// </summary>
    /// <param name="base64"></param>
    /// <returns></returns>
    public static MemoryStream FromImageBase64(string? base64) => new(GetBuffer.FromImageBase64(base64));

    #endregion
}
