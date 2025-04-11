using System.Globalization;

namespace Mr1Ceng.Util;

/// <summary>
/// 日期转换工具类
/// </summary>
public class DateHelper
{
    #region 年度

    #region GetYearString

    /// <summary>
    /// 将 当前日期 转换为 ""yyyy" 格式 string
    /// </summary>
    /// <returns></returns>
    public static string GetYearString() => GetYearString(DateTime.Now);

    /// <summary>
    /// 将 DateTime 转换为 ""yyyy" 格式 string
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string GetYearString(DateTime date) => date.ToString("yyyy");

    /// <summary>
    /// 获取yyyy格式的年度
    /// </summary>
    /// <param name="data">yyyy-MM-dd 或 yyyyMM</param>
    /// <returns></returns>
    public static string GetYearString(string data) => data[..4];

    #endregion

    #endregion


    #region 月度

    #region GetMonthString

    /// <summary>
    /// 将 当前日期 转换为 ""yyyyMM" 格式 string
    /// </summary>
    /// <returns></returns>
    public static string GetMonthString() => GetMonthString(DateTime.Now);

    /// <summary>
    /// 将 DateTimeString 转换为 ""yyyyMM" 格式 string
    /// </summary>
    /// <param name="dateString"></param>
    /// <returns></returns>
    public static string GetMonthString(string dateString)
    {
        var date = GetDateTime(dateString);
        return GetMonthString(date);
    }

    /// <summary>
    /// 将 DateTime 转换为 ""yyyyMM" 格式 string
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string GetMonthString(DateTime date) => date.ToString("yyyyMM");

    #endregion

    #region GetMonthFirstDay、GetMonthLastDay

    /// <summary>
    /// 获取月份的第一天
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    public static DateTime GetMonthFirstDay(DateTime day) => new(day.Year, day.Month, 1);

    /// <summary>
    /// 获取月份的最后一天
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    public static DateTime GetMonthLastDay(DateTime day) => GetMonthFirstDay(day).AddMonths(1).AddDays(-1);

    /// <summary>
    /// 获取月份的第一天
    /// </summary>
    /// <param name="month"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static DateTime GetMonthFirstDay(string month, bool throwException = false)
    {
        try
        {
            if (month.Length == 6)
            {
                var yearIndex = int.Parse(month[..4]);
                var monthIndex = int.Parse(month.Substring(4, 2));
                return new DateTime(yearIndex, monthIndex, 1);
            }
            throw new Exception();
        }
        catch
        {
            if (throwException)
            {
                throw;
            }
            return GetMonthFirstDay();
        }
    }

    /// <summary>
    /// 获取月份的最后一天
    /// </summary>
    /// <param name="month"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    public static DateTime GetMonthLastDay(string month, bool throwException = false)
        => GetMonthFirstDay(month, throwException).AddMonths(1).AddDays(-1);

    /// <summary>
    /// 获取当前月份的第一天
    /// </summary>
    /// <returns></returns>
    public static DateTime GetMonthFirstDay()
    {
        var today = DateTime.Now;
        return new DateTime(today.Year, today.Month, 1);
    }

    /// <summary>
    /// 获取当前月份的最后一天
    /// </summary>
    /// <returns></returns>
    public static DateTime GetMonthLastDay() => GetMonthFirstDay().AddMonths(1).AddDays(-1);

    #endregion

    /// <summary>
    /// 获取月份匹配前缀
    /// </summary>
    /// <param name="month">yyyy-MM</param>
    /// <returns></returns>
    public static string GetMonthPrefix(string month)
    {
        try
        {
            if (month.Length == 6)
            {
                return string.Concat(month.AsSpan(0, 4), "-", month.AsSpan(4, 2));
            }
            throw new Exception();
        }
        catch
        {
            return DateTime.Now.ToString("yyyy-MM");
        }
    }

    /// <summary>
    /// 获取后一个月
    /// </summary>
    /// <param name="month">当前月</param>
    /// <param name="count">后面的月数（1为后一个月，2为后两个月，-1为前一个月）</param>
    /// <returns></returns>
    public static string GetNextMonth(string month, int count)
    {
        try
        {
            if (month.Length == 6)
            {
                var yearIndex = int.Parse(month[..4]);
                var monthIndex = int.Parse(month.Substring(4, 2));
                return GetMonthString(new DateTime(yearIndex, monthIndex, 1).AddMonths(count));
            }
            throw new Exception();
        }
        catch
        {
            var today = DateTime.Now;
            return GetMonthString(new DateTime(today.Year, today.Month, 1));
        }
    }

    #endregion


    #region 季度

    #region GetQuarterNumber

    /// <summary>
    /// 获取 当前日期 的季度
    /// </summary>
    /// <returns></returns>
    public static int GetQuarterNumber() => GetQuarterNumber(DateTime.Now);

    /// <summary>
    /// 获取 DateTimeString 的季度
    /// </summary>
    /// <param name="data">yyyy-MM-dd 或 yyyyMM</param>
    /// <returns></returns>
    public static int GetQuarterNumber(string data)
    {
        if (data.Length == 10)
        {
            var date = GetDateTime(data);
            return GetQuarterNumber(date);
        }
        if (data.Length == 6)
        {
            var month = data.Substring(4, 2);
            return month switch
            {
                "01" or "02" or "03" => 1,
                "04" or "05" or "06" => 2,
                "07" or "08" or "09" => 3,
                "10" or "11" or "12" => 4,
                _ => 0
            };
        }
        return 0;
    }

    /// <summary>
    /// 获取 DateTime 的季度
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static int GetQuarterNumber(DateTime date)
    {
        var month = date.ToString("MM");
        return month switch
        {
            "01" or "02" or "03" => 1,
            "04" or "05" or "06" => 2,
            "07" or "08" or "09" => 3,
            "10" or "11" or "12" => 4,
            _ => 0
        };
    }

    #endregion


    #region GetYearQuarterString

    /// <summary>
    /// 获取 当前日期 的季度（yyyyQ1）
    /// </summary>
    /// <returns></returns>
    public static string GetYearQuarterString() => GetYearQuarterString(DateTime.Now);

    /// <summary>
    /// 获取 monthString 的季度（yyyyQ1）
    /// </summary>
    /// <param name="monthString"></param>
    /// <returns></returns>
    public static string GetYearQuarterString(string monthString)
    {
        if (monthString == "")
        {
            return GetYearQuarterString(DateTime.Now);
        }

        var year = monthString[..4];
        var month = monthString.Substring(4, 2);
        switch (month)
        {
            case "01":
            case "02":
            case "03":
                return year + "Q" + 1;

            case "04":
            case "05":
            case "06":
                return year + "Q" + 2;

            case "07":
            case "08":
            case "09":
                return year + "Q" + 3;

            case "10":
            case "11":
            case "12":
                return year + "Q" + 4;
        }
        return "";
    }

    /// <summary>
    /// 获取 DateTime 的季度（yyyyQ1）
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string GetYearQuarterString(DateTime date)
    {
        var year = date.ToString("yyyy");
        var month = date.ToString("MM");

        switch (month)
        {
            case "01":
            case "02":
            case "03":
                return year + "Q" + 1;

            case "04":
            case "05":
            case "06":
                return year + "Q" + 2;

            case "07":
            case "08":
            case "09":
                return year + "Q" + 3;

            case "10":
            case "11":
            case "12":
                return year + "Q" + 4;
        }

        return "";
    }

    #endregion


    /// <summary>
    /// 获取季度的第一天
    /// </summary>
    /// <param name="quarter">2018Q1</param>
    /// <returns></returns>
    public static DateTime GetQuarterFirstday(string quarter)
    {
        if (quarter == "")
        {
            quarter = GetYearQuarterString();
        }

        var yearIndex = int.Parse(quarter[..4]);
        var quarterIndex = int.Parse(quarter.Substring(5, 1));
        return quarterIndex switch
        {
            4 => new DateTime(yearIndex, 10, 1),
            3 => new DateTime(yearIndex, 7, 1),
            2 => new DateTime(yearIndex, 4, 1),
            _ => new DateTime(yearIndex, 1, 1)
        };
    }

    /// <summary>
    /// 获取当前季度的月份
    /// </summary>
    /// <param name="yearQuarter"></param>
    /// <returns></returns>
    public static string[] GetQuarterMonths(string yearQuarter)
    {
        var yearIndex = int.Parse(yearQuarter[..4]);
        var quarterIndex = int.Parse(yearQuarter.Substring(5, 1));

        switch (quarterIndex)
        {
            case 1:
                return [yearIndex + "01", yearIndex + "02", yearIndex + "03"];
            case 2:
                return [yearIndex + "04", yearIndex + "05", yearIndex + "06"];
            case 3:
                return [yearIndex + "07", yearIndex + "08", yearIndex + "09"];
            case 4:
                return [yearIndex + "10", yearIndex + "11", yearIndex + "12"];
        }
        return Array.Empty<string>();
    }

    /// <summary>
    /// 获取季度的最后一个月
    /// </summary>
    /// <param name="year"></param>
    /// <param name="quarter"></param>
    /// <returns>yyyyMM</returns>
    public static string GetQuarterLastMonth(int year, int quarter)
    {
        return quarter switch
        {
            1 => year + "03",
            2 => year + "06",
            3 => year + "09",
            4 => year + "12",
            _ => ""
        };
    }

    #endregion


    #region 周

    /// <summary>
    /// 获取当前日期所在周的全部日期
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime[] GetWeekDays(DateTime date)
    {
        // int dateIndex = (int)date.DayOfWeek;
        var dateIndex = (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        DateTime[] weekdays =
        [
            date.AddDays(1 - dateIndex), date.AddDays(2 - dateIndex), date.AddDays(3 - dateIndex), date.AddDays(4 - dateIndex),
            date.AddDays(5 - dateIndex), date.AddDays(6 - dateIndex), date.AddDays(7 - dateIndex)
        ];
        return weekdays;
    }

    /// <summary>
    /// 获取当前日期是第几周（周一为第一周）
    /// </summary>
    /// <returns></returns>
    public static int GetWeekOfYear() => GetWeekOfYear(DateTime.Now);

    /// <summary>
    /// 获取当前日期是第几周（周一为第一周）
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static int GetWeekOfYear(DateTime date)
    {
        var sunday = GetSunday(date);
        if (sunday.Year == date.Year)
        {
            var gap = sunday.DayOfYear % 7 == 0 ? 0 : 1;
            return sunday.DayOfYear / 7 + gap;
        }
        else
        {
            var lastSunday = sunday.AddDays(-7);
            var gap = lastSunday.DayOfYear % 7 == 0 ? 0 : 1;
            return lastSunday.DayOfYear / 7 + gap + 1;
        }
    }

    #endregion


    #region 星期几

    /// <summary>
    /// 获取日期所在的周一
    /// </summary>
    /// <returns></returns>
    public static DateTime GetMonday(DateTime date)
    {
        var dateIndex = (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        return date.AddDays(1 - dateIndex);
    }

    /// <summary>
    /// 获取日期所在的周二
    /// </summary>
    /// <returns></returns>
    public static DateTime GetTuesday(DateTime date)
    {
        var dateIndex = (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        return date.AddDays(2 - dateIndex);
    }

    /// <summary>
    /// 获取日期所在的周三
    /// </summary>
    /// <returns></returns>
    public static DateTime GetWednesday(DateTime date)
    {
        var dateIndex = (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        return date.AddDays(3 - dateIndex);
    }

    /// <summary>
    /// 获取日期所在的周四
    /// </summary>
    /// <returns></returns>
    public static DateTime GetThursday(DateTime date)
    {
        var dateIndex = (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        return date.AddDays(4 - dateIndex);
    }

    /// <summary>
    /// 获取日期所在的周五
    /// </summary>
    /// <returns></returns>
    public static DateTime GetFriday(DateTime date)
    {
        var dateIndex = (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        return date.AddDays(5 - dateIndex);
    }

    /// <summary>
    /// 获取日期所在的周六
    /// </summary>
    /// <returns></returns>
    public static DateTime GetSaturday(DateTime date)
    {
        var dateIndex = (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        return date.AddDays(6 - dateIndex);
    }

    /// <summary>
    /// 获取日期所在的周日
    /// </summary>
    /// <returns></returns>
    public static DateTime GetSunday(DateTime date)
    {
        var dateIndex = (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        return date.AddDays(7 - dateIndex);
    }

    #endregion


    #region 日

    #region GetDayString

    /// <summary>
    /// 将 当前日期 转换为 "yyyy-MM-dd" 格式 string
    /// </summary>
    /// <returns></returns>
    public static string GetDayString() => GetDayString(DateTime.Now);

    /// <summary>
    /// 将 DateTime 转换为 "yyyy-MM-dd" 格式 string
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string GetDayString(DateTime date) => TimeZoneInfo
        .ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"))
        .ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

    #endregion

    #region GetDateString

    /// <summary>
    /// 将 当前日期 转换为 "yyyyMMddHHmmss" 格式 string
    /// </summary>
    /// <returns></returns>
    public static string GetDateStringMark1() => GetDateString(DateTime.Now, "yyyyMMddHHmmss");

    /// <summary>
    /// 将 当前日期 转换为 "yyyyMMdd_HHmmss" 格式 string
    /// </summary>
    /// <returns></returns>
    public static string GetDateStringMark2() => GetDateString(DateTime.Now, "yyyyMMdd_HHmmss");

    /// <summary>
    /// 将 当前日期 转换为 ""yyyy-MM-dd HH:mm:ss" 格式 string
    /// </summary>
    /// <returns></returns>
    public static string GetDateString() => GetDateString(DateTime.Now);

    /// <summary>
    /// 将 当前日期 转换为指定格式
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string GetDateString(string format) => GetDateString(DateTime.Now, format);

    /// <summary>
    /// 将 DateTime 转换为 ""yyyy-MM-dd HH:mm:ss" 格式 string
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string GetDateString(DateTime date) => GetDateString(date, "yyyy-MM-dd HH:mm:ss");

    /// <summary>
    /// 将 DateTime 转换为指定格式
    /// </summary>
    /// <param name="date"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string GetDateString(DateTime date, string format) => TimeZoneInfo
        .ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"))
        .ToString(format, DateTimeFormatInfo.InvariantInfo);

    #endregion

    #region GetDateTime

    /// <summary>
    /// 将 当前日期
    /// </summary>
    /// <returns></returns>
    public static DateTime GetDateTime() => DateTime.Now;

    /// <summary>
    /// 将类型转换为DateTime
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>转换失败默认返回当前时间</returns>
    public static DateTime GetDateTime(object obj) => GetDateTime(obj, DateTime.Now);

    /// <summary>
    /// 将类型转换为DateTime
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">转换失败后的默认值</param>
    /// <returns></returns>
    public static DateTime GetDateTime(object obj, DateTime defaultValue)
        => DateTime.TryParse(obj.ToString(), out var date) ? date : defaultValue;

    #endregion

    #endregion


    #region 时间间隔

    #region GetDurationString

    /// <summary>
    /// 获取持续时长的文本显示
    /// </summary>
    /// <param name="beginTime"></param>
    /// <returns></returns>
    public static string GetDurationString(string beginTime) => GetDurationString(GetDateTime(beginTime));

    /// <summary>
    /// 获取持续时长的文本显示
    /// </summary>
    /// <param name="beginTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static string GetDurationString(string beginTime, string endTime)
        => GetDurationString(GetDateTime(beginTime), GetDateTime(endTime));

    /// <summary>
    /// 获取持续时长的文本显示
    /// </summary>
    /// <param name="beginTime"></param>
    /// <returns></returns>
    public static string GetDurationString(DateTime beginTime) => GetDurationString(beginTime, DateTime.Now);

    /// <summary>
    /// 获取持续时长的文本显示
    /// </summary>
    /// <param name="beginTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static string GetDurationString(DateTime beginTime, DateTime endTime)
    {
        var span = endTime - beginTime;
        var totalHours = GetInt.FromObject(span.TotalHours);

        var strSpan = "";
        strSpan += totalHours > 0 ? totalHours + "小时" : "";
        strSpan += span.Minutes > 0 ? span.Minutes + "分钟" : "";
        strSpan += span.Seconds > 0 ? span.Seconds + "秒" : "";
        strSpan += span.Milliseconds > 0 ? span.Milliseconds + "毫秒" : "";
        strSpan += strSpan == "" ? "0秒" : "";
        return strSpan;
    }

    /// <summary>
    /// 获取持续时长的文本显示【根据秒数转换】
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string GetDurationString(int seconds)
    {
        TimeSpan ts = new(0, 0, seconds);
        var totalHours = GetInt.FromObject(ts.TotalHours);
        if (totalHours > 0)
        {
            return totalHours + "小时" + ts.Minutes + "分" + ts.Seconds + "秒";
        }
        if (ts.Minutes > 0)
        {
            return ts.Minutes + "分" + ts.Seconds + "秒";
        }
        return seconds + "秒";
    }

    #endregion

    #region GetDurationSeconds

    /// <summary>
    /// 获取持续时长的秒数
    /// </summary>
    /// <param name="beginTime"></param>
    /// <returns></returns>
    public static int GetDurationSeconds(string beginTime) => GetDurationSeconds(GetDateTime(beginTime));

    /// <summary>
    /// 获取持续时长的秒数
    /// </summary>
    /// <param name="beginTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static int GetDurationSeconds(string beginTime, string endTime)
        => GetDurationSeconds(GetDateTime(beginTime), GetDateTime(endTime));

    /// <summary>
    /// 获取持续时长的秒数
    /// </summary>
    /// <param name="beginTime"></param>
    /// <returns></returns>
    public static int GetDurationSeconds(DateTime beginTime) => GetDurationSeconds(beginTime, DateTime.Now);

    /// <summary>
    /// 获取持续时长的秒数
    /// </summary>
    /// <param name="beginTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static int GetDurationSeconds(DateTime beginTime, DateTime endTime)
    {
        var span = endTime - beginTime;
        return GetInt.FromObject(span.TotalSeconds);
    }

    #endregion

    #region GetDurationMilliseconds

    /// <summary>
    /// 获取持续时长的毫秒数
    /// </summary>
    /// <param name="beginTime"></param>
    /// <returns></returns>
    public static int GetDurationMilliseconds(string beginTime) => GetDurationMilliseconds(GetDateTime(beginTime));

    /// <summary>
    /// 获取持续时长的毫秒数
    /// </summary>
    /// <param name="beginTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static int GetDurationMilliseconds(string beginTime, string endTime)
        => GetDurationMilliseconds(GetDateTime(beginTime), GetDateTime(endTime));

    /// <summary>
    /// 获取持续时长的毫秒数
    /// </summary>
    /// <param name="beginTime"></param>
    /// <returns></returns>
    public static int GetDurationMilliseconds(DateTime beginTime) => GetDurationMilliseconds(beginTime, DateTime.Now);

    /// <summary>
    /// 获取持续时长的毫秒数
    /// </summary>
    /// <param name="beginTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static int GetDurationMilliseconds(DateTime beginTime, DateTime endTime)
    {
        var span = endTime - beginTime;
        return GetInt.FromObject(span.TotalMilliseconds);
    }

    #endregion

    #endregion


    #region 时间范围比较

    /// <summary>
    /// 检查日期是否在起始日期范围内，如果是则返回true，否则返回false
    /// </summary>
    /// <param name="startDay">起始时间：空字符串则不作限制</param>
    /// <param name="endDay">终止时间：</param>
    /// <param name="day">当前时间：默认当天</param>
    /// <returns></returns>
    public static bool CheckDateRange(string startDay, string endDay, string day = "")
    {
        if (day == "")
        {
            day = GetDayString();
        }

        if (startDay != "" && endDay != "")
        {
            return string.CompareOrdinal(day, startDay) >= 0 && string.CompareOrdinal(day, endDay) <= 0;
        }

        if (startDay != "")
        {
            return string.CompareOrdinal(day, startDay) >= 0;
        }

        if (endDay != "")
        {
            return string.CompareOrdinal(day, endDay) <= 0;
        }

        return true;
    }

    #endregion
}
