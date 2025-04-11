namespace Mr1Ceng.Tools.ORMapping.V7.PersistenceEntity;

/// <summary>
/// ConnectInfo 的摘要说明。
/// </summary>
public class CustomerInfo
{
    public CustomerInfo()
    {
        DataSource = "";
        NameSpace = "";
        Function = "";
        Language = "";
    }

    public string DataSource { get; set; }

    public string NameSpace { get; set; }

    public string Function { get; set; }

    public string Language { get; set; }
}
