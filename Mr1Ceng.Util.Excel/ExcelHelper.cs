using System.Data;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Mr1Ceng.Util.Excel.Models;

namespace Mr1Ceng.Util.Excel;

/// <summary>
/// Excel工具类
/// </summary>
public class ExcelHelper
{
    /// <summary>
    /// 把DataTable的内容生产Excel并作为Stream输出
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="sheetName">excel工作薄sheet的名称</param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns></returns>
    public static Stream ConvertDataTableToExcelStream(DataTable dt, string sheetName = "Sheet1", bool isFirstRowColumn = true)
        => GetExportStream([
            new ExportDataItem
            {
                Data = dt,
                SheetName = sheetName,
                IsFirstRowColumn = isFirstRowColumn
            }
        ]);

    /// <summary>
    /// 把DataTable的内容生产Excel并作为Stream输出
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static Stream GetExportStream(ExportDataItem item)
        => GetExportStream([item]);

    /// <summary>
    /// 把DataTable的内容生产Excel并作为Stream输出
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static Stream GetExportStream(List<ExportDataItem> list)
    {
        var ms = new NpoiMemoryStream();
        var workbook = new XSSFWorkbook();

        foreach (var item in list)
        {
            var sheet = workbook.CreateSheet(item.SheetName);

            #region 数据表头

            var rowIndex = 0;
            if (item.IsFirstRowColumn)
            {
                var headerRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in item.Data.Columns)
                {
                    headerRow.CreateCell(column.Ordinal)
                        .SetCellValue(column.Caption); //If Caption not set, returns the ColumnName value
                }
                rowIndex++;
            }

            #endregion

            #region 数据内容

            foreach (DataRow row in item.Data.Rows)
            {
                var dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in item.Data.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            #endregion
        }

        workbook.Write(ms);
        ms.Flush();
        ms.Position = 0;

        return ms;
    }


    #region GetDataTable

    /// <summary>
    /// 读取Excel文件流的数据
    /// </summary>
    /// <param name="stream">Excel文件流</param>
    /// <param name="sheetName"></param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns></returns>
    public static DataTable GetDataTableFromExcelStream(Stream stream,
        string sheetName = "",
        bool isFirstRowColumn = true)
    {
        IWorkbook workbook;
        try
        {
            workbook = new XSSFWorkbook(stream); //2007及以上版本
        }
        catch
        {
            workbook = new HSSFWorkbook(stream); //2003版本
        }

        var sheet = sheetName == "" ? workbook.GetSheetAt(0) : workbook.GetSheet(sheetName);
        if (sheet == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), $"没有找到指定的Sheet：{sheetName}");
        }

        return GetSheetData(sheet, isFirstRowColumn);
    }

    /// <summary>
    /// 读取Excel2003文件流的数据
    /// </summary>
    /// <param name="stream">Excel文件流</param>
    /// <param name="sheetName"></param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns></returns>
    public static DataTable GetDataTableFromExcel2003Stream(Stream stream,
        string sheetName = "",
        bool isFirstRowColumn = true)
    {
        IWorkbook workbook;
        try
        {
            workbook = new HSSFWorkbook(stream); //2003版本
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Office 2007"))
            {
                workbook = new XSSFWorkbook(stream); //2007及以上版本
            }
            else
            {
                throw;
            }
        }

        var sheet = sheetName == "" ? workbook.GetSheetAt(0) : workbook.GetSheet(sheetName);
        if (sheet == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), $"没有找到指定的Sheet：{sheetName}");
        }

        return GetSheetData(sheet, isFirstRowColumn);
    }

    /// <summary>
    /// 读取Excel2003文件流的数据
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
    /// <returns></returns>
    private static DataTable GetSheetData(ISheet sheet, bool isFirstRowColumn)
    {
        DataTable dt = new()
        {
            TableName = sheet.SheetName
        };

        var firstRow = sheet.GetRow(0);
        int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

        #region 数据表头

        int startRow;
        if (isFirstRowColumn)
        {
            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
            {
                var cell = firstRow.GetCell(i);
                if (cell != null)
                {
                    var cellValue = GetString.FromObject(cell.StringCellValue);
                    {
                        var column = new DataColumn(cellValue);
                        dt.Columns.Add(column);
                    }
                }
            }
            startRow = sheet.FirstRowNum + 1;
        }
        else
        {
            startRow = sheet.FirstRowNum;
        }

        #endregion

        #region 数据内容

        var rowCount = sheet.LastRowNum; //最后一列的标号
        for (var i = startRow; i <= rowCount; ++i)
        {
            var row = sheet.GetRow(i);
            if (row == null)
            {
                continue; //没有数据的行默认是null　　　　　　　
            }

            var dataRow = dt.NewRow();
            for (int j = row.FirstCellNum; j < cellCount; ++j)
            {
                if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                {
                    dataRow[j] = row.GetCell(j).ToString();
                    if (row.GetCell(j).CellType == CellType.Numeric
                        && DateUtil.IsCellDateFormatted(row.GetCell(j))) //如果类型是日期形式的，转成合理的日期字符串
                    {
                        dataRow[j] = row.GetCell(j).DateCellValue.ToString();
                    }
                }
            }
            dt.Rows.Add(dataRow);
        }

        #endregion

        dt.AcceptChanges();
        return dt;
    }

    #endregion
}
