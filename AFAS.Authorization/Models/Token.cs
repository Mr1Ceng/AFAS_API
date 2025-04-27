namespace AFAS.Authorization.Models;

/// <summary>
/// Token
/// </summary>
public class Token
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public Token()
    {
        Value = "";
        ExpireTime = 0;
        TimeSpan = 0;
    }

    /// <summary>
    /// Token
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 到期时间期到1970年1月1日之间的秒数
    /// </summary>
    public int ExpireTime { get; set; }

    /// <summary>
    /// 客户端与服务器的时间间隔毫秒数
    /// </summary>
    internal long TimeSpan { get; set; }
}
