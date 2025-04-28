using AFAS.Entity;
using Mr1Ceng.Util;
using Mr1Ceng.Util.Extensions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AFAS.Infrastructure;

/// <summary>
/// 系统基础配置
/// </summary>
public class SystemConfig
{
    /// <summary>
    /// 构造基础构建函数
    /// </summary>
    /// <param name="serviceId"></param>
    public static void Setup(string? serviceId)
    {
        ServiceId = GetString.FromObject(serviceId);
        var service = new SService();
        using (var context = new AfasContext())
        {
            service = context.SServices.Where(x => x.ServiceId == ServiceId).FirstOrDefault();
            if (service == null) {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), $"服务{ServiceId}未定义。");
            }
        }

        ServiceId = service.ServiceId;
        ServiceName = service.ServiceName;
        ServiceType = service.ServiceType;
        SystemId = service.SystemId;
        RootUrl = service.RootUrl;
        VirtualPath = service.VirtualPath;
        _corsUrls = service.CorsUrls;
        Timeout = service.Timeout;

        #region 服务端口号

        var envUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
        if (envUrls != null)
        {
            ServicePort = GetInt.FromObject(Regex.Match(envUrls, @":(\d+)").Groups[1].Value);
        }

        #endregion
    }

    #region 内部变量

    /// <summary>
    /// 域名白名单列表
    /// </summary>
    private static string _corsUrls = "";

    #endregion


    #region 属性

    /// <summary>
    /// 服务代码
    /// </summary>
    public static string ServiceId { get; private set; } = "";

    /// <summary>
    /// 服务名称
    /// </summary>
    public static string ServiceName { get; private set; } = "";

    /// <summary>
    /// 服务类型
    /// </summary>
    public static string ServiceType { get; private set; } = "";

    /// <summary>
    /// 系统编码
    /// </summary>
    public static string SystemId { get; private set; } = "";

    /// <summary>
    /// 域名白名单（多个地址，半角逗号分割）
    /// </summary>
    public static string[] CorsUrls => _corsUrls.Split(',').Select(x => x.Trim()).ToArray();

    /// <summary>
    /// 鉴权头失效毫秒数
    /// </summary>
    public static int Timeout { get; private set; }

    /// <summary>
    /// 站点跟地址
    /// </summary>
    public static string RootUrl { get; private set; } = "";

    /// <summary>
    /// 二级路径
    /// </summary>
    public static string VirtualPath { get; private set; } = "";

    /// <summary>
    /// 是否显示Swagger【0:不加载Swagger、1:加载swagger.json但不显示UI、2:显示完整Swagger、3:启动Swagger本地调试】
    /// </summary>
    public static int ShowSwagger => GetInt.FromObject(AppSettings.Configuration.GetSection("ShowSwagger").Value);

    /// <summary>
    /// 服务端口号
    /// </summary>
    public static int ServicePort { get; private set; }

    /// <summary>
    /// 是否开启本地调试
    /// </summary>
    public static bool LocalDebug => GetBoolean.FromObject(AppSettings.Configuration.GetSection("LocalDebug").Value);

    /// <summary>
    /// 证书文件
    /// </summary>
    public static string PfxFile => LocalDebug ? "../mr1ceng.top.pfx" : "";

    /// <summary>
    /// 证书密码
    /// </summary>
    public static string Password => LocalDebug ? "22mcp1ni" : "";

    #endregion
}
