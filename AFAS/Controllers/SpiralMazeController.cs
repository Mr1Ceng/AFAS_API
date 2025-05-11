using AFAS.Authorization.Attributes;
using AFAS.Business.SpiralMaze;
using AFAS.Entity;
using AFAS.Models.SpiralMaze;
using Microsoft.AspNetCore.Mvc;
using Mr1Ceng.Util;
using WingWell.WebApi.Platform;

namespace AFAS.Controllers
{
    [UserToken]
    [ApiController]
    [ApiExplorerSettings(GroupName = WebApiConfig.Setting)]
    [Route("[controller]/[action]")]
    public class SpiralMazeController : ControllerBase
    {
        private readonly ILogger<SpiralMazeController> _logger;
        private readonly ISpiralMazeService _service;

        public SpiralMazeController(ISpiralMazeService service, ILogger<SpiralMazeController> logger)
        {
            _logger = logger;
            _service = service;
        }


        #region SpiralMaze

        /// <summary>
        /// ªÒ»°‰ˆŒ–√‘π¨≈‰÷√¡–±Ì
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<List<SpiralMazeForm>>> GetSpiralMazeList()
            => new(await _service.GetSpiralMazeListAsync());

        /// <summary>
        /// ªÒ»°‰ˆŒ–√‘π¨≈‰÷√
        /// </summary>
        /// <returns></returns>
        [HttpPost("{age}")]
        public async Task<ResponseModel<BSpiralMaze>> GetSpiralMazeAsync(int age)
        => new(await _service.GetSpiralMazeAsync(age));

        /// <summary>
        /// ±£¥Ê‰ˆŒ–√‘π¨≈‰÷√
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<int>> SaveSpiralMazeAsync(SpiralMazeForm data)
        => new(await _service.SaveSpiralMazeAsync(data));

        /// <summary>
        /// …æ≥˝‰ˆŒ–√‘π¨≈‰÷√
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        [HttpPost("{age}")]
        public async Task<ResponseModel> RemoveSpiralMazeAsync(int age)
        {
            await _service.RemoveSpiralMazeAsync(age);
            return new ResponseModel();
        }

        #endregion
    }
}
