using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 文本数据对象
/// </summary>
public class TextModel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TextModel()
    {
        Text = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="text"></param>
    public TextModel(string text)
    {
        Text = text;
    }

    /// <summary>
    /// 文本内容
    /// </summary>
    [Description("文本内容")]
    public string Text { get; set; }
}
