namespace Mr1Ceng.Util.Database.Internal.Models;

/// <summary>
/// 排序
/// </summary>
internal class OrderBy
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="attrName"></param>
    /// <param name="isAsc"></param>
    public OrderBy(string attrName, FieldOrderBy isAsc)
    {
        AttributeName = attrName;
        IsAscend = isAsc;
    }

    #endregion


    #region 属性

    /// <summary>
    /// </summary>
    internal string AttributeName { get; set; }

    /// <summary>
    /// </summary>
    internal FieldOrderBy IsAscend { get; set; }

    #endregion
}
