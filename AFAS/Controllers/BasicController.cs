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
        /// ��ȡ������׼�б�
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
        /// ��ȡ�ֵ��б�
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
        /// ��ȡ�ֵ���Ϣ
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
        /// ��ȡ�û��б�by��ɫ
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
        /// ��ȡ�û��б�
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
        /// ��ȡTerminalAuthorization
        /// </summary>
        /// <param name="terminalId"></param>
        /// <returns></returns>
        [HttpPost("{terminalId}")]
        public ResponseModel<KeyValue<TerminalData>> GetTerminalAuthorization(string terminalId)
        {
            //У��TerminalId
            var terminal = TerminalHelper.GetTerminalInfo(terminalId);
            if (terminal.TerminalKey == "")
            {
                throw new MessageException("δ֪��TerminalId");
            }

            try
            {
                //����Authorization
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
                throw new MessageException("������TerminalAuthorizationʧ�ܡ�" + ex.Message);
            }
        }
    }
}
