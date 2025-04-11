using Mr1Ceng.Tools.ORMapping.V7.Common;

namespace Mr1Ceng.Tools.ORMapping.V7.PersistenceEntity;

public class ManagerClassFile : ClassFile
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entityObject"></param>
    /// <param name="FileName"></param>
    public ManagerClassFile(EntityObject entityObject, string FileName)
        : base(entityObject, FileName)
    {
    }

    /// <summary>
    /// 写类头文件
    /// </summary>
    protected override void WriteClassHead()
    {
        m_file.WriteLine("using System.Data;");
        m_file.WriteLine("using Mr1Ceng.Util.Database;");
        m_file.WriteLine();
    }

    /// <summary>
    /// 写类文件
    /// </summary>
    protected override void WriteClass()
    {
        var rmkparam = "";
        var keypara = "";
        var keyvalue = "";
        foreach (Field field in m_EntityObject.FieldArrayLists)
        {
            if (field.IsKey)
            {
                rmkparam += $@"
    /// <param name=""{field.Name.FirstCharToLower()}""></param>";
                keypara += GetTypeSimpleName(field.DataType) + " " + field.Name.FirstCharToLower() + ", ";
                keyvalue += $@"
            {field.Name} = {field.Name.FirstCharToLower()},";
            }
        }
        keypara = keypara.TrimEnd(' ').TrimEnd(',');
        keyvalue = keyvalue.TrimEnd(',');


        m_file.Write($@"namespace {NameSpace};

/// <summary>
/// {m_EntityObject.EntityDescription}
/// </summary>
{Function} class {m_EntityObject.EntityName}Manager : ManagerBase
{{
");

        m_file.WriteLine(GetTab(1) + "#region 基础成员");

        if (m_EntityObject.EntityType == "U")
        {
            #region InsEntityObject (基础成员)新增实体对象

            m_file.Write(@"
    /// <summary>
    /// (基础成员)新增实体对象
    /// </summary>
    /// <param name=""obj"">实体对象</param>
    public static void InsEntityObject({0} obj) => obj.Insert();
", m_EntityObject.EntityName);


            m_file.Write(@"
    /// <summary>
    /// (基础成员)新增实体对象(事务中)
    /// </summary>
    /// <param name=""obj"">实体对象</param>
    /// <param name=""ts"">事务</param>
    public static void InsEntityObject({0} obj, Transaction ts) => obj.Insert(ts);
", m_EntityObject.EntityName);

            #endregion

            #region UpdEntityObject (基础成员)修改实体对象

            m_file.Write(@"
    /// <summary>
    /// (基础成员)修改实体对象
    /// </summary>
    /// <param name=""obj"">实体对象</param>
    public static void UpdEntityObject({0} obj) => obj.Update();
", m_EntityObject.EntityName);

            m_file.Write(@"
    /// <summary>
    /// (基础成员)修改实体对象(事务中)
    /// </summary>
    /// <param name=""obj"">实体对象</param>
    /// <param name=""ts"">事务</param>
    public static void UpdEntityObject({0} obj, Transaction ts) => obj.Update(ts);
", m_EntityObject.EntityName);

            #endregion

            #region DelEntityObject (基础成员)删除实体对象

            m_file.Write($@"
    /// <summary>
    /// (基础成员)删除实体对象
    /// </summary>{rmkparam}
    public static void DelEntityObject({keypara})
    {{
        var obj = new {m_EntityObject.EntityName}
        {{{keyvalue}
        }};
        obj.Delete();
    }}
");

            m_file.Write($@"
    /// <summary>
    /// (基础成员)删除实体对象(事务中)
    /// </summary>{rmkparam}
    /// <param name=""ts"">事务</param>
    public static void DelEntityObject({keypara}, Transaction ts)
    {{
        var obj = new {m_EntityObject.EntityName}
        {{{keyvalue}
        }};
        obj.Delete(ts);
    }}
");

            #endregion

            #region DelEntityObjects (基础成员)删除实体集合

            m_file.Write(@"
    /// <summary>
    /// (基础成员)删除实体集合
    /// </summary>
    /// <returns></returns>
    public static int DelEntityObjects() => GetDeleteCriteria().Execute();
");

            m_file.Write(@"
    /// <summary>
    /// (基础成员)删除实体集合(事务中)
    /// </summary>
    /// <param name=""ts"">事务</param>
    /// <returns></returns>
    public static int DelEntityObjects(Transaction ts) => GetDeleteCriteria().Execute(ts);
");

            #endregion

            #region GetEntityObject (基础成员)取回实体对象

            m_file.Write($@"
    /// <summary>
    /// (基础成员)取回实体对象
    /// </summary>{rmkparam}
    /// <returns></returns>
    public static {m_EntityObject.EntityName} GetEntityObject({keypara})
    {{
        var obj = new {m_EntityObject.EntityName}
        {{{keyvalue}
        }};
        obj.Retrieve();
        return obj;
    }}
");

            m_file.Write($@"
    /// <summary>
    /// (基础成员)取回实体对象(事务中)
    /// </summary>{rmkparam}
    /// <param name=""ts"">事务</param>
    /// <returns></returns>
    public static {m_EntityObject.EntityName} GetEntityObject({keypara}, Transaction ts)
    {{
        var obj = new {m_EntityObject.EntityName}
        {{{keyvalue}
        }};
        obj.Retrieve(ts);
        return obj;
    }}
");

            #endregion
        }

        #region GetEntityObjects (基础成员)取回实体集合

        m_file.Write($@"
    /// <summary>
    /// (基础成员)取回实体集合
    /// </summary>
    /// <returns>List</returns>
    public static List<{m_EntityObject.EntityName}> GetEntityObjects() => GetRetrieveCriteria().GetCollection();
");

        m_file.Write($@"
    /// <summary>
    /// (基础成员)取回实体集合
    /// </summary>
    /// <param name=""ts"">事务</param>
    /// <returns>List</returns>
    public static List<{m_EntityObject.EntityName}> GetEntityObjects(Transaction ts) => GetRetrieveCriteria().GetCollection(ts);
");

        #endregion

        #region GetEntityObjects (基础成员)取回实体数据

        m_file.Write(@"
    /// <summary>
    /// (基础成员)取回实体数据
    /// </summary>
    /// <returns>DataTable</returns>
    public static DataTable GetDataTable() => GetRetrieveCriteria().GetDataTable();
");

        m_file.Write(@"
    /// <summary>
    /// (基础成员)取回实体数据
    /// </summary>
    /// <param name=""ts"">事务</param>
    /// <returns>DataTable</returns>
    public static DataTable GetDataTable(Transaction ts) => GetRetrieveCriteria().GetDataTable(ts);
");

        #endregion


        m_file.WriteLine();
        m_file.WriteLine(GetTab(1) + "#endregion");
        m_file.WriteLine();
        m_file.WriteLine();
        m_file.WriteLine(GetTab(1) + "#region 私有方法");

        #region GetDataTable (私有方法)字段查询

        m_file.Write($@"
    /// <summary>
    /// (私有方法)字段查询
    /// </summary>
    /// <returns></returns>
    private static RetrieveCriteria<{m_EntityObject.EntityName}> GetRetrieveCriteria() => new();
", m_EntityObject.EntityName);

        #endregion

        if (m_EntityObject.EntityType == "U")
        {
            #region GetDataTable (私有方法)字段删除

            m_file.Write($@"
    /// <summary>
    /// (私有方法)字段删除
    /// </summary>
    /// <returns></returns>
    private static DeleteCriteria<{m_EntityObject.EntityName}> GetDeleteCriteria() => new();
", m_EntityObject.EntityName);

            #endregion
        }

        m_file.WriteLine();
        m_file.WriteLine(GetTab(1) + "#endregion");
        m_file.WriteLine();
        m_file.WriteLine();
        m_file.WriteLine(GetTab(1) + "#region 静态方法");

        m_file.Write($@"
    /// <summary>
    /// 把实体数组转换为DataTable
    /// </summary>
    /// <param name=""list""></param>
    /// <returns></returns>
    public static DataTable ToDataTable(IEnumerable<{m_EntityObject.EntityName}> list)
    {{
        var dt = ToDataTable();
");
        m_file.WriteLine(GetTab(2) + "foreach (var item in list)");
        m_file.WriteLine(GetTab(2) + "{");
        m_file.WriteLine(GetTab(3) + "var dr = dt.NewRow();");
        foreach (Field field in m_EntityObject.FieldArrayLists)
        {
            m_file.WriteLine(GetTab(3) + $"dr[\"{field.Name}\"] = item.{field.Name};");
        }
        m_file.WriteLine(GetTab(3) + "dt.Rows.Add(dr);");
        m_file.WriteLine(GetTab(2) + "}");
        m_file.WriteLine(GetTab(2) + "dt.AcceptChanges();");
        m_file.WriteLine(GetTab(2) + "return dt;");
        m_file.WriteLine(GetTab(1) + "}");

        m_file.Write(@"
    /// <summary>
    /// 获取空白的DataTable
    /// </summary>
    /// <returns></returns>
    public static DataTable ToDataTable()
    {
        var dt = new DataTable();
");
        foreach (Field field in m_EntityObject.FieldArrayLists)
        {
            m_file.WriteLine(GetTab(2) + $"dt.Columns.Add(\"{field.Name}\", typeof({GetTypeSimpleName(field.DataType)}));");
        }
        m_file.WriteLine(GetTab(2) + "return dt;");
        m_file.WriteLine(GetTab(1) + "}");

        m_file.WriteLine();
        m_file.WriteLine(GetTab(1) + "#endregion");

        WriteEndChar(GetTab(0));
    }
}
