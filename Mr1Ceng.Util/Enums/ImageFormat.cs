using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 图片格式
/// </summary>
public enum ImageFormat
{
    /// <summary>
    /// 默认格式
    /// </summary>
    [Description("默认格式")] DEFAULT = 0,

    /// <summary>
    /// 将原图保存成 bmp 格式
    /// </summary>
    [Description("BMP")] BMP = 1,

    /// <summary>
    /// 将原图保存成 jpg 格式，如果原图是 png、webp、bmp 存在透明通道，默认会把透明填充成白色。
    /// </summary>
    [Description("JPG")] JPG = 2,

    /// <summary>
    /// 将 gif 格式保存成 gif 格式，非 gif 格式是按原图格式保存
    /// </summary>
    [Description("GIF")] GIF = 3,

    /// <summary>
    /// 将原图保存成 png 格式。
    /// </summary>
    [Description("PNG")] PNG = 4,

    /// <summary>
    /// 将原图保存成 tiff 格式
    /// </summary>
    [Description("TIFF")] TIFF = 5,

    /// <summary>
    /// 将原图保存成 webp 格式。
    /// </summary>
    [Description("WEBP")] WEBP = 6
}
