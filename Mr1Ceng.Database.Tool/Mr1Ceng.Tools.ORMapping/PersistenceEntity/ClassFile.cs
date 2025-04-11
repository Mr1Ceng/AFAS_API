namespace Mr1Ceng.Tools.ORMapping.V7.PersistenceEntity;

/// <summary>
/// ClassFile 的摘要说明。
/// </summary>
public class ClassFile
{
    protected string m_FileName;
    protected StreamWriter m_file;
    protected EntityObject m_EntityObject;
    protected string m_NameSpace;
    protected string m_Function;

    protected const int IntTab = 4;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entityObject"></param>
    /// <param name="FileName"></param>
    public ClassFile(EntityObject entityObject, string FileName)
    {
        m_EntityObject = entityObject;
        m_FileName = FileName;
    }

    /// <summary>
    /// 名称空间
    /// </summary>
    public string NameSpace
    {
        get => m_NameSpace;
        set => m_NameSpace = value;
    }

    /// <summary>
    /// 类的私密范围（public 或 internal）
    /// </summary>
    public string Function
    {
        get => m_Function;
        set => m_Function = value;
    }

    public void BuildFile()
    {
        m_file = File.CreateText(m_FileName);

        //写实体类头文件
        WriteClassHead();

        //写类文件
        WriteClass();

        //关闭文件
        m_file.Close();
    }


    /// <summary>
    /// 写类头文件
    /// </summary>
    /// <param name="sr"></param>
    protected virtual void WriteClassHead()
    {
    }

    /// <summary>
    /// 写类文件
    /// </summary>
    protected virtual void WriteClass()
    {
    }

    protected void WriteStartChar(string space)
    {
        m_file.WriteLine(space + "{");
    }

    protected void WriteEndChar(string space)
    {
        m_file.WriteLine(space + "}");
    }

    /// <summary>
    /// 取得制表符
    /// </summary>
    /// <param name="tab"></param>
    /// <returns></returns>
    protected string GetTab(int tab)
    {
        var space = "";
        for (var i = 0; i < tab * IntTab; i++)
        {
            space += " ";
        }
        return space;
    }

    /// <summary>
    /// 根据类型取得简化名称
    /// </summary>
    /// <param name="dataType"></param>
    /// <returns></returns>
    protected string GetTypeSimpleName(string dataType)
    {
        var type = dataType;
        switch (dataType)
        {
            case "System.Boolean":
                type = "bool";
                break;
            case "System.Int64":
                type = "long";
                break;
            case "System.Int32":
                type = "int";
                break;
            case "System.Double":
                type = "double";
                break;
            case "System.Single":
                type = "float";
                break;
            case "System.Int16":
                type = "short";
                break;
            case "System.Byte":
                type = "byte";
                break;
            case "System.Decimal":
                type = "decimal";
                break;
            case "System.Byte[]":
                type = "byte[]";
                break;
            case "System.String":
                type = "string";
                break;
        }
        return type;
    }


    /// <summary>
    /// 根据类型取得默认值
    /// </summary>
    /// <param name="dataType"></param>
    /// <returns></returns>
    protected string GetTypeDefaultValue(string dataType)
    {
        var type = "\"\"";
        switch (dataType)
        {
            case "System.Boolean":
                type = "false";
                break;
            case "System.Int64":
            case "System.Int32":
            case "System.Double":
            case "System.Single":
            case "System.Int16":
            case "System.Byte":
            case "System.Decimal":
                type = "0";
                break;
            case "System.DateTime":
                type = "DateTime.Now";
                break;
            case "System.Byte[]":
                type = "null";
                break;
            case "System.String":
                type = "\"\"";
                break;
            default:
                type = "\"\"";
                break;
        }
        return type;
    }
}
