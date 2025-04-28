using AFAS.Authorization.Models;
using AFAS.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;
using System.Reflection;

namespace AFAS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(ILogger<AuthorizationController> logger)
        {
            _logger = logger;
        }



        /// <summary>
        /// 获取TerminalAuthorization
        /// </summary>
        /// <param name="terminalId"></param>
        /// <returns></returns>

        [HttpPost("{terminalId}")]
        public ResponseModel<KeyValue<TerminalData>> GetTerminalAuthorization(string terminalId)
        {
            //校对TerminalId
            var terminal = TerminalHelper.GetTerminalInfo(terminalId);
            if (terminal.TerminalKey == "")
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "未知的TerminalId");
            }

            try
            {
                //生成Authorization
                var authorization = WebApiAuthorization.GetString(terminal.TerminalKey, terminal.TerminalSecret);
                return new ResponseModel<KeyValue<TerminalData>>(new KeyValue<TerminalData>
                {
                    Key = authorization,
                    Value = new TerminalData
                    {
                        TerminalId = terminal.TerminalId,
                        TerminalName = terminal.TerminalName,
                        TerminalType = terminal.TerminalType,
                        SystemId = terminal.SystemId
                    }
                });
            }
            catch (Exception ex)
            {
                throw MessageException.Get(MethodBase.GetCurrentMethod(), "【生成TerminalAuthorization失败】" + ex.Message);
            }
        }
    }
}
