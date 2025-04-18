using AFAS.Entitys;
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
}
