using AFAS.Authorization.Enums;
using AFAS.Authorization.Internals.Models;
using AFAS.Authorization.Models;
using AFAS.Entity;
using Microsoft.AspNetCore.Http;
using Mr1Ceng.Util;

namespace AFAS.Authorization;

/// <summary>
/// 鉴权基础数据
/// </summary>
public class Identifier
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">HTTP客户端</param>
    /// <param name="authType">请求头类型</param>
    protected Identifier(HttpContext context, AttributeType authType)
    {
        AuthType = authType.ToString();
        AppId = "";
        User = new UserIdentity();
        Token = new Token();
        Terminal = new TerminalData();
        ActionHead = new ActionHeadData();

        #region 读取请求数据

        RequestAction = new RequestAction
        {
            SiteUrl = GetString.FromObject(context.Request.Host.Value, 100)
                + GetString.FromObject(context.Request.PathBase.Value, 100),
            PhysicalPath = GetString.FromObject(context.Request.Path.Value, 500)
        };

        RequestAgent = new RequestAgent
        {
            IpAddress = GetString.FromObject(context.Request.Headers["X-Forwarded-For"].FirstOrDefault(), 20),
            AbsoluteUri = GetString.FromObject(context.Request.Headers["Referer"], 500),
            UserLanguages = GetString.FromObject(context.Request.Headers["Accept-Language"], 10),
            UserAgent = GetString.FromObject(context.Request.Headers["User-Agent"], 500)
        };

        #endregion
    }

    /// <summary>
    /// 终端信息
    /// </summary>
    public TerminalData GetTerminal() => Terminal;

    /// <summary>
    /// 获取用户身份信息
    /// </summary>
    /// <returns></returns>
    public UserIdentity GetUserIdentity() => User;

    #region 属性

    /// <summary>
    /// 认证类型
    /// </summary>
    internal string AuthType { get; }

    /// <summary>
    /// 客户端与服务器的时间间隔毫秒数
    /// </summary>
    internal double TimeSpan { get; set; }

    /// <summary>
    /// 公众号AppId
    /// </summary>
    internal string AppId { get; set; }

    /// <summary>
    /// UserId
    /// </summary>
    internal string UserId => User.UserId;

    /// <summary>
    /// Token
    /// </summary>
    internal Token Token { get; set; }

    /// <summary>
    /// 用户Token身份信息
    /// </summary>
    internal UserTokenData UserToken
    {
        get => new()
        {
            User = User,
            Token = Token
        };
        set
        {
            User = value.User;
            Token = value.Token;
        }
    }

    /// <summary>
    /// 用户身份信息
    /// </summary>
    internal UserIdentity User { get; set; }

    /// <summary>
    /// 终端信息
    /// </summary>
    internal TerminalData Terminal { get; set; }

    /// <summary>
    /// 请求头数据
    /// </summary>
    public ActionHeadData ActionHead { get; set; }

    /// <summary>
    /// 浏览器信息
    /// </summary>
    public RequestAgent RequestAgent { get; }

    /// <summary>
    /// Action信息
    /// </summary>
    public RequestAction RequestAction { get; }

    /// <summary>
    /// 获取Token值
    /// </summary>
    public string TokenValue => Token.Value;

    #endregion


    #region 内部方法

    internal void RecordAPI() => RecordAPI(true);

    /// <summary>
    /// 写API访问日志
    /// </summary>
    /// <param name="exception"></param>
    internal void RecordAPI(string exception) => RecordAPI(false, exception);

    /// <summary>
    /// 写API访问日志
    /// </summary>
    /// <param name="success"></param>
    /// <param name="exception"></param>
    private void RecordAPI(bool success, string exception = "")
    {
        using (var context = new AfasContext())
        {
            context.LogApis.Add(new LogApi
            {
                TimeStamp = DateHelper.GetDateString(),
                TerminalId = Terminal.TerminalId,
                AuthType = AuthType,
                Token = Token.Value,
                SiteUrl = RequestAction.SiteUrl,
                PhysicalPath = RequestAction.PhysicalPath,
                AbsoluteUri = RequestAgent.AbsoluteUri,
                IpAddress = RequestAgent.IpAddress,
                UserLanguages = RequestAgent.UserLanguages,
                UserAgent = RequestAgent.UserAgent,
                TerminalSpan = Terminal.TimeSpan,
                TokenSpan = Token.TimeSpan,
                IsSuccess = success,
                Exception = exception
            });
            context.SaveChanges();
        };
    }


    #endregion
}
