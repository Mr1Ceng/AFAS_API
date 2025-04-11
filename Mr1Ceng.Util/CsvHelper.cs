using System.Data;
using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// Csv工具类
/// </summary>
public class CsvHelper
{
    #region 数据获取

    /// <summary>
    /// 读取Csv文件流的数据
    /// </summary>
    /// <param name="stream">文件UTF8编码格式的Stream</param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns></returns>
    public static DataTable GetDataFromStream(Stream stream, bool isFirstRowColumn = true)
        => GetDataFromStream(stream, Encoding.UTF8, isFirstRowColumn);

    /// <summary>
    /// 读取Csv文件数据
    /// </summary>
    /// <param name="stream">文件的Stream</param>
    /// <param name="encoding">编码格式</param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns></returns>
    public static DataTable GetDataFromStream(Stream stream, Encoding encoding, bool isFirstRowColumn = true)
        => GetStreamReaderData(new StreamReader(stream, encoding), isFirstRowColumn);

    /// <summary>
    /// 读取Csv文件数据
    /// </summary>
    /// <param name="filePath">文件的物理路径，文件是UTF8编码格式</param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns></returns>
    public static DataTable GetDataFromFile(string filePath, bool isFirstRowColumn = true)
        => GetDataFromFile(filePath, Encoding.UTF8, isFirstRowColumn);

    /// <summary>
    /// 读取Csv文件数据
    /// </summary>
    /// <param name="filePath">文件的物理路径</param>
    /// <param name="encoding">编码格式</param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns></returns>
    public static DataTable GetDataFromFile(string filePath, Encoding encoding, bool isFirstRowColumn = true)
    {
        using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read);
        return GetStreamReaderData(new StreamReader(fs, encoding), isFirstRowColumn);
    }

    /// <summary>
    /// 解析CSV数据
    /// </summary>
    /// <param name="sr"></param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns></returns>
    private static DataTable GetStreamReaderData(StreamReader sr, bool isFirstRowColumn)
    {
        DataTable dt = new();

        //分隔符
        string[] separators = [","];

        //记录每次读取的一行记录
        string? strLine;

        //逐行读取CSV文件
        while ((strLine = sr.ReadLine()) != null)
        {
            strLine = strLine.Trim(); //去除头尾空格

            //记录每行记录中的各字段内容
            var arrayLine = strLine.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var dtColumns = arrayLine.Length; //列的个数

            if (isFirstRowColumn) //建立表头
            {
                for (var i = 0; i < dtColumns; i++)
                {
                    dt.Columns.Add(arrayLine[i]); //每一列名称
                }
            }
            else //表内容
            {
                var dataRow = dt.NewRow(); //新建一行
                for (var j = 0; j < dtColumns; j++)
                {
                    dataRow[j] = arrayLine[j];
                }
                dt.Rows.Add(dataRow); //添加一行
            }
        }
        sr.Close();

        dt.AcceptChanges();
        return dt;
    }

    #endregion


    #region Csv文件

    /// <summary>
    /// 写入CSV
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="dt">要写入的datatable</param>
    public static void WriteFile(string fileName, DataTable dt)
    {
        FileStream fs;
        StreamWriter sw;
        var data = string.Empty;

        //判断文件是否存在,存在就不再次写入列名
        if (!File.Exists(fileName))
        {
            fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs, Encoding.UTF8);

            //写出列名称
            for (var i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName;
                if (i < dt.Columns.Count - 1)
                {
                    data += ","; //中间用，隔开
                }
            }
            sw.WriteLine(data);
        }
        else
        {
            fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs, Encoding.UTF8);
        }

        //写出各行数据
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            data = string.Empty;
            for (var j = 0; j < dt.Columns.Count; j++)
            {
                data += dt.Rows[i][j].ToString();
                if (j < dt.Columns.Count - 1)
                {
                    data += ","; //中间用，隔开
                }
            }
            sw.WriteLine(data);
        }
        sw.Close();
        fs.Close();
    }

    /// <summary>
    /// 读取CSV文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static DataTable ReadFile(string fileName)
    {
        DataTable dt = new();
        FileStream fs = new(fileName, FileMode.Open, FileAccess.Read);
        StreamReader sr = new(fs, Encoding.UTF8);

        //分隔符
        string[] separators = [","];

        //判断，若是第一次，建立表头
        var isFirst = true;


        //记录每次读取的一行记录
        string? strLine;

        //逐行读取CSV文件
        while ((strLine = sr.ReadLine()) != null)
        {
            strLine = strLine.Trim(); //去除头尾空格

            //记录每行记录中的各字段内容
            var arrayLine = strLine.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var dtColumns = arrayLine.Length; //列的个数

            if (isFirst) //建立表头
            {
                for (var i = 0; i < dtColumns; i++)
                {
                    dt.Columns.Add(arrayLine[i]); //每一列名称
                }
                isFirst = false;
            }
            else //表内容
            {
                var dataRow = dt.NewRow(); //新建一行
                for (var j = 0; j < dtColumns; j++)
                {
                    dataRow[j] = arrayLine[j];
                }
                dt.Rows.Add(dataRow); //添加一行
            }
        }
        sr.Close();
        fs.Close();

        return dt;
    }

    #endregion
}
