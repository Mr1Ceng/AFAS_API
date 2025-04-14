using AFAS.Entitys;
using AFAS.Models;
using Microsoft.EntityFrameworkCore;

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
    Task<List<BQuestion>> GetQuestionListAsync(string questionnaireId);

    /// <summary>
    /// 获取题目S1信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<List<BQuestionS1>> GetQuestionS1Async(string questionId);

    /// <summary>
    /// 获取题目S2信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<List<BQuestionS2>> GetQuestionS2Async(string questionId);

    /// <summary>
    /// 获取题目S3信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<List<BQuestionS3>> GetQuestionS3Async(string questionId);

    /// <summary>
    /// 获取题目S4信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<BQuestionS4> GetQuestionS4Async(string questionId);

    /// <summary>
    /// 获取题目S5信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<List<BQuestionS5>> GetQuestionS5Async(string questionId);

    /// <summary>
    /// 获取题目T1信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<QuestionT1Model> GetQuestionT1Async(string questionId);

    /// <summary>
    /// 获取题目T2信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<QuestionT2Model> GetQuestionT2Async(string questionId);

    /// <summary>
    /// 获取题目T3信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<List<BQuestionT3>> GetQuestionT3Async(string questionId);
    #endregion
}
