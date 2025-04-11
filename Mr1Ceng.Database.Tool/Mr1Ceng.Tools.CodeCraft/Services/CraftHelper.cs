using Mr1Ceng.Util;
using System.Reflection;

namespace Mr1Ceng.Tools.CodeCraft.Services;

/// <summary>
/// 流水线配置工具类
/// </summary>
public class CraftHelper
{
    static string m_path = "template.txt";


    public static string GetTemplate()
    {
        if (File.Exists(m_path))
        {
            return File.ReadAllText(m_path);
        }
        throw MessageException.Get(MethodBase.GetCurrentMethod(), "文件 template.ymal 不存在。");
    }


    public static void SaveTemplate(string text)
    {
        var file = new FileInfo(m_path);
        var sw = file.CreateText();
        sw.Write(text);
        sw.Close();
    }

    /// <summary>
    /// 执行代码生成
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    /// <exception cref="MessageException"></exception>
    public static string CraftCode(IList<KeyValueText> config)
    {
        var template = GetTemplate();
        if (template != "")
        {
            foreach (var param in config)
            {
                template = template.Replace($"${{{param.Text}}}", param.Value);
            }
            return template;
        }

        throw MessageException.Get(MethodBase.GetCurrentMethod(), "模板内容不存在。");
    }
}
