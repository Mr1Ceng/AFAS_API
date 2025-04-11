using System.Data;

namespace Mr1Ceng.Util.Excel.Models;

/// <summary>
/// 导出数据项
/// </summary>
public class ExportDataItem
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ExportDataItem()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public ExportDataItem(DataTable data, string sheetName = "Sheet1", bool isFirstRowColumn = true)
    {
        Data = data;
        SheetName = sheetName;
        IsFirstRowColumn = isFirstRowColumn;
    }

    /// <summary>
    /// DataTable数据
    /// </summary>
    public DataTable Data { get; set; } = new();

    /// <summary>
    /// excel工作薄sheet的名称
    /// </summary>
    public string SheetName { get; set; } = "Sheet1";

    /// <summary>
    /// 第一行是否是DataTable的列名
    /// </summary>
    public bool IsFirstRowColumn { get; set; } = true;
}
