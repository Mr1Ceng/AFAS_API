using System.Reflection;
using System.Xml;
using Mr1Ceng.Util;

namespace Mr1Ceng.Tools.ORMapping.V7.Common;

public class Configuration
{
    private readonly string m_path;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configFile"></param>
    public Configuration(string configFile)
    {
        InitConfiguration();

        var strLocation = Assembly.GetAssembly(GetType()).Location;
        strLocation = strLocation.Substring(0, strLocation.LastIndexOf('\\') + 1);
        m_path = strLocation + configFile;

        if (!File.Exists(m_path))
        {
            NewConnectInfoFile();
        }
    }

    /// <summary>
    /// 初始化配置文件
    /// </summary>
    private void InitConfiguration()
    {
        ConnectInfo = new ConnectInfo
        {
            Server = ".",
            UserID = "sa"
        };
        CommonInfo = new CommonInfo();
        InitCustomerConfiguration();
    }

    /// <summary>
    /// 客户化配置节（初始化）
    /// </summary>
    protected virtual void InitCustomerConfiguration()
    {
    }

    /// <summary>
    /// 客户化配置节(读)
    /// </summary>
    /// <param name="sw"></param>
    protected virtual void RetrieveCustomerInfo(XmlNode root)
    {
    }

    /// <summary>
    /// 客户化配置节(写)
    /// </summary>
    /// <param name="sw"></param>
    protected virtual void WriteCustomerInfo(StreamWriter sw)
    {
    }

    /// <summary>
    /// 获取配置文件内容
    /// </summary>
    public void Retrieve()
    {
        try
        {
            var xmldom = new XmlDocument();
            xmldom.Load(m_path);
            XmlNode root = xmldom.DocumentElement;

            ConnectInfo.Server = root.SelectSingleNode("connectinfo/parameter[@name=\"server\"]").Attributes["value"].Value;
            ConnectInfo.DataBase = root.SelectSingleNode("connectinfo/parameter[@name=\"database\"]").Attributes["value"].Value;
            ConnectInfo.UserID = root.SelectSingleNode("connectinfo/parameter[@name=\"userid\"]").Attributes["value"].Value;
            ConnectInfo.Password = root.SelectSingleNode("connectinfo/parameter[@name=\"password\"]").Attributes["value"].Value;
            ConnectInfo.Encrypt = root.SelectSingleNode("connectinfo/parameter[@name=\"encrypt\"]").Attributes["value"].Value;
            CommonInfo.ORMappingName
                = root.SelectSingleNode("commoninfo/parameter[@name=\"ormappingname\"]").Attributes["value"].Value;
            CommonInfo.OutputDir = root.SelectSingleNode("commoninfo/parameter[@name=\"outputdir\"]").Attributes["value"].Value;
            CommonInfo.EntityClassFiles
                = GetBoolean.FromObject(
                    root.SelectSingleNode("commoninfo/parameter[@name=\"entityclass\"]")?.Attributes["value"].Value);
            CommonInfo.AppSettingsFiles
                = GetBoolean.FromObject(
                    root.SelectSingleNode("commoninfo/parameter[@name=\"appsettings\"]")?.Attributes["value"].Value);

            RetrieveCustomerInfo(root);
        }
        catch {}
    }

    /// <summary>
    /// 保存修改
    /// </summary>
    public void Save()
    {
        DelConnectInfoFile();
        NewConnectInfoFile();
    }

    /// <summary>
    /// 创建 configuration.dll 文件
    /// </summary>
    private void NewConnectInfoFile()
    {
        var file = new FileInfo(m_path);
        var sw = file.CreateText();
        sw.Write("<?xml version='1.0' encoding='utf-8'?>" + sw.NewLine);
        sw.Write("<configuration>" + sw.NewLine);
        sw.Write("    <connectinfo>" + sw.NewLine);
        sw.Write("        <parameter name=\"server\" value=\"" + ConnectInfo.Server + "\" />" + sw.NewLine);
        sw.Write("        <parameter name=\"database\" value=\"" + ConnectInfo.DataBase + "\" />" + sw.NewLine);
        sw.Write("        <parameter name=\"userid\" value=\"" + ConnectInfo.UserID + "\" />" + sw.NewLine);
        sw.Write("        <parameter name=\"password\" value=\"" + ConnectInfo.Password + "\" />" + sw.NewLine);
        sw.Write("        <parameter name=\"encrypt\" value=\"" + ConnectInfo.Encrypt + "\" />" + sw.NewLine);
        sw.Write("    </connectinfo>" + sw.NewLine);
        sw.Write("    <commoninfo>" + sw.NewLine);
        sw.Write("        <parameter name=\"ormappingname\" value=\"" + CommonInfo.ORMappingName + "\" />" + sw.NewLine);
        sw.Write("        <parameter name=\"outputdir\" value=\"" + CommonInfo.OutputDir + "\" />" + sw.NewLine);
        if (CommonInfo.EntityClassFiles)
        {
            sw.Write("        <parameter name=\"entityclass\" value=\"1\" />" + sw.NewLine);
        }
        else
        {
            sw.Write("        <parameter name=\"entityclass\" value=\"0\" />" + sw.NewLine);
        }
        if (CommonInfo.AppSettingsFiles)
        {
            sw.Write("        <parameter name=\"appsettings\" value=\"1\" />" + sw.NewLine);
        }
        else
        {
            sw.Write("        <parameter name=\"appsettings\" value=\"0\" />" + sw.NewLine);
        }
        sw.Write("    </commoninfo>" + sw.NewLine);
        WriteCustomerInfo(sw);
        sw.Write("</configuration>" + sw.NewLine);
        sw.Close();
    }

    /// <summary>
    /// 删除 configuration.dll 文件
    /// </summary>
    private void DelConnectInfoFile()
    {
        File.Delete(m_path);
    }


    /// <summary>
    /// 数据库连接信息
    /// </summary>
    public ConnectInfo ConnectInfo { get; set; }

    /// <summary>
    /// 公共信息
    /// </summary>
    public CommonInfo CommonInfo { get; set; }
}
