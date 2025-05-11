using AFAS.Entity;
using AFAS.Models.SpiralMaze;

namespace AFAS.Business.SpiralMaze;

/// <summary>
/// 漩涡迷宫配置-服务
/// </summary>
public interface ISpiralMazeService
{
    #region SpiralMaze

    /// <summary>
    /// 获取漩涡迷宫配置列表
    /// </summary>
    /// <returns></returns>
    Task<List<SpiralMazeForm>> GetSpiralMazeListAsync();

    /// <summary>
    /// 获取漩涡迷宫配置
    /// </summary>
    /// <returns></returns>
    Task<BSpiralMaze> GetSpiralMazeAsync(int age);

    /// <summary>
    /// 保存漩涡迷宫配置
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<int> SaveSpiralMazeAsync(SpiralMazeForm data);

    /// <summary>
    /// 删除漩涡迷宫配置
    /// </summary>
    /// <param name="age"></param>
    /// <returns></returns>
    Task RemoveSpiralMazeAsync(int age);

    #endregion
}
