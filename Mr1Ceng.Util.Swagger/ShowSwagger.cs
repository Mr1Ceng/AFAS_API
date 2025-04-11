namespace Mr1Ceng.Util.Swagger;

/// <summary>
/// 是否显示Swagger【0:不加载Swagger、1:加载swagger.json但不显示UI、2:显示完整Swagger、3:启动Swagger本地调试】
/// </summary>
public enum ShowSwagger
{
    /// <summary>
    /// 不加载Swagger
    /// </summary>
    HIDE = 0,

    /// <summary>
    /// 加载swagger.json但不显示UI
    /// </summary>
    ONLY_JSON = 1,

    /// <summary>
    /// 显示完整Swagger
    /// </summary>
    SHOW_UI = 2,

    /// <summary>
    /// 启动Swagger本地调试
    /// </summary>
    LOCAL_DEBUG = 3
}
