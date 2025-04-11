using System.ComponentModel;
using System.Reflection;

namespace Mr1Ceng.Util;

/// <summary>
/// 枚举工具类
/// </summary>
public class EnumHelper
{
    /// <summary>
    /// 获取枚举字段描述
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="length">返回文本的最大长度，0则不做限制</param>
    /// <returns></returns>
    public static string GetDescription<T>(T obj, int length = 0)
    {
        if (obj == null)
        {
            return GetDescription<T>("");
        }
        var val = obj.ToString();
        if (val == null)
        {
            return GetDescription<T>("");
        }
        return GetDescription(obj.GetType(), val, length);
    }

    /// <summary>
    /// 获取枚举字段描述
    /// </summary>
    /// <param name="type"></param>
    /// <param name="obj"></param>
    /// <param name="length">返回文本的最大长度，0则不做限制</param>
    /// <returns></returns>
    public static string GetDescription(Type type, string? obj, int length = 0)
    {
        var description = GetString.FromObject(obj);

        if (description != "")
        {
            try
            {
                //目标查找的描述类型
                var descType = typeof(DescriptionAttribute);

                //目标字段
                var field = type.GetField(description);
                if (field != null)
                {
                    //判断描述是否在字段的特性
                    if (field.IsDefined(descType, false))
                    {
                        var enumAttributes = (DescriptionAttribute[])field.GetCustomAttributes(descType, false);
                        description = enumAttributes.FirstOrDefault()?.Description ?? string.Empty;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        return GetString.FromObject(description, length);
    }

    /// <summary>
    /// 获取枚举字段描述
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="length">返回文本的最大长度，0则不做限制</param>
    /// <returns></returns>
    private static string GetDescription<T>(string? obj, int length = 0)
    {
        var description = GetString.FromObject(obj);

        if (description != "")
        {
            try
            {
                //目标查找的描述类型
                var descType = typeof(DescriptionAttribute);

                //目标字段
                var field = typeof(T).GetField(description);
                if (field != null)
                {
                    //判断描述是否在字段的特性
                    if (field.IsDefined(descType, false))
                    {
                        var enumAttributes = (DescriptionAttribute[])field.GetCustomAttributes(descType, false);
                        description = enumAttributes.FirstOrDefault()?.Description ?? string.Empty;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        return GetString.FromObject(description, length);
    }
}

/// <summary>
/// 枚举工具类
/// </summary>
public class EnumHelper<T> where T : Enum
{
    /// <summary>
    /// 获取枚举字段描述
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="length">返回文本的最大长度，0则不做限制</param>
    /// <returns></returns>
    public static string GetDescription(string? obj, int length = 0)
    {
        var description = GetString.FromObject(obj);

        if (description != "")
        {
            try
            {
                //目标查找的描述类型
                var descType = typeof(DescriptionAttribute);

                //目标字段
                var field = typeof(T).GetField(description);
                if (field != null)
                {
                    //判断描述是否在字段的特性
                    if (field.IsDefined(descType, false))
                    {
                        var enumAttributes = (DescriptionAttribute[])field.GetCustomAttributes(descType, false);
                        description = enumAttributes.FirstOrDefault()?.Description ?? string.Empty;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        return GetString.FromObject(description, length);
    }

    /// <summary>
    /// 获取String对应的枚举类型
    /// </summary>
    /// <param name="value"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    public static T? GetEnum(string value, bool throwException = false)
    {
        if (Enum.TryParse(typeof(T), value, true, out var enumValue))
        {
            if (GetInt.FromObject(enumValue.ToString()) == 0)
            {
                return (T)enumValue;
            }
        }

        if (throwException)
        {
            throw MessageException.Get(MethodBase.GetCurrentMethod(), "枚举转换失败");
        }

        return default;
    }

    /// <summary>
    /// 获取枚举字段列表
    /// </summary>
    /// <returns></returns>
    public static List<KeyTextSort> GetValues()
    {
        var list = new List<KeyTextSort>();
        foreach (T item in Enum.GetValues(typeof(T)))
        {
            list.Add(new KeyTextSort
            {
                Key = item.ToString(),
                Text = EnumHelper.GetDescription(item),
                Sort = (int)Enum.Parse(typeof(T), item.ToString())
            });
        }
        return list;
    }
}
