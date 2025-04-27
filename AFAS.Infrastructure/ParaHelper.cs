using AFAS.Entity;
using AFAS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Mr1Ceng.Util;
using System.Data;

namespace AFAS.Infrastructure;

/// <summary>
/// 系统参数工具类
/// </summary>
public class ParaHelper
{
    /// <summary>
    /// 保存参数
    /// </summary>
    /// <param name="data"></param>
    /// <param name="modifyUserId"></param>
    public static async Task SaveParaItemAsync(ParaItem data, string modifyUserId)
    {
        data.ParaId = GetString.FromObject(data.ParaId, 50);
        data.ParaValue = GetString.FromObject(data.ParaValue, 200);

        var para = new SPara();
        using (var context = new AfasContext())
        {
            para = await context.SParas.Where(x => x.ParaId == data.ParaId).FirstOrDefaultAsync();
            if (para != null && data.ParaValue == "")
            {
                context.SParas.Remove(para);
            }
            else if(para != null && data.ParaValue != "")
            {
                para.ParaType = GetString.FromObject(data.ParaType, 50);
                para.ParaName = GetString.FromObject(data.ParaName, 50);
                para.ParaValue = GetString.FromObject(data.ParaValue, 200);
                para.Remark = GetString.FromObject(data.Remark, 200);
                context.SParas.Update(para);
            }
            else
            {
                para = new SPara() { ParaId = data.ParaId };
                para.ParaType = GetString.FromObject(data.ParaType, 50);
                para.ParaName = GetString.FromObject(data.ParaName, 50);
                para.ParaValue = GetString.FromObject(data.ParaValue, 200);
                para.Remark = GetString.FromObject(data.Remark, 200);
                context.SParas.Add(para);
            }

            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 获取参数值
    /// </summary>
    /// <param name="prefix">匹配前缀</param>
    /// <returns></returns>
    public static async Task<IList<ParaItem>> GetParaList(string prefix)
    {
        var paras = new List<SPara>();
        using (var context = new AfasContext())
        {
            paras = await context.SParas.Where(x => x.ParaId.StartsWith(GetString.FromObject(prefix, 10))).ToListAsync();
        }
        return paras.Select(x => new ParaItem
        {
            ParaType = x.ParaType,
            ParaId = x.ParaId,
            ParaName = x.ParaName,
            ParaValue = x.ParaValue,
            Remark = x.Remark
        }).ToList();
    }

    /// <summary>
    /// 获取参数值
    /// </summary>
    /// <param name="paraId"></param>
    /// <returns></returns>
    public static string GetValue(string paraId) 
    {
        var para = new SPara();
        using (var context = new AfasContext())
        {
            para = context.SParas.Where(x => x.ParaId == GetString.FromObject(paraId, 50)).FirstOrDefault();
        }
        return GetString.FromObject(para?.ParaValue);
    }

    /// <summary>
    /// 获取参数值
    /// </summary>
    /// <param name="paraId"></param>
    /// <returns></returns>
    public static int GetIntValue(string paraId)
    {
        var para = new SPara();
        using (var context = new AfasContext())
        {
            para = context.SParas.Where(x => x.ParaId == GetString.FromObject(paraId, 50)).FirstOrDefault();
        }
        return GetInt.FromObject(para?.ParaValue);
    }

    /// <summary>
    /// 登录有效时长（单位秒，默认值2小时）
    /// </summary>
    /// <returns></returns>
    public static int GetLoginExpires()
    {
        var seconds = 7200;
        var para = new SPara();
        using (var context = new AfasContext())
        {
            para = context.SParas.Where(x => x.ParaId == "LOGIN_EXPIRES").FirstOrDefault();
        }
        return GetInt.FromObject(para?.ParaValue, seconds);
    }

    /// <summary>
    /// 超级口令
    /// </summary>
    /// <returns></returns>
    public static string GetSuperToken() => GetValue("SUPER_TOKEN");
}
