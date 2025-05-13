using AFAS.Authorization;
using AFAS.Authorization.AuthInfos;
using AFAS.Entity;
using AFAS.Models.EvaluationStandard;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using System.Reflection;


namespace AFAS.Business.EvaluationStandard;

/// <summary>
/// 测评标准配置-服务
/// </summary>
public class EvaluationStandardService : UserTokenAuthorization, IEvaluationStandardService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name=""></param>
    public EvaluationStandardService(IAuthInfo authInfo) : base(authInfo)
    {
    }

    #region EvaluationStandard

    /// <summary>
    /// 获取测评标准配置列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<EvaluationStandardForm>> GetEvaluationStandardListAsync()
    {
        var result = new List<EvaluationStandardForm>();
        using (var context = new AfasContext())
        {
            var list = await context.BEvaluationStandards.ToListAsync();
            result = list.Select(data => new EvaluationStandardForm()
            {
                LevelCode = data.LevelCode,
                LevelName = data.LevelName,
                S1 = data.S1,
                S2 = data.S2,
                S3 = data.S3,
                S4 = data.S4,
                S5 = data.S5,
                T1 = data.T1,
                T2 = data.T2,
                T3 = data.T3,
            }).ToList();
        }
        return new(result);
    }

    /// <summary>
    /// 获取测评标准配置
    /// </summary>
    /// <returns></returns>
    public async Task<BEvaluationStandard> GetEvaluationStandardAsync(string levelCode)
    {
        var spiralMaze = new BEvaluationStandard();
        using (var context = new AfasContext())
        {
            spiralMaze = await context.BEvaluationStandards.FirstOrDefaultAsync(b => b.LevelCode == levelCode);
        }
        return spiralMaze ?? new BEvaluationStandard();
    }

    /// <summary>
    /// 保存测评标准配置
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<string> SaveEvaluationStandardAsync(EvaluationStandardForm data)
    {
        var spiralMaze = new BEvaluationStandard();
        using (var context = new AfasContext())
        {
            spiralMaze = await context.BEvaluationStandards.FirstOrDefaultAsync(b => b.LevelCode == data.LevelCode);
            if (spiralMaze == null)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "测评标准配置不存在！");
            }
            else
            {
                spiralMaze.LevelName = data.LevelName;
                spiralMaze.S1 = data.S1;
                spiralMaze.S2 = data.S2;
                spiralMaze.S3 = data.S3;
                spiralMaze.S4 = data.S4;
                spiralMaze.S5 = data.S5;
                spiralMaze.T1 = data.T1;
                spiralMaze.T2 = data.T2;
                spiralMaze.T3 = data.T3;
                context.BEvaluationStandards.Update(spiralMaze);
            }

            await context.SaveChangesAsync();
        }
        return spiralMaze.LevelCode;
    }

    /// <summary>
    /// 删除测评标准配置
    /// </summary>
    /// <param name="levelCode"></param>
    /// <returns></returns>
    public async Task RemoveEvaluationStandardAsync(string levelCode)
    {
        var spiralMaze = new BEvaluationStandard();
        using (var context = new AfasContext())
        {
            spiralMaze = context.BEvaluationStandards.FirstOrDefault(b => b.LevelCode == levelCode);
            if (spiralMaze == null)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "测评标准配置不存在！");
            }
            else
            {
                context.BEvaluationStandards.Remove(spiralMaze);
            }

            await context.SaveChangesAsync();
        }
    }

    #endregion

}
