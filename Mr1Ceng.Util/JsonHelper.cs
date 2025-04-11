using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mr1Ceng.Util;

/// <summary>
/// Json序列化扩展
/// </summary>
public static class JsonHelper
{
    #region 对象序列化

    /// <summary>
    /// 对象序列化
    /// </summary>
    /// <param name="value"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string Serialize(object? value, JsonSerializerSettings? options = null)
        => value == null ? "" : JsonConvert.SerializeObject(value, options);

    /// <summary>
    /// 对象序列化，以JSON文档的方式输出
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>
    /// 1、格式化为易读的形式
    /// 2、忽略为NULL的属性
    /// </remarks>
    public static string SerializeAsDocument(object? value)
    {
        if (value == null)
        {
            return "";
        }

        var options = GetSerializeOptions();
        options.Formatting = Formatting.Indented;
        options.NullValueHandling = NullValueHandling.Ignore;
        return JsonConvert.SerializeObject(value, options).Replace("\\n", "\n").Replace("\\t", "   ");
    }

    /// <summary>
    /// 以JSON文档的方式输出
    /// </summary>
    /// <param name="jsonString"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    public static string FormatAsDocument(string jsonString, bool throwException = false)
    {
        try
        {
            return SerializeAsDocument(Deserialize(jsonString));
        }
        catch
        {
            if (throwException)
            {
                throw;
            }
            return jsonString;
        }
    }

    #endregion


    #region 对象反序列化

    /// <summary>
    /// 对象反序列化
    /// </summary>
    /// <param name="json"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static dynamic? Deserialize(string json, JsonSerializerSettings? options = null) => options == null
        ? JsonConvert.DeserializeObject(json)
        : JsonConvert.DeserializeObject(json, options);

    /// <summary>
    /// 对象反序列化
    /// </summary>
    /// <param name="json"></param>
    /// <param name="options"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? Deserialize<T>(string json, JsonSerializerSettings? options = null) => options == null
        ? JsonConvert.DeserializeObject<T>(json)
        : JsonConvert.DeserializeObject<T>(json, options);

    #endregion


    #region GetSerializeOptions

    /// <summary>
    /// 获取对象序列化配置
    /// </summary>
    /// <returns></returns>
    public static JsonSerializerSettings GetSerializeOptions()
    {
        return new JsonSerializerSettings
        {
            #region 格式化输出

            /*
                控制序列化的格式
                    Formatting.None：不进行格式化（默认值），适合性能敏感的场景。
                    Formatting.Indented：美化输出，适合需要可读性的场景。
            */
            Formatting = Formatting.None,

            /*
                控制浮点数的格式化方式
                    FloatFormatHandling.String: 将浮点数序列化为字符串（默认）。
                    FloatFormatHandling.DefaultValue: 使用默认格式序列化浮点数。
                    FloatFormatHandling.Symbol: 将浮点数序列化为符号字符串。
             */
            FloatFormatHandling = FloatFormatHandling.String,

            /*
                控制浮点数的解析方式
                    FloatParseHandling.Double：将浮点数解析为 double 类型（默认值），适合大多数场景。
                    FloatParseHandling.Decimal：将浮点数解析为 decimal 类型，适合需要更高精度的场景。
            */
            FloatParseHandling = FloatParseHandling.Double,

            #endregion

            #region 日期时间处理

            /*
                控制日期时间的序列化处理方式。
                    DateFormatHandling.IsoDateFormat：使用 ISO 8601 格式（默认值），适合国际化场景。
                    DateFormatHandling.MicrosoftDateFormat：使用 Microsoft 日期格式，适合与 Microsoft 生态系统兼容的场景。
            */
            DateFormatHandling = DateFormatHandling.IsoDateFormat,

            /*
                控制日期时间的时区处理方式
                    DateTimeZoneHandling.Local：使用本地时间（默认值），适合大多数场景。
                    DateTimeZoneHandling.Utc：使用 UTC 时间，适合需要全球统一时间的场景。
                    DateTimeZoneHandling.RoundtripKind：保留
            */
            DateTimeZoneHandling = DateTimeZoneHandling.Local,

            /*
                控制如何处理日期时间格式
                    "yyyy-MM-dd"：输出格式为 "2024-10-20"。
                    "MM/dd/yyyy HH:mm:ss"：输出格式为 "10/20/2024 12:34:56"。
            */
            DateParseHandling = DateParseHandling.DateTime,

            /*
                控制如何处理日期时间格式
                    "yyyy-MM-dd"：输出格式为 "2024-10-20"。
                    "MM/dd/yyyy HH:mm:ss"：输出格式为 "10/20/2024 12:34:56"。
            */
            DateFormatString = "yyyy-MM-dd HH:mm:ss",

            #endregion
        };
    }

    /// <summary>
    /// 获取对象反序列化配置
    /// </summary>
    /// <returns></returns>
    public static JsonSerializerSettings GetDeserializeOptions() => new();

    #endregion


    #region 解析

    /// <summary>
    /// 对象序列化（加一道JObject转化）
    /// </summary>
    /// <param name="jsonString"></param>
    /// <param name="throwException"></param>
    /// <returns></returns>
    public static string SerializeByJObject(string jsonString, bool throwException = false)
    {
        if (jsonString == "")
        {
            return "";
        }

        try
        {
            // 反序列化为 JObject
            var obj = JObject.Parse(jsonString);

            // 动态遍历字段，处理可能需要 URL 解码的字段
            foreach (var property in obj.Properties())
            {
                var key = property.Name; // 字段名
                var value = property.Value.ToString(); // 字段值

                // 判断是否需要 URL 解码（可以根据业务逻辑优化）
                if (value.Contains('%') || value.Contains('+'))
                {
                    value = HttpUtility.UrlDecode(value); // 解码
                }

                obj[key] = value; // 更新解码后的值
            }

            return Serialize(obj);
        }
        catch
        {
            if (throwException)
            {
                throw;
            }
            return jsonString;
        }
    }

    #endregion
}
