using AFAS.Entitys;
using AFAS.Internals;
using AFAS.Models;
using Mr1Ceng.Util;
using System.Reflection;

namespace AFAS.Business.Questionnaire;

/// <summary>
/// 试卷-服务
/// </summary>
public interface IQuestionnaireService
{

    /// <summary>
    /// 获取试卷列表
    /// </summary>
    /// <returns></returns>

    Task<List<BQuestionnaire>> GetQuestionnaireListAsync();

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

    Task<string> SaveQuestionnaireAsync(BQuestionnaireForm data);

    /// <summary>
    /// 删除试卷
    /// </summary>
    /// <param name="questionnaireId"></param>
    /// <returns></returns>

    Task RemoveQuestionnaireAsync(string questionnaireId);
}
