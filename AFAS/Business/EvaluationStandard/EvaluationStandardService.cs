using AFAS.Authorization;
using AFAS.Authorization.AuthInfos;
using AFAS.Entity;
using AFAS.Internals;
using AFAS.Models.EvaluationStandard;
using AFAS.Service;
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
       => await EvaluationStandardHelper.GetEvaluationStandardListAsync();

    /// <summary>
    /// 获取测评标准配置
    /// </summary>
    /// <returns></returns>
    public async Task<BEvaluationStandard> GetEvaluationStandardAsync(string levelCode)
    {
        var evaluationStandard = new BEvaluationStandard();
        using (var context = new AfasContext())
        {
            evaluationStandard = await context.BEvaluationStandards.FirstOrDefaultAsync(b => b.LevelCode == levelCode);
            if (evaluationStandard == null)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "测评标准配置不存在！");
            }
        }
        return evaluationStandard;
    }

    /// <summary>
    /// 保存测评标准配置
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<string> SaveEvaluationStandardAsync(EvaluationStandardForm data)
    {
        var evaluationStandard = new BEvaluationStandard();
        using (var context = new AfasContext())
        {
            evaluationStandard = await context.BEvaluationStandards.FirstOrDefaultAsync(b => b.LevelCode == data.LevelCode);
            if (evaluationStandard == null)
            {
                evaluationStandard = new BEvaluationStandard()
                {
                    LevelCode = NewKey.NewLevelCode(),
                    LevelName = data.LevelName,
                    S1 = data.S1,
                    S2 = data.S2,
                    S3 = data.S3,
                    S4 = data.S4,
                    S5 = data.S5,
                    T1 = data.T1,
                    T2 = data.T2,
                    T3 = data.T3,
                };
                context.BEvaluationStandards.Add(evaluationStandard);
            }
            else
            {
                evaluationStandard.LevelName = data.LevelName;
                evaluationStandard.S1 = data.S1;
                evaluationStandard.S2 = data.S2;
                evaluationStandard.S3 = data.S3;
                evaluationStandard.S4 = data.S4;
                evaluationStandard.S5 = data.S5;
                evaluationStandard.T1 = data.T1;
                evaluationStandard.T2 = data.T2;
                evaluationStandard.T3 = data.T3;
                context.BEvaluationStandards.Update(evaluationStandard);
            }

            await context.SaveChangesAsync();
        }
        return evaluationStandard.LevelCode;
    }

    /// <summary>
    /// 删除测评标准配置
    /// </summary>
    /// <param name="levelCode"></param>
    /// <returns></returns>
    public async Task RemoveEvaluationStandardAsync(string levelCode)
    {
        var evaluationStandard = new BEvaluationStandard();
        using (var context = new AfasContext())
        {
            evaluationStandard = await context.BEvaluationStandards.FirstOrDefaultAsync(b => b.LevelCode == levelCode);
            if (evaluationStandard == null)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "测评标准配置不存在！");
            }
            else
            {
                context.BEvaluationStandards.Remove(evaluationStandard);
            }

            await context.SaveChangesAsync();
        }
    }

    #endregion

}
