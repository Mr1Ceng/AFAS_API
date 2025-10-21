using AFAS.Authorization.Attributes;
using AFAS.Business.Questionnaire;
using AFAS.Business.User;
using AFAS.Entity;
using AFAS.Infrastructure.Models;
using AFAS.Models.Question;
using AFAS.Models.TestResult;
using AFAS.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;
using System.Reflection;
using AFAS.WebApi.Platform;

namespace AFAS.Controllers
{
    [UserToken]
    [ApiController]
    [ApiExplorerSettings(GroupName = WebApiConfig.Questionnaire)]
    [Route("[controller]/[action]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly ILogger<QuestionnaireController> _logger;
        private readonly IQuestionnaireService _service;

        public QuestionnaireController(IQuestionnaireService service, ILogger<QuestionnaireController> logger)
        {
            _logger = logger;
            _service = service;
        }


        #region Questionnaire

        /// <summary>
        /// ��ȡ�Ծ��б�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<List<BQuestionnaire>>> GetQuestionnaireListAsync()
        => new(await _service.GetQuestionnaireListAsync());

        /// <summary>
        /// ��ȡ�Ծ��б�
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel<QuestionnaireModel>> GetQuestionnaireModelAsync(string questionnaireId)
        => new(await _service.GetQuestionnaireModelAsync(questionnaireId));

        /// <summary>
        /// ��ȡ�Ծ�
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel<BQuestionnaire>> GetQuestionnaireAsync(string questionnaireId)
        => new(await _service.GetQuestionnaireAsync(questionnaireId));

        /// <summary>
        /// �����Ծ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionnaireAsync(QuestionnaireForm data)
        => new(await _service.SaveQuestionnaireAsync(data));

        /// <summary>
        /// ɾ���Ծ�
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        [HttpPost("{questionnaireId}")]
        public async Task<ResponseModel> RemoveQuestionnaireAsync(string questionnaireId)
        {
            await _service.RemoveQuestionnaireAsync(questionnaireId);
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
        => new(await _service.GetQuestionListAsync(questionnaireId));

        /// <summary>
        /// ��ȡ��ĿS1��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS1Model>> GetQuestionS1Async(string questionId)
        => new(await _service.GetQuestionS1Async(questionId));

        /// <summary>
        /// ��ȡ��ĿS2��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS2Model>> GetQuestionS2Async(string questionId)
        => new(await _service.GetQuestionS2Async(questionId));

        /// <summary>
        /// ��ȡ��ĿS3��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS3Model>> GetQuestionS3Async(string questionId)
        => new(await _service.GetQuestionS3Async(questionId));

        /// <summary>
        /// ��ȡ��ĿS4��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS4Model>> GetQuestionS4Async(string questionId)
        => new(await _service.GetQuestionS4Async(questionId));

        /// <summary>
        /// ��ȡ��ĿS5��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionS5Model>> GetQuestionS5Async(string questionId)
        => new(await _service.GetQuestionS5Async(questionId));

        /// <summary>
        /// ��ȡ��ĿT1��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionT1Model>> GetQuestionT1Async(string questionId)
        => new(await _service.GetQuestionT1Async(questionId));

        /// <summary>
        /// ��ȡ��ĿT2��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionT2Model>> GetQuestionT2Async(string questionId)
        => new(await _service.GetQuestionT2Async(questionId));

        /// <summary>
        /// ��ȡ��ĿT3��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel<QuestionT3Model>> GetQuestionT3Async(string questionId)
        => new(await _service.GetQuestionT3Async(questionId));

        #endregion

        #region Qusetion ����

        /// <summary>
        /// ������ĿS1��Ϣ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionS1Async(QuestionS1Model data)
            => new(await _service.SaveQuestionS1Async(data));

        /// <summary>
        /// ������ĿS2��Ϣ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionS2Async(QuestionS2Model data)
            => new(await _service.SaveQuestionS2Async(data));

        /// <summary>
        /// ������ĿS3��Ϣ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionS3Async(QuestionS3Model data)
            => new(await _service.SaveQuestionS3Async(data));

        /// <summary>
        /// ������ĿS4��Ϣ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionS4Async(QuestionS4Model data)
            => new(await _service.SaveQuestionS4Async(data));

        /// <summary>
        /// ������ĿS5��Ϣ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionS5Async(QuestionS5Model data)
            => new(await _service.SaveQuestionS5Async(data));

        /// <summary>
        /// ������ĿT1��Ϣ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionT1Async(QuestionT1Model data)
        {
            var result = await _service.SaveQuestionT1Async(data);
            return new(result);
        }

        /// <summary>
        /// ������ĿT2��Ϣ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionT2Async(QuestionT2Model data)
            => new(await _service.SaveQuestionT2Async(data));

        /// <summary>
        /// ������ĿT3��Ϣ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveQuestionT3Async(QuestionT3Model data)
            => new(await _service.SaveQuestionT3Async(data));

        /// <summary>
        /// ������Ŀ¼���ļ�
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        [RequestSizeLimit(104857600)] // 100MB
        public ResponseModel SaveQuestionAudio(string questionId)
        {
            if (Request.Form.Files.Count == 0)
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "û���ҵ�Ҫ�ϴ��������ļ�");
            }
            _service.SaveQuestionAudio(questionId, Request.Form.Files);
            return new ResponseModel();
        }

        /// <summary>
        /// ɾ����Ŀ
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public async Task<ResponseModel> RemoveQuestionAsync(string questionId)
        {
            await _service.RemoveQuestionAsync(questionId);
            return new ResponseModel();
        }

        #endregion

        #region Answer

        /// <summary>
        /// ��ȡ��Ŀ��
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        [HttpPost("{answerId}")]
        public async Task<ResponseModel<AnswerModel>> GetAnswerListAsync(string answerId)
            => new(await _service.GetAnswerListAsync(answerId));

        /// <summary>
        /// ������ĿS1��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS1Async(AnswerS1Model data, string userId = "")
            => new(await _service.SaveAnswerS1Async(data, userId));

        /// <summary>
        /// ������ĿS2��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS2Async(AnswerS2Model data, string userId = "")
            => new(await _service.SaveAnswerS2Async(data, userId));

        /// <summary>
        /// ������ĿS3��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS3Async(AnswerS3Model data, string userId = "")
            => new(await _service.SaveAnswerS3Async(data, userId));

        /// <summary>
        /// ������ĿS4��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS4Async(AnswerS4Model data, string userId = "")
            => new(await _service.SaveAnswerS4Async(data, userId));

        /// <summary>
        /// ������ĿS5��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerS5Async(AnswerS5Model data, string userId = "")
            => new(await _service.SaveAnswerS5Async(data, userId));

        /// <summary>
        /// ������ĿT1��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerT1Async(AnswerT1Model data, string userId = "")
            => new(await _service.SaveAnswerT1Async(data, userId));

        /// <summary>
        /// ������ĿT2��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerT2Async(AnswerT2Model data, string userId = "")
            => new(await _service.SaveAnswerT2Async(data, userId));

        /// <summary>
        /// ������ĿT3��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<string>> SaveAnswerT3Async(AnswerT3Model data, string userId = "")
            => new(await _service.SaveAnswerT3Async(data, userId));
        #endregion

        #region TestResult�����������

        /// <summary>
        /// ���������ѯ
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<DataList<TestResultQueryRow>> TestResultGridQuery(TableQueryModel<TestResultQueryFields> query)
            => new(_service.TestResultGridQuery(query));

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveTestResultAsync(AnswerForm data)
            => new(await _service.SaveTestResultAsync(data));

        /// <summary>
        /// ������������ѯ
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<DataList<TestResultImportQueryRow>> TestResultImportGridQuery(TableQueryModel<TestResultImportQueryFields> query)
            => new(_service.TestResultImportGridQuery(query));

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<DataImportResult> TestResultImport()
        {
            if (Request.Form.Files.Count == 0)
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "û���ҵ�Ҫ�ϴ��������ļ�");
            }
            return new ResponseModel<DataImportResult>(_service.TestResultImport(Request.Form.Files[0].OpenReadStream()));
        }

        /// <summary>
        /// ɾ���������
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        [HttpPost("{answerId}")]
        public ResponseModel RemoveTestResult(string answerId)
        {
            _service.RemoveTestResult(answerId);
            return new ResponseModel();
        }
        #endregion
    }
}
