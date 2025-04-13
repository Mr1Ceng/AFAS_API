using AFAS.Entitys;
using AFAS.Internals;
using AFAS.Models;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using System.Reflection;

namespace AFAS.Business.Questionnaire;

/// <summary>
/// 试卷-服务
/// </summary>
public interface IQuestionnaireService
{


    #region Questionnaire

    /// <summary>
    /// 获取试卷列表
    /// </summary>
    /// <returns></returns>

    Task<List<BQuestionnaire>> GetQuestionnaireListAsync();

    /// <summary>
    /// 获取题目模型
    /// </summary>
    /// <param name="questionnaireId"></param>
    /// <returns></returns>
    Task<QuestionnaireModel> GetQuestionnaireModelAsync(string questionnaireId);

    /// <summary>
    /// 获取试卷
    /// </summary>
    /// <returns></returns>
    Task<BQuestionnaire> GetQuestionnaireAsync(string questionnaireId);

    /// <summary>
    /// 保存试卷
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>

    Task<string> SaveQuestionnaireAsync(QuestionnaireForm data);

    /// <summary>
    /// 删除试卷
    /// </summary>
    /// <param name="questionnaireId"></param>
    /// <returns></returns>

    Task RemoveQuestionnaireAsync(string questionnaireId);
    #endregion

    #region Question

    /// <summary>
    /// 获取题目列表
    /// </summary>
    /// <param name="questionnaireId"></param>
    /// <returns></returns>
    public async Task<List<BQuestion>> GetQuestionListAsync(string questionnaireId)
    {
        var questions = new List<BQuestion>();
        using (var context = new AfasContext())
        {
            questions = await context.BQuestions.Where(x => x.QuestionnaireId == questionnaireId).ToListAsync();
        }
        return questions;
    }

    #endregion
}
