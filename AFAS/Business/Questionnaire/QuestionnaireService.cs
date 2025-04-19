using AFAS.Entitys;
using AFAS.Internals;
using AFAS.Models.Question;
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
            question.QuestionList = await context.BQuestionS1s.Where(x => x.QuestionId == questionId).OrderBy(x=>x.GridSort).ToListAsync();
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
            question.QuestionList = await context.BQuestionS3s.Where(x => x.QuestionId == questionId).ToListAsync();
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
            var questionT1 = await context.BQuestionT1s.SingleAsync(x => x.QuestionId == questionId);
            question = new QuestionT1Model()
            {
                QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId),
                NumberQuestion = questionT1.NumberQuestion,
                StoryQuestion = questionT1.StoryQuestion,
                Number1 = questionT1.Number1,
                Number2 = questionT1.Number2,
                Number3 = questionT1.Number3,
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
            var questionT2 = await context.BQuestionT2s.SingleAsync(x => x.QuestionId == questionId);
            question = new QuestionT2Model()
            {
                QuestionInfo = await context.BQuestions.SingleAsync(x => x.QuestionId == questionId),
                Question = questionT2.Question,
                Number1 = questionT2.Number1,
                Number2 = questionT2.Number2,
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

    #region Answer

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
                if (question == null) {
                    throw new Exception("问题不存在");
                }
                answerBasic = await context.BAnswers.FirstOrDefaultAsync(b => b.AnswerId == data.AnswerId);
                if (answerBasic == null)
                {
                    answerBasic = new BAnswer()
                    {
                        AnswerId = NewCode.Ul25Key,
                        QuestionnaireId = question.QuestionnaireId,
                        UserId = userId,
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
                        AnswerId = NewCode.Ul25Key,
                        QuestionnaireId = question.QuestionnaireId,
                        UserId = userId,
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
                        AnswerId = NewCode.Ul25Key,
                        QuestionnaireId = question.QuestionnaireId,
                        UserId = userId,
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

    #endregion
}
