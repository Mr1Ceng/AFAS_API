using AFAS.Entity;
using AFAS.Models.EvaluationStandard;
using Microsoft.EntityFrameworkCore;

namespace AFAS.Service;

/// <summary>
/// 测评标准工具类
/// </summary>
public class EvaluationStandardHelper
{
    #region 测评标准

    /// <summary>
    /// 获取测评标准列表
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task<List<EvaluationStandardForm>> GetEvaluationStandardListAsync()
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
        return result;
    }

    #endregion
}
