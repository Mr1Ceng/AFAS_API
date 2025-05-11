using AFAS.Authorization.Attributes;
using AFAS.Business.Account;
using AFAS.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;
using WingWell.WebApi.Platform;

namespace AFAS.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = WebApiConfig.Account)]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserLoginService loginService;
        private readonly IUserLogoutService logoutService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserLoginService userLoginService, IUserLogoutService userLogoutService, ILogger<AccountController> logger)
        {
            _logger = logger;
            loginService = userLoginService;
            logoutService = userLogoutService;
        }

        /// <summary>
        /// Õ¯“≥√‹¬Îµ«¬º
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Terminal]
        public async Task<ResponseModel<WebAppIdentityModel>> WebAppLoginByPassword(WebAppPasswordLoginPostData data)
        => new(await loginService.WebAppLoginByPasswordAsync(data.Account, data.Password));


        /// <summary>
        /// Õ¯“≥√‹¬Îµ«¬º
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [UserToken]
        public async Task<ResponseModel> WebAppLogout()
        {
            await logoutService.WebAppLogoutAsync();
            return new(); 
        }


        /// <summary>
        /// ÷ÿ÷√√‹¬Î
        /// </summary>
        /// <returns></returns>
        [HttpPost("{password}")]
        [UserToken]
        public async Task<ResponseModel> WebAppResetPassword(string password)
        {
            await logoutService.WebAppResetPasswordAsync(password,"");
            return new();
        }

        /// <summary>
        /// ÷ÿ÷√√‹¬Î
        /// </summary>
        /// <returns></returns>
        [HttpPost("{userId}/{password}")]
        [UserToken]
        public async Task<ResponseModel> WebAppResetPassword(string password,string userId)
        {
            await logoutService.WebAppResetPasswordAsync(password,userId);
            return new();
        }
    }
}
