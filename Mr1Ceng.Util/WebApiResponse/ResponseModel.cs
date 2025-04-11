using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// WebApi返回对象
/// </summary>
public class ResponseModel<T>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="data"></param>
    public ResponseModel(T data)
    {
        Status = ResponseStatus.SUCCESS;
        Data = data;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="status"></param>
    [Description("构造函数")]
    public ResponseModel(ResponseStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// 返回状态
    /// </summary>
    [Description("返回状态")]
    public ResponseStatus Status { get; }

    /// <summary>
    /// 返回数据
    /// </summary>
    [Description("返回数据")]
    public T? Data { get; set; }
}

/// <summary>
/// WebApi返回对象
/// </summary>
public class ResponseModel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ResponseModel()
    {
        Status = ResponseStatus.SUCCESS;
        Data = "";
    }

    /// <summary>
    /// 返回状态
    /// </summary>
    [Description("返回状态")]
    public ResponseStatus Status { get; }

    /// <summary>
    /// 返回数据
    /// </summary>
    [Description("返回数据")]
    public string Data { get; set; }
}
