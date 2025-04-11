using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 异常信息类型
/// </summary>
public enum ExceptionInfoType
{
    #region Type

    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")] Success = 200,

    /// <summary>
    /// 常规错误
    /// </summary>
    [Description("常规错误")] Exception = 600,

    /// <summary>
    /// 消息提醒
    /// </summary>
    [Description("消息提醒")] Message = 700,

    /// <summary>
    /// 未被授权
    /// </summary>
    [Description("未被授权")] Forbidden = 403,

    /// <summary>
    /// 事件拦截
    /// </summary>
    [Description("事件拦截")] Intercept = 800,

    #endregion

    #region Error

    /// <summary>
    /// 部署配置错误
    /// </summary>
    [Description("部署配置错误")] Config = 602,

    /// <summary>
    /// 开发逻辑错误
    /// </summary>
    [Description("开发逻辑错误")] Develop = 603,

    /// <summary>
    /// 请求外部服务报错
    /// </summary>
    [Description("请求外部服务报错")] Remote = 604,

    /// <summary>
    /// 业务逻辑错误
    /// </summary>
    [Description("业务逻辑错误")] Business = 605,

    /// <summary>
    /// 数据存取错误
    /// </summary>
    [Description("数据存取错误")] Database = 606,

    /// <summary>
    /// 重要错误
    /// </summary>
    [Description("重要错误")] Important = 608

    #endregion
}
