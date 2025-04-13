using AFAS.Entitys;
using AFAS.Internals;
using AFAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using System.Reflection;

namespace AFAS.Business.Questionnaire;

/// <summary>
/// 试卷-服务
/// </summary>
public class QuestionnaireService : IQuestionnaireService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name=""></param>
    public QuestionnaireService() : base()
    {
    }

    #region Questionnaire

    /// <summary>
    /// 获取试卷列表
    /// </summary>
    /// <returns></returns>

    public async Task<List<BQuestionnaire>> GetQuestionnaireListAsync()
    {
        var questionnaries = new List<BQuestionnaire>();
        using (var context = new AfasContext())
        {
            questionnaries = await context.BQuestionnaires.ToListAsync();
        }
        return questionnaries;
    }

    /// <summary>
    /// 获取题目模型
    /// </summary>
    /// <param name="questionnaireId"></param>
    /// <returns></returns>
    public async Task<QuestionnaireModel> GetQuestionnaireModelAsync(string questionnaireId)
    {
        var questionnaire = new QuestionnaireModel();
        using (var context = new AfasContext())
        {
            questionnaire.bQuestionList = await context.BQuestions.Where(x => x.QuestionnaireId == questionnaireId).ToListAsync();
            var s1 = questionnaire.bQuestionList.Find(y => y.QuestionCode == "S1")?.QuestionId ?? "";
            var s2 = questionnaire.bQuestionList.Find(y => y.QuestionCode == "S2")?.QuestionId ?? "";
            var s3 = questionnaire.bQuestionList.Find(y => y.QuestionCode == "S3")?.QuestionId ?? "";
            var s4 = questionnaire.bQuestionList.Find(y => y.QuestionCode == "S4")?.QuestionId ?? "";
            var s5 = questionnaire.bQuestionList.Find(y => y.QuestionCode == "S5")?.QuestionId ?? "";
            var t1 = questionnaire.bQuestionList.Find(y => y.QuestionCode == "T1")?.QuestionId ?? "";
            var t2 = questionnaire.bQuestionList.Find(y => y.QuestionCode == "T2")?.QuestionId ?? "";
            var t3 = questionnaire.bQuestionList.Find(y => y.QuestionCode == "T3")?.QuestionId ?? "";
            questionnaire.bQuestionS1List = await context.BQuestionS1s.Where(x => x.QuestionId == s1).ToListAsync();
            questionnaire.bQuestionS2List = await context.BQuestionS2s.Where(x => x.QuestionId == s2).ToListAsync();
            questionnaire.bQuestionS3List = await context.BQuestionS3s.Where(x => x.QuestionId == s3).ToListAsync();
            questionnaire.bQuestionS4 = await context.BQuestionS4s.SingleAsync(x => x.QuestionId == s4);
            questionnaire.bQuestionS5List = await context.BQuestionS5s.Where(x => x.QuestionId == s5).ToListAsync();
            var questionT1 = await context.BQuestionT1s.SingleAsync(x => x.QuestionId == t1);
            questionnaire.bQuestionT1 = new QuestionT1Model() {
                QuestionId = questionT1.QuestionId,
                NumberQuestion = questionT1.NumberQuestion,
                StoryQuestion = questionT1.StoryQuestion,
                Number1 = questionT1.Number1,
                Number2 = questionT1.Number2,
                Number3 = questionT1.Number3,
                bQuestionT1QList = await context.BQuestionT1Qs.Where(x => x.QuestionId == t1).ToListAsync(),
                bQuestionT1AList = await context.BQuestionT1As.Where(x => x.QuestionId == t1).ToListAsync()
            };
            var questionT2 = await context.BQuestionT2s.SingleAsync(x => x.QuestionId == t2);
            questionnaire.bQuestionT2 = new QuestionT2Model()
            {
                QuestionId = questionT2.QuestionId,
                Question = questionT2.Question,
                Number1 = questionT2.Number1,
                Number2 = questionT2.Number2,
                bQuestionT2QList = await context.BQuestionT2Qs.Where(x => x.QuestionId == t2).ToListAsync(),
                bQuestionT2AList = await context.BQuestionT2As.Where(x => x.QuestionId == t2).ToListAsync()
            };
            questionnaire.bQuestionT3List = await context.BQuestionT3s.Where(x => x.QuestionId == t3).ToListAsync();
        }
        return questionnaire;
    }

    /// <summary>
    /// 获取试卷
    /// </summary>
    /// <returns></returns>
    public async Task<BQuestionnaire> GetQuestionnaireAsync(string questionnaireId)
    {
        var questionnarie = new BQuestionnaire();
        using (var context = new AfasContext())
        {
            questionnarie = await context.BQuestionnaires.FirstOrDefaultAsync(b => b.QuestionnaireId == questionnaireId);
        }
        return questionnarie ?? new BQuestionnaire();
    }

    /// <summary>
    /// 保存试卷
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>

    public async Task<string> SaveQuestionnaireAsync(QuestionnaireForm data)
    {
        var questionnarie = new BQuestionnaire();
        using (var context = new AfasContext())
        {
            questionnarie = await context.BQuestionnaires.FirstOrDefaultAsync(b => b.QuestionnaireId == data.QuestionnaireId);
            if (questionnarie == null)
            {
                questionnarie = new BQuestionnaire()
                {
                    QuestionnaireId = NewKey.NewQuestionnaireId(),
                    QuestionnaireName = data.QuestionnaireName,
                    VersionName = data.VersionName,
                    Remark = data.Remark,
                };
                context.BQuestionnaires.Add(questionnarie);
            }
            else
            {
                questionnarie.QuestionnaireName = data.QuestionnaireName;
                questionnarie.VersionName = data.VersionName;
                questionnarie.Remark = data.Remark;
                context.BQuestionnaires.Update(questionnarie);
            }

            await context.SaveChangesAsync();
        }
        return questionnarie.QuestionnaireId;
    }

    /// <summary>
    /// 删除试卷
    /// </summary>
    /// <param name="questionnaireId"></param>
    /// <returns></returns>

    public async Task RemoveQuestionnaireAsync(string questionnaireId)
    {
        var questionnarie = new BQuestionnaire();
        using (var context = new AfasContext())
        {
            questionnarie = context.BQuestionnaires.FirstOrDefault(b => b.QuestionnaireId == questionnaireId);
            if (questionnarie == null)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "试卷不存在！");
            }
            else
            {
                context.BQuestionnaires.Remove(questionnarie);
            }

            await context.SaveChangesAsync();
        }
    }

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
            questions = await context.BQuestions.Where(x=>x.QuestionnaireId == questionnaireId).ToListAsync();
        }
        return questions;
    }

    #endregion
}
