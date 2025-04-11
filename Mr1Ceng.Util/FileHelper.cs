using System.Reflection;
using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// 文件工具类
/// </summary>
public class FileHelper
{
    #region 文件工具

    /// <summary>
    /// 剔除文件名的非法字符串
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="maxLength">字符串最大长度</param>
    /// <returns></returns>
    public static string TrimFileName(string? fileName, int maxLength = 200)
    {
        if (fileName == null)
        {
            return "";
        }

        return GetString.FromObject(fileName
            .ReplaceLineEndings("\r\n")
            .Replace("\\", string.Empty)
            .Replace("/", string.Empty)
            .Replace(":", string.Empty)
            .Replace("*", string.Empty)
            .Replace("?", string.Empty)
            .Replace("\"", string.Empty)
            .Replace("<", string.Empty)
            .Replace(">", string.Empty)
            .Replace("|", string.Empty), maxLength);
    }

    /// <summary>
    /// 根据路径获取文件名称
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetFileName(string filePath) => Path.GetFileName(filePath);

    /// <summary>
    /// 获取文件名称的后缀名
    /// </summary>
    /// <param name="path"></param>
    /// <param name="includeDot">是否包含.</param>
    /// <returns></returns>
    public static string GetPathExtension(string? path, bool includeDot = false)
    {
        var ext = Path.GetExtension(path);
        if (ext == null)
        {
            return "";
        }
        return includeDot ? ext : ext.TrimStart('.');
    }

    /// <summary>
    /// 计算文件大小函数(保留两位小数),Size为字节大小
    /// </summary>
    /// <param name="size">初始文件大小</param>
    /// <returns></returns>
    public static string FormatFileSize(long size)
    {
        var num = 1024.00; //byte

        if (size < num)
        {
            return size + "B";
        }
        if (size < Math.Pow(num, 2))
        {
            return (size / num).ToString("f2") + "KB"; //kb
        }
        if (size < Math.Pow(num, 3))
        {
            return (size / Math.Pow(num, 2)).ToString("f2") + "MB"; //M
        }
        if (size < Math.Pow(num, 4))
        {
            return (size / Math.Pow(num, 3)).ToString("f2") + "GB"; //G
        }

        return (size / Math.Pow(num, 4)).ToString("f2") + "TB"; //T
    }

    #endregion


    #region 读取文件

    /// <summary>
    /// 读取文件到二进制
    /// </summary>
    /// <param name="path">要读取的文件路径</param>
    /// <returns></returns>
    public static byte[] ReadFileToBuffer(string? path)
    {
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        return Array.Empty<byte>();
    }

    /// <summary>
    /// 读取文件到Base64
    /// </summary>
    /// <param name="path">要读取的文件路径</param>
    /// <returns></returns>
    public static string ReadFileToBase64(string? path)
    {
        if (File.Exists(path))
        {
            return GetBase64.FromBytes(ReadFileToBuffer(path));
        }
        return string.Empty;
    }

    /// <summary>
    /// 读取文件文本
    /// </summary>
    /// <param name="path">要读取的文件路径</param>
    /// <returns></returns>
    public static string ReadFileToString(string? path) => ReadFileToString(path, Encoding.UTF8);

    /// <summary>
    /// 读取文件文本
    /// </summary>
    /// <param name="path">要读取的文件路径</param>
    /// <param name="encoding">编码方式</param>
    /// <returns></returns>
    public static string ReadFileToString(string? path, Encoding encoding)
    {
        if (File.Exists(path))
        {
            return File.ReadAllText(path, encoding);
        }
        return string.Empty;
    }

    #endregion


    #region 保存文件

    /// <summary>
    /// 将二进制写入文件
    /// </summary>
    /// <param name="path">要保存的文件路径</param>
    /// <param name="buffer">文件二进制</param>
    public static void SaveFileFromBuffer(string? path, byte[]? buffer)
    {
        if (path == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), "文件路径不能为空");
        }
        if (buffer == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), "文件内容不能为空");
        }

        DeleteFile(path);
        FileStream fileStream = new(path, FileMode.Create);
        BinaryWriter binaryWriter = new(fileStream);
        binaryWriter.Write(buffer);
        binaryWriter.Close();
    }

    /// <summary>
    /// 将二进制写入文件
    /// </summary>
    /// <param name="path">要保存的文件路径</param>
    /// <param name="base64">Base64编码的文件内容</param>
    public static void SaveFileFromBase64(string? path, string? base64)
        => SaveFileFromBuffer(path, GetBuffer.FromBase64(base64));

    /// <summary>
    /// 将Stream写入文件
    /// </summary>
    /// <param name="path">要保存的文件路径</param>
    /// <param name="stream">文件的stream</param>
    public static void SaveFileFromStream(string? path, Stream? stream)
        => SaveFileFromBuffer(path, GetBuffer.FromStream(stream));


    #region SaveFileFromString

    /// <summary>
    /// 将String写入文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    public static void SaveFileFromString(string? path, string? content)
        => SaveFileFromString(path, content, Encoding.UTF8);

    /// <summary>
    /// 将String写入文件
    /// </summary>
    /// <param name="path">要保存的文件路径</param>
    /// <param name="content">文件String值</param>
    /// <param name="encoding">编码方式</param>
    public static void SaveFileFromString(string? path, string? content, Encoding encoding)
    {
        if (path == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), "文件路径不能为空");
        }

        DeleteFile(path);
        FileStream fileStream = new(path, FileMode.OpenOrCreate);
        StreamWriter streamWriter = new(fileStream, encoding);
        streamWriter.WriteLine(content);
        streamWriter.Close();
    }

    /// <summary>
    /// 将String写入文件
    /// </summary>
    /// <param name="path">要保存的文件路径</param>
    /// <param name="content">文件String值</param>
    /// <param name="append">是否允许追加写入</param>
    public static void SaveFileFromString(string? path, string? content, bool append)
        => SaveFileFromString(path, content, append, Encoding.UTF8);

    /// <summary>
    /// 将String写入文件
    /// </summary>
    /// <param name="path">要保存的文件路径</param>
    /// <param name="content">文件String值</param>
    /// <param name="append">是否允许追加写入</param>
    /// <param name="encoding">编码方式</param>
    public static void SaveFileFromString(string? path, string? content, bool append, Encoding encoding)
    {
        if (File.Exists(path) && append)
        {
            //如果文件存在，并且允许追加写入
            StreamWriter streamWriter = new(path, append, encoding);
            streamWriter.WriteLine(content);
            streamWriter.Close();
        }
        else
        {
            //文件不存在或不允许追加写入
            SaveFileFromString(path, content, encoding);
        }
    }

    #endregion

    #endregion


    #region 删除文件

    /// <summary>
    /// 根据物理路径删除文件
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteFile(string? path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    /// <summary>
    /// 删除整个文件夹
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="recursive">是否连同文件夹中的文件一起删除</param>
    public static void DeleteFolder(string? folder, bool recursive = true)
    {
        if (!Directory.Exists(folder))
        {
            //文件夹如果不存在，直接返回
            return;
        }
        new DirectoryInfo(folder).Delete(recursive);
    }

    #endregion
}
