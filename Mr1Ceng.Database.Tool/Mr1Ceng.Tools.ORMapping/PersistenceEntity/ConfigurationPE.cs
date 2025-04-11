using System.Xml;
using Mr1Ceng.Tools.ORMapping.V7.Common;

namespace Mr1Ceng.Tools.ORMapping.V7.PersistenceEntity;

public class ConfigurationPE : Configuration
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configFile"></param>
    public ConfigurationPE() : base(ConfigFile.PERSISTENCEENTITY)
    {
    }

    /// <summary>
    /// 客户化信息
    /// </summary>
    public CustomerInfo CustomerInfo { get; set; }

    /// <summary>
    /// InitCustomerConfiguration
    /// </summary>
    protected override void InitCustomerConfiguration()
    {
        CustomerInfo = new CustomerInfo
        {
            Language = "C#",
            Function = "public"
        };
    }

    /// <summary>
    /// RetrieveCustomerInfo
    /// </summary>
    /// <param name="root"></param>
    protected override void RetrieveCustomerInfo(XmlNode root)
    {
        try
        {
            CustomerInfo.DataSource = root.SelectSingleNode("customerinfo/parameter[@name=\"datasource\"]").Attributes["value"].Value;
            CustomerInfo.NameSpace = root.SelectSingleNode("customerinfo/parameter[@name=\"namespace\"]").Attributes["value"].Value;
            CustomerInfo.Function = root.SelectSingleNode("customerinfo/parameter[@name=\"function\"]").Attributes["value"].Value;
            CustomerInfo.Language = root.SelectSingleNode("customerinfo/parameter[@name=\"language\"]").Attributes["value"].Value;
        }
        catch {}
    }

    /// <summary>
    /// WriteCustomerInfo
    /// </summary>
    /// <param name="sw"></param>
    protected override void WriteCustomerInfo(StreamWriter sw)
    {
        sw.Write("    <customerinfo>" + sw.NewLine);
        sw.Write("        <parameter name=\"datasource\" value=\"" + CustomerInfo.DataSource + "\" />" + sw.NewLine);
        sw.Write("        <parameter name=\"namespace\" value=\"" + CustomerInfo.NameSpace + "\" />" + sw.NewLine);
        sw.Write("        <parameter name=\"function\" value=\"" + CustomerInfo.Function + "\" />" + sw.NewLine);
        sw.Write("        <parameter name=\"language\" value=\"" + CustomerInfo.Language + "\" />" + sw.NewLine);
        sw.Write("    </customerinfo>" + sw.NewLine);
    }
}
