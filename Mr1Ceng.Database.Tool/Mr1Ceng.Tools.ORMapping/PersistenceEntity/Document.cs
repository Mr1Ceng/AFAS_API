using System.Collections;
using Mr1Ceng.Tools.ORMapping.V7.Common;

namespace Mr1Ceng.Tools.ORMapping.V7.PersistenceEntity;

/// <summary>
/// Document 的摘要说明。
/// </summary>
public class Document
{
    private DBOperator m_DBOperator;
    private readonly string m_FilePath;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filepath"></param>
    public Document(string filepath)
    {
        var configuration = new Configuration(ConfigFile.PERSISTENCEENTITY);
        configuration.Retrieve();

        EntityObjects = new ArrayList();
        m_DBOperator = DBOperator.Instance(configuration);
        if (filepath.Substring(filepath.Length - 1, 1) == @"\")
        {
            m_FilePath = filepath;
        }
        else
        {
            m_FilePath = filepath + @"\";
        }
    }

    /// <summary>
    /// 写文件
    /// </summary>
    public void BuildFile(CommonInfo commonInfo)
    {
        try
        {
            XmlFile xmlFile = new(m_FilePath, EntityObjects);
            xmlFile.DataSource = DataSource;
            xmlFile.BuildORMappingXmlFile();

            if (commonInfo.AppSettingsFiles)
            {
                xmlFile.BuildApplicationXmlFile();
            }

            if (commonInfo.EntityClassFiles)
            {
                WriteClass();
            }
        }
        catch {}
    }

    /// <summary>
    /// 写类文件
    /// </summary>
    private void WriteClass()
    {
        var folderEntity = m_FilePath + "EntityObject\\";
        var folderManager = m_FilePath + "EntityManager\\";
        if (!Directory.Exists(folderEntity))
        {
            Directory.CreateDirectory(folderEntity);
        }
        if (!Directory.Exists(folderManager))
        {
            Directory.CreateDirectory(folderManager);
        }

        foreach (EntityObject entityObject in EntityObjects)
        {
            var entityClassFile = new EntityClassFile(entityObject,
                folderEntity + entityObject.EntityName + ".cs");
            entityClassFile.NameSpace = NameSpace;
            entityClassFile.Function = Function;
            entityClassFile.BuildFile();

            var managerClassFile = new ManagerClassFile(entityObject,
                folderManager + entityObject.EntityName + "Manager.cs");
            managerClassFile.NameSpace = NameSpace;
            managerClassFile.Function = Function;
            managerClassFile.BuildFile();
        }
    }

    #region 属性集合

    /// <summary>
    /// 实体对象集
    /// </summary>
    public ArrayList EntityObjects
    {
        get;
        set;
    }

    public string DataSource
    {
        get;
        set;
    }

    public string SystemId
    {
        get;
        set;
    }

    public string UserCompany
    {
        get;
        set;
    }

    public string DevelopeCompany
    {
        get;
        set;
    }

    public string NameSpace
    {
        get;
        set;
    }

    public string Function
    {
        get;
        set;
    }

    #endregion
}
