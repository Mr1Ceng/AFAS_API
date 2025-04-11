namespace Mr1Ceng.Util;

/// <summary>
/// 获取Boolean
/// </summary>
public class GetBoolean
{
    #region FromNumber

    /// <summary>
    /// 将类型转换为Boolean
    /// </summary>
    /// <param name="obj">大于0，为True，否则为False</param>
    /// <returns>大于0，为True，否则为False</returns>
    public static bool FromInt32(int? obj)
    {
        if (obj > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 将类型转换为Boolean
    /// </summary>
    /// <param name="obj">大于0，为True，否则为False</param>
    /// <returns>大于0，为True，否则为False</returns>
    public static bool FromLong(long? obj)
    {
        if (obj > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 将类型转换为Boolean
    /// </summary>
    /// <param name="obj">大于0，为True，否则为False</param>
    /// <returns>大于0，为True，否则为False</returns>
    public static bool FromFloat(float? obj)
    {
        if (obj > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 将类型转换为Boolean
    /// </summary>
    /// <param name="obj">大于0，为True，否则为False</param>
    /// <returns>大于0，为True，否则为False</returns>
    public static bool FromDecimal(decimal? obj)
    {
        if (obj > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 将类型转换为Boolean
    /// </summary>
    /// <param name="obj">大于0，为True，否则为False</param>
    /// <returns>大于0，为True，否则为False</returns>
    public static bool FromDouble(double? obj)
    {
        if (obj > 0)
        {
            return true;
        }
        return false;
    }

    #endregion


    #region FromString

    /// <summary>
    /// 将类型转换为Boolean
    /// </summary>
    /// <param name="obj">可接受的值：1/0、TRUE/FALSE、YES/NO、T/F、Y/N</param>
    /// <param name="defaultValue">不在上述范围内的默认值 返回 false</param>
    /// <returns></returns>
    public static bool FromString(string? obj, bool defaultValue = false)
    {
        if (obj is null)
        {
            return defaultValue;
        }

        obj = obj.ToUpper();
        switch (obj)
        {
            case "TRUE":
            case "T":
            case "YES":
            case "Y":
            case "1":
                return true;
            case "FALSE":
            case "F":
            case "NO":
            case "N":
            case "0":
                return false;
        }

        var bit = GetInt.FromObject(obj);
        return bit switch
        {
            1 => true,
            0 => false,
            _ => defaultValue
        };
    }

    #endregion


    #region FromObject

    /// <summary>
    /// 将类型转换为Boolean
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>如果转换失败默认返回 0 </returns>
    public static bool FromObject(object? obj) => FromString(GetString.FromObject(obj));

    /// <summary>
    /// 将类型转换为Boolean
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">转换失败后的默认值</param>
    /// <returns></returns>
    public static bool FromObject(object? obj, bool defaultValue)
        => obj is null ? defaultValue : FromString(GetString.FromObject(obj), defaultValue);

    #endregion
}
