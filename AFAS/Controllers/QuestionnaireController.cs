using AFAS.Business.Questionnaire;
using AFAS.Entitys;
using AFAS.Models;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;

namespace AFAS.Controllers
{
    [ApiController]
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
        /// ��ȡ�Ծ��б�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<List<BQuestionnaire>>> GetQuestionnaireListAsync()
        => new (await _questionnaireService.GetQuestionnaireListAsync());

        /// <summary>
        /// ��ȡ�Ծ��б�
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel<QuestionnaireModel>> GetQuestionnaireModelAsync(string questionnaireId)
        => new(await _questionnaireService.GetQuestionnaireModelAsync(questionnaireId));

        /// <summary>
        /// ��ȡ�Ծ�
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel<BQuestionnaire>> GetQuestionnaireAsync(string questionnaireId)
        => new(await _questionnaireService.GetQuestionnaireAsync(questionnaireId));

        /// <summary>
        /// �����Ծ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionnaireAsync(QuestionnaireForm data)
        => new(await _questionnaireService.SaveQuestionnaireAsync(data));

        /// <summary>
        /// ɾ���Ծ�
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
        /// ��ȡ��Ŀ�б�
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel<List<BQuestion>>> GetQuestionListAsync(string questionnaireId)
        => new(await _questionnaireService.GetQuestionListAsync(questionnaireId));

        /// <summary>
        /// ��ȡ��ĿS1��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<List<BQuestionS1>>> GetQuestionS1Async(string questionId)
        => new(await _questionnaireService.GetQuestionS1Async(questionId));

        /// <summary>
        /// ��ȡ��ĿS2��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<List<BQuestionS2>>> GetQuestionS2Async(string questionId)
        => new(await _questionnaireService.GetQuestionS2Async(questionId));

        /// <summary>
        /// ��ȡ��ĿS3��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<List<BQuestionS3>>> GetQuestionS3Async(string questionId)
        => new(await _questionnaireService.GetQuestionS3Async(questionId));

        /// <summary>
        /// ��ȡ��ĿS4��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<BQuestionS4>> GetQuestionS4Async(string questionId)
        => new(await _questionnaireService.GetQuestionS4Async(questionId));

        /// <summary>
        /// ��ȡ��ĿS5��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<List<BQuestionS5>>> GetQuestionS5Async(string questionId)
        => new(await _questionnaireService.GetQuestionS5Async(questionId));

        /// <summary>
        /// ��ȡ��ĿT1��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionT1Model>> GetQuestionT1Async(string questionId)
        => new(await _questionnaireService.GetQuestionT1Async(questionId));

        /// <summary>
        /// ��ȡ��ĿT2��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionT2Model>> GetQuestionT2Async(string questionId)
        => new(await _questionnaireService.GetQuestionT2Async(questionId));

        /// <summary>
        /// ��ȡ��ĿT3��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<List<BQuestionT3>>> GetQuestionT3Async(string questionId)
        => new(await _questionnaireService.GetQuestionT3Async(questionId));

        #endregion
    }
}
