using System.Reflection;

namespace Mr1Ceng.Util;

/// <summary>
/// 获取对象
/// </summary>
public class GetObject
{
    #region FromProperty

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static object? FromProperty(object? obj, string propertyName)
    {
        if (obj == null)
        {
            return null;
        }

        try
        {
            // 获取对象的类型
            Type type = obj.GetType();
            // 获取指定的属性
            PropertyInfo? property = type.GetProperty(propertyName);

            // 检查属性是否存在
            if (property == null)
            {
                return null;
            }
            return property.GetValue(obj);
        }
        catch
        {
            return null;
        }
    }

    #endregion
}
