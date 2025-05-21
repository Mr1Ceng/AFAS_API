using AFAS.Entity;
using Microsoft.EntityFrameworkCore;
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
    /// 获取新的试卷题目编号
    /// </summary>
    /// <returns></returns>
    public static string NewQuestionId()
    {
        var newkey = "QS" + NewCode.GetUpper(4);
        using (var context = new AfasContext())
        {
            if (context.BQuestions.Any(b => b.QuestionId == newkey))
            {
                newkey = NewQuestionId();
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
            var list = context.BUsers.Where(b => b.Account.StartsWith(pinyin)).OrderBy(x=>x.Account).ToList();
            if (list.Count() > 0)
            {
                var maxNum = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    var currentNum = GetInt.FromObject(item.Account.Replace(userName, ""));
                    if (i == list.Count - 1)
                    {
                        maxNum = (currentNum + 1);
                    }
                    else
                    {
                        var nextItem = list[i + 1];

                        var nextNum = GetInt.FromObject(nextItem.Account.Replace(userName, ""));
                        if(nextNum != currentNum + 1)
                        {
                            maxNum = (currentNum + 1);
                            break;
                        }

                    }
                }
                pinyin += maxNum.ToString().PadLeft(3, '0');
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
            var maxAnswer = context.BAnswers.Where(x=> x.AnswerId.StartsWith(answerId)).ToList().MaxBy(b => b.AnswerId);
            if (maxAnswer != null)
            {
                answerId += (GetInt.FromObject(maxAnswer.AnswerId.Replace(answerId, "")) + 1).ToString().PadLeft(3, '0');
            }
            else
            {
                answerId += "001";
            }

        }
        return answerId;
    }

    /// <summary>
    /// 获取测评标准编码
    /// </summary>
    /// <returns></returns>
    public static string NewLevelCode()
    {
        var result = "LEVEL";
        using (var context = new AfasContext())
        {
            var list = context.BEvaluationStandards.Where(b => b.LevelCode.StartsWith(result)).OrderBy(x => x.LevelCode).ToList();
            if (list.Count() > 0)
            {
                var maxNum = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    var currentNum = GetInt.FromObject(item.LevelCode.Replace(result, ""));
                    if (i == list.Count - 1)
                    {
                        maxNum = (currentNum + 1);
                    }
                    else
                    {
                        var nextItem = list[i + 1];

                        var nextNum = GetInt.FromObject(nextItem.LevelCode.Replace(result, ""));
                        if (nextNum != currentNum + 1)
                        {
                            maxNum = (currentNum + 1);
                            break;
                        }

                    }
                }
                result += maxNum.ToString().PadLeft(2, '0');
            }
        }
        return result;
    }


}
