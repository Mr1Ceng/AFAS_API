using AFAS.Entitys;
using AFAS.Models.Question;

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
    Task<QuestionS1Model> GetQuestionS1Async(string questionId);

    /// <summary>
    /// 获取题目S2信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<QuestionS2Model> GetQuestionS2Async(string questionId);

    /// <summary>
    /// 获取题目S3信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<QuestionS3Model> GetQuestionS3Async(string questionId);

    /// <summary>
    /// 获取题目S4信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<QuestionS4Model> GetQuestionS4Async(string questionId);

    /// <summary>
    /// 获取题目S5信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    Task<QuestionS5Model> GetQuestionS5Async(string questionId);

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
    Task<QuestionT3Model> GetQuestionT3Async(string questionId);
    #endregion

    #region Answer

    /// <summary>
    /// 保存题目S1答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<string> SaveAnswerS1Async(AnswerS1Model data, string userId);

    /// <summary>
    /// 保存题目S2答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<string> SaveAnswerS2Async(AnswerS2Model data, string userId);

    /// <summary>
    /// 保存题目S3答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<string> SaveAnswerS3Async(AnswerS3Model data, string userId);

    /// <summary>
    /// 保存题目S4答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<string> SaveAnswerS4Async(AnswerS4Model data, string userId);

    /// <summary>
    /// 保存题目S5答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<string> SaveAnswerS5Async(AnswerS5Model data, string userId);

    /// <summary>
    /// 保存题目T1答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<string> SaveAnswerT1Async(AnswerT1Model data, string userId);

    /// <summary>
    /// 保存题目T2答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<string> SaveAnswerT2Async(AnswerT2Model data, string userId);
    #endregion
}
