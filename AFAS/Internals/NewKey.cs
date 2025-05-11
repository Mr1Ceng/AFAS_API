using AFAS.Entity;
using Mr1Ceng.Util;

namespace AFAS.Internals;

/// <summary>
/// 创建数据编码
/// </summary>
public class NewKey
{
    /// <summary>
    /// 获取新的试卷编码
    /// </summary>
    /// <returns></returns>
    public static string NewQuestionnaireId()
    {
        var newkey = "QN" + NewCode.GetUpper(4);
        using (var context = new AfasContext())
        {
            if(context.BQuestionnaires.Any(b => b.QuestionnaireId == newkey))
            {
                newkey = NewQuestionnaireId();
            }
        }
        return newkey;
    }

    /// <summary>
    /// 获取新的账号
    /// </summary>
    /// <returns></returns>
    public static string NewAccount(string userName)
    {
        var pinyin = PinYinHelper.GetPinYin(userName);
        using (var context = new AfasContext())
        {
            var count = context.BUsers.Count(b => b.Account.StartsWith(pinyin));
            if (count>0)
            {
                pinyin += count.ToString().PadLeft(3, '0');
            }
        }
        return pinyin;
    }


    /// <summary>
    /// 获取新的答案编码
    /// </summary>
    /// <returns></returns>
    public static string NewAnswerId(string date)
    {
        var answerId = date.Replace("-", "");
        using (var context = new AfasContext())
        {
            var count = context.BAnswers.Count(b => b.AnswerId.StartsWith(answerId));
            answerId += (count + 1).ToString().PadLeft(3, '0');
        }
        return answerId;
    }
}
