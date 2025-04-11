using System.Reflection;
using Mr1Ceng.Util.Database.EntityHelper;
using Mr1Ceng.Util.Extensions;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// 系统环境信息类
/// </summary>
public class SystemEnvironment
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public static readonly SystemEnvironment Instance = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    private SystemEnvironment()
    {
        try
        {
            AesHelper aesHelper = new(GetString.FromObject(AppSettings.Configuration.GetSection("Encrypt").Value));

            var config = AppSettings.Configuration.GetSection("Mr1Ceng.Util.Database");

            DataSourceList = [];
            DataSourceDictionary = new Dictionary<string, DataSourceInfo>();
            foreach (var item in config.GetSection("DataSources").GetChildren())
            {
                DataSourceInfo dataSourceInfo = new()
                {
                    Name = GetString.FromObject(item.GetSection("Name").Value),
                    DataSource = GetString.FromObject(item.GetSection("DataSource").Value),
                    DatabaseName = GetString.FromObject(item.GetSection("DatabaseName").Value),
                    UserID = GetString.FromObject(item.GetSection("UserID").Value),
                    Password = GetString.FromObject(item.GetSection("Password").Value),
                    ORMappingName = GetString.FromObject(item.GetSection("ORMappingName").Value)
                };

                if (dataSourceInfo.Password != "")
                {
                    dataSourceInfo.Password = aesHelper.Decrypt(dataSourceInfo.Password);
                }

                DataSourceList.Add(dataSourceInfo);
                DataSourceDictionary.Add(dataSourceInfo.Name, dataSourceInfo);
            }
        }
        catch
        {
            throw new Exception("获取“appsettings.json”发生错误");
        }
    }


    #region 属性

    /// <summary>
    /// 数据源列表
    /// </summary>
    internal List<DataSourceInfo> DataSourceList { get; }

    /// <summary>
    /// 数据源字典
    /// </summary>
    internal Dictionary<string, DataSourceInfo> DataSourceDictionary { get; }

    #endregion


    #region 方法

    /// <summary>
    /// 数据源列表
    /// </summary>
    internal DataSourceInfo GetDataSourceInfo(string name)
    {
        if (DataSourceDictionary.TryGetValue(name, out var info))
        {
            return info;
        }
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), "数据源不存在");
    }

    /// <summary>
    /// 获取数据源名称列表
    /// </summary>
    /// <returns></returns>
    public List<string> GetDataSourceNames()
    {
        return DataSourceList.Select(x => x.Name).ToList();
    }

    #endregion
}
