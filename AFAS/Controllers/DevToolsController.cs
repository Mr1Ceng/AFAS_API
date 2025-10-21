using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;
using AFAS.WebApi.Platform;

namespace AFAS.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = WebApiConfig.DevTools)]
    [Route("[controller]/[action]")]
    public class DevToolsController : ControllerBase
    {
        private readonly ILogger<DevToolsController> _logger;

        public DevToolsController(ILogger<DevToolsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// �����ֻ���
        /// </summary>
        /// <returns></returns>
        [HttpPost("{mobile}")]
        public ResponseModel<string> GetMobileEncrypt(string mobile)
        {
            return new(GetMobile.Encrypt(mobile));
        }

        /// <summary>
        /// �����ֻ���
        /// </summary>
        /// <returns></returns>
        [HttpPost("{data}")]
        public ResponseModel<string> GetMobileDecrypt(string data)
        {
            return new(GetMobile.Decrypt(data));
        }
    }
}
