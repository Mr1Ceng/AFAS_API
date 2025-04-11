# 提供Excel相关服务的类库

基于 NPOI 实现 Excel 的基本操作

# 1、版本历史

- 2.3.0（2022-11-06），更新nuget引用
- 2.1.0（2022-09-10），更新nuget引用
- 2.0.0（2022-08-27），在1.2.0基础上更新Nuget引用

# 2、功能描述

- ExcelHelper.`ConvertDataTableToExcelStream` 把DataTable的内容生产Excel并作为Stream输出

#####读取Excel文件流的数据

- ExcelHelper.`GetDataTableFromExcelStream` 读取Excel文件流的数据到DataTable
- ExcelHelper.`GetDataTableFromExcel2003Stream` 读取Excel2003文件流的数据

# 3、配置信息

无额外配置信息