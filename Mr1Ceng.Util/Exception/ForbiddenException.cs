using System.Reflection;

namespace Mr1Ceng.Util;

/// <summary>
/// 返回的消息处理
/// </summary>
public static class ForbiddenException
{
    /// <summary>
    /// 获取异常
    /// </summary>
    /// <param name="method"></param>
    /// <param name="message"></param>
    /// <param name="debugData"></param>
    /// <returns></returns>
    public static BusinessException Get(MethodBase? method, string message = "", object? debugData = null)
        => BusinessException.Create(method, ExceptionType.Forbidden, message, debugData);
}
