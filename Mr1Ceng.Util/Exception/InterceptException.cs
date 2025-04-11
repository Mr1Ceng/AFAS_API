using System.Reflection;

namespace Mr1Ceng.Util;

/// <summary>
/// 返回事件拦截
/// </summary>
public static class InterceptException
{
    /// <summary>
    /// 获取异常
    /// </summary>
    /// <param name="method"></param>
    /// <param name="interceptCode"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public static BusinessException Get(MethodBase? method, string interceptCode, object? debugData = null)
        => BusinessException.Create(method, ExceptionType.Intercept, interceptCode, debugData);
}
