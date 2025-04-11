namespace Mr1Ceng.Util;

/// <summary>
/// 转换1970年1月1日以来的秒数
/// </summary>
public class UnixTimeHelper
{
    #region GetDateTime

    /// <summary>
    /// 将1970年1月1日以来的毫秒数转换为DateTime
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(string milliseconds) => GetDateTime(GetLong.FromObject(milliseconds));

    /// <summary>
    /// 将1970年1月1日以来的毫秒数转换为DateTime
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(long milliseconds)
        => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliseconds).ToLocalTime();

    /// <summary>
    /// 将1970年1月1日以来的秒数转换为DateTime
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static DateTime GetDateTimeFromSeconds(string seconds)
        => GetDateTimeFromSeconds(GetLong.FromObject(seconds));

    /// <summary>
    /// 将1970年1月1日以来的秒数转换为DateTime
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static DateTime GetDateTimeFromSeconds(long seconds)
        => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds).ToLocalTime();

    #endregion


    #region GetDateString

    /// <summary>
    /// 将1970年1月1日以来的毫秒数转换为 ""yyyy-MM-dd HH:mm:ss" 格式 string
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static string GetDateString(string milliseconds) => GetDateString(GetLong.FromObject(milliseconds));

    /// <summary>
    /// 将1970年1月1日以来的毫秒数转换为 ""yyyy-MM-dd HH:mm:ss" 格式 string
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static string GetDateString(long milliseconds) => DateHelper.GetDateString(GetDateTime(milliseconds));

    /// <summary>
    /// 将1970年1月1日以来的秒数转换为 ""yyyy-MM-dd HH:mm:ss" 格式 string
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string GetDateStringFromSeconds(string seconds)
        => GetDateStringFromSeconds(GetLong.FromObject(seconds));

    /// <summary>
    /// 将1970年1月1日以来的秒数转换为 ""yyyy-MM-dd HH:mm:ss" 格式 string
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string GetDateStringFromSeconds(long seconds)
        => DateHelper.GetDateString(GetDateTimeFromSeconds(seconds));

    #endregion


    #region GetDayString

    /// <summary>
    /// 将1970年1月1日以来的毫秒数转换为 "yyyy-MM-dd" 格式 string
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static string GetDayString(string milliseconds) => GetDayString(GetLong.FromObject(milliseconds));

    /// <summary>
    /// 将1970年1月1日以来的毫秒数转换为 "yyyy-MM-dd" 格式 string
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static string GetDayString(long milliseconds) => DateHelper.GetDayString(GetDateTime(milliseconds));

    /// <summary>
    /// 将1970年1月1日以来的秒数转换为 "yyyy-MM-dd" 格式 string
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string GetDayStringFromSeconds(string seconds)
        => GetDayStringFromSeconds(GetLong.FromObject(seconds));

    /// <summary>
    /// 将1970年1月1日以来的秒数转换为 "yyyy-MM-dd" 格式 string
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string GetDayStringFromSeconds(long seconds)
        => DateHelper.GetDayString(GetDateTimeFromSeconds(seconds));

    #endregion


    #region GetUnixSeconds

    /// <summary>
    /// 获取当前日期到1970年1月1日之间的秒数
    /// </summary>
    /// <returns></returns>
    public static int GetUnixSeconds() => GetUnixSeconds(DateTime.Now);

    /// <summary>
    /// 获取指定日期到1970年1月1日之间的秒数
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static int GetUnixSeconds(string data) => GetUnixSeconds(DateHelper.GetDateTime(data));

    /// <summary>
    /// 获取指定日期到1970年1月1日之间的秒数
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static int GetUnixSeconds(DateTime date)
    {
        var ts = date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return (int)ts.TotalSeconds; // 相差秒数
    }

    #endregion


    #region GetUnixMilliseconds

    /// <summary>
    /// 获取当前日期到1970年1月1日之间的毫秒数
    /// </summary>
    /// <returns></returns>
    public static long GetUnixMilliseconds() => GetUnixMilliseconds(DateTime.Now);

    /// <summary>
    /// 获取指定日期到1970年1月1日之间的毫秒数
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static long GetUnixMilliseconds(string data) => GetUnixMilliseconds(DateHelper.GetDateTime(data));

    /// <summary>
    /// 获取指定日期到1970年1月1日之间的毫秒数
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static long GetUnixMilliseconds(DateTime data)
    {
        var ts = data.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return (long)ts.TotalMilliseconds; // 相差毫秒数
    }

    #endregion
}
