using System.Reflection;
using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// 获取中国大陆格式的手机号码
/// </summary>
public class GetMobile
{
    /// <summary>
    /// 获取中国大陆格式的手机号码
    /// </summary>
    /// <param name="mobile"></param>
    /// <param name="throwException">转换失败是否抛异常</param>
    /// <returns></returns>
    private static string FromString(string? mobile, bool throwException)
    {
        var result = GetString.FromObject(mobile);
        result = result.Replace(" ", "");
        result = result.Replace("-", "");
        result = result.Replace("+", "");
        if (result.Length > 11)
        {
            result = result.Substring(result.Length - 11, 11);
        }
        if (result.Length != 11 || result[..1] != "1" || result[..2] == "10" || result[..2] == "11"
            || result[..2] == "12")
        {
            if (throwException)
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "仅支持中国大陆手机号码");
            }
            return "";
        }

        return result;
    }

    /// <summary>
    /// 加密手机号，如果加密失败，返回原字符串
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string TryEncrypt(string mobile)
    {
        var val = Encrypt(mobile);
        return val == "" ? mobile : val;
    }

    /// <summary>
    /// 加密手机号
    /// </summary>
    /// <param name="mobile"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    public static string Encrypt(string mobile, bool throwException = false)
    {
        //如果字符串长度为11为，并且以X开头，且最后四位为数字
        if (mobile.Length == 11 && mobile[0] == 'X' && mobile[7..].All(char.IsDigit))
        {
            return mobile;
        }

        if (string.IsNullOrEmpty(mobile) || mobile.Length != 11 || !mobile.All(char.IsDigit) || mobile[0] != '1')
        {
            if (throwException)
            {
                throw new ArgumentException("手机号必须是1开头的11位数字");
            }
            return "";
        }

        try
        {
            // 获取第4到7位作为加密输入
            var input = mobile.Substring(3, 4); // 第4-7位为input

            // 拼接key
            var key = GetMobileKey(mobile);

            // 使用 EncryptString 加密 input
            var newStr = EncryptString(input, key); // 获取加密后的字符串

            // 返回替换后的手机号
            return string.Concat("X", mobile.AsSpan(1, 2), newStr, mobile.AsSpan()[7..]);
        }
        catch (Exception)
        {
            if (throwException)
            {
                throw;
            }
        }

        return "";
    }

    /// <summary>
    /// 解密手机号
    /// </summary>
    /// <param name="data"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string Decrypt(string data, bool throwException = false)
    {
        if (FromString(data, false) != "")
        {
            return data;
        }

        if (data.StartsWith('1'))
        {
            data = string.Concat("X", data.AsSpan(1));
        }

        if (string.IsNullOrEmpty(data) || data.Length != 11 || data[0] != 'X')
        {
            if (throwException)
            {
                throw new ArgumentException("加密字符串必须是X开头的11位字符");
            }
            return "";
        }

        try
        {
            // 获取加密后的第4到7位
            var encryptedInput = data.Substring(3, 4);

            // 拼接key
            var key = GetMobileKey(data);

            // 使用 DecryptString 解密 encryptedInput
            var originalInput = DecryptString(encryptedInput, key); // 获取解密后的原始字符串

            // 返回还原后的手机号
            return string.Concat("1", data.AsSpan(1, 2), originalInput, data.AsSpan()[7..]);
        }
        catch (Exception)
        {
            if (throwException)
            {
                throw;
            }
        }

        return "";
    }

    /// <summary>
    /// 获取显示文本
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Display(string data) => data.Length == 11
        ? string.Concat("1", data.AsSpan(1, 2), "****", data.AsSpan()[7..]) // 假设手机号长度为11
        : data; // 如果手机号不合法，返回原始手机号


    #region 加解密字符串

    /// <summary>
    /// 加密四位数字为全大写或数字的四位字符串
    /// </summary>
    /// <param name="input"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private static string EncryptString(string input, string key)
    {
        if (input.Length != 4 || !input.All(char.IsDigit))
        {
            throw new ArgumentException("输入必须是4位数字字符串");
        }

        var k = key.Select(ch => ch - '0').ToArray(); // 将密钥转换为整数数组

        var encrypted = new StringBuilder(4);

        // 计算偏移量x
        var x = CalculateX(key);

        for (var i = 0; i < 4; i++)
        {
            // 根据新的加密规则进行偏移
            var shift = x - k[i] + k[7 - i];

            // 计算新的字符位置
            var newIndex = CHARSET.IndexOf(input[i]) + shift;

            encrypted.Append(CHARSET[newIndex]);
        }

        return encrypted.ToString();
    }

    /// <summary>
    /// 解密四位字符串为原始四位数字
    /// </summary>
    /// <param name="encrypted"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static string DecryptString(string encrypted, string key)
    {
        if (encrypted.Length != 4)
        {
            throw new ArgumentException("输入必须是4位合法加密字符串");
        }

        var k = key.Select(ch => ch - '0').ToArray(); // 将密钥转换为整数数组

        var decrypted = new StringBuilder(4);

        // 计算偏移量x
        var x = CalculateX(key);

        for (var i = 0; i < 4; i++)
        {
            // 当前字符
            var encChar = encrypted[i];

            // 根据新的解密规则进行反向偏移
            var shift = x - k[i] + k[7 - i];

            // 计算原始字符位置
            var currentIndex = CHARSET.IndexOf(encChar);
            var originalIndex = currentIndex - shift;

            decrypted.Append(CHARSET[originalIndex]);
        }

        return decrypted.ToString();
    }

    #endregion


    #region 私有方法和变量

    /// <summary>
    /// 目标字符集
    /// </summary>
    private const string CHARSET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// 计算偏移量x：将密钥转换为整数并求和 / 8 取余数 + 10
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private static int CalculateX(string key) => key.Sum(ch => ch - '0') % 8 + 10;

    /// <summary>
    /// 拼接成的8位key
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    private static string GetMobileKey(string mobile) => string.Join(string.Empty,
        GetInt.FromObject(mobile[1]) * GetInt.FromObject(mobile[10]),
        mobile[2],
        mobile[7],
        mobile[9],
        mobile[1],
        mobile[2],
        mobile[10],
        mobile[8]
    );

    #endregion
}
