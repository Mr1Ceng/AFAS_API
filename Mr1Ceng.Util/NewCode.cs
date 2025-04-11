using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// 创建新的ID
/// </summary>
public static class NewCode
{
    #region 公共属性

    /// <summary>
    /// 【HEX】36位全小写字符串
    /// </summary>
    public static string UUID => Guid.NewGuid().ToString().ToLower();

    /// <summary>
    /// 【HEX】26位全大写字符串
    /// </summary>
    public static string ULID
    {
        get
        {
            var ulid = Ulid.NewUlid().ToString();
            return ulid != null ? ulid.ToUpper() : "";
        }
    }

    /// <summary>
    /// 【UUID】32位大写字符串
    /// </summary>
    /// <returns></returns>
    public static string KeyId => Guid.NewGuid().ToString().Replace("-", "").ToUpper();

    /// <summary>
    /// 【ULID】26位大写字符串
    /// </summary>
    public static string UlidKey => ULID;

    /// <summary>
    /// 【ULID】25位大写字符串（113年内不会重复）
    /// </summary>
    public static string Ul25Key => ULID.Substring(1, 25);

    /// <summary>
    /// 【ULID】24位大写字符串（8.7年后可能重复）
    /// </summary>
    public static string Ul24Key => ULID.Substring(2, 24);

    /// <summary>
    /// 【HEX】4位小写字符串
    /// </summary>
    /// <returns></returns>
    public static string Mark => GetUpper(4);

    /// <summary>
    /// 【HEX】18位小写字符串
    /// </summary>
    /// <returns></returns>
    public static string Key => GetLower(18);

    /// <summary>
    /// 【HEX】32位小写字符串
    /// </summary>
    /// <returns></returns>
    public static string Secret => GetLower(32);

    /// <summary>
    /// 【HEX】32位小写字符串
    /// </summary>
    /// <returns></returns>
    public static string Token => GetLower(32);

    /// <summary>
    /// 【数字】获取6位数字验证码
    /// </summary>
    /// <returns></returns>
    public static string NumberToken => GetNumber(6);

    /// <summary>
    /// 获取12位含数字和大小写字母的随机字符
    /// </summary>
    /// <returns></returns>
    public static string Password => GetString(12);

    /// <summary>
    /// 获取4位长度的当前年份字符串【yyyy】
    /// </summary>
    /// <returns></returns>
    public static string CurrentYear => DateTime.Now.Year.ToString();

    /// <summary>
    /// 获取6位长度的当前月份字符串【yyyyMM】
    /// </summary>
    /// <returns></returns>
    public static string CurrentMonth => DateTime.Now.ToString("yyyyMM");

    /// <summary>
    /// 获取8位长度的当前日期字符串【yyyyMMdd】
    /// </summary>
    public static string CurrentDay => DateTime.Now.ToString("yyyyMMdd");

    /// <summary>
    /// 获取14位长度的当前时间字符串【yyyyMMddHHmmss】
    /// </summary>
    public static string CurrentTime => DateTime.Now.ToString("yyyyMMddHHmmss");

    #endregion

    #region 公共方法

    /// <summary>
    /// 【HEX】获取指定长度的小写字符串
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetLower(int length)
    {
        var str = "";
        while (str.Length < length)
        {
            str += UUID.Replace("-", "");
        }
        return str[..length];
    }

    /// <summary>
    /// 【HEX】获取指定长度的大写字符串
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetUpper(int length) => GetLower(length).ToUpper();

    /// <summary>
    /// 【数字】获取指定长度的数字
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetNumber(int length)
    {
        var str = "";
        while (str.Length < length + 3)
        {
            str += UUID.Replace("-", "").Replace("a", "").Replace("b", "").Replace("c", "").Replace("d", "")
                .Replace("e", "").Replace("f", "");
        }
        return str.Substring(3, length);
    }

    #endregion

    #region GetString

    /// <summary>
    /// 获取随机字符串
    /// </summary>
    /// <param name="length"></param>
    /// <param name="allowNumber"></param>
    /// <param name="allowUpper"></param>
    /// <param name="allowLower"></param>
    /// <returns></returns>
    public static string GetString(int length, bool allowNumber = true, bool allowUpper = true, bool allowLower = true)
        => GetString(length, allowNumber, allowUpper, allowLower, false, false);

    /// <summary>
    /// 获取16进制随机字符串
    /// </summary>
    /// <param name="length"></param>
    /// <param name="allowNumber"></param>
    /// <param name="allowUpper"></param>
    /// <param name="allowLower"></param>
    /// <returns></returns>
    public static string GetHexString(int length,
        bool allowNumber = true,
        bool allowUpper = true,
        bool allowLower = true)
        => GetString(length, allowNumber, allowUpper, allowLower, true, false);

    /// <summary>
    /// 获取含特殊符号的随机字符串
    /// </summary>
    /// <param name="length"></param>
    /// <param name="allowNumber"></param>
    /// <param name="allowUpper"></param>
    /// <param name="allowLower"></param>
    /// <returns></returns>
    public static string GetCharString(int length,
        bool allowNumber = true,
        bool allowUpper = true,
        bool allowLower = true)
        => GetString(length, allowNumber, allowUpper, allowLower, false, true);

    /// <summary>
    /// 获取字符串
    /// </summary>
    /// <param name="length"></param>
    /// <param name="allowNumber"></param>
    /// <param name="allowUpper"></param>
    /// <param name="allowLower"></param>
    /// <param name="onlyHex"></param>
    /// <param name="allowChar"></param>
    /// <returns></returns>
    private static string GetString(int length,
        bool allowNumber,
        bool allowUpper,
        bool allowLower,
        bool onlyHex,
        bool allowChar)
    {
        var hasNumber = false;
        var hasUpper = false;
        var hasLower = false;
        var hasChar = false;

        StringBuilder sb = new();

        while (sb.Length < length)
        {
            Random random = new(Guid.NewGuid().GetHashCode());
            var num = random.Next(0, 123);

            #region 数据过滤

            if (!allowNumber)
            {
                if (num >= 48 && num <= 57)
                {
                    continue; //禁止数字
                }
            }

            if (!allowUpper)
            {
                if (num >= 65 && num <= 90)
                {
                    continue; //禁止大写
                }
            }

            if (!allowLower)
            {
                if (num >= 97 && num <= 122)
                {
                    continue; //禁止小写
                }
            }

            if (onlyHex)
            {
                if (num >= 48 && num <= 57)
                {
                    //允许数字
                }
                else if (num >= 65 && num <= 70)
                {
                    //允许A-F
                }
                else if (num >= 97 && num <= 102)
                {
                    //允许a-f
                }
                else
                {
                    continue; //禁止其他字符
                }
            }

            #endregion

            #region 字符串拼接

            switch (num)
            {
                case >= 48 and <= 57:
                {
                    var s = Convert.ToChar(num).ToString();
                    sb.Append(s);
                    hasNumber = true;
                    break;
                }

                case >= 65 and <= 90:
                {
                    var s = Convert.ToChar(num).ToString();
                    sb.Append(s);
                    hasUpper = true;
                    break;
                }

                case >= 97 and <= 122:
                {
                    var s = Convert.ToChar(num).ToString();
                    sb.Append(s);
                    hasLower = true;
                    break;
                }

                default:
                {
                    if (allowChar)
                    {
                        if (!hasChar)
                        {
                            if (num is 33 or 61 or 46 or 63)
                            {
                                var s = Convert.ToChar(num).ToString();
                                sb.Append(s);
                                hasChar = true;
                            }
                        }
                    }
                    break;
                }
            }

            #endregion
        }

        if (allowNumber && !hasNumber)
        {
            return GetString(length, allowNumber, allowUpper, allowLower, onlyHex, allowChar);
        }

        if (allowUpper && !hasUpper)
        {
            return GetString(length, allowNumber, allowUpper, allowLower, onlyHex, allowChar);
        }

        if (allowLower && !hasLower)
        {
            return GetString(length, allowNumber, allowUpper, allowLower, onlyHex, allowChar);
        }

        if (allowChar && !hasChar)
        {
            return GetString(length, allowNumber, allowUpper, allowLower, onlyHex, allowChar);
        }

        return sb.ToString();
    }

    #endregion

    #region IdKey

    /// <summary>
    /// 基于GUID生成16位全大写数字字母字符串
    /// </summary>
    /// <returns></returns>
    public static string GetIdKey() => GetIdKey(KeyId);

    /// <summary>
    /// 基于GUID生成16位全大写数字字母字符串
    /// </summary>
    /// <param name="key">32位大写字符串</param>
    /// <returns></returns>
    public static string GetIdKey(string key)
    {
        if (key.Length != 32)
        {
            return "";
        }

        var array = key.ToUpper().ToCharArray();

        var sb = new StringBuilder();
        for (var i = 0; i < 16; i++)
        {
            int first = array[i];
            int last = array[i + 16];

            first = first switch
            {
                >= 48 and <= 57 => first - 48,
                >= 65 and <= 70 => first - 55,
                _ => 16
            };

            last = last switch
            {
                >= 48 and <= 57 => last - 48,
                >= 65 and <= 70 => last - 55,
                _ => 16
            };

            var x = first + last;
            if (x < 10)
            {
                sb.Append((char)(x + 48));
            }
            else
            {
                sb.Append((char)(x + 55));
            }
        }
        return sb.ToString();
    }

    #endregion
}
