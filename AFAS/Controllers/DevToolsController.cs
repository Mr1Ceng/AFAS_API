using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;

namespace AFAS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DevToolsController : ControllerBase
    {
        private readonly ILogger<DevToolsController> _logger;

        public DevToolsController(ILogger<DevToolsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 加密手机号
        /// </summary>
        /// <returns></returns>
        [HttpPost("{mobile}")]
        public ResponseModel<string> GetMobileEncrypt(string mobile)
        {
            return new(GetMobile.Encrypt(mobile));
        }

        /// <summary>
        /// 解密手机号
        /// </summary>
        /// <returns></returns>
        [HttpPost("{data}")]
        public ResponseModel<string> GetMobileDecrypt(string data)
        {
            return new(GetMobile.Decrypt(data));
        }
    }
}
