using AFAS.Authorization.Attributes;
using AFAS.Business.Account;
using AFAS.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;
using WingWell.WebApi.Platform;

namespace AFAS.Controllers
{
    [Terminal]
    [ApiController]
    [ApiExplorerSettings(GroupName = WebApiConfig.Account)]
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
