namespace Mr1Ceng.Util;

/// <summary>
/// 获取Int
/// </summary>
public class GetInt
{
    #region FromObject

    /// <summary>
    /// 将类型转换为Int32
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">转换失败后的默认值，默认值为0</param>
    /// <returns></returns>
    public static int FromObject(object? obj, int defaultValue = 0)
    {
        try
        {
            return int.Parse(Math.Round(GetDecimal.FromObject(obj, defaultValue)).ToString());
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion


    #region FromBoolen

    /// <summary>
    /// 将Bool转换为Int32
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static int FromBoolen(bool obj) => obj ? 1 : 0;

    #endregion
}
