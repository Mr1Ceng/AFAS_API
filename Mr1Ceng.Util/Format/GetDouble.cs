namespace Mr1Ceng.Util;

/// <summary>
/// 获取Double
/// </summary>
public class GetDouble
{
    /// <summary>
    /// 将类型转换为Double
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">转换失败后的默认值，默认值0</param>
    /// <returns></returns>
    public static double FromObject(object? obj, double defaultValue = 0.0)
    {
        if (double.TryParse(GetString.FromObject(obj), out var result))
        {
            return result;
        }

        return defaultValue;
    }
}
