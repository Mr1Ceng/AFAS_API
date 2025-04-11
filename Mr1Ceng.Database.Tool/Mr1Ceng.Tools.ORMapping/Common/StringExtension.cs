namespace Mr1Ceng.Tools.ORMapping.V7.Common;

/// <summary>
/// String扩展
/// </summary>
internal static class StringExtension
{
    /// <summary>
    /// 字符串首字母小写
    /// </summary>
    /// <param name="str">源字符串</param>
    /// <param name="length">截取长度</param>
    /// <returns></returns>
    public static string FirstCharToLower(this string str)
    {
        if (str != "")
        {
            return str[..1].ToLower() + str[1..];
        }
        return "";
    }
}
