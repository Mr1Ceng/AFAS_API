namespace Mr1Ceng.Util;

/// <summary>
/// 获取Decimal
/// </summary>
public class GetDecimal
{
    #region FromObject

    /// <summary>
    /// 将类型转换为Decimal
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue">转换失败后的默认值，默认值为0</param>
    /// <returns></returns>
    public static decimal FromObject(object? obj, decimal defaultValue = 0)
        => decimal.TryParse(GetString.FromObject(obj), out var num) ? num : defaultValue;

    #endregion
}
