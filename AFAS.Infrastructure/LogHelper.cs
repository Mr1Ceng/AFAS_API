using Mr1Ceng.Util;

namespace AFAS.Infrastructure;

/// <summary>
/// 日志工具类
/// </summary>
public class LogHelper
{
    #region 写异常日志

    /// <summary>
    /// 记录异常
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static int RecordException(BusinessException ex)
    {
        if (ex.ExceptionType == ExceptionType.Intercept)
        {
            return 0;
        }

        //var log = new log_Exception();
        //try
        //{
        //    log = new log_Exception
        //    {
        //        TimeStamp = DateHelper.GetDateString(),
        //        Token = ex.Token,
        //        PageKey = ex.PageKey,
        //        ActionKey = ex.ActionKey,
        //        ExceptionCode = (int)ex.ExceptionType,
        //        ExceptionType = ex.ExceptionType.ToString(),
        //        ExceptionName = EnumHelper.GetDescription(ex.ExceptionType, 50),
        //        Message = GetString.FromObject(((Exception)ex).Message, 500),
        //        DebugData = ex.DebugData == null ? "" : JsonConvert.SerializeObject(ex.DebugData),
        //        Exception = JsonConvert.SerializeObject(ex)
        //    };
        //    log.Insert();

        //    return log.LogId;
        //}
        //catch
        //{
        //    log.Insert();
        //    return -1;
        //}
        //finally
        //{

        //}
        return 0;
    }

    #endregion

    #region 写调试日志

    /// <summary>
    /// 写调试日志
    /// </summary>
    /// <param name="message"></param>
    public static void Debug(string message) => Debug(message, "", "", null);

    /// <summary>
    /// 写调试日志
    /// </summary>
    /// <param name="message"></param>
    /// <param name="remark"></param>
    public static void Debug(string message, string remark) => Debug(message, remark, "", null);

    /// <summary>
    /// 写调试日志
    /// </summary>
    /// <param name="message"></param>
    /// <param name="remark"></param>
    /// <param name="content"></param>
    public static void Debug(string message, string remark, string content) => Debug(message, remark, content, null);

    /// <summary>
    /// 写调试日志
    /// </summary>
    /// <param name="message"></param>
    /// <param name="data"></param>
    public static void Debug(string message, object data) => Debug(message, "", "", data);

    /// <summary>
    /// 写调试日志
    /// </summary>
    /// <param name="message"></param>
    /// <param name="remark"></param>
    /// <param name="data"></param>
    public static void Debug(string message, string remark, object data) => Debug(message, remark, "", data);

    /// <summary>
    /// 写调试日志
    /// </summary>
    /// <param name="message"></param>
    /// <param name="remark"></param>
    /// <param name="content"></param>
    /// <param name="data"></param>
    public static void Debug(string message, string remark, string content, object? data)
    {
            //try
            //{
            //    new log_Debug
            //    {
            //        TimeStamp = DateHelper.GetDateString(),
            //        Message = GetString.FromObject(message, 50),
            //        Remark = GetString.FromObject(remark, 200),
            //        Content = GetString.FromObject(content, 500),
            //        Data = data == null ? "" : JsonConvert.SerializeObject(data)
            //    }.Insert();
            //}
            //catch (Exception ex)
            //{
            //    RecordException(new BusinessException(ExceptionType.DatabaseError, "写调试日志失败", ex));
            //}
    }

    #endregion
}
