using AFAS.Authorization;
using AFAS.Authorization.AuthInfos;
using AFAS.Authorization.Enums;
using AFAS.Authorization.Models;
using AFAS.Entity;
using AFAS.Enums;
using AFAS.Infrastructure;
using AFAS.Internals;
using AFAS.Models.Question;
using AFAS.Models.TestResult;
using AFAS.Models.User;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
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
                AnswerId,
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

            //综合查询
            var queryText = GetString.FromObject(query.Data?.QueryText, 50);
            if (queryText != "")
            {
                strsql = GetString.SplitList(query.Data?.QueryText)
                    .Aggregate(strsql, (current, text)
                        => current + $@" AND (QuestionnaireName LIKE '%{text}%'
                    OR IFNULL(Student.UserName,'') LIKE '%{text}%'
                    OR IFNULL(Teacher.UserName,'') LIKE '%{text}%'
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

            string templatePath = "../AFAS.Static/Words/ELA学习能力测评报告模板.docx"; // 模板文件路径
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
        catch (Exception)
        {
            return "";
        }
        return answerBasic.AnswerId;
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
