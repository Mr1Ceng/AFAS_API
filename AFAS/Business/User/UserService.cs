using AFAS.Authorization;
using AFAS.Authorization.AuthInfos;
using AFAS.Entity;
using AFAS.Models.User;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using System.Reflection;


namespace AFAS.Business.User;

/// <summary>
/// 用户-服务
/// </summary>
public class UserService : UserTokenAuthorization, IUserService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name=""></param>
    public UserService(IAuthInfo authInfo) : base(authInfo)
    {
    }

    #region User

    /// <summary>
    /// 获取用户
    /// </summary>
    /// <returns></returns>
    public async Task<BUser> GetUserAsync(string userId)
    {
        var user = new BUser();
        using (var context = new AfasContext())
        {
            user = await context.BUsers.FirstOrDefaultAsync(b => b.UserId == userId);
        }
        return user ?? new BUser();
    }

    /// <summary>
    /// 保存用户
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<string> SaveUserAsync(UserForm data)
    {
        var user = new BUser();
        using (var context = new AfasContext())
        {
            user = await context.BUsers.FirstOrDefaultAsync(b => b.UserId == data.UserId);
            if (user == null)
            {
                user = new BUser()
                {
                    UserId = NewCode.KeyId,
                    UserName = data.UserName,
                    NickName = data.NickName,
                    AvatarUrl = data.AvatarUrl,
                    Gender = data.Gender,
                    Age = data.Age,
                    Password = PasswordHelper.Encrypt("gps"),
                    Account = PinYinHelper.GetPinYin(data.UserName),
                    Mobile = data.Mobile,
                    Role = data.Role,
                };
                context.BUsers.Add(user);
            }
            else
            {
                user.UserName = data.UserName;
                user.NickName = data.NickName;
                user.AvatarUrl = data.AvatarUrl;
                user.Gender = data.Gender;
                user.Age = data.Age;
                user.Mobile = data.Mobile;
                user.Role = data.Role;
                context.BUsers.Update(user);
            }

            await context.SaveChangesAsync();
        }
        return user.UserId;
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task RemoveUserAsync(string userId)
    {
        var user = new BUser();
        using (var context = new AfasContext())
        {
            user = context.BUsers.FirstOrDefault(b => b.UserId == userId);
            if (user == null)
            {
                throw BusinessException.Get(MethodBase.GetCurrentMethod(), "用户不存在！");
            }
            else
            {
                context.BUsers.Remove(user);
            }

            await context.SaveChangesAsync();
        }
    }

    #endregion

    #region 查询

    /// <summary>
    /// 用户查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public DataList<UserQueryRow> UserGridQuery(TableQueryModel<UserQueryFields> query)
    {
        var paras = new List<Parameter>();
        var strsql = $@"
            SELECT
                UserId,
                UserName,
                NickName,
                AvatarUrl,
                Gender,
                Age,
                Password,
                Account,
                Mobile,
                Role,
                IsDeveloper
            FROM b_User User
            WHERE 1=1
        ";
        if (query.Data != null)
        {
            #region 构建查询过滤条件

            var role = GetString.FromObject(query.Data.Role);
            if (role != "")
            {
                strsql += " AND Role = @Role ";
                paras.Add(new Parameter("Role", role));
            }

            //综合查询
            var queryText = GetString.FromObject(query.Data?.QueryText, 50);
            if (queryText != "")
            {
                strsql = GetString.SplitList(query.Data?.QueryText)
                    .Aggregate(strsql, (current, text)
                        => current + $@" AND (UserName LIKE '%{text}%'
                    OR NickName LIKE '%{text}%'
                    OR UserId LIKE '%{text}%'
                    OR Account LIKE '%{text}%'
                    OR Mobile LIKE '%{text}%'
                )");
            }

            #endregion
        }
        var sortors = new List<KeySorterValue>();
        if (query.Sorter == null)
        {
            sortors.Add(new KeySorterValue());
        }
        else
        {
            sortors.Add(query.Sorter);
        }
        var resultData = new DataList<UserQueryRow>();
        using (var context = new AfasContext())
        {
            if (query.Size == 0)
            {
                resultData = new EFCoreExtentions(context).ExecuteSortedQuery<UserQueryRow>(strsql, sortors, paras);
            }
            else
            {
                resultData = new EFCoreExtentions(context).ExecutePagedQuery<UserQueryRow>(strsql, sortors, query.Index, query.Size, paras);
            }
        }

        return resultData;
    }

    #endregion
}
