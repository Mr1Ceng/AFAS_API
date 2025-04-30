using AFAS.Entity;
using AFAS.Infrastructure.Models;
using Mr1Ceng.Util;
using System.Data;

namespace AFAS.Infrastructure;

/// <summary>
/// 系统字典工具类
/// </summary>
public class DictionaryHelper
{
    /// <summary>
    /// 获取有效的系统字典选项
    /// </summary>
    /// <param name="dictionaryId"></param>
    /// <returns></returns>
    public static IList<KeyTextSort> GetDictionaryOptionList(string dictionaryId)
    {
        var result = new List<BDictionaryItem>();
        using (var context = new AfasContext())
        {
            result = context.BDictionaryItems.Where(x => x.DictionaryId == dictionaryId && x.Status == DataStatus.ACTIVE.ToString()).OrderBy(x => x.Sort).ToList();
        }
        return result.Select(x => new KeyTextSort
        {
            Key = x.ItemId,
            Text = x.ItemName,
            Sort = x.Sort
        }).ToList();
    }

    /// <summary>
    /// 获取有效的系统字典选项
    /// </summary>
    /// <param name="dictionaryId"></param>
    /// <param name="parentId"></param>
    /// <param name="includeBlank">是否包含ParentId为空的选项</param>
    /// <returns></returns>
    public static IList<KeyTextSort> GetDictionaryOptionList(string dictionaryId,
        string parentId,
        bool includeBlank = false)
    {
        var result = new List<BDictionaryItem>();
        using (var context = new AfasContext())
        {
            result = context.BDictionaryItems.Where(x => x.DictionaryId == dictionaryId && x.Status == DataStatus.ACTIVE.ToString()
                        && (includeBlank ? x.ParentItemId == parentId : (x.ParentItemId == parentId || x.ParentItemId == ""))).OrderBy(x => x.Sort).ToList();
        }
        return result.Select(x => new KeyTextSort
        {
            Key = x.ItemId,
            Text = x.ItemName,
            Sort = x.Sort
        }).ToList();
    }

    /// <summary>
    /// 获取系统字典选项
    /// </summary>
    /// <param name="dictionaryId"></param>
    /// <param name="onlyActive"></param>
    /// <returns></returns>
    public static IList<DictionaryItem> GetDictionaryList(string dictionaryId, bool onlyActive = true)
    {
        var result = new List<BDictionaryItem>();
        using (var context = new AfasContext())
        {
            result = context.BDictionaryItems.Where(x => x.DictionaryId == dictionaryId
                        && (onlyActive ? x.Status == DataStatus.ACTIVE.ToString() : true)).OrderBy(x => x.Sort).ToList();
        }
        return result.Select(x => new DictionaryItem
        {
            DictionaryId = x.DictionaryId,
            ItemId = x.ItemId,
            ItemName = x.ItemName,
            ParentItemId = x.ParentItemId,
            Field1 = x.Field1,
            Field2 = x.Field2,
            Field3 = x.Field3,
            Introduce = x.Introduce,
            Sort = x.Sort,
            Status = x.Status
        }).ToList();
    }

    /// <summary>
    /// 获取系统字典选项
    /// </summary>
    /// <param name="dictionaryId"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public static DictionaryItem GetDictionaryItem(string dictionaryId, string itemId)
    {
        dictionaryId = GetString.FromObject(dictionaryId);
        itemId = GetString.FromObject(itemId);
        if (itemId == "")
        {
            return new DictionaryItem();
        }
        var item = new BDictionaryItem();
        using (var context = new AfasContext())
        {
            item = context.BDictionaryItems.Where(x => x.DictionaryId == dictionaryId
                        && x.ItemId == itemId).FirstOrDefault()??new BDictionaryItem();
        }
        return new DictionaryItem
        {
            DictionaryId = item.DictionaryId,
            ItemId = item.ItemId,
            ItemName = item.ItemName,
            ParentItemId = item.ParentItemId,
            Field1 = item.Field1,
            Field2 = item.Field2,
            Field3 = item.Field3,
            Introduce = item.Introduce,
            Sort = item.Sort,
            Status = item.Status
        };
    }

    /// <summary>
    /// 获取系统字典选项名称
    /// </summary>
    /// <param name="dictionaryId"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public static string GetDictionaryItemName(string dictionaryId, string itemId)
    {
        dictionaryId = GetString.FromObject(dictionaryId);
        itemId = GetString.FromObject(itemId);
        if (itemId == "")
        {
            return "";
        }
        var item = new BDictionaryItem();
        using (var context = new AfasContext())
        {
            item = context.BDictionaryItems.Where(x => x.DictionaryId == dictionaryId
                        && x.ItemId == itemId).FirstOrDefault() ?? new BDictionaryItem();
        }
        return item.ItemName;
    }
}
