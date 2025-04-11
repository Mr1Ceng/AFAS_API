using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// 格式化字符串
/// </summary>
public class GetString
{
    #region FromObject

    /// <summary>
    /// 将类型转换为string
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="maxLength">字符串最大长度,0则不做限制</param>
    /// <returns></returns>
    public static string FromObject(object? obj, int maxLength) => FromObject(obj, "", maxLength);

    /// <summary>
    /// 将类型转换为string
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">转换失败后的默认值</param>
    /// <param name="maxLength">字符串最大长度,0则不做限制</param>
    /// <returns></returns>
    public static string FromObject(object? obj, string defaultValue = "", int maxLength = 0)
    {
        if (obj == null)
        {
            return defaultValue;
        }

        try
        {
            var returnStr = Convert.ToString(obj);
            if (returnStr == null)
            {
                return defaultValue;
            }

            returnStr = returnStr.Replace("\r\n", " ").Trim();
            if (maxLength > 0 && returnStr.Length > maxLength)
            {
                return returnStr[..maxLength];
            }
            return returnStr;
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion


    #region FromBase64

    /// <summary>
    /// 将Base64编码的文本转换成普通文本
    /// </summary>
    /// <param name="obj">Base64编码的文本</param>
    /// <returns></returns>
    public static string FromBase64(string? obj) => FromBase64(obj, Encoding.UTF8);

    /// <summary>
    /// 将Base64编码的文本转换成普通文本
    /// </summary>
    /// <param name="obj">普通文本</param>
    /// <param name="encoding">编码方式</param>
    /// <returns></returns>
    public static string FromBase64(string? obj, Encoding encoding)
    {
        if (obj == null)
        {
            return "";
        }
        return encoding.GetString(Convert.FromBase64String(obj));
    }

    #endregion


    #region FromStream

    /// <summary>
    /// 将Stream转换成普通文本
    /// </summary>
    /// <param name="obj">Stream</param>
    /// <returns></returns>
    public static string FromStream(Stream? obj) => FromBase64(GetBase64.FromStream(obj), Encoding.UTF8);

    /// <summary>
    /// 将Stream转换成普通文本
    /// </summary>
    /// <param name="obj">Stream</param>
    /// <param name="encoding">编码方式</param>
    /// <returns></returns>
    public static string FromStream(Stream? obj, Encoding encoding) => FromBase64(GetBase64.FromStream(obj), encoding);

    #endregion


    #region FromBytes

    /// <summary>
    /// 将二进制数组转换成普通文本
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static string FromBytes(byte[]? buffer) => FromBytes(buffer, Encoding.UTF8);

    /// <summary>
    /// 将二进制数组转换成普通文本
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string FromBytes(byte[]? buffer, Encoding encoding)
    {
        if (buffer == null)
        {
            return "";
        }
        return encoding.GetString(buffer);
    }

    #endregion


    #region FromDateTime

    /// <summary>
    /// 将 当前日期 转换为指定格式 string
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string FromCurrentTime(string format) => FromDateTime(DateTime.Now, format);

    /// <summary>
    /// 将 DateTimeString 转换为指定格式 string
    /// </summary>
    /// <param name="dateString"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string FromDateTime(string dateString, string format)
        => FromDateTime(DateHelper.GetDateTime(dateString), format);

    /// <summary>
    /// 将 DateTime 转换为指定格式 string
    /// </summary>
    /// <param name="date"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string FromDateTime(DateTime date, string format) => date.ToString(format);

    #endregion


    /// <summary>
    /// 字符串根据特殊符号分词
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static IEnumerable<string> SplitList(object? obj, char[]? separator = null)
    {
        var text = FromObject(obj);

        var result = new List<string>();
        if (separator == null)
        {
            foreach (var item in text.Split(',', ';', '|', ' ', '、', '；', '，', '\''))
            {
                var val = item.Trim();
                if (!string.IsNullOrWhiteSpace(val))
                {
                    result.Add(val);
                }
            }
        }
        else
        {
            foreach (var item in text.Split(separator))
            {
                var val = item.Trim();
                if (!string.IsNullOrWhiteSpace(val))
                {
                    result.Add(val);
                }
            }
        }
        return result;
    }
}
