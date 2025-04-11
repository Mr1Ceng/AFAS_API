using System.Reflection;

namespace Mr1Ceng.Util;

/// <summary>
/// 反射帮助类
/// </summary>
public class ReflectionHelper
{
    /// <summary>
    /// 获取方法名
    /// </summary>
    /// <param name="method"></param>
    /// <returns>最长200字符串</returns>
    public static string GetFunctionName(MethodBase? method)
    {
        if (method == null)
        {
            return "";
        }

        var methodInfo = method as MethodInfo;
        if (methodInfo == null)
        {
            return "";
        }

        // 获取原始方法的定义（异步方法会包含 `MoveNext` 等信息）
        var declaringType = methodInfo.DeclaringType;
        if (declaringType?.Name.Contains('<') == true)
        {
            // 尝试从状态机类中推断原始方法名
            var originalName = declaringType.Name.Split('<', '>')[1];
            var originalMethod = declaringType.DeclaringType?.GetMethod(originalName);
            if (originalMethod != null)
            {
                methodInfo = originalMethod;
                declaringType = methodInfo.DeclaringType;
            }
            else if (declaringType.Namespace != null)
            {
                return GetString.FromObject(declaringType.Namespace, 200);
            }
        }

        // 获取完整方法名
        var functionName = declaringType == null
            ? methodInfo.Name
            : $"{declaringType.Namespace}.{declaringType.Name}.{methodInfo.Name}";

        return GetString.FromObject(functionName, 200);
    }
}
