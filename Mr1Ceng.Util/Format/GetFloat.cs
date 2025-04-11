namespace Mr1Ceng.Util;

/// <summary>
/// 获取Float
/// </summary>
public class GetFloat
{
    /// <summary>
    /// 将类型转换为Float
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">转换失败后的默认值，默认值为0</param>
    /// <returns></returns>
    public static float FromObject(object? obj, float defaultValue = 0)
    {
        if (float.TryParse(GetString.FromObject(obj), out var result))
        {
            return result;
        }

        return defaultValue;
    }
}
