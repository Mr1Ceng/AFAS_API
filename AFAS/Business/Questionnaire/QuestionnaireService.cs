using AFAS.Authorization;
using AFAS.Authorization.AuthInfos;
using AFAS.Authorization.Enums;
using AFAS.Entity;
using AFAS.Enums;
using AFAS.Infrastructure;
using AFAS.Infrastructure.Models;
using AFAS.Internals;
using AFAS.Models.Question;
using AFAS.Models.TestResult;
using AFAS.Service;
using DocumentFormat.OpenXml.Office.SpreadSheetML.Y2023.MsForms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using Mr1Ceng.Util.Excel;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;


namespace AFAS.Business.Questionnaire;

/// <summary>
/// 试卷-服务
/// </summary>
public class QuestionnaireService :UserTokenAuthorization, IQuestionnaireService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name=""></param>
    public QuestionnaireService(IAuthInfo authInfo) : base(authInfo)
    {
    }

    #region Questionnaire

    /// <summary>
    /// 获取试卷列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<BQuestionnaire>> GetQuestionnaireListAsync()
     => await QuestionnaireHelper.GetQuestionnaireListAsync();

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
            questionnaire.bQuestionT1 = new QuestionT1Model()
            {
                QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == t1),
                AnswerInfo = await context.BQuestionT1s.SingleAsync(x => x.QuestionId == t1),
                bQuestionT1QList = await context.BQuestionT1Qs.Where(x => x.QuestionId == t1).ToListAsync(),
                bQuestionT1AList = await context.BQuestionT1As.Where(x => x.QuestionId == t1).ToListAsync()
            };
            questionnaire.bQuestionT2 = new QuestionT2Model()
            {
                QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == t2),
                AnswerInfo = await context.BQuestionT2s.SingleAsync(x => x.QuestionId == t2),
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
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // 获取所有需要删除的 `QuestionId`
                    var questionIds = await context.BQuestions
                        .Where(x => x.QuestionnaireId == questionnaireId)
                        .Select(x => x.QuestionId)
                        .ToListAsync();
                    if (questionIds.Any()) // 确保有数据需要删除
                    {
                        await context.BQuestionS1s.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionS2s.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionS3s.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionS4s.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionS5s.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionT1s.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionT1As.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionT1Qs.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionT2s.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionT2As.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionT2Qs.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestionT3s.Where(x => questionIds.Contains(x.QuestionId)).ExecuteDeleteAsync();
                        await context.BQuestions.Where(x => x.QuestionnaireId == questionnaireId).ExecuteDeleteAsync();
                    }
                    context.BQuestionnaires.Remove(questionnarie);
                    await context.SaveChangesAsync();// 保存数据
                    transaction.Commit(); // 提交事务
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // 发生异常时回滚事务
                    throw BusinessException.Get(ex).AddMessage(MethodBase.GetCurrentMethod(), "删除题目失败");
                }
            }
        }
    }

    #endregion

    #region Question 获取

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

    /// <summary>
    /// 获取题目S1信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    public async Task<QuestionS1Model> GetQuestionS1Async(string questionId)
    {
        var question = new QuestionS1Model();
        using (var context = new AfasContext())
        {
            question.QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId);
            question.QuestionList = await context.BQuestionS1s.Where(x => x.QuestionId == questionId).OrderBy(x => x.GridSort).ToListAsync();
        }
        return question;
    }

    /// <summary>
    /// 获取题目S2信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    public async Task<QuestionS2Model> GetQuestionS2Async(string questionId)
    {
        var question = new QuestionS2Model();
        using (var context = new AfasContext())
        {
            question.QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId);
            question.QuestionList = await context.BQuestionS2s.Where(x => x.QuestionId == questionId).ToListAsync();
        }
        return question;
    }

    /// <summary>
    /// 获取题目S3信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    public async Task<QuestionS3Model> GetQuestionS3Async(string questionId)
    {
        var question = new QuestionS3Model();
        using (var context = new AfasContext())
        {
            question.QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId);
            question.QuestionList = await context.BQuestionS3s.Where(x => x.QuestionId == questionId).OrderBy(x => x.GridRow).ThenBy(x => x.GridColumn).ToListAsync();
            question.bQuestionS3AList = await context.BQuestionS3As.Where(x => x.QuestionId == questionId).ToListAsync();
        }
        return question;
    }

    /// <summary>
    /// 获取题目S4信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    public async Task<QuestionS4Model> GetQuestionS4Async(string questionId)
    {
        var question = new QuestionS4Model();
        using (var context = new AfasContext())
        {
            question.QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId);
            question.QuestionList = await context.BQuestionS4s.SingleAsync(x => x.QuestionId == questionId);
        }
        return question;
    }

    /// <summary>
    /// 获取题目S5信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    public async Task<QuestionS5Model> GetQuestionS5Async(string questionId)
    {
        var question = new QuestionS5Model();
        using (var context = new AfasContext())
        {
            question.QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId);
            question.QuestionList = await context.BQuestionS5s.Where(x => x.QuestionId == questionId).ToListAsync();
        }
        return question;
    }

    /// <summary>
    /// 获取题目T1信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    public async Task<QuestionT1Model> GetQuestionT1Async(string questionId)
    {
        var question = new QuestionT1Model();
        using (var context = new AfasContext())
        {
            question = new QuestionT1Model()
            {
                QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId),
                AnswerInfo = await context.BQuestionT1s.SingleAsync(x => x.QuestionId == questionId),
                bQuestionT1QList = await context.BQuestionT1Qs.Where(x => x.QuestionId == questionId).ToListAsync(),
                bQuestionT1AList = await context.BQuestionT1As.Where(x => x.QuestionId == questionId).ToListAsync()
            };
        }
        return question;
    }

    /// <summary>
    /// 获取题目T2信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    public async Task<QuestionT2Model> GetQuestionT2Async(string questionId)
    {
        var question = new QuestionT2Model();
        using (var context = new AfasContext())
        {
            question = new QuestionT2Model()
            {
                QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId),
                AnswerInfo = await context.BQuestionT2s.SingleAsync(x => x.QuestionId == questionId),
                bQuestionT2QList = await context.BQuestionT2Qs.Where(x => x.QuestionId == questionId).ToListAsync(),
                bQuestionT2AList = await context.BQuestionT2As.Where(x => x.QuestionId == questionId).ToListAsync()
            };
        }
        return question;
    }

    /// <summary>
    /// 获取题目T3信息
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    public async Task<QuestionT3Model> GetQuestionT3Async(string questionId)
    {
        var question = new QuestionT3Model();
        using (var context = new AfasContext())
        {
            question.QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId);
            question.QuestionList = await context.BQuestionT3s.Where(x => x.QuestionId == questionId).ToListAsync();
        }
        return question;
    }

    #endregion

    #region Question 管理

    /// <summary>
    /// 保存题目S1信息
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    public async Task<string> SaveQuestionS1Async(QuestionS1Model question)
    {
        using (var context = new AfasContext())
        {
            if (question.QuestionInfo.QuestionId == "")
            {
                question.QuestionInfo.QuestionId = NewKey.NewQuestionId();
                context.BQuestions.Add(question.QuestionInfo);
            }
            else
            { 
                context.BQuestions.Update(question.QuestionInfo);
            }
            _ = context.BQuestionS1s.Where(x=>x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            question.QuestionList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            context.BQuestionS1s.AddRange(question.QuestionList.OrderBy(x => x.GridSort).ToList());
            await context.SaveChangesAsync();
        }
        return question.QuestionInfo.QuestionId;
    }


    /// <summary>
    /// 保存题目S2信息
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    public async Task<string> SaveQuestionS2Async(QuestionS2Model question)
    {
        using (var context = new AfasContext())
        {
            if (question.QuestionInfo.QuestionId == "")
            {
                question.QuestionInfo.QuestionId = NewKey.NewQuestionId();
                context.BQuestions.Add(question.QuestionInfo);
            }
            else
            {
                context.BQuestions.Update(question.QuestionInfo);
            }
            _ = context.BQuestionS2s.Where(x => x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            question.QuestionList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            context.BQuestionS2s.AddRange(question.QuestionList.OrderBy(x => x.GridRow).ThenBy(x=>x.GridColumn).ToList());
            await context.SaveChangesAsync();
        }
        return question.QuestionInfo.QuestionId;
    }


    /// <summary>
    /// 保存题目S3信息
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    public async Task<string> SaveQuestionS3Async(QuestionS3Model question)
    {
        using (var context = new AfasContext())
        {
            if (question.QuestionInfo.QuestionId == "")
            {
                question.QuestionInfo.QuestionId = NewKey.NewQuestionId();
                context.BQuestions.Add(question.QuestionInfo);
            }
            else
            {
                context.BQuestions.Update(question.QuestionInfo);
            }
            _ = context.BQuestionS3s.Where(x => x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            question.QuestionList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            context.BQuestionS3s.AddRange(question.QuestionList.OrderBy(x => x.GridRow).ThenBy(x => x.GridColumn).ToList());
            _ = context.BQuestionS3As.Where(x => x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            question.bQuestionS3AList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            context.BQuestionS3As.AddRange(question.bQuestionS3AList.OrderBy(x => x.GridValue).ToList());
            await context.SaveChangesAsync();
        }
        return question.QuestionInfo.QuestionId;
    }


    /// <summary>
    /// 保存题目S4信息
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    public async Task<string> SaveQuestionS4Async(QuestionS4Model question)
    {
        using (var context = new AfasContext())
        {
            if (question.QuestionInfo.QuestionId == "")
            {
                question.QuestionInfo.QuestionId = NewKey.NewQuestionId();
                context.BQuestions.Add(question.QuestionInfo);
                question.QuestionList.QuestionId = question.QuestionInfo.QuestionId;
                context.BQuestionS4s.Add(question.QuestionList);
            }
            else
            {
                context.BQuestions.Update(question.QuestionInfo);
                context.BQuestionS4s.Update(question.QuestionList);
            }
            await context.SaveChangesAsync();
        }
        return question.QuestionInfo.QuestionId;
    }


    /// <summary>
    /// 保存题目S5信息
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    public async Task<string> SaveQuestionS5Async(QuestionS5Model question)
    {
        using (var context = new AfasContext())
        {
            if (question.QuestionInfo.QuestionId == "")
            {
                question.QuestionInfo.QuestionId = NewKey.NewQuestionId();
                context.BQuestions.Add(question.QuestionInfo);
            }
            else
            {
                context.BQuestions.Update(question.QuestionInfo);
            }
            _ = context.BQuestionS5s.Where(x => x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            question.QuestionList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            context.BQuestionS5s.AddRange(question.QuestionList.OrderBy(x => x.ImageId).ToList());
            await context.SaveChangesAsync();
        }
        return question.QuestionInfo.QuestionId;
    }

    /// <summary>
    /// 保存题目T1信息
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    public async Task<string> SaveQuestionT1Async(QuestionT1Model question)
    {
        using (var context = new AfasContext())
        {
            if (question.QuestionInfo.QuestionId == "")
            {
                question.QuestionInfo.QuestionId = NewKey.NewQuestionId();
                context.BQuestions.Add(question.QuestionInfo);
                question.AnswerInfo.QuestionId = question.QuestionInfo.QuestionId;
                context.BQuestionT1s.Add(question.AnswerInfo);
            }
            else
            {
                context.BQuestions.Update(question.QuestionInfo);
                context.BQuestionT1s.Update(question.AnswerInfo);
            }
            _ = context.BQuestionT1Qs.Where(x => x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            _ = context.BQuestionT1As.Where(x => x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            question.bQuestionT1QList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            question.bQuestionT1AList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            context.BQuestionT1Qs.AddRange(question.bQuestionT1QList.OrderBy(x => x.QuestionSort).ToList());
            context.BQuestionT1As.AddRange(question.bQuestionT1AList.OrderBy(x => x.QuestionSort).ThenBy(x => x.AnswerSort).ToList());
            await context.SaveChangesAsync();
        }
        return question.QuestionInfo.QuestionId;
    }

    /// <summary>
    /// 保存题目T2信息
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    public async Task<string> SaveQuestionT2Async(QuestionT2Model question)
    {
        using (var context = new AfasContext())
        {
            if (question.QuestionInfo.QuestionId == "")
            {
                question.QuestionInfo.QuestionId = NewKey.NewQuestionId();
                context.BQuestions.Add(question.QuestionInfo);
                question.AnswerInfo.QuestionId = question.QuestionInfo.QuestionId;
                context.BQuestionT2s.Add(question.AnswerInfo);
            }
            else
            {
                context.BQuestions.Update(question.QuestionInfo);
                context.BQuestionT2s.Update(question.AnswerInfo);
            }
            _ = context.BQuestionT2Qs.Where(x => x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            _ = context.BQuestionT2As.Where(x => x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            question.bQuestionT2QList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            question.bQuestionT2AList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            context.BQuestionT2Qs.AddRange(question.bQuestionT2QList.OrderBy(x => x.QuestionSort).ToList());
            context.BQuestionT2As.AddRange(question.bQuestionT2AList.OrderBy(x => x.QuestionSort).ThenBy(x => x.AnswerSort).ToList());
            await context.SaveChangesAsync();
        }
        return question.QuestionInfo.QuestionId;
    }

    /// <summary>
    /// 保存题目T3信息
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    public async Task<string> SaveQuestionT3Async(QuestionT3Model question)
    {
        using (var context = new AfasContext())
        {
            if (question.QuestionInfo.QuestionId == "")
            {
                question.QuestionInfo.QuestionId = NewKey.NewQuestionId();
                context.BQuestions.Add(question.QuestionInfo);
            }
            else
            {
                context.BQuestions.Update(question.QuestionInfo);
            }
            _ = context.BQuestionT3s.Where(x => x.QuestionId == question.QuestionInfo.QuestionId).ExecuteDelete();
            question.QuestionList.ForEach(x => x.QuestionId = question.QuestionInfo.QuestionId);
            context.BQuestionT3s.AddRange(question.QuestionList.OrderBy(x => x.QuestionType).ThenBy(x=>x.QuestionSort).ToList());
            await context.SaveChangesAsync();
        }
        return question.QuestionInfo.QuestionId;
    }

    /// <summary>
    /// 删除题目
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    public async Task RemoveQuestionAsync(string questionId)
    {
        var question = new BQuestion();
        using (var context = new AfasContext())
        {
            question = context.BQuestions.AsNoTracking().FirstOrDefault(b => b.QuestionId == questionId);
            if (question == null)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "试卷不存在！");
            }
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.BQuestions.Remove(question); 
                    await context.SaveChangesAsync();// 保存数据
                    await context.BQuestionS1s.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionS2s.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionS3s.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionS4s.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionS5s.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionT1s.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionT1As.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionT1Qs.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionT2s.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionT2As.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionT2Qs.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    await context.BQuestionT3s.Where(x => x.QuestionId == questionId).ExecuteDeleteAsync();
                    transaction.Commit(); // 提交事务
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // 发生异常时回滚事务
                    throw BusinessException.Get(ex).AddMessage(MethodBase.GetCurrentMethod(), "删除题目失败");
                }
            }

        }
    }

    #endregion

    #region Answer

    /// <summary>
    /// 获取题目答案
    /// </summary>
    /// <param name="answerId"></param>
    /// <returns></returns>
    public async Task<AnswerModel> GetAnswerListAsync(string answerId)
    {
        var answer = new AnswerModel();
        using (var context = new AfasContext())
        {
            var bAnswer = await context.BAnswers.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            if (bAnswer == null)
            {
                return answer;
            }
            answer.AnswerId = bAnswer.AnswerId;
            answer.QuestionnaireId = bAnswer.QuestionnaireId;
            answer.UserId = bAnswer.UserId;
            answer.QuestionnaireDate = bAnswer.QuestionnaireDate;
            answer.TeacherId = bAnswer.TeacherId;
            answer.Status = bAnswer.Status;
            answer.RadarImage = bAnswer.RadarImage;
            answer.SImage = bAnswer.Simage;
            answer.SResult = bAnswer.Sresult;
            answer.TImage = bAnswer.Timage;
            answer.TResult = bAnswer.Tresult;
            answer.Weak = bAnswer.Weak;
            answer.Advantage = bAnswer.Advantage;
            answer.Remark = bAnswer.Remark;
            answer.SuggestedCourse = bAnswer.SuggestedCourse;
            answer.LevelCode = bAnswer.LevelCode;

            var bAnswerS1 = await context.BAnswerS1s.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var bAnswerS2 = await context.BAnswerS2s.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var bAnswerS3 = await context.BAnswerS3s.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var bAnswerS4 = await context.BAnswerS4s.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var bAnswerS5 = await context.BAnswerS5s.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var bAnswerT1 = await context.BAnswerT1s.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var bAnswerT2 = await context.BAnswerT2s.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            var bAnswerT3 = await context.BAnswerT3s.Where(x => x.AnswerId == answerId).FirstOrDefaultAsync();
            answer.answerList.Add(new AnswerItem() { QuestionCode = "S1", Remark = bAnswerS1?.Remark ?? "", StandardScore = bAnswerS1?.StandardScore ?? 0 });
            answer.answerList.Add(new AnswerItem() { QuestionCode = "S2", Remark = bAnswerS2?.Remark ?? "", StandardScore = bAnswerS2?.StandardScore ?? 0 });
            answer.answerList.Add(new AnswerItem() { QuestionCode = "S3", Remark = bAnswerS3?.Remark ?? "", StandardScore = bAnswerS3?.StandardScore ?? 0 });
            answer.answerList.Add(new AnswerItem() { QuestionCode = "S4", Remark = bAnswerS4?.Remark ?? "", StandardScore = bAnswerS4?.StandardScore ?? 0 });
            answer.answerList.Add(new AnswerItem() { QuestionCode = "S5", Remark = bAnswerS5?.Remark ?? "", StandardScore = bAnswerS5?.StandardScore ?? 0 });
            answer.answerList.Add(new AnswerItem() { QuestionCode = "T1", Remark = bAnswerT1?.Remark ?? "", StandardScore = bAnswerT1?.StandardScore ?? 0 });
            answer.answerList.Add(new AnswerItem() { QuestionCode = "T2", Remark = bAnswerT2?.Remark ?? "", StandardScore = bAnswerT2?.StandardScore ?? 0 });
            answer.answerList.Add(new AnswerItem() { QuestionCode = "T3", Remark = bAnswerT3?.Remark ?? "", StandardScore = bAnswerT3?.StandardScore ?? 0 });
        }
        return answer;
    }

    /// <summary>
    /// 保存题目S1答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> SaveAnswerS1Async(AnswerS1Model data, string userId)
    {
        var answerBasic = new BAnswer();
        try
        {
            using (var context = new AfasContext())
            {
                var question = await context.BQuestions.FirstOrDefaultAsync(b => b.QuestionId == data.QuestionId);
                if (question == null)
                {
                    throw new Exception("问题不存在");
                }
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    answerBasic = new BAnswer()
                    {
                        AnswerId = NewKey.NewAnswerId(DateHelper.GetDayString()),
                        QuestionnaireId = question.QuestionnaireId,
                        QuestionnaireDate = DateHelper.GetDayString(),
                        UserId = userId,
                        Status = DataStatus.DRAFT.ToString(),
                    };
                    context.BAnswers.Add(answerBasic);
                }

                var answer = await context.BAnswerS1s.FirstOrDefaultAsync(b => b.AnswerId == answerBasic.AnswerId && b.QuestionId == data.QuestionId);
                if (answer == null)
                {
                    answer = new BAnswerS1()
                    {
                        AnswerId = answerBasic.AnswerId,
                        QuestionId = question.QuestionId,
                        OriginScore = data.OriginScore,
                        StandardScore = data.StandardScore,
                        Remark = data.Remark,
                    };
                    context.BAnswerS1s.Add(answer);
                    context.BAnswerS1As.AddRange(data.answerList.Select(x => new BAnswerS1A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        GridType = x.GridType,
                        TimeConsume = x.TimeConsume,
                    }));
                }
                else
                {
                    answer.OriginScore = data.OriginScore;
                    answer.StandardScore = data.StandardScore;
                    answer.Remark = data.Remark;
                    context.BAnswerS1s.Update(answer);
                    context.BAnswerS1As.UpdateRange(data.answerList.Select(x => new BAnswerS1A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        GridType = x.GridType,
                        TimeConsume = x.TimeConsume,
                    }));
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            return "";
        }
        return answerBasic.AnswerId;
    }

    /// <summary>
    /// 保存题目S2答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> SaveAnswerS2Async(AnswerS2Model data, string userId)
    {
        var answerBasic = new BAnswer();
        try
        {
            using (var context = new AfasContext())
            {
                var question = await context.BQuestions.FirstOrDefaultAsync(b => b.QuestionId == data.QuestionId);
                if (question == null)
                {
                    throw new Exception("问题不存在");
                }
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    answerBasic = new BAnswer()
                    {
                        AnswerId = NewKey.NewAnswerId(DateHelper.GetDayString()),
                        QuestionnaireId = question.QuestionnaireId,
                        QuestionnaireDate = DateHelper.GetDayString(),
                        UserId = userId,
                        Status = DataStatus.DRAFT.ToString(),
                    };
                    context.BAnswers.Add(answerBasic);
                }

                var answer = await context.BAnswerS2s.FirstOrDefaultAsync(b => b.AnswerId == answerBasic.AnswerId && b.QuestionId == data.QuestionId);
                if (answer == null)
                {
                    answer = new BAnswerS2()
                    {
                        AnswerId = answerBasic.AnswerId,
                        QuestionId = question.QuestionId,
                        TimeConsume = data.TimeConsume,
                        MarkNumber = data.MarkNumber,
                        ErrorNumber = data.ErrorNumber,
                        ErrorRate = data.ErrorRate,
                        StandardScore = data.StandardScore,
                        Remark = data.Remark,
                    };
                    context.BAnswerS2s.Add(answer);
                    context.BAnswerS2As.AddRange(data.answerList.Select(x => new BAnswerS2A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        GridRow = x.GridRow,
                        GridColumn = x.GridColumn,
                        Selected = x.Selected,
                    }));
                }
                else
                {
                    answer.TimeConsume = data.TimeConsume;
                    answer.MarkNumber = data.MarkNumber;
                    answer.ErrorNumber = data.ErrorNumber;
                    answer.ErrorRate = data.ErrorRate;
                    answer.StandardScore = data.StandardScore;
                    answer.Remark = data.Remark;
                    context.BAnswerS2s.Update(answer);
                    context.BAnswerS2As.UpdateRange(data.answerList.Select(x => new BAnswerS2A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        GridRow = x.GridRow,
                        GridColumn = x.GridColumn,
                        Selected = x.Selected,
                    }));
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            return "";
        }
        return answerBasic.AnswerId;
    }

    /// <summary>
    /// 保存题目S3答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> SaveAnswerS3Async(AnswerS3Model data, string userId)
    {
        var answerBasic = new BAnswer();
        try
        {
            using (var context = new AfasContext())
            {
                var question = await context.BQuestions.FirstOrDefaultAsync(b => b.QuestionId == data.QuestionId);
                if (question == null)
                {
                    throw new Exception("问题不存在");
                }
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    answerBasic = new BAnswer()
                    {
                        AnswerId = NewKey.NewAnswerId(DateHelper.GetDayString()),
                        QuestionnaireId = question.QuestionnaireId,
                        QuestionnaireDate = DateHelper.GetDayString(),
                        UserId = userId,
                        Status = DataStatus.DRAFT.ToString(),
                    };
                    context.BAnswers.Add(answerBasic);
                }

                var answer = await context.BAnswerS3s.FirstOrDefaultAsync(b => b.AnswerId == answerBasic.AnswerId && b.QuestionId == data.QuestionId);
                if (answer == null)
                {
                    answer = new BAnswerS3()
                    {
                        AnswerId = answerBasic.AnswerId,
                        QuestionId = question.QuestionId,
                        TimeConsume = data.TimeConsume,
                        RightNumber = data.RightNumber,
                        ErrorNumber = data.ErrorNumber,
                        StandardScore = data.StandardScore,
                        Remark = data.Remark,
                    };
                    context.BAnswerS3s.Add(answer);
                    context.BAnswerS3As.AddRange(data.answerList.Select(x => new BAnswerS3A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        GridRow = x.GridRow,
                        GridColumn = x.GridColumn,
                        Value = x.Value,
                    }));
                }
                else
                {
                    answer.TimeConsume = data.TimeConsume;
                    answer.RightNumber = data.RightNumber;
                    answer.ErrorNumber = data.ErrorNumber;
                    answer.StandardScore = data.StandardScore;
                    answer.Remark = data.Remark;
                    context.BAnswerS3s.Update(answer);
                    context.BAnswerS3As.UpdateRange(data.answerList.Select(x => new BAnswerS3A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        GridRow = x.GridRow,
                        GridColumn = x.GridColumn,
                        Value = x.Value,
                    }));
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            return "";
        }
        return answerBasic.AnswerId;
    }

    /// <summary>
    /// 保存题目S4答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> SaveAnswerS4Async(AnswerS4Model data, string userId)
    {
        var answerBasic = new BAnswer();
        try
        {
            using (var context = new AfasContext())
            {
                var question = await context.BQuestions.FirstOrDefaultAsync(b => b.QuestionId == data.QuestionId);
                if (question == null)
                {
                    throw new Exception("问题不存在");
                }
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    answerBasic = new BAnswer()
                    {
                        AnswerId = NewKey.NewAnswerId(DateHelper.GetDayString()),
                        QuestionnaireId = question.QuestionnaireId,
                        QuestionnaireDate = DateHelper.GetDayString(),
                        UserId = userId,
                        Status = DataStatus.DRAFT.ToString(),
                    };
                    context.BAnswers.Add(answerBasic);
                }

                var answer = await context.BAnswerS4s.FirstOrDefaultAsync(b => b.AnswerId == answerBasic.AnswerId && b.QuestionId == data.QuestionId);
                if (answer == null)
                {
                    answer = new BAnswerS4()
                    {
                        AnswerId = answerBasic.AnswerId,
                        QuestionId = question.QuestionId,
                        TimeConsume = data.TimeConsume,
                        QuestionImage = data.QuestionImage,
                        AnswerImage = data.AnswerImage,
                        CrossNumber = data.CrossNumber,
                        StandardScore = data.StandardScore,
                        Remark = data.Remark,
                    };
                    context.BAnswerS4s.Add(answer);
                }
                else
                {
                    answer.TimeConsume = data.TimeConsume;
                    answer.QuestionImage = data.QuestionImage;
                    answer.AnswerImage = data.AnswerImage;
                    answer.CrossNumber = data.CrossNumber;
                    answer.StandardScore = data.StandardScore;
                    answer.Remark = data.Remark;
                    context.BAnswerS4s.Update(answer);
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            return "";
        }
        return answerBasic.AnswerId;
    }

    /// <summary>
    /// 保存题目S5答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> SaveAnswerS5Async(AnswerS5Model data, string userId)
    {
        var answerBasic = new BAnswer();
        try
        {
            using (var context = new AfasContext())
            {
                var question = await context.BQuestions.FirstOrDefaultAsync(b => b.QuestionId == data.QuestionId);
                if (question == null)
                {
                    throw new Exception("问题不存在");
                }
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    answerBasic = new BAnswer()
                    {
                        AnswerId = NewKey.NewAnswerId(DateHelper.GetDayString()),
                        QuestionnaireId = question.QuestionnaireId,
                        QuestionnaireDate = DateHelper.GetDayString(),
                        UserId = userId,
                        Status = DataStatus.DRAFT.ToString(),
                    };
                    context.BAnswers.Add(answerBasic);
                }

                var answer = await context.BAnswerS5s.FirstOrDefaultAsync(b => b.AnswerId == answerBasic.AnswerId && b.QuestionId == data.QuestionId);
                if (answer == null)
                {
                    answer = new BAnswerS5()
                    {
                        AnswerId = answerBasic.AnswerId,
                        QuestionId = question.QuestionId,
                        TimeConsume = data.TimeConsume,
                        QuestionImage = data.QuestionImage,
                        AnswerImage = data.AnswerImage,
                        ShapeNumber = data.ShapeNumber,
                        ErrorNumber = data.ErrorNumber,
                        StandardScore = data.StandardScore,
                        Remark = data.Remark,
                    };
                    context.BAnswerS5s.Add(answer);
                }
                else
                {
                    answer.TimeConsume = data.TimeConsume;
                    answer.QuestionImage = data.QuestionImage;
                    answer.AnswerImage = data.AnswerImage;
                    answer.ShapeNumber = data.ShapeNumber;
                    answer.ErrorNumber = data.ErrorNumber;
                    answer.StandardScore = data.StandardScore;
                    answer.Remark = data.Remark;
                    context.BAnswerS5s.Update(answer);
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            return "";
        }
        return answerBasic.AnswerId;
    }

    /// <summary>
    /// 保存题目T1答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> SaveAnswerT1Async(AnswerT1Model data, string userId)
    {
        var answerBasic = new BAnswer();
        try
        {
            using (var context = new AfasContext())
            {
                var question = await context.BQuestions.FirstOrDefaultAsync(b => b.QuestionId == data.QuestionId);
                if (question == null)
                {
                    throw new Exception("问题不存在");
                }
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    answerBasic = new BAnswer()
                    {
                        AnswerId = NewKey.NewAnswerId(DateHelper.GetDayString()),
                        QuestionnaireId = question.QuestionnaireId,
                        QuestionnaireDate = DateHelper.GetDayString(),
                        UserId = userId,
                        Status = DataStatus.DRAFT.ToString(),
                    };
                    context.BAnswers.Add(answerBasic);
                }

                var answer = await context.BAnswerT1s.FirstOrDefaultAsync(b => b.AnswerId == answerBasic.AnswerId && b.QuestionId == data.QuestionId);
                if (answer == null)
                {
                    answer = new BAnswerT1()
                    {
                        AnswerId = answerBasic.AnswerId,
                        QuestionId = question.QuestionId,
                        Number1 = data.Number1,
                        Number2 = data.Number2,
                        Number3 = data.Number3,
                        ErrorNumber = data.ErrorNumber,
                        StandardScore = data.StandardScore,
                        Remark = data.Remark,
                    };
                    context.BAnswerT1s.Add(answer);
                    context.BAnswerT1As.AddRange(data.answerList.Select(x => new BAnswerT1A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        QuestionSort = x.QuestionSort,
                        AnswerSort = x.AnswerSort,
                    }));
                }
                else
                {
                    answer.Number1 = data.Number1;
                    answer.Number2 = data.Number2;
                    answer.Number3 = data.Number3;
                    answer.ErrorNumber = data.ErrorNumber;
                    answer.StandardScore = data.StandardScore;
                    answer.Remark = data.Remark;
                    context.BAnswerT1s.Update(answer);
                    context.BAnswerT1As.UpdateRange(data.answerList.Select(x => new BAnswerT1A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        QuestionSort = x.QuestionSort,
                        AnswerSort = x.AnswerSort,
                    }));
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            return "";
        }
        return answerBasic.AnswerId;
    }

    /// <summary>
    /// 保存题目T2答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> SaveAnswerT2Async(AnswerT2Model data, string userId)
    {
        var answerBasic = new BAnswer();
        try
        {
            using (var context = new AfasContext())
            {
                var question = await context.BQuestions.FirstOrDefaultAsync(b => b.QuestionId == data.QuestionId);
                if (question == null)
                {
                    throw new Exception("问题不存在");
                }
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    answerBasic = new BAnswer()
                    {
                        AnswerId = NewKey.NewAnswerId(DateHelper.GetDayString()),
                        QuestionnaireId = question.QuestionnaireId,
                        QuestionnaireDate = DateHelper.GetDayString(),
                        UserId = userId,
                        Status = DataStatus.DRAFT.ToString(),
                    };
                    context.BAnswers.Add(answerBasic);
                }

                var answer = await context.BAnswerT2s.FirstOrDefaultAsync(b => b.AnswerId == answerBasic.AnswerId && b.QuestionId == data.QuestionId);
                if (answer == null)
                {
                    answer = new BAnswerT2()
                    {
                        AnswerId = answerBasic.AnswerId,
                        QuestionId = question.QuestionId,
                        Number1 = data.Number1,
                        Number2 = data.Number2,
                        StandardScore = data.StandardScore,
                        Remark = data.Remark,
                    };
                    context.BAnswerT2s.Add(answer);
                    context.BAnswerT2As.AddRange(data.answerList.Select(x => new BAnswerT2A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        QuestionSort = x.QuestionSort,
                        AnswerSort = x.AnswerSort,
                    }));
                }
                else
                {
                    answer.Number1 = data.Number1;
                    answer.Number2 = data.Number2;
                    answer.StandardScore = data.StandardScore;
                    answer.Remark = data.Remark;
                    context.BAnswerT2s.Update(answer);
                    context.BAnswerT2As.UpdateRange(data.answerList.Select(x => new BAnswerT2A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        QuestionSort = x.QuestionSort,
                        AnswerSort = x.AnswerSort,
                    }));
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            return "";
        }
        return answerBasic.AnswerId;
    }

    /// <summary>
    /// 保存题目T3答案
    /// </summary>
    /// <param name="data"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> SaveAnswerT3Async(AnswerT3Model data, string userId)
    {
        var answerBasic = new BAnswer();
        try
        {
            using (var context = new AfasContext())
            {
                var question = await context.BQuestions.FirstOrDefaultAsync(b => b.QuestionId == data.QuestionId);
                if (question == null)
                {
                    throw new Exception("问题不存在");
                }
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    answerBasic = new BAnswer()
                    {
                        AnswerId = NewKey.NewAnswerId(DateHelper.GetDayString()),
                        QuestionnaireId = question.QuestionnaireId,
                        QuestionnaireDate = DateHelper.GetDayString(),
                        UserId = userId,
                        Status = DataStatus.DRAFT.ToString(),
                    };
                    context.BAnswers.Add(answerBasic);
                }

                var answer = await context.BAnswerT3s.FirstOrDefaultAsync(b => b.AnswerId == answerBasic.AnswerId && b.QuestionId == data.QuestionId);
                if (answer == null)
                {
                    answer = new BAnswerT3()
                    {
                        AnswerId = answerBasic.AnswerId,
                        QuestionId = question.QuestionId,
                        Level1 = data.Level1,
                        Level2 = data.Level2,
                        StandardScore = data.StandardScore,
                        Remark = data.Remark,
                    };
                    context.BAnswerT3s.Add(answer);
                    context.BAnswerT3As.AddRange(data.answerList.Select(x => new BAnswerT3A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        QuestionType = x.QuestionType,
                        QuestionSort = x.QuestionSort,
                        Level = x.Level,
                        Value = x.Value,
                    }));
                }
                else
                {
                    answer.Level1 = data.Level1;
                    answer.Level2 = data.Level2;
                    answer.StandardScore = data.StandardScore;
                    answer.Remark = data.Remark;
                    context.BAnswerT3s.Update(answer);
                    context.BAnswerT3As.UpdateRange(data.answerList.Select(x => new BAnswerT3A()
                    {
                        AnswerId = answer.AnswerId,
                        QuestionId = answer.QuestionId,
                        QuestionType = x.QuestionType,
                        QuestionSort = x.QuestionSort,
                        Level = x.Level,
                        Value = x.Value,
                    }));
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            return "";
        }
        return answerBasic.AnswerId;
    }

    #endregion

    #region TestResult【测评结果】

    /// <summary>
    /// 测评结果查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public DataList<TestResultQueryRow> TestResultGridQuery(TableQueryModel<TestResultQueryFields> query)
    {
        var paras = new List<Parameter>();
        var strsql = $@"
            SELECT
                Answer.AnswerId,
                Answer.QuestionnaireId,
                QuestionnaireName,
                VersionName,
                QuestionnaireDate,
                Answer.UserId,
                IFNULL(Student.UserName,'') AS UserName,
                IFNULL(Student.Gender,'') AS Gender,
                IFNULL(Student.Age,0) AS Age,
                Answer.TeacherId,
                IFNULL(Teacher.UserName,'') AS TeacherName,
                Answer.Status,
                RadarImage,
                SImage,
                SResult,
                TImage,
                TResult,
                Weak,
                Advantage,
                Answer.Remark,
                SuggestedCourse,
                IFNULL(SCourse.ItemName,'') AS SuggestedCourseName,
                Answer.LevelCode,
                IFNULL(Standard.LevelName,'') AS LevelName
            FROM b_Answer Answer
            INNER JOIN b_Questionnaire Questionnaire ON Answer.QuestionnaireId = Questionnaire.QuestionnaireId
            LEFT JOIN b_User Student ON Answer.UserId = Student.UserId
            LEFT JOIN b_User Teacher ON Answer.TeacherId = Teacher.UserId
            LEFT JOIN b_Evaluation_Standard Standard ON Answer.LevelCode = Standard.LevelCode
            LEFT JOIN b_Dictionary_Item SCourse ON Answer.SuggestedCourse = SCourse.ItemId AND SCourse.DictionaryId = 'SuggestedCourse'
            LEFT JOIN r_Answer_Import_Detail Import ON Answer.AnswerId = Import.AnswerId
            WHERE 1=1
        ";
        if (query.Data != null)
        {
            #region 构建查询过滤条件
            //测评日期
            var startDay = GetString.FromObject(query.Data.StartDay);
            if (startDay != "")
            {
                strsql += " AND QuestionnaireDate >= @StartDay ";
                paras.Add(new Parameter("StartDay", startDay));
            }
            var endDay = GetString.FromObject(query.Data.EndDay);
            if (endDay != "")
            {
                strsql += " AND QuestionnaireDate <= @EndDay";
                paras.Add(new Parameter("EndDay", endDay));
            }

            //测评状态
            var status = GetString.FromObject(query.Data.Status);
            if (status != "")
            {
                strsql += " AND Answer.Status = @Status";
                paras.Add(new Parameter("Status", status));
            }

            //测评状态
            var importId = GetString.FromObject(query.Data.ImportId);
            if (importId != "")
            {
                strsql += " AND ImportId = @ImportId";
                paras.Add(new Parameter("ImportId", importId));
            }

            //综合查询
            var queryText = GetString.FromObject(query.Data?.QueryText, 50);
            if (queryText != "")
            {
                strsql = GetString.SplitList(query.Data?.QueryText)
                    .Aggregate(strsql, (current, text)
                        => current + $@" AND (QuestionnaireName LIKE '%{text}%'
                    OR IFNULL(Student.UserName,'') LIKE '%{text}%'
                    OR IFNULL(Teacher.UserName,'') LIKE '%{text}%'
                    OR IFNULL(Answer.AnswerId,'') LIKE '%{text}%'
                )");
            }

            #endregion
        }
        if (!userIdentity.IsStaff)
        {
            strsql += " AND Answer.UserId = @UserId";
            paras.Add(new Parameter("UserId", userIdentity.UserId));
        }
        var sortors = new List<KeySorterValue>();
        if (query.Sorter == null)
        {
            sortors.Add(new KeySorterValue());
        }
        else
        {
            sortors.Add(query.Sorter);
        }
        var resultData = new DataList<TestResultQueryRow>();
        using (var context = new AfasContext())
        {
            if (query.Size == 0)
            {
                resultData = new EFCoreExtentions(context).ExecuteSortedQuery<TestResultQueryRow>(strsql, sortors, paras);
            }
            else
            {
                resultData = new EFCoreExtentions(context).ExecutePagedQuery<TestResultQueryRow>(strsql, sortors, query.Index, query.Size, paras);
            }
        }

        return resultData;
    }

    /// <summary>
    /// 保存测评结果
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<string> SaveTestResultAsync(AnswerForm data)
    {
        var answerBasic = new BAnswer();
        try
        {
            #region 保存结果

            using (var context = new AfasContext())
            {
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    throw MessageException.Get(MethodBase.GetCurrentMethod(), "请先答题，再保存结果");
                }
                answerBasic.AnswerId = GetString.FromObject(data.AnswerId, 25);
                answerBasic.QuestionnaireId = GetString.FromObject(data.QuestionnaireId, 6);
                answerBasic.UserId = GetString.FromObject(data.UserId, 32);
                answerBasic.QuestionnaireDate = GetString.FromObject(data.QuestionnaireDate, 10);
                answerBasic.TeacherId = GetString.FromObject(data.TeacherId, 32);
                answerBasic.Status = DataStatus.ACTIVE.ToString();
                answerBasic.RadarImage = GetString.FromObject(data.RadarImage);
                answerBasic.Simage = GetString.FromObject(data.SImage);
                answerBasic.Sresult = GetString.FromObject(data.SResult, 500);
                answerBasic.Timage = GetString.FromObject(data.TImage);
                answerBasic.Tresult = GetString.FromObject(data.TResult, 500);
                answerBasic.Weak = GetString.FromObject(data.Weak, 50);
                answerBasic.Advantage = GetString.FromObject(data.Advantage, 50);
                answerBasic.Remark = GetString.FromObject(data.Remark);
                answerBasic.SuggestedCourse = GetString.FromObject(data.SuggestedCourse, 50);
                answerBasic.LevelCode = GetString.FromObject(data.LevelCode, 50);
                context.BAnswers.Update(answerBasic);
                await context.SaveChangesAsync();
            }

            #endregion

            #region 导出报告文件

            string templatePath = "../AFAS.Static/Template/ELA学习能力测评报告模板.docx"; // 模板文件路径
            string outputPath = $"../AFAS.Static/PDFs/{answerBasic.AnswerId}/";
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            string fileName = Path.Combine(outputPath, "ELA学习能力测评报告.docx");// 导出的文件路径
            if (!Path.Exists(fileName))
            {
                using (FileStream fs = File.Create(fileName))
                {
                }
            }
            string pdfFileName = Path.Combine(outputPath, "ELA学习能力测评报告.pdf");// 导出的文件路径
            if (!Path.Exists(pdfFileName))
            {
                using (FileStream fs = File.Create(pdfFileName))
                {
                }
            }
            // 复制模板文件为输出文件
            File.Copy(templatePath, fileName, true);

            // 替换标记内容
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, true))
            {
                if (wordDoc.MainDocumentPart == null || wordDoc.MainDocumentPart.Document.Body == null)
                {
                    throw MessageException.Get(MethodBase.GetCurrentMethod(), "读取报告模板失败");
                }
                var mainPart = wordDoc.MainDocumentPart;
                var body = wordDoc.MainDocumentPart.Document.Body;
                var student = UserIdentityHelper.GetUserByUserId(answerBasic.UserId, false);
                var teacher = UserIdentityHelper.GetUserByUserId(answerBasic.TeacherId, false);
                var suggestedCourse = DictionaryHelper.GetDictionaryItemName("SuggestedCourse", answerBasic.SuggestedCourse);
                var evaluationStandard = new BEvaluationStandard() { };
                using (var context = new AfasContext())
                {
                    evaluationStandard = await context.BEvaluationStandards.SingleAsync(x => x.LevelCode == answerBasic.LevelCode);
                }
                var answerModel = await GetAnswerListAsync(answerBasic.AnswerId);
                var spassList = answerModel.answerList.FindAll(x => x.QuestionCode.StartsWith("S") && x.StandardScore >= GetInt.FromObject(GetObject.FromProperty(evaluationStandard, x.QuestionCode))).Select(x => EnumHelper<QuestionCodeEnum>.GetDescription(x.QuestionCode));
                var sfailList = answerModel.answerList.FindAll(x => x.QuestionCode.StartsWith("S") && x.StandardScore < GetInt.FromObject(GetObject.FromProperty(evaluationStandard, x.QuestionCode))).Select(x => EnumHelper<QuestionCodeEnum>.GetDescription(x.QuestionCode));
                var tpassList = answerModel.answerList.FindAll(x => x.QuestionCode.StartsWith("T") && x.StandardScore >= GetInt.FromObject(GetObject.FromProperty(evaluationStandard, x.QuestionCode))).Select(x => EnumHelper<QuestionCodeEnum>.GetDescription(x.QuestionCode));
                var tfailList = answerModel.answerList.FindAll(x => x.QuestionCode.StartsWith("T") && x.StandardScore < GetInt.FromObject(GetObject.FromProperty(evaluationStandard, x.QuestionCode))).Select(x => EnumHelper<QuestionCodeEnum>.GetDescription(x.QuestionCode));
                // 遍历所有段落和替换标记
                foreach (var text in body.Descendants<Text>())
                {
                    if (text.Text.Contains("{Name}"))
                    {
                        text.Text = text.Text.Replace("{Name}", student.UserName.ToString());
                    }
                    if (text.Text.Contains("{Sex}"))
                    {
                        text.Text = text.Text.Replace("{Sex}", EnumHelper<GerderEnum>.GetDescription(student.Gender.ToString()));
                    }
                    if (text.Text.Contains("{Age}"))
                    {
                        text.Text = text.Text.Replace("{Age}", student.Age.ToString());
                    }
                    if (text.Text.Contains("{Test Date}"))
                    {
                        text.Text = text.Text.Replace("{Test Date}", answerBasic.QuestionnaireDate.ToString());
                    }
                    if (text.Text.Contains("{Tester}"))
                    {
                        text.Text = text.Text.Replace("{Tester}", teacher.UserName.ToString());
                    }
                    if (text.Text.Contains("{Test No}"))
                    {
                        text.Text = text.Text.Replace("{Test No}", answerBasic.AnswerId.ToString());
                    }
                    if (text.Text.Contains("{视知觉未达标}"))
                    {
                        text.Text = text.Text.Replace("{视知觉未达标}", (sfailList.Count() > 0 ? string.Join("、", sfailList) : ""));
                    }
                    if (text.Text.Contains("{视知觉未达标个数}"))
                    {
                        text.Text = text.Text.Replace("{视知觉未达标个数}", sfailList.Count().ToString());
                    }
                    if (text.Text.Contains("{视知觉未达标均}"))
                    {
                        text.Text = text.Text.Replace("{视知觉未达标均}", (sfailList.Count() > 1 ? "均" : ""));
                    }
                    if (text.Text.Contains("{视知觉达标}"))
                    {
                        text.Text = text.Text.Replace("{视知觉达标}", (spassList.Count() > 0 ? string.Join("、", spassList) : ""));
                    }
                    if (text.Text.Contains("{视知觉达标个数}"))
                    {
                        text.Text = text.Text.Replace("{视知觉达标个数}", spassList.Count().ToString());
                    }
                    if (text.Text.Contains("{视知觉达标均}"))
                    {
                        text.Text = text.Text.Replace("{视知觉达标均}", (spassList.Count() > 1 ? "均" : ""));
                    }
                    if (text.Text.Contains("{听知觉未达标}"))
                    {
                        text.Text = text.Text.Replace("{听知觉未达标}", (tfailList.Count() > 0 ? string.Join("、", tfailList) : ""));
                    }
                    if (text.Text.Contains("{听知觉未达标个数}"))
                    {
                        text.Text = text.Text.Replace("{听知觉未达标个数}", tfailList.Count().ToString());
                    }
                    if (text.Text.Contains("{听知觉未达标均}"))
                    {
                        text.Text = text.Text.Replace("{听知觉未达标均}", (tfailList.Count() > 1 ? "均" : ""));
                    }
                    if (text.Text.Contains("{听知觉达标}"))
                    {
                        text.Text = text.Text.Replace("{听知觉达标}", (tpassList.Count() > 0 ? string.Join("、", tpassList) : ""));
                    }
                    if (text.Text.Contains("{听知觉达标个数}"))
                    {
                        text.Text = text.Text.Replace("{听知觉达标个数}", tpassList.Count().ToString());
                    }
                    if (text.Text.Contains("{听知觉达标均}"))
                    {
                        text.Text = text.Text.Replace("{听知觉达标均}", (tpassList.Count() > 1 ? "均" : ""));
                    }
                    if (text.Text.Contains("{Remark}"))
                    {
                        text.Text = text.Text.Replace("{Remark}", answerBasic.Remark.ToString());
                    }
                    if (text.Text.Contains("{Advantage}"))
                    {
                        text.Text = text.Text.Replace("{Advantage}", "孩子的优势是: " + answerBasic.Advantage.ToString());
                    }
                    if (text.Text.Contains("{Weak}"))
                    {
                        text.Text = text.Text.Replace("{Weak}", "孩子的弱势是: " + answerBasic.Weak.ToString());
                    }
                    if (text.Text.Contains("{Suggest Course}"))
                    {
                        text.Text = text.Text.Replace("{Suggest Course}", suggestedCourse.ToString());
                    }
                }
                // 获取所有图片部分
                var index = 0;
                foreach (var imagePart in mainPart.ImageParts)
                {
                    if (index != 1 && index != 3 && index != 7)
                    {
                        index++;
                        continue;
                    }
                    try
                    {
                        var base64 = "";
                        switch (index)
                        {
                            case 3:
                                base64 = answerModel.SImage;
                                break;
                            case 1:
                                base64 = answerModel.TImage;
                                break;
                            case 7:
                                base64 = answerModel.RadarImage;
                                break;
                            default:
                                break;
                        }
                        // 将 Base64 数据转换为字节数组
                        byte[] imageBytes = Convert.FromBase64String(base64.Replace("data:image/png;base64,", ""));
                        // 用解码后的字节数组替换图片
                        using (Stream imageStream = imagePart.GetStream(FileMode.Create, FileAccess.Write))
                        {
                            imageStream.Write(imageBytes, 0, imageBytes.Length);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    index++;
                }
            }

            FileHelper.Word2Pdf(outputPath, "ELA学习能力测评报告.docx", outputPath, "ELA学习能力测评报告.pdf");


            #endregion
        }
        catch (Exception ex)
        {
            LogHelper.Debug("保存测评结果", "", JsonConvert.SerializeObject(data), ex);
            throw BusinessException.Get(ex).AddMessage(MethodBase.GetCurrentMethod(), "word转pdf失败");
        }
        return answerBasic.AnswerId;
    }

    /// <summary>
    /// 测评结果导入查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public DataList<TestResultImportQueryRow> TestResultImportGridQuery(TableQueryModel<TestResultImportQueryFields> query)
    {
        var paras = new List<Parameter>();
        var strsql = $@"
            SELECT
                Import.ImportId,
                ImportResult,
                ImportStamp,
                IsSuccess,
                IFNULL(ImportCount,'') AS ImportCount,
                ImportUser.UserId,
                IFNULL(ImportUser.UserName,'') AS UserName
            FROM r_Answer_Import Import
            LEFT JOIN (
                SELECT ImportId, COUNT(AnswerId) AS ImportCount FROM r_Answer_Import_Detail
                GROUP BY ImportId
            ) ImportDetail ON ImportDetail.ImportId = Import.ImportId
            LEFT JOIN b_User ImportUser ON ImportUser.UserId = Import.UserId
            WHERE 1=1
        ";
        if (query.Data != null)
        {
            #region 构建查询过滤条件
            //测评日期
            var startDay = GetString.FromObject(query.Data.StartDay);
            if (startDay != "")
            {
                strsql += " AND ImportStamp >= @StartDay ";
                paras.Add(new Parameter("StartDay", startDay));
            }
            var endDay = GetString.FromObject(query.Data.EndDay);
            if (endDay != "")
            {
                strsql += " AND ImportStamp <= @EndDay";
                paras.Add(new Parameter("EndDay", endDay));
            }

            //导入结果状态
            var status = GetInt.FromObject(query.Data.Status);
            if (status != -1)
            {
                strsql += $" AND IsSuccess = {status.ToString()}";
            }

            //综合查询
            var queryText = GetString.FromObject(query.Data?.QueryText, 50);
            if (queryText != "")
            {
                strsql = GetString.SplitList(query.Data?.QueryText)
                    .Aggregate(strsql, (current, text)
                        => current + $@" AND (Import.ImportId LIKE '%{text}%'
                    OR IFNULL(ImportUser.UserName,'') LIKE '%{text}%'
                )");
            }

            #endregion
        }
        var sortors = new List<KeySorterValue>();
        if (query.Sorter == null)
        {
            sortors.Add(new KeySorterValue());
        }
        else
        {
            sortors.Add(query.Sorter);
        }
        var resultData = new DataList<TestResultImportQueryRow>();
        using (var context = new AfasContext())
        {
            if (query.Size == 0)
            {
                resultData = new EFCoreExtentions(context).ExecuteSortedQuery<TestResultImportQueryRow>(strsql, sortors, paras);
            }
            else
            {
                resultData = new EFCoreExtentions(context).ExecutePagedQuery<TestResultImportQueryRow>(strsql, sortors, query.Index, query.Size, paras);
            }
        }

        return resultData;
    }

    /// <summary>
    /// 导入测评结果
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public DataImportResult TestResultImport(Stream stream)
    {
        var result = new DataImportResult();
        try
        {
            var dt = ExcelHelper.GetDataTableFromExcelStream(stream);
            if (dt == null || dt.Rows.Count == 0)
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "导入数据为空");
            }

            var valid = CheckImportDataValid(dt, out var errorMessages, out var testResultList, out var addStudentlist);

            if (valid)
            {
                #region 执行导入操作
                var answerList = new List<BAnswer>();
                var answerS1List = new List<BAnswerS1>();
                var answerS2List = new List<BAnswerS2>();
                var answerS3List = new List<BAnswerS3>();
                var answerS4List = new List<BAnswerS4>();
                var answerS5List = new List<BAnswerS5>();
                var answerT1List = new List<BAnswerT1>();
                var answerT2List = new List<BAnswerT2>();
                var answerT3List = new List<BAnswerT3>();

                foreach (var item in testResultList)
                {
                    answerList.Add(new BAnswer()
                    {
                        AnswerId = item.AnswerId,
                        QuestionnaireDate = item.QuestionnaireDate,
                        QuestionnaireId = item.QuestionnaireId,
                        UserId = item.UserId,
                        LevelCode = item.LevelCode,
                        SuggestedCourse = item.SuggestedCourse,
                        Status = item.Status,
                        TeacherId = item.TeacherId
                    });
                    var s1 = item.answerList.Find(x => x.QuestionCode == "S1"); if (s1 != null) answerS1List.Add(new BAnswerS1() { AnswerId = item.AnswerId, StandardScore = s1.StandardScore, Remark = s1.Remark });
                    var s2 = item.answerList.Find(x => x.QuestionCode == "S2"); if (s2 != null) answerS2List.Add(new BAnswerS2() { AnswerId = item.AnswerId, StandardScore = s2.StandardScore, Remark = s2.Remark });
                    var s3 = item.answerList.Find(x => x.QuestionCode == "S3"); if (s3 != null) answerS3List.Add(new BAnswerS3() { AnswerId = item.AnswerId, StandardScore = s3.StandardScore, Remark = s3.Remark });
                    var s4 = item.answerList.Find(x => x.QuestionCode == "S4"); if (s4 != null) answerS4List.Add(new BAnswerS4() { AnswerId = item.AnswerId, StandardScore = s4.StandardScore, Remark = s4.Remark });
                    var s5 = item.answerList.Find(x => x.QuestionCode == "S5"); if (s5 != null) answerS5List.Add(new BAnswerS5() { AnswerId = item.AnswerId, StandardScore = s5.StandardScore, Remark = s5.Remark });
                    var t1 = item.answerList.Find(x => x.QuestionCode == "T1"); if (t1 != null) answerT1List.Add(new BAnswerT1() { AnswerId = item.AnswerId, StandardScore = t1.StandardScore, Remark = t1.Remark });
                    var t2 = item.answerList.Find(x => x.QuestionCode == "T2"); if (t2 != null) answerT2List.Add(new BAnswerT2() { AnswerId = item.AnswerId, StandardScore = t2.StandardScore, Remark = t2.Remark });
                    var t3 = item.answerList.Find(x => x.QuestionCode == "T3"); if (t3 != null) answerT3List.Add(new BAnswerT3() { AnswerId = item.AnswerId, StandardScore = t3.StandardScore, Remark = t3.Remark });
                }
                using(var context = new AfasContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.BAnswers.AddRange(answerList);
                            context.BAnswerS1s.AddRange(answerS1List);
                            context.BAnswerS2s.AddRange(answerS2List);
                            context.BAnswerS3s.AddRange(answerS3List);
                            context.BAnswerS4s.AddRange(answerS4List);
                            context.BAnswerS5s.AddRange(answerS5List);
                            context.BAnswerT1s.AddRange(answerT1List);
                            context.BAnswerT2s.AddRange(answerT2List);
                            context.BAnswerT3s.AddRange(answerT3List);
                            context.BUsers.AddRange(addStudentlist);
                            context.SaveChanges(); // 保存数据

                            transaction.Commit(); // 提交事务
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // 发生异常时回滚事务
                            throw BusinessException.Get(ex).AddMessage(MethodBase.GetCurrentMethod(), "保存导入测评结果失败");
                        }
                    }
                }
                
                #endregion

                result.Success = true;
                result.SuccessCount = testResultList.Count;
                result.Data = testResultList;

                var answerImport = new RAnswerImport()
                {
                    ImportId = NewCode.Ul25Key,
                    ImportStamp = DateHelper.GetDateString(),
                    ImportResult = JsonConvert.SerializeObject(result),
                    IsSuccess = result.Success,
                    UserId = userIdentity.UserId,
                };
                var answerImportDetail = testResultList.Select(x => new RAnswerImportDetail() 
                { 
                    ImportId = answerImport.ImportId, 
                    AnswerId = x.AnswerId 
                });

                using (var context = new AfasContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.RAnswerImports.AddRange(answerImport);
                            context.RAnswerImportDetails.AddRange(answerImportDetail);
                            context.SaveChanges(); // 保存数据

                            transaction.Commit(); // 提交事务
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // 发生异常时回滚事务
                            throw BusinessException.Get(ex).AddMessage(MethodBase.GetCurrentMethod(), "生成导入测评结果记录失败");
                        }
                    }
                }

            }
            else
            {
                result.Success = false;
                result.ErrorCount = dt.AsEnumerable().Count(x => !string.IsNullOrWhiteSpace(GetString.FromObject(x["校验备注"])));
                result.ErrorMessages = errorMessages;

                //导出错误提示
                var fileName = $"测评结果导入_错误文件_{DateHelper.GetDateStringMark2()}.xlsx";
                result.OutputUrl = $"Excels/{fileName}";

                var answerImport = new RAnswerImport()
                {
                    ImportId = NewCode.Ul25Key,
                    ImportStamp = DateHelper.GetDateString(),
                    ImportResult = JsonConvert.SerializeObject(result),
                    IsSuccess = result.Success,
                    UserId = userIdentity.UserId,
                };
                using (var context = new AfasContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.RAnswerImports.AddRange(answerImport);
                            context.SaveChanges(); // 保存数据

                            transaction.Commit(); // 提交事务
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // 发生异常时回滚事务
                            throw BusinessException.Get(ex).AddMessage(MethodBase.GetCurrentMethod(), "生成导入测评结果记录失败");
                        }
                    }
                }
            }
        }
        catch (BusinessException ex)
        {
            result.Success = false;
            result.SuccessCount = 0;
            result.ErrorMessages.Add(ex.Message);
        }
        finally
        {
            LogHelper.Debug($"导入测评结果{(result.Success ? "成功" : "失败")}: ", result);
        }

        return result;
    }

    /// <summary>
    /// 检查导入数据是否有效
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="errorMessages"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    private static bool CheckImportDataValid(DataTable dt,
        out List<string> errorMessages,
        out List<AnswerModel> list,
        out List<BUser> addStudentlist)
    {
        var success = true;
        errorMessages = [];
        list = new List<AnswerModel>();
        addStudentlist = new List<BUser>();

        #region 检查导入模版格式

        if (dt.Columns[0].ColumnName != "测评版本")
        {
            throw new Exception("导入数据第一列必须为测评版本");
        }

        if (!dt.Columns.Contains("姓名"))
        {
            throw new Exception("导入数据必须包含姓名列");
        }

        if (!dt.Columns.Contains("测评标准"))
        {
            throw new Exception("导入数据必须包含测评标准列");
        }

        if (!dt.Columns.Contains("测评日期"))
        {
            throw new Exception("导入数据必须包含测评日期列");
        }

        if (!dt.Columns.Contains("性别"))
        {
            throw new Exception("导入数据必须包含性别列");
        }

        if (!dt.Columns.Contains("年龄"))
        {
            throw new Exception("导入数据必须包含年龄列");
        }

        if (!dt.Columns.Contains("视觉广度"))
        {
            throw new Exception("导入数据必须包含视觉广度列");
        }

        if (!dt.Columns.Contains("视觉广度_备注"))
        {
            throw new Exception("导入数据必须包含视觉广度_备注列");
        }

        if (!dt.Columns.Contains("视觉稳定性"))
        {
            throw new Exception("导入数据必须包含视觉稳定性列");
        }

        if (!dt.Columns.Contains("视觉稳定性_备注"))
        {
            throw new Exception("导入数据必须包视觉稳定性_备注列");
        }

        if (!dt.Columns.Contains("视觉转移"))
        {
            throw new Exception("导入数据必须包含视觉转移列");
        }

        if (!dt.Columns.Contains("视觉转移_备注"))
        {
            throw new Exception("导入数据必须包含视觉转移_备注列");
        }

        if (!dt.Columns.Contains("手眼协调"))
        {
            throw new Exception("导入数据必须包含手眼协调列");
        }

        if (!dt.Columns.Contains("手眼协调_备注"))
        {
            throw new Exception("导入数据必须包含手眼协调_备注列");
        }

        if (!dt.Columns.Contains("视觉工作记忆"))
        {
            throw new Exception("导入数据必须包含视觉工作记忆列");
        }

        if (!dt.Columns.Contains("视觉工作记忆_备注"))
        {
            throw new Exception("导入数据必须包含视觉工作记忆_备注列");
        }

        if (!dt.Columns.Contains("听觉集中"))
        {
            throw new Exception("导入数据必须包含听觉集中列");
        }

        if (!dt.Columns.Contains("听觉集中_备注"))
        {
            throw new Exception("导入数据必须包含听觉集中_备注列");
        }

        if (!dt.Columns.Contains("听觉分辨"))
        {
            throw new Exception("导入数据必须包含听觉分辨列");
        }

        if (!dt.Columns.Contains("听觉分辨_备注"))
        {
            throw new Exception("导入数据必须包含听觉分辨_备注列");
        }

        if (!dt.Columns.Contains("听觉记忆"))
        {
            throw new Exception("导入数据必须包含听觉记忆列");
        }

        if (!dt.Columns.Contains("听觉记忆_备注"))
        {
            throw new Exception("导入数据必须包含听觉记忆_备注列");
        }

        if (!dt.Columns.Contains("建议课程"))
        {
            throw new Exception("导入数据必须包含建议课程列");
        }

        if (!dt.Columns.Contains("测评老师"))
        {
            throw new Exception("导入数据必须包含测评老师列");
        }

        if (dt.Columns.Contains("校验备注"))
        {
            dt.Columns.Remove("校验备注");
        }
        dt.Columns.Add("校验备注");

        #endregion

        #region 校验数据

        var studentList = UserIdentityHelper.GetUserListByRoleId(RoleEnum.STUDENT.ToString()).ToList();
        var teacherList = UserIdentityHelper.GetUserListByRoleId(RoleEnum.TEACHER.ToString()).ToList(); 
        var evaluationStandardList = EvaluationStandardHelper.GetEvaluationStandardListAsync().GetAwaiter().GetResult();
        var suggestedCourseList = DictionaryHelper.GetDictionaryList("SuggestedCourse").ToList();
        var questionnaireList = QuestionnaireHelper.GetQuestionnaireListAsync().GetAwaiter().GetResult();

        #endregion

        #region 导入数据赋值
        var dayStr = DateHelper.GetDayString().Replace("-", "");
        var currentAnswerIndex = GetInt.FromObject(NewKey.NewAnswerId(dayStr).Replace(dayStr, ""));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];   
            var checkEmptyRow = true;
            foreach (DataColumn column in dt.Columns)
            {
                if (GetString.FromObject(dr[column.ColumnName]) != "")
                {
                    checkEmptyRow = false;
                    break;
                }
            }
            if (checkEmptyRow)
                continue;

            var tips = new List<string>();

            var item = new AnswerModel
            {
                AnswerId = dayStr + (currentAnswerIndex + i).ToString().PadLeft(3, '0'),
                Status = DataStatus.DRAFT.ToString() 
            };

            var versionName = GetString.FromObject(dr["测评版本"]);
            var questionnaire = questionnaireList.Find(x => x.VersionName == versionName);
            if (questionnaire == null)
            {
                tips.Add("测评版本不存在");
            }
            else 
            {
                item.QuestionnaireId = questionnaire.QuestionnaireId;
            }

            var userName = GetString.FromObject(dr["姓名"]);
            var gerder = GetString.FromObject(dr["性别"]) == "男" ? GerderEnum.MALE.ToString() : GerderEnum.FEMALE.ToString();
            var age = GetInt.FromObject(dr["年龄"]);
            var student = studentList.Find(x => x.UserName == userName && x.Age == age && x.Gender == gerder);
            if (student == null)
            {
                //按姓名和年龄、性别控制唯一测评对象
                var addStudent = addStudentlist.Find(x => x.UserName == userName && x.Age == age && x.Gender == gerder);
                //不存在时新增
                if (addStudent == null)
                {
                    addStudent = new BUser()
                    {
                        UserId = NewCode.KeyId,
                        UserName = userName,
                        Gender = gerder,
                        Age = age,
                        Account = NewKey.NewAccount(userName),
                        NickName = userName,
                        Password = PasswordHelper.Encrypt(PinYinHelper.GetFirstPinYin(userName) + "123"),
                        Role = RoleEnum.STUDENT.ToString(),
                    };
                    addStudentlist.Add(addStudent);
                }
                item.UserId = addStudent.UserId;
            }
            else
            {
                item.UserId = student.UserId;
            }

            var levelName = GetString.FromObject(dr["测评标准"]);
            var evaluationStandard = evaluationStandardList.Find(x => x.LevelName == levelName);
            if (evaluationStandard == null)
            {
                tips.Add("测评标准不存在");
            }
            else
            {
                item.LevelCode = evaluationStandard.LevelCode;
            }

            var questionnaireDate = GetString.FromObject(dr["测评日期"]);
            questionnaireDate = DateHelper.GetDayString(DateHelper.GetDateTime(questionnaireDate, DateTime.MinValue));
            if (questionnaireDate == DateHelper.GetDayString(DateTime.MinValue))
            {
                tips.Add("测评日期格式不正确");
            }
            else
            {
                item.QuestionnaireDate = questionnaireDate;
            }

            var suggestedCourseName = GetString.FromObject(dr["建议课程"]);
            var suggestedCourse = suggestedCourseList.Find(x => x.ItemName == suggestedCourseName);
            if (suggestedCourse == null)
            {
                tips.Add("建议课程不存在");
            }
            else
            {
                item.SuggestedCourse = suggestedCourse.ItemId;
            }

            var teacherName = GetString.FromObject(dr["测评老师"]);
            var teacher = teacherList.Find(x => x.UserName == teacherName);
            if (teacher == null)
            {
                tips.Add("测评老师不存在");
            }
            else
            {
                item.TeacherId = teacher.UserId;
            }

            var s1 = GetInt.FromObject(dr["视觉广度"], -1);
            if (s1 == -1)
            {
                tips.Add("视觉广度格式不正确");
            }
            else
            {
                var remark = GetString.FromObject(dr["视觉广度_备注"]);
                item.answerList.Add(new AnswerItem()
                {
                    QuestionCode = "S1",
                    Remark = remark,
                    StandardScore = s1
                });
            }

            var s2 = GetInt.FromObject(dr["视觉稳定性"], -1);
            if (s2 == -1)
            {
                tips.Add("视觉稳定性格式不正确");
            }
            else
            {
                var remark = GetString.FromObject(dr["视觉稳定性_备注"]);
                item.answerList.Add(new AnswerItem()
                {
                    QuestionCode = "S2",
                    Remark = remark,
                    StandardScore = s2
                });
            }

            var s3 = GetInt.FromObject(dr["视觉转移"], -1);
            if (s3 == -1)
            {
                tips.Add("视觉转移格式不正确");
            }
            else
            {
                var remark = GetString.FromObject(dr["视觉转移_备注"]);
                item.answerList.Add(new AnswerItem()
                {
                    QuestionCode = "S3",
                    Remark = remark,
                    StandardScore = s3
                });
            }

            var s4 = GetInt.FromObject(dr["手眼协调"], -1);
            if (s4 == -1)
            {
                tips.Add("手眼协调格式不正确");
            }
            else
            {
                var remark = GetString.FromObject(dr["手眼协调_备注"]);
                item.answerList.Add(new AnswerItem()
                {
                    QuestionCode = "S4",
                    Remark = remark,
                    StandardScore = s4
                });
            }

            var s5 = GetInt.FromObject(dr["视觉工作记忆"], -1);
            if (s5 == -1)
            {
                tips.Add("视觉工作记忆格式不正确");
            }
            else
            {
                var remark = GetString.FromObject(dr["视觉工作记忆_备注"]);
                item.answerList.Add(new AnswerItem()
                {
                    QuestionCode = "S5",
                    Remark = remark,
                    StandardScore = s5
                });
            }

            var t1 = GetInt.FromObject(dr["听觉集中"], -1);
            if (t1 == -1)
            {
                tips.Add("听觉集中格式不正确");
            }
            else
            {
                var remark = GetString.FromObject(dr["听觉集中_备注"]);
                item.answerList.Add(new AnswerItem()
                {
                    QuestionCode = "T1",
                    Remark = remark,
                    StandardScore = t1
                });
            }

            var t2 = GetInt.FromObject(dr["听觉分辨"], -1);
            if (t2 == -1)
            {
                tips.Add("听觉分辨格式不正确");
            }
            else
            {
                var remark = GetString.FromObject(dr["听觉分辨_备注"]);
                item.answerList.Add(new AnswerItem()
                {
                    QuestionCode = "T2",
                    Remark = remark,
                    StandardScore = t2
                });
            }

            var t3 = GetInt.FromObject(dr["听觉记忆"], -1);
            if (t3 == -1)
            {
                tips.Add("听觉记忆格式不正确");
            }
            else
            {
                var remark = GetString.FromObject(dr["听觉记忆_备注"]);
                item.answerList.Add(new AnswerItem()
                {
                    QuestionCode = "T3",
                    Remark = remark,
                    StandardScore = t3
                });
            }


            if (tips.Count == 0)
            {
                list.Add(item);
            }
            else
            {
                success = false;
                dr["校验备注"] = string.Join("、", tips);
            }
        }

        #endregion

        #region 提炼错误信息

        var errorRows = dt.AsEnumerable()
            .Where(dr => !string.IsNullOrWhiteSpace(GetString.FromObject(dr["校验备注"])))
            .Select(dr => GetString.FromObject(dr["校验备注"])).ToList();

        var errorCount = errorRows.Count(x => x.Contains("测评版本不存在"));
        if (errorCount > 0)
        {
            errorMessages.Add($"测评版本不存在:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("测评标准不存在"));
        if (errorCount > 0)
        {
            errorMessages.Add($"测评标准不存在:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("测评日期格式不正确"));
        if (errorCount > 0)
        {
            errorMessages.Add($"测评日期格式不正确:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("建议课程不存在"));
        if (errorCount > 0)
        {
            errorMessages.Add($"建议课程不存在:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("测评老师不存在"));
        if (errorCount > 0)
        {
            errorMessages.Add($"测评老师不存在:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("视觉广度格式不正确"));
        if (errorCount > 0)
        {
            errorMessages.Add($"视觉广度格式不正确:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("视觉稳定性格式不正确"));
        if (errorCount > 0)
        {
            errorMessages.Add($"视觉稳定性格式不正确:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("视觉转移格式不正确"));
        if (errorCount > 0)
        {
            errorMessages.Add($"视觉转移格式不正确:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("手眼协调格式不正确"));
        if (errorCount > 0)
        {
            errorMessages.Add($"手眼协调格式不正确:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("视觉工作记忆格式不正确"));
        if (errorCount > 0)
        {
            errorMessages.Add($"视觉工作记忆格式不正确:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("听觉集中格式不正确"));
        if (errorCount > 0)
        {
            errorMessages.Add($"听觉集中格式不正确:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("听觉分辨格式不正确"));
        if (errorCount > 0)
        {
            errorMessages.Add($"听觉分辨格式不正确:{errorCount}行");
        }

        errorCount = errorRows.Count(x => x.Contains("听觉记忆格式不正确"));
        if (errorCount > 0)
        {
            errorMessages.Add($"听觉记忆格式不正确:{errorCount}行");
        }

        #endregion

        return success;
    }

    /// <summary>
    /// 删除测评结果
    /// </summary>
    /// <param name="answerId"></param>
    /// <returns></returns>
    public void RemoveTestResult(string answerId)
    {
        var paras = new List<Parameter>();
        try
        {
            using (var context = new AfasContext())
            {
                var strsql = @"
                DELETE FROM b_Answer WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_S1 WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_S1_A WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_S2 WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_S2_A WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_S3 WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_S3_A WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_S4 WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_S5 WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_T1 WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_T1_A WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_T2 WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_T2_A WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_T3 WHERE AnswerId = @AnswerId;
                DELETE FROM b_Answer_T3_A WHERE AnswerId = @AnswerId;            
                ";

                paras.Add(new Parameter("AnswerId", answerId));
                var res = new EFCoreExtentions(context).ExecuteSqlNonQuery(strsql, paras);
                string outputPath = $"../AFAS.Static/PDFs/{answerId}/ELA学习能力测评报告.docx";
                if (Path.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
                string outputPath1 = $"../AFAS.Static/PDFs/{answerId}/ELA学习能力测评报告.pdf";
                if (Path.Exists(outputPath1))
                {
                    File.Delete(outputPath1);
                }
            }

        }
        catch (Exception ex)
        {
            throw BusinessException.Get(ex).AddMessage(MethodBase.GetCurrentMethod(), "删除测试结果报错");
        }
    }

    #endregion
}
