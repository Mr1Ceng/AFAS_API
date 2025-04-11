namespace Mr1Ceng.Tools.ORMapping.V7.Common;

/// <summary>
/// ConnectInfo 的摘要说明。
/// </summary>
public class ConnectInfo
{
    public string Server { get; set; } = "";
    public string DataBase { get; set; } = "";
    public string UserID { get; set; } = "";
    public string Password { get; set; } = "";
    public string Encrypt { get; set; } = "";

    /// <summary>
    /// 取连接串
    /// </summary>
    public string ConnectionString
    {
        get
        {
            var strConnect = "server=" + Server;
            strConnect += ";database=" + DataBase;
            strConnect += ";user id=" + UserID;
            strConnect += ";password=" + Password;
            strConnect += ";max pool size=1024";
            return strConnect;
        }
    }
}
