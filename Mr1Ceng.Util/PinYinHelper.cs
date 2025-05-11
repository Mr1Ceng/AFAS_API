using Microsoft.International.Converters.PinYinConverter;
namespace Mr1Ceng.Util;

/// <summary>
/// 拼音工具类
/// </summary>
public class PinYinHelper
{
    #region 中文转拼音

    /// <summary>
    /// 中文转拼音
    /// </summary>
    /// <param name="chinese">中文</param>
    /// <returns></returns>
    public static string GetPinYin(string chinese)
    {
        string result = "";
        foreach (char item in chinese)
        {
            try
            {
                ChineseChar cc = new ChineseChar(item);
                if (cc.Pinyins.Count > 0)
                {
                    string temp = cc.Pinyins[0].Remove(cc.Pinyins[0].Length -1);
                    result += temp.Substring(0, 1) + temp.Substring(1, temp.Length - 1).ToLower();
                }
            }
            catch
            {
                result += item; // 非汉字直接返回
            }
        }
        return result;
    }


    /// <summary>
    /// 中文转拼音
    /// </summary>
    /// <param name="chinese">中文</param>
    /// <returns></returns>
    public static string GetFirstPinYin(string chinese)
    {
        string result = "";
        foreach (char item in chinese)
        {
            try
            {
                ChineseChar cc = new ChineseChar(item);
                if (cc.Pinyins.Count > 0)
                {
                    string temp = cc.Pinyins[0].Remove(cc.Pinyins[0].Length - 1);
                    result += temp.Substring(0, 1);
                }
            }
            catch
            {
                result += item; // 非汉字直接返回
            }
        }
        return result;
    }

    #endregion
}
