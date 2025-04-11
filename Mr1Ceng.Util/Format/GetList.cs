using System.Data;

namespace Mr1Ceng.Util;

/// <summary>
/// 获取数据列表
/// </summary>
public class GetList
{
    /// <summary>
    /// 将DataTable转换成List
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="clearBlank"></param>
    /// <returns></returns>
    public static List<DataRow> FromDataTable(DataTable dt, bool clearBlank = false)
    {
        if (clearBlank)
        {
            return dt.AsEnumerable().Where(dr =>
            {
                var checkEmptyRow = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (GetString.FromObject(dr[column.ColumnName]) != "")
                    {
                        checkEmptyRow = false;
                        break;
                    }
                }
                return !checkEmptyRow;
            }).ToList();
        }
        return dt.AsEnumerable().ToList();
    }

    /// <summary>
    /// 将DataTable转换成List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static List<T> FromDataTable<T>(DataTable? dt)
    {
        if (dt == null || dt.Columns.Count == 0)
        {
            return [];
        }

        var array = new string[dt.Columns.Count];
        for (var i = 0; i < dt.Columns.Count; i++)
        {
            array[i] = dt.Columns[i].Caption;
        }

        var list = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            var val = Activator.CreateInstance<T>();
            if (val != null)
            {
                var properties = val.GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    if (dt.Columns.Contains(propertyInfo.Name) && propertyInfo.CanWrite
                        && row[propertyInfo.Name] != DBNull.Value)
                    {
                        switch (propertyInfo.PropertyType.FullName)
                        {
                            case "System.Guid":
                                propertyInfo.SetValue(val, Guid.Parse(GetString.FromObject(row[propertyInfo.Name])), null);
                                break;
                            case "System.Boolean":
                                propertyInfo.SetValue(val, GetBoolean.FromObject(row[propertyInfo.Name]), null);
                                break;

                            default:
                            {
                                propertyInfo.SetValue(val,
                                    propertyInfo.PropertyType == typeof(int)
                                        ? GetInt.FromObject(row[propertyInfo.Name])
                                        : row[propertyInfo.Name], null);
                                break;
                            }
                        }
                    }
                }
                list.Add(val);
            }
        }

        return list;
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dt"></param>
    /// <param name="replaceColumn">需要替换中文映射字段</param>
    /// <returns></returns>
    public static List<T> FromDataTable<T>(DataTable? dt, Dictionary<string, string> replaceColumn)
    {
        if (dt == null || dt.Columns.Count == 0)
        {
            return [];
        }

        var array = new string[dt.Columns.Count];
        for (var i = 0; i < dt.Columns.Count; i++)
        {
            array[i] = dt.Columns[i].Caption;
        }

        var list = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            var val = Activator.CreateInstance<T>();
            if (val != null)
            {
                var properties = val.GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    if (replaceColumn.TryGetValue(propertyInfo.Name, out var columnName))
                    {
                        if (string.IsNullOrEmpty(columnName))
                        {
                            continue;
                        }
                        if (dt.Columns.Contains(columnName) && propertyInfo.CanWrite && row[columnName] != DBNull.Value)
                        {
                            switch (propertyInfo.PropertyType.FullName)
                            {
                                case "System.Guid":
                                    propertyInfo.SetValue(val, Guid.Parse(GetString.FromObject(row[columnName])), null);
                                    break;
                                case "System.Boolean":
                                    propertyInfo.SetValue(val, GetBoolean.FromObject(row[columnName]), null);
                                    break;

                                default:
                                {
                                    propertyInfo.SetValue(val,
                                        propertyInfo.PropertyType == typeof(int)
                                            ? GetInt.FromObject(row[columnName])
                                            : row[columnName],
                                        null);
                                    break;
                                }
                            }
                        }
                    }
                }
                list.Add(val);
            }
        }

        return list;
    }
}
