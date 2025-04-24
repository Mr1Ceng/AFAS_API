using AFAS.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;

namespace AFAS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BasicController : ControllerBase
    {
        private readonly ILogger<BasicController> _logger;

        public BasicController(ILogger<BasicController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取评估标准列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<List<BEvaluationStandard>>> GetEvaluationStandardListAsync(){
            var result = new List<BEvaluationStandard>();
            using (var context = new AfasContext())
            {
                result = await context.BEvaluationStandards.ToListAsync();
            }
            return new(result);
        }

    }
}
