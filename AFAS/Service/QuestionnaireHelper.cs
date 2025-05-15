using AFAS.Entity;
using AFAS.Models.EvaluationStandard;
using Microsoft.EntityFrameworkCore;

namespace AFAS.Service;

/// <summary>
/// 测评试卷工具类
/// </summary>
public class QuestionnaireHelper
{
    #region 测评试卷

    /// <summary>
    /// 获取测评试卷
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task<List<BQuestionnaire>> GetQuestionnaireListAsync()
    {
        var questionnaries = new List<BQuestionnaire>();
        using (var context = new AfasContext())
        {
            questionnaries = await context.BQuestionnaires.ToListAsync();
        }
        return questionnaries;
    }

    #endregion
}
