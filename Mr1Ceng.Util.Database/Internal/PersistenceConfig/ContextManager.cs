using System.Data;
using System.Reflection;
using System.Xml;
using Mr1Ceng.Util.Database.EntityHelper;
using Mr1Ceng.Util.Database.Internal.Core;
using Mr1Ceng.Util.Database.Internal.Provider;

namespace Mr1Ceng.Util.Database.Internal.PersistenceConfig;

/// <summary>
/// 根据XML配置文件，装载类映射关系
/// </summary>
internal sealed class ContextManager : IContextManager
{
    #region 属性

    /// <summary>
    /// 数据库连接池
    /// </summary>
    public Dictionary<string, DatabaseProvider> DatabasePool { get; } = new();

    /// <summary>
    /// ApplicationConfig中数据库映射的集合
    /// </summary>
    public Dictionary<string, DatabaseMap> DatabaseMaps { get; } = new();

    /// <summary>
    /// ORMapping中类映射的集合
    /// </summary>
    public Dictionary<string, ClassMap> ClassMaps { get; } = new();

    #endregion


    #region 接口方法

    /// <summary>
    /// 加载配置文件
    /// </summary>
    public void LoadConfig()
    {
        foreach (var dataSourceInfo in SystemEnvironment.Instance.DataSourceList)
        {
            //添加数据源配置信息
            AnalyseDataSourceInfo(dataSourceInfo);

            //解析ORMapping文件
            AnalyseORMapping(dataSourceInfo.ORMappingName);
        }

        //
        // SystemEnvironment.Instance.DataSourceList.ForEach(dataSourceInfo =>
        // {
        //     //添加数据源配置信息
        //     AnalyseDataSourceInfo(dataSourceInfo);
        //
        //     //解析ORMapping文件
        //     AnalyseORMapping(dataSourceInfo.ORMappingName);
        // });
    }

    #region 私有方法

    /// <summary>
    /// 添加数据源配置信息(子方法)
    /// </summary>
    /// <param name="dataSourceInfo"></param>
    /// <exception cref="BusinessException"></exception>
    private void AnalyseDataSourceInfo(DataSourceInfo dataSourceInfo)
    {
        DatabaseMaps.Add(dataSourceInfo.Name, new DatabaseMap(dataSourceInfo.Name));

        try
        {
            var connectionString
                = $"Data Source={dataSourceInfo.DataSource};Initial Catalog={dataSourceInfo.DatabaseName};User ID={dataSourceInfo.UserID};";
            if (dataSourceInfo.Password != "")
            {
                connectionString += $"Password={dataSourceInfo.Password}";
            }

            var rdb = new DatabaseProvider(dataSourceInfo.Name, connectionString);
            rdb.TryConnection(connectionString);

            DatabasePool.Add(dataSourceInfo.Name, rdb);
        }
        catch (BusinessException ex)
        {
            ex.Add(MethodBase.GetCurrentMethod());
            throw;
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config, "错误：解析数据源配置信息失败！");
        }
    }

    /// <summary>
    /// 解析ORMapping文件(子方法)
    /// </summary>
    /// <param name="fileName"></param>
    private void AnalyseORMapping(string fileName)
    {
        if (fileName == "")
        {
            return;
        }

        #region 获取ORMapping文件

        var filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
        if (!File.Exists(filePath))
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(),
                ExceptionInfoType.Config, "错误：ORMapping文件不存在：" + filePath + "！", new
                {
                    filePath
                });
        }

        #endregion

        #region 解析ORMapping信息

        try
        {
            var oXmlDocument = new XmlDocument();
            oXmlDocument.Load(filePath);
            XmlNodeReader oXmlReader = new(oXmlDocument);
            while (oXmlReader.Read())
            {
                if (oXmlReader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                if (oXmlReader.Name.Equals("class", StringComparison.CurrentCultureIgnoreCase))
                {
                    AnalyseClass(oXmlReader);
                }
            }
        }
        catch (BusinessException ex)
        {
            ex.Add(MethodBase.GetCurrentMethod());
            throw;
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config,
                "错误：解析ORMapping文件失败：" + filePath + "！", new
                {
                    filePath
                });
        }

        #endregion
    }

    /// <summary>
    /// 解析ORMapping文件中的单个Class(孙方法)
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private void AnalyseClass(XmlNodeReader node)
    {
        #region 数据检查

        var className = GetString.FromObject(node.GetAttribute("name"));
        var tableName = GetString.FromObject(node.GetAttribute("table"));
        var databaseName = GetString.FromObject(node.GetAttribute("datasource"));

        if (className == "" || databaseName == "" || tableName == "")
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config,
                "ClassMap 缺少className,databaseName,tableName 这些必要属性！");
        }
        var provider = DatabasePool[databaseName];
        if (provider == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config,
                "名为：" + databaseName + "的数据库连接池未找到！");
        }

        var databaseMap = DatabaseMaps[databaseName];
        if (databaseMap == null)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config,
                "名为：" + databaseName + "的数据库映射信息未找到！");
        }

        #endregion

        var tableMap = new TableMap(provider.GetTableName(tableName), databaseMap);

        var classMap = new ClassMap(provider, tableMap, className);


        #region 解析Class中的Attribute

        var clsDep = node.Depth;
        while (node.Read() && node.Depth > clsDep)
        {
            if (node.NodeType == XmlNodeType.Element && node.Name == "attribute")
            {
                classMap.AddAttributeMap(CreateAttributeMap(node, classMap));
            }
        }

        #endregion

        #region 必须拥有主键

        if (classMap.PrimaryKeys.Count == 0)
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config,
                "在表 [" + classMap.Table.TableName + "] 中未定义主键！");
        }

        #endregion

        ClassMaps.Add(classMap.ClassName, classMap);
    }

    /// <summary>
    /// 解析ORMapping文件中的Class中的三个Attribute属性(重孙方法)
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="classMap"></param>
    /// <returns></returns>
    private static AttributeMap CreateAttributeMap(XmlReader reader, ClassMap classMap)
    {
        #region 数据检查

        //  <attribute name="KeyId" column="KeyId" type="Integer" key="primary" increment="true" size="4" />
        //  <attribute name="DoctorId" column="DoctorId" type="String" size="16" />
        //  <attribute name="AdvertId" column="AdvertId" type="String" key="primary" size="12" />
        //  <attribute name="Class_ID" column="Class_ID" type="String" key="primary" size="20" />

        var data = new
        {
            AttrName = GetString.FromObject(reader.GetAttribute("name")),
            AttrColumn = GetString.FromObject(reader.GetAttribute("column")),
            DataType = GetString.FromObject(reader.GetAttribute("type")),
            DataKey = GetString.FromObject(reader.GetAttribute("key")),
            Increment = GetBoolean.FromObject(reader.GetAttribute("increment")),
            Size = Convert.ToInt32(reader.GetAttribute("size"))
        };

        if (data.AttrName == "")
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config, "属性字段不能为空", data);
        }
        if (data.AttrColumn == "")
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config, "数据表的列名不能为空", data);
        }

        #endregion

        var attributeMap = new AttributeMap(data.AttrName,
            new ColumnMap(classMap.PersistenceProvider.GetName(data.AttrColumn), classMap.Table)
            {
                ColumnSize = data.Size,
                IsPrimaryKey = data.DataKey.Equals("primary", StringComparison.CurrentCultureIgnoreCase),
                IsAutoIdentityKey = data.Increment
            });

        #region 转换字段类型

        switch (data.DataType.ToLower())
        {
            case "boolean":
                attributeMap.Column.ColumnType = DbType.Boolean;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Boolean);
                break;
            case "bigint":
                attributeMap.Column.ColumnType = DbType.Int64;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Int64);
                break;
            case "binary":
                attributeMap.Column.ColumnType = DbType.Binary;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Binary);
                break;
            case "currency":
                attributeMap.Column.ColumnType = DbType.Currency;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Currency);
                break;
            case "date":
                attributeMap.Column.ColumnType = DbType.DateTime;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.DateTime);
                break;
            case "dbdate":
                attributeMap.Column.ColumnType = DbType.Date;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Date);
                break;
            case "decimal":
                attributeMap.Column.ColumnType = DbType.Decimal;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Decimal);
                break;
            case "double":
                attributeMap.Column.ColumnType = DbType.Double;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Double);
                break;
            case "guid":
                attributeMap.Column.ColumnType = DbType.Guid;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Guid);
                break;
            case "object":
                attributeMap.Column.ColumnType = DbType.Object;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Object);
                break;
            case "single":
                attributeMap.Column.ColumnType = DbType.Single;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Single);
                break;
            case "smallint":
                attributeMap.Column.ColumnType = DbType.Int16;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Int16);
                break;
            case "tinyint":
                attributeMap.Column.ColumnType = DbType.SByte;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.SByte);
                break;
            case "long":
                attributeMap.Column.ColumnType = DbType.Int64;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Int64);
                break;
            case "integer":
                attributeMap.Column.ColumnType = DbType.Int32;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.Int32);
                break;
            case "varchar":
                attributeMap.Column.ColumnType = DbType.String;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.String);
                break;
            case "string":
                attributeMap.Column.ColumnType = DbType.String;
                attributeMap.SqlValueStringType = DatabaseProvider.GetSqlValueType(DbType.String);
                break;
            default:
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config,
                    $"不支持的数据字段类型:{data.DataType}", data);
        }

        #endregion

        return attributeMap;
    }

    #endregion

    #endregion
}
