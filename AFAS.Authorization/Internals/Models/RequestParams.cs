using Mr1Ceng.Util;

namespace AFAS.Authorization.Internals.Models;

/// <summary>
/// 请求的参数
/// </summary>
internal class RequestParams
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public RequestParams()
    {
        Parameter = new List<Parameter>();
    }


    /// <summary>
    /// 参数1
    /// </summary>
    public IList<Parameter> Parameter { get; set; }
}
