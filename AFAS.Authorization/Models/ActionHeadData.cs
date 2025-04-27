namespace AFAS.Authorization.Models;

/// <summary>
/// 请求头传递的数据
/// </summary>
public class ActionHeadData
{
    /// <summary>
    /// 请求头传递的 KeyId
    /// </summary>
    internal string KeyId { get; set; } = "";

    /// <summary>
    /// 请求头传递的 Token
    /// </summary>
    internal string Token { get; set; } = "";
}
