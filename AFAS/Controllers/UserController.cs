using AFAS.Authorization.Attributes;
using AFAS.Business.User;
using AFAS.Entity;
using AFAS.Models.User;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;
using AFAS.WebApi.Platform;

namespace AFAS.Controllers
{
    [UserToken]
    [ApiController]
    [ApiExplorerSettings(GroupName = WebApiConfig.Setting)]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = service;
        }


        #region User

        /// <summary>
        /// ��ȡ�û�
        /// </summary>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel<BUser>> GetUserAsync(string userId)
        => new(await _userService.GetUserAsync(userId));

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<string>> SaveUserAsync(UserForm data)
        => new(await _userService.SaveUserAsync(data));

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<ResponseModel> RemoveUserAsync(string userId)
        {
            await _userService.RemoveUserAsync(userId);
            return new ResponseModel();
        }

        #endregion

        #region ��ѯ


        /// <summary>
        /// �û���ѯ
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<DataList<UserQueryRow>> UserGridQuery(TableQueryModel<UserQueryFields> query)
            => new(_userService.UserGridQuery(query));

        #endregion
    }
}
