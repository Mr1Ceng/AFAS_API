namespace Mr1Ceng.Util.Excel;

/// <summary>
/// 坑爹的NPOI的Bug，必须重写流
/// </summary>
internal class NpoiMemoryStream : MemoryStream
{
    /// <summary>
    /// 获取流是否关闭
    /// </summary>
    public bool IsClose
    {
        get;
    }

    public NpoiMemoryStream(bool close = false)
    {
        IsClose = close;
    }

    public override void Close()
    {
        if (IsClose)
        {
            base.Close();
        }
    }
}
