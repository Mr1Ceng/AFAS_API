using Newtonsoft.Json;
using System.Xml;
using Mr1Ceng.Util;

namespace Mr1Ceng.Tools.CodeCraft.Services;

/// <summary>
/// 配置工具类
/// </summary>
internal class ConfigHelper
{

    static string m_config_path = "config.xml";
    static string m_form_path = "form.xml";

    /// <summary>
    /// 获取流水线列表
    /// </summary>
    /// <returns></returns>
    public static IList<IList<KeyValueText>> GetConfigList(IList<ColumnConfig> columnList)
    {
        var result = new List<IList<KeyValueText>>();

        try
        {
            var xmldom = new XmlDocument();
            xmldom.Load(m_config_path);
            if (xmldom.DocumentElement != null)
            {
                XmlNode root = xmldom.DocumentElement;
                var nodes = root.SelectNodes("item");
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes != null)
                        {
                            var flowItem = new List<KeyValueText>();
                            foreach (var columnItem in columnList)
                            {
                                flowItem.Add(new KeyValueText(columnItem.ColumnName, GetString.FromObject(node.Attributes[columnItem.ColumnName]?.Value), columnItem.HeaderText));
                            }
                            result.Add(flowItem);
                        }
                    }
                }

            }
        }
        catch { }

        return result;
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    /// <param name="flowList"></param>
    public static void SaveConfigFile(IList<IList<KeyValueText>> flowList)
    {
        FileInfo file = new FileInfo(m_config_path);
        StreamWriter sw = file.CreateText();
        sw.Write("<?xml version='1.0' encoding='utf-8'?>" + sw.NewLine);
        sw.Write("<config>" + sw.NewLine);
        foreach (var flow in flowList)
        {
            var paras = "";
            foreach (var param in flow)
            {
                paras += $"{param.Key}='{param.Value}' ";
            }
            sw.Write($"    <item {paras}/>" + sw.NewLine);
        }
        sw.Write("</config>" + sw.NewLine);
        sw.Close();
    }


    #region 表单列

    public static IList<ColumnConfig> GetColumnConfigList()
    {
        if (File.Exists(m_form_path))
        {
            var json = File.ReadAllText(m_form_path);

            var result = JsonConvert.DeserializeObject<IList<ColumnConfig>>(json);
            if (result != null)
            {
                return result;
            }
        }

        var list = new List<ColumnConfig>
        {
            new ColumnConfig
            {
                ColumnName = "Param1",
                HeaderText = "Param1",
                AutoFill = false,
                Width = 100
            },
            new ColumnConfig
            {
                ColumnName = "Param2",
                HeaderText = "Param2",
                AutoFill = false,
                Width = 300
            },
            new ColumnConfig
            {
                ColumnName = "Param3",
                HeaderText = "Param3",
                AutoFill = true
            }
        };

        var file = new FileInfo(m_form_path);
        var sw = file.CreateText();
        sw.Write(JsonConvert.SerializeObject(list));
        sw.Close();

        return list;
    }

    #endregion

}
