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

        #endregion
    }
}
