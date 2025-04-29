using AFAS.Authorization.Attributes;
using AFAS.Business.Questionnaire;
using AFAS.Entity;
using AFAS.Models.Question;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;
using WingWell.WebApi.Platform;

namespace AFAS.Controllers
{
    [UserToken]
    [ApiController]
    [ApiExplorerSettings(GroupName = WebApiConfig.Questionnaire)]
    [Route("[controller]/[action]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly ILogger<QuestionnaireController> _logger;
        private readonly IQuestionnaireService _questionnaireService;

        public QuestionnaireController(IQuestionnaireService service, ILogger<QuestionnaireController> logger)
        {
            _logger = logger;
            _questionnaireService = service;
        }


        #region Questionnaire

        /// <summary>
        /// 获取试卷列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<List<BQuestionnaire>>> GetQuestionnaireListAsync()
        => new(await _questionnaireService.GetQuestionnaireListAsync());

        /// <summary>
        /// 获取试卷列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel<QuestionnaireModel>> GetQuestionnaireModelAsync(string questionnaireId)
        => new(await _questionnaireService.GetQuestionnaireModelAsync(questionnaireId));

        /// <summary>
        /// 获取试卷
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel<BQuestionnaire>> GetQuestionnaireAsync(string questionnaireId)
        => new(await _questionnaireService.GetQuestionnaireAsync(questionnaireId));

        /// <summary>
        /// 保存试卷
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionnaireAsync(QuestionnaireForm data)
        => new(await _questionnaireService.SaveQuestionnaireAsync(data));

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel> RemoveQuestionnaireAsync(string questionnaireId)
        {
            await _questionnaireService.RemoveQuestionnaireAsync(questionnaireId);
            return new ResponseModel();
        }

        #endregion

        #region Question

        /// <summary>
        /// 获取题目列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel<List<BQuestion>>> GetQuestionListAsync(string questionnaireId)
        => new(await _questionnaireService.GetQuestionListAsync(questionnaireId));

        /// <summary>
        /// 获取题目S1信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS1Model>> GetQuestionS1Async(string questionId)
        => new(await _questionnaireService.GetQuestionS1Async(questionId));

        /// <summary>
        /// 获取题目S2信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS2Model>> GetQuestionS2Async(string questionId)
        => new(await _questionnaireService.GetQuestionS2Async(questionId));

        /// <summary>
        /// 获取题目S3信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS3Model>> GetQuestionS3Async(string questionId)
        => new(await _questionnaireService.GetQuestionS3Async(questionId));

        /// <summary>
        /// 获取题目S4信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS4Model>> GetQuestionS4Async(string questionId)
        => new(await _questionnaireService.GetQuestionS4Async(questionId));

        /// <summary>
        /// 获取题目S5信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS5Model>> GetQuestionS5Async(string questionId)
        => new(await _questionnaireService.GetQuestionS5Async(questionId));

        /// <summary>
        /// 获取题目T1信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionT1Model>> GetQuestionT1Async(string questionId)
        => new(await _questionnaireService.GetQuestionT1Async(questionId));

        /// <summary>
        /// 获取题目T2信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionT2Model>> GetQuestionT2Async(string questionId)
        => new(await _questionnaireService.GetQuestionT2Async(questionId));

        /// <summary>
        /// 获取题目T3信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionT3Model>> GetQuestionT3Async(string questionId)
        => new(await _questionnaireService.GetQuestionT3Async(questionId));

        #endregion

        #region Answer

        /// <summary>
        /// 获取题目答案
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        [HttpPost("{answerId}")]
        public async Task<ResponseModel<AnswerModel>> GetAnswerListAsync(string answerId)
            => new(await _questionnaireService.GetAnswerListAsync(answerId));

        ///// <summary>
        ///// 保存题目答案
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ResponseModel<string>> SaveAnswerAsync(AnswerForm data)
        //    => new(await _questionnaireService.SaveAnswerAsync(data));

        /// <summary>
        /// 保存题目S1答案
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS1Async(AnswerS1Model data, string userId = "")
            => new(await _questionnaireService.SaveAnswerS1Async(data, userId));

        /// <summary>
        /// 保存题目S2答案
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS2Async(AnswerS2Model data, string userId = "")
            => new(await _questionnaireService.SaveAnswerS2Async(data, userId));

        /// <summary>
        /// 保存题目S3答案
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS3Async(AnswerS3Model data, string userId = "")
            => new(await _questionnaireService.SaveAnswerS3Async(data, userId));

        /// <summary>
        /// 保存题目S4答案
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS4Async(AnswerS4Model data, string userId = "")
            => new(await _questionnaireService.SaveAnswerS4Async(data, userId));

        /// <summary>
        /// 保存题目S5答案
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS5Async(AnswerS5Model data, string userId = "")
            => new(await _questionnaireService.SaveAnswerS5Async(data, userId));

        /// <summary>
        /// 保存题目T1答案
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerT1Async(AnswerT1Model data, string userId = "")
            => new(await _questionnaireService.SaveAnswerT1Async(data, userId));

        /// <summary>
        /// 保存题目T2答案
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerT2Async(AnswerT2Model data, string userId = "")
            => new(await _questionnaireService.SaveAnswerT2Async(data, userId));

        /// <summary>
        /// 保存题目T3答案
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerT3Async(AnswerT3Model data, string userId = "")
            => new(await _questionnaireService.SaveAnswerT3Async(data, userId));
        #endregion

        #region Report


        #endregion
    }
}
