namespace Mr1Ceng.Util;

/// <summary>
/// 获取Long
/// </summary>
public class GetLong
{
    #region FromObject

    /// <summary>
    /// 将类型转换为Int64
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">转换失败后的默认值，默认值为0</param>
    /// <returns></returns>
    public static long FromObject(object? obj, long defaultValue = 0)
    {
        try
        {
            return long.Parse(Math.Round(GetDecimal.FromObject(obj, defaultValue)).ToString());
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion
}
