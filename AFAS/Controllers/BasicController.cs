using AFAS.Authorization.Models;
using AFAS.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using System.Data;

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

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<List<BDictionary>>> GetDictionaryListAsync()
        {
            var result = new List<BDictionary>();
            using (var context = new AfasContext())
            {
                result = await context.BDictionaries.ToListAsync();
            }
            return new(result);
        }


        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("{dictionaryId}")]
        public async Task<ResponseModel<List<BDictionaryItem>>> GetDictionaryItemListAsync(string dictionaryId)
        {
            var result = new List<BDictionaryItem>();
            using (var context = new AfasContext())
            {
                result = await context.BDictionaryItems.Where(x=>x.DictionaryId == dictionaryId).ToListAsync();
            }
            return new(result);
        }

        /// <summary>
        /// 获取用户列表by角色
        /// </summary>
        /// <returns></returns>
        [HttpPost("{role}")]
        public async Task<ResponseModel<List<BUser>>> GetUserListByRoleAsync(string role)
        {
            var result = new List<BUser>();
            using (var context = new AfasContext())
            {
                result = await context.BUsers.Where(x => x.Role == role).ToListAsync();
            }
            return new(result);
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<List<BUser>>> GetUserListAsync()
        {
            var result = new List<BUser>();
            using (var context = new AfasContext())
            {
                result = await context.BUsers.ToListAsync();
            }
            return new(result);
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
                throw new MessageException("未知的TerminalId");
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
                throw new MessageException("【生成TerminalAuthorization失败】" + ex.Message);
            }
        }
    }
}
