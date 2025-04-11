using System.Collections;
using System.Text;
using System.Xml;
using Mr1Ceng.Tools.ORMapping.V7.Common;
using Mr1Ceng.Util;

namespace Mr1Ceng.Tools.ORMapping.V7.PersistenceEntity;

/// <summary>
/// XmlFile 的摘要说明。
/// </summary>
public class XmlFile
{
    private readonly string m_FilePath;
    private readonly ArrayList m_EntityObjects;
    private readonly string m_ApplicationFile;
    private const string ApplicationFile = "appsettings.json";

    public XmlFile(string filepath, ArrayList EntityObjects)
    {
        m_FilePath = filepath;
        m_EntityObjects = EntityObjects;
        m_ApplicationFile = filepath + ApplicationFile;
    }

    public string DataSource { get; set; }


    /// <summary>
    /// 写ORMapping 文件
    /// </summary>
    public void BuildORMappingXmlFile()
    {
        var configuration = new Configuration(ConfigFile.PERSISTENCEENTITY);
        configuration.Retrieve();

        var w = new XmlTextWriter(m_FilePath + configuration.CommonInfo.ORMappingName, Encoding.UTF8);
        w.Formatting = Formatting.Indented;

        //应用程序头
        w.WriteStartElement("Sys-mapping");
        foreach (EntityObject entityObject in m_EntityObjects)
        {
            w.WriteStartElement("class");
            w.WriteAttributeString("name", entityObject.EntityName);
            w.WriteAttributeString("table", entityObject.EntityName);
            w.WriteAttributeString("datasource", DataSource);
            foreach (Field field in entityObject.FieldArrayLists)
            {
                w.WriteStartElement("attribute");
                w.WriteAttributeString("name", field.Name);
                w.WriteAttributeString("column", field.Name);
                w.WriteAttributeString("type", CshartTypeToConfigType(field.DataType));
                if (field.IsKey)
                {
                    w.WriteAttributeString("key", "primary");
                }
                if (field.IsIdentity)
                {
                    w.WriteAttributeString("increment", "true");
                }
                w.WriteAttributeString("size", field.Length.ToString());
                w.WriteEndElement();
            }
            w.WriteEndElement();
        }
        w.WriteEndElement();
        w.Close();
        var oXmlDocument = new XmlDocument();
        oXmlDocument.WriteTo(w);
    }

    /// <summary>
    /// 写应用程序配置文件
    /// </summary>
    public void BuildApplicationXmlFile()
    {
        var configuration = new Configuration(ConfigFile.PERSISTENCEENTITY);
        configuration.Retrieve();

        var json = $@"{{
  ""Mr1Ceng.Util.Database"": {{
    ""Encrypt"": ""{configuration.ConnectInfo.Encrypt}"",
    ""DefaultDataSource"": ""{DataSource}"",
    ""DataSources"": [
      {{
        ""Name"": ""{DataSource}"",
        ""DataSource"": ""{configuration.ConnectInfo.Server}"",
        ""DatabaseName"": ""{configuration.ConnectInfo.DataBase}"",
        ""UserID"": ""{configuration.ConnectInfo.UserID}"",
        ""Password"": ""{new AesHelper(configuration.ConnectInfo.Encrypt).Encrypt(configuration.ConnectInfo.Password)}"",
        ""ORMappingName"": ""{configuration.CommonInfo.ORMappingName}""
      }}
    ]
  }}
}}";

        FileHelper.SaveFileFromString(m_ApplicationFile, json, Encoding.UTF8);
    }

    /// <summary>
    /// c#类型转换成配置文件类型
    /// </summary>
    /// <param name="dataType"></param>
    /// <returns></returns>
    private string CshartTypeToConfigType(string dataType)
    {
        var type = "";
        switch (dataType)
        {
            case "System.Boolean":
                type = "Boolean";
                break;
            case "System.Int64":
                type = "BigInt";
                break;
            case "System.Int32":
                type = "Integer";
                break;
            case "System.Double":
                type = "Double";
                break;
            case "System.Decimal":
                type = "Decimal";
                break;
            case "System.DateTime":
                type = "Date";
                break;
            case "System.Single":
                type = "Single";
                break;
            case "System.Int16":
                type = "SmallInt";
                break;
            case "System.Byte":
                type = "TinyInt";
                break;
            case "System.Byte[]":
                type = "binary";
                break;
            case "System.String":
                type = "String";
                break;
            default:
                type = "String";
                break;
        }
        return type;
    }
}
