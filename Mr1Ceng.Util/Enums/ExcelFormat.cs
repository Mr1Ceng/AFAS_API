using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// Excel文件格式
/// </summary>
public enum ExcelFormat
{
    /// <summary>
    /// *.csv
    /// </summary>
    [Description("*.csv")] CSV,

    /// <summary>
    /// *.xls，2003版本
    /// </summary>
    [Description("*.xls，2003版本")] XLS,

    /// <summary>
    /// *.xlsx，2007及以上版本
    /// </summary>
    [Description("*.xlsx，2007及以上版本")] XLSX
}
