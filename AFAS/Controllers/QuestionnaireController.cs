using AFAS.Business.Questionnaire;
using AFAS.Entitys;
using AFAS.Internals;
using AFAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using System.Reflection;

namespace AFAS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestControllerController : ControllerBase
    {
        private readonly ILogger<TestControllerController> _logger;
        private readonly IQuestionnaireService _questionnaireService;

        public TestControllerController(IQuestionnaireService service, ILogger<TestControllerController> logger)
        {
            _logger = logger;
            _questionnaireService = service;
        }

        /// <summary>
        /// ��ȡ�Ծ��б�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<List<BQuestionnaire>>> GetQuestionnaireListAsync()
        => new (await _questionnaireService.GetQuestionnaireListAsync());

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
        public async Task<ResponseModel<string>> SaveQuestionnaireAsync(BQuestionnaireForm data)
        => new(await _questionnaireService.SaveQuestionnaireAsync(data));

        /// <summary>
        /// ɾ���Ծ�
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> RemoveQuestionnaireAsync(string questionnaireId)
        {
            _ = await _questionnaireService.RemoveQuestionnaireAsync(questionnaireId);
            return new ResponseModel();
        }
    }
}
