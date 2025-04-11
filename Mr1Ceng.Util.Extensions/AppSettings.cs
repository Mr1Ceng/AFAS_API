using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Mr1Ceng.Util.Extensions;

/// <summary>
/// 应用程序设置
/// </summary>
public class AppSettings
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public static IConfiguration Configuration { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    static AppSettings()
    {
        var appPath = Directory.GetCurrentDirectory();
        if (File.Exists(Path.Combine(appPath, "appsettings.json")))
        {
            Configuration = new ConfigurationBuilder().SetBasePath(appPath)
                .Add(new JsonConfigurationSource
                {
                    Path = "appsettings.json",
                    ReloadOnChange = true
                }).Build();
            return;
        }

        var solutionPath = Directory.GetParent(appPath)?.FullName;
        if (solutionPath != null && File.Exists(Path.Combine(solutionPath, "appsettings.json")))
        {
            Configuration = new ConfigurationBuilder().SetBasePath(solutionPath)
                .Add(new JsonConfigurationSource
                {
                    Path = "appsettings.json",
                    ReloadOnChange = true
                }).Build();
            return;
        }

        throw new Exception("未找到“appsettings.json”文件");
    }

    /// <summary>
    /// 获取 appsettings.json 配置节的值
    /// </summary>
    /// <param name="sectionName">配置节名称，如果子节点，用“:”分割</param>
    /// <returns></returns>
    public static string GetValue(string sectionName) => GetString.FromObject(Configuration.GetSection(sectionName).Value);
}
