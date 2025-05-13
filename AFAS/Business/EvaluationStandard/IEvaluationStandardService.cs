using AFAS.Entity;
using AFAS.Models.EvaluationStandard;

namespace AFAS.Business.EvaluationStandard;

/// <summary>
/// 测评标准配置-服务
/// </summary>
public interface IEvaluationStandardService
{
    #region EvaluationStandard

    /// <summary>
    /// 获取测评标准配置列表
    /// </summary>
    /// <returns></returns>
    Task<List<EvaluationStandardForm>> GetEvaluationStandardListAsync();

    /// <summary>
    /// 获取测评标准配置
    /// </summary>
    /// <returns></returns>
    Task<BEvaluationStandard> GetEvaluationStandardAsync(string levelCode);

    /// <summary>
    /// 保存测评标准配置
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<string> SaveEvaluationStandardAsync(EvaluationStandardForm data);

    /// <summary>
    /// 删除测评标准配置
    /// </summary>
    /// <param name="age"></param>
    /// <returns></returns>
    Task RemoveEvaluationStandardAsync(string levelCode);

    #endregion
}
