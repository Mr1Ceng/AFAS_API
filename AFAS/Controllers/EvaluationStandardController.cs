using AFAS.Authorization.Attributes;
using AFAS.Business.EvaluationStandard;
using AFAS.Entity;
using AFAS.Models.EvaluationStandard;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;
using WingWell.WebApi.Platform;

namespace AFAS.Controllers
{
    [UserToken]
    [ApiController]
    [ApiExplorerSettings(GroupName = WebApiConfig.Setting)]
    [Route("[controller]/[action]")]
    public class EvaluationStandardController : ControllerBase
    {
        private readonly ILogger<EvaluationStandardController> _logger;
        private readonly IEvaluationStandardService _service;

        public EvaluationStandardController(IEvaluationStandardService service, ILogger<EvaluationStandardController> logger)
        {
            _logger = logger;
            _service = service;
        }


        #region EvaluationStandard

        /// <summary>
        /// 获取测评标准配置列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<List<EvaluationStandardForm>>> GetEvaluationStandardList()
            => new(await _service.GetEvaluationStandardListAsync());

        /// <summary>
        /// 获取测评标准配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("{levelCode}")]
        public async Task<ResponseModel<BEvaluationStandard>> GetEvaluationStandardAsync(string levelCode)
        => new(await _service.GetEvaluationStandardAsync(levelCode));

        /// <summary>
        /// 保存测评标准配置
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveEvaluationStandardAsync(EvaluationStandardForm data)
        => new(await _service.SaveEvaluationStandardAsync(data));

        /// <summary>
        /// 删除测评标准配置
        /// </summary>
        /// <param name="levelCode"></param>
        /// <returns></returns>
        [HttpPost("{levelCode}")]
        public async Task<ResponseModel> RemoveEvaluationStandardAsync(string levelCode)
        {
            await _service.RemoveEvaluationStandardAsync(levelCode);
            return new ResponseModel();
        }

        #endregion
    }
}
