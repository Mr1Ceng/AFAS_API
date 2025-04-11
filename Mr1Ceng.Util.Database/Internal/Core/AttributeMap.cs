using Mr1Ceng.Util.Database.Internal.Enums;

namespace Mr1Ceng.Util.Database.Internal.Core;

/// <summary>
/// AttributeMap 实例信息
/// </summary>
internal sealed class AttributeMap
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="column"></param>
    internal AttributeMap(string name, ColumnMap column)
    {
        Name = name;
        Column = column;
    }

    #endregion


    #region 属性

    /// <summary>
    /// 字段名称
    /// </summary>
    internal string Name { get; }

    /// <summary>
    /// 字段对应的数据列
    /// </summary>
    internal ColumnMap Column { get; set; }

    /// <summary>
    /// 属性字段类型
    /// </summary>
    internal SqlValueType SqlValueStringType { get; set; } = SqlValueType.NotSupport;

    #endregion
}
