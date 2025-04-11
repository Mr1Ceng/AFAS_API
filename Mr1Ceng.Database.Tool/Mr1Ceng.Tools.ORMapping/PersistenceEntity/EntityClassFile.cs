using Mr1Ceng.Util;

namespace Mr1Ceng.Tools.ORMapping.V7.PersistenceEntity;

public class EntityClassFile : ClassFile
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entityObject"></param>
    /// <param name="FileName"></param>
    public EntityClassFile(EntityObject entityObject, string FileName)
        : base(entityObject, FileName)
    {
    }

    /// <summary>
    /// 写类头文件
    /// </summary>
    /// <param name="sr"></param>
    protected override void WriteClassHead()
    {
        m_file.WriteLine("using Mr1Ceng.Util.Database;");
        m_file.WriteLine();
    }

    /// <summary>
    /// 写类文件
    /// </summary>
    protected override void WriteClass()
    {
        m_file.WriteLine("namespace " + NameSpace + ";");
        m_file.WriteLine($@"
/// <summary>
/// {m_EntityObject.EntityDescription}
/// </summary>
{Function} class {m_EntityObject.EntityName} : PersistentObject");
        WriteStartChar(GetTab(0));

        //写属性
        WriteAttribute();

        //写常量
        WriteConst();
        WriteEndChar(GetTab(0));
    }

    /// <summary>
    /// 写实体属性
    /// </summary>
    private void WriteAttribute()
    {
        m_file.WriteLine(GetTab(1) + "#region Attributes");
        foreach (Field field in m_EntityObject.FieldArrayLists)
        {
            var remark = GetString.FromObject(m_EntityObject.FieldRemarkLists.Where(x => x.Value == field.Name).FirstOrDefault()
                ?.Text);
            m_file.WriteLine($@"
    /// <summary>
    /// {remark}
    /// </summary>
    public {GetTypeSimpleName(field.DataType)} {field.Name} {{ get; set; }} = {GetTypeDefaultValue(field.DataType)};");
        }
        m_file.WriteLine();
        m_file.WriteLine(GetTab(1) + "#endregion");
        m_file.WriteLine();
    }

    /// <summary>
    /// 写常量信息
    /// </summary>
    private void WriteConst()
    {
        m_file.WriteLine(GetTab(1) + "#region Constant");
        foreach (Field field in m_EntityObject.FieldArrayLists)
        {
            var remark = GetString.FromObject(m_EntityObject.FieldRemarkLists.Where(x => x.Value == field.Name).FirstOrDefault()
                ?.Text);
            m_file.WriteLine($@"
    /// <summary>
    /// {remark}
    /// </summary>
    public const string F_{field.Name.ToUpper()} = ""{field.Name}"";");
        }
        m_file.WriteLine();
        m_file.WriteLine(GetTab(1) + "#endregion");
    }
}
