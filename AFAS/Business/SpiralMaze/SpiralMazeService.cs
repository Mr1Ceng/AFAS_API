using AFAS.Authorization;
using AFAS.Authorization.AuthInfos;
using AFAS.Entity;
using AFAS.Models.SpiralMaze;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using System.Reflection;


namespace AFAS.Business.SpiralMaze;

/// <summary>
/// 漩涡迷宫配置-服务
/// </summary>
public class SpiralMazeService : UserTokenAuthorization, ISpiralMazeService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name=""></param>
    public SpiralMazeService(IAuthInfo authInfo) : base(authInfo)
    {
    }

    #region SpiralMaze

    /// <summary>
    /// 获取漩涡迷宫配置列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<SpiralMazeForm>> GetSpiralMazeListAsync()
    {
        var result = new List<SpiralMazeForm>();
        using (var context = new AfasContext())
        {
            var list = await context.BSpiralMazes.ToListAsync();
            result = list.Select(data => new SpiralMazeForm()
            {
                Age = data.Age,
                Spacing = data.Spacing,
                Perturbation = data.Perturbation,
                RingNumber = data.RingNumber,
            }).ToList();
        }
        return new(result);
    }

    /// <summary>
    /// 获取漩涡迷宫配置
    /// </summary>
    /// <returns></returns>
    public async Task<BSpiralMaze> GetSpiralMazeAsync(int age)
    {
        var spiralMaze = new BSpiralMaze();
        using (var context = new AfasContext())
        {
            spiralMaze = await context.BSpiralMazes.FirstOrDefaultAsync(b => b.Age == age);
        }
        return spiralMaze ?? new BSpiralMaze();
    }

    /// <summary>
    /// 保存漩涡迷宫配置
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<int> SaveSpiralMazeAsync(SpiralMazeForm data)
    {
        var spiralMaze = new BSpiralMaze();
        using (var context = new AfasContext())
        {
            spiralMaze = await context.BSpiralMazes.FirstOrDefaultAsync(b => b.Age == data.Age);
            if (spiralMaze == null)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "漩涡迷宫配置不存在！");
            }
            else
            {
                spiralMaze.Spacing = data.Spacing;
                spiralMaze.Perturbation = data.Perturbation;
                spiralMaze.RingNumber = data.RingNumber;
                context.BSpiralMazes.Update(spiralMaze);
            }

            await context.SaveChangesAsync();
        }
        return spiralMaze.Age;
    }

    /// <summary>
    /// 删除漩涡迷宫配置
    /// </summary>
    /// <param name="age"></param>
    /// <returns></returns>
    public async Task RemoveSpiralMazeAsync(int age)
    {
        var spiralMaze = new BSpiralMaze();
        using (var context = new AfasContext())
        {
            spiralMaze = context.BSpiralMazes.FirstOrDefault(b => b.Age == age);
            if (spiralMaze == null)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "漩涡迷宫配置不存在！");
            }
            else
            {
                context.BSpiralMazes.Remove(spiralMaze);
            }

            await context.SaveChangesAsync();
        }
    }

    #endregion

}
