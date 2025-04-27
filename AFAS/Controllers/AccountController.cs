using AFAS.Authorization.Attributes;
using AFAS.Business.Account;
using AFAS.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;

namespace AFAS.Controllers
{
    [Terminal]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserLoginService service;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserLoginService userLoginService,ILogger<AccountController> logger)
        {
            _logger = logger;
            service = userLoginService;
        }

        /// <summary>
        /// ÍøÒ³ÃÜÂëµÇÂ¼
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<WebAppIdentityModel>> WebAppLoginByPassword(WebAppPasswordLoginPostData data)
        => new(await service.WebAppLoginByPasswordAsync(data.Account, data.Password));
    }
}
