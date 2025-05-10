using AFAS.Entity;
using AFAS.Models.User;
using Mr1Ceng.Util;

namespace AFAS.Business.User;

/// <summary>
/// 用户-服务
/// </summary>
public interface IUserService
{
    #region User
    /// <summary>
    /// 获取用户
    /// </summary>
    /// <returns></returns>
    Task<BUser> GetUserAsync(string userId);

    /// <summary>
    /// 保存用户
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>

    Task<string> SaveUserAsync(UserForm data);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>

    Task RemoveUserAsync(string userId);
    #endregion

    #region 查询

    /// <summary>
    /// 用户查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    DataList<UserQueryRow> UserGridQuery(TableQueryModel<UserQueryFields> query);
    #endregion
}
