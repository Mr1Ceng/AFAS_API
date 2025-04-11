using System.Net;
using System.Net.Http.Headers;
using System.Reflection;

namespace Mr1Ceng.Util;

/// <summary>
/// 封装Http请求
/// </summary>
public class HttpClientHelper
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="timeout">默认300秒超时</param>
    public HttpClientHelper(int timeout = 300)
    {
        HttpClientHandler handler = new()
        {
            UseCookies = true
        };
        httpClient = new HttpClient(handler)
        {
            DefaultRequestVersion = new Version(2, 0), //默认HTTP/2
            Timeout = new TimeSpan(0, 0, 0, timeout) //超时时间             
        };
    }

    /// <summary>
    /// HttpClient对象
    /// </summary>
    private readonly HttpClient httpClient;


    #region Get

    /// <summary>
    /// 同步Get请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public string Get(string url, string authorization = "") => GetAsync(url, authorization).Result;

    /// <summary>
    /// 同步Get请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public string Get(string url, List<KeyValue> headers, string authorization = "")
        => GetAsync(url, headers, authorization).Result;

    /// <summary>
    /// 异步Get请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public async Task<string> GetAsync(string url, string authorization = "")
    {
        if (!string.IsNullOrWhiteSpace(authorization))
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
        }

        var response = await httpClient.GetAsync(new Uri(url));
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        var errmsg = $"[{(int)response.StatusCode}]{response.StatusCode}";
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), errmsg);
    }

    /// <summary>
    /// 异步Get请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public async Task<string> GetAsync(string url, List<KeyValue> headers, string authorization = "")
    {
        if (!string.IsNullOrWhiteSpace(authorization))
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
        }
        foreach (var param in headers)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(param.Key, param.Value);
        }

        var response = await httpClient.GetAsync(new Uri(url));
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        var errmsg = $"[{(int)response.StatusCode}]{response.StatusCode}";
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), errmsg);
    }

    #endregion


    #region Post

    /// <summary>
    /// 同步提交
    /// </summary>
    /// <param name="url"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public string Post(string url, HttpContent content) => PostAsync(url, content).Result;

    /// <summary>
    /// 同步提交
    /// </summary>
    /// <param name="url"></param>
    /// <param name="str"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public string PostString(string url, string str, string authorization = "")
        => PostStringAsync(url, str, authorization).Result;

    /// <summary>
    /// 同步提交
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public string PostJson(string url, string json, string authorization = "")
        => PostJsonAsync(url, json, authorization).Result;

    /// <summary>
    /// 同步提交
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    /// <param name="headers"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public string PostJson(string url, string json, List<KeyValue> headers, string authorization = "")
        => PostJsonAsync(url, json, headers, authorization).Result;

    /// <summary>
    /// 异步提交
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<string> PostAsync(string url)
    {
        var response = await httpClient.PostAsync(new Uri(url), null);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        var errmsg = $"[{(int)response.StatusCode}]{response.StatusCode}";
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), errmsg);
    }

    /// <summary>
    /// 异步提交
    /// </summary>
    /// <param name="url"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public async Task<string> PostAsync(string url, HttpContent content)
    {
        var response = await httpClient.PostAsync(new Uri(url), content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        var errmsg = $"[{(int)response.StatusCode}]{response.StatusCode}";
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), errmsg);
    }

    /// <summary>
    /// 异步提交
    /// </summary>
    /// <param name="url"></param>
    /// <param name="str">字符串</param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public async Task<string> PostStringAsync(string url, string str, string authorization = "")
    {
        if (!string.IsNullOrWhiteSpace(authorization))
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
        }

        HttpContent content = new StringContent(str);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        var response = await httpClient.PostAsync(new Uri(url), content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        var errmsg = $"[{(int)response.StatusCode}]{response.StatusCode}";
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), errmsg);
    }

    /// <summary>
    /// 异步提交
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public async Task<string> PostJsonAsync(string url, string json, string authorization = "")
    {
        if (!string.IsNullOrWhiteSpace(authorization))
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
        }

        HttpContent content = new StringContent(json);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await httpClient.PostAsync(new Uri(url), content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        var errmsg = $"[{(int)response.StatusCode}]{response.StatusCode}";
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), errmsg);
    }

    /// <summary>
    /// 异步提交
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    /// <param name="headers"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public async Task<string> PostJsonAsync(string url, string json, List<KeyValue> headers, string authorization = "")
    {
        if (!string.IsNullOrWhiteSpace(authorization))
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
        }

        HttpContent content = new StringContent(json);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        foreach (var param in headers)
        {
            content.Headers.Add(param.Key, param.Value);
        }

        var response = await httpClient.PostAsync(new Uri(url), content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStringAsync();
        }

        var errmsg = $"[{(int)response.StatusCode}]{response.StatusCode}";
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), errmsg);
    }

    #endregion


    #region 获取Url内容

    /// <summary>
    /// 将网络文件转换成Stream
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<Stream> GetStreamAsync(string url)
    {
        var response = await httpClient.GetAsync(new Uri(url));
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsStreamAsync();
        }

        var errmsg = $"[{(int)response.StatusCode}]{response.StatusCode}";
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), errmsg);
    }

    /// <summary>
    /// 将网络文件转换成Stream
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public Stream GetStream(string url) => GetStreamAsync(url).Result;


    /// <summary>
    /// 将网络文件转换成Byte[]
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<byte[]> GetBufferAsync(string url)
    {
        var response = await httpClient.GetAsync(new Uri(url));
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }

        var errmsg = $"[{(int)response.StatusCode}]{response.StatusCode}";
        throw BusinessException.Get(MethodBase.GetCurrentMethod(), errmsg);
    }

    /// <summary>
    /// 将网络文件转换成Byte[]
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public byte[] GetBuffer(string url) => GetBufferAsync(url).Result;


    /// <summary>
    /// 将网络文件转换成Base64编码文本
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<string> GetBase64Async(string url) => Util.GetBase64.FromBytes(await GetBufferAsync(url));

    /// <summary>
    /// 将网络文件转换成Base64编码文本
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public string GetBase64(string url) => Util.GetBase64.FromBytes(GetBuffer(url));

    #endregion
}
