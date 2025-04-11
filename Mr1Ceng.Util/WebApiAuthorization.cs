using System.Reflection;
using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// WebApi的认证工具类
/// </summary>
public class WebApiAuthorization
{
    #region 加密

    /// <summary>
    /// 获取认证字符串
    /// </summary>
    /// <param name="key"></param>
    /// <param name="secret"></param>
    /// <param name="keyId">非必须，Redis的KeyId（UserId 或 OpenId）</param>
    /// <param name="token">非必须，认证Token</param>
    /// <returns></returns>
    public static string GetString(string key, string secret, string keyId = "", string token = "")
    {
        var random = Guid.NewGuid().ToString();
        var timeStamp = UnixTimeHelper.GetUnixMilliseconds(); //获取当前日期到1970年1月1日之间的毫秒数
        var signature = Sha256Helper.Encrypt(random + timeStamp + secret);
        var text = $"{key}|{random}|{timeStamp}|{signature}";
        if (keyId != "")
        {
            if (keyId.Contains('|'))
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "账号包含非法字符");
            }
            text += "|" + keyId;
        }
        if (token != "")
        {
            if (token.Length != 32)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "非法的Token格式");
            }

            text += "|" + token;
        }
        var authorization = GetBase64.FromString(text);
        return authorization;
    }

    #endregion


    #region 解密

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="authorization"></param>
    public WebApiAuthorization(string authorization)
    {
        author = authorization;
        try
        {
            var contexts = Encoding.UTF8.GetString(Convert.FromBase64String(author)).Split('|');
            ProviderKey = contexts[0];
            Random = contexts[1];
            TimeStamp = Convert.ToInt64(contexts[2]);
            Signature = contexts[3];
            KeyId = contexts.Length > 4 ? contexts[4] : "";
            Token = contexts.Length > 5 ? contexts[5] : "";
        }
        catch
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), "Authorization格式错误:", new
            {
                author
            });
        }
    }

    /// <summary>
    /// 验证终端密钥
    /// </summary>
    /// <param name="secret">终端密钥</param>
    /// <returns>客户端和服务器之间的时差（毫秒数）</returns>
    public long CheckTerminal(string secret)
    {
        //验证密钥
        if (string.Equals(Sha256Helper.Encrypt(Random + TimeStamp + secret, Encoding.UTF8), Signature.ToUpper()))
        {
            return TimeStamp - UnixTimeHelper.GetUnixMilliseconds();
        }

        throw ForbiddenException.Get(MethodBase.GetCurrentMethod(), "Signature错误", new
        {
            author
        });
    }

    /// <summary>
    /// ProviderKey
    /// </summary>
    public string ProviderKey { get; set; }

    /// <summary>
    /// 请求头传递的KeyId
    /// </summary>
    public string KeyId { get; set; }

    /// <summary>
    /// 请求头传递的Token
    /// </summary>
    public string Token { get; set; }


    #region 私有变量或属性

    /// <summary>
    /// 认证字符串
    /// </summary>
    private readonly string author;

    /// <summary>
    /// 随机数
    /// </summary>
    private string Random
    {
        get;
    }

    /// <summary>
    /// 时间戳
    /// </summary>
    private long TimeStamp
    {
        get;
    }

    /// <summary>
    /// Sha256签名
    /// </summary>
    private string Signature
    {
        get;
    }

    #endregion

    #endregion
}
