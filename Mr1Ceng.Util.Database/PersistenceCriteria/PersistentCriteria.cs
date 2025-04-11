using System.Reflection;
using Mr1Ceng.Util.Database.Internal.Core;
using Mr1Ceng.Util.Database.Internal.Enums;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// PersistentCriteria 类
/// </summary>
/// <remarks>
/// 这个类层次封装了根据根据指定条件进行获取，更新，删除操作所需的行为
/// </remarks>
public class PersistentCriteria
{
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="criteriaType"></param>
    /// <param name="classType"></param>
    internal PersistentCriteria(CriteriaTypes criteriaType, Type classType)
    {
        CriteriaType = criteriaType;
        ForClass = classType;
        ForClassName = PersistentObject.GetClassName(classType);
        ThisClassMap = broker.GetClassMap(ForClassName);
    }

    #endregion


    #region 变量、属性

    /// <summary>
    /// Broker对象
    /// </summary>
    internal readonly Broker broker = Broker.Instance;

    /// <summary>
    /// ClassMap信息
    /// </summary>
    internal ClassMap ThisClassMap { get; }

    /// <summary>
    /// Criteria操作类型
    /// </summary>
    internal CriteriaTypes CriteriaType { get; }

    /// <summary>
    /// Criteria对应的实体对象类型
    /// </summary>
    public Type ForClass { get; }

    /// <summary>
    /// Criteria对应的实体对象类型名称
    /// </summary>
    public string ForClassName { get; }

    /// <summary>
    /// 过滤条件数组
    /// </summary>
    internal List<string> Selections { get; set; } = [];

    /// <summary>
    /// 过滤条件值
    /// </summary>
    internal List<Parameter> Parameters { get; set; } = [];

    /// <summary>
    /// 返回的Sql语句
    /// </summary>
    public string SqlString { get; protected set; } = "";

    #endregion


    #region 方法

    #region NULL值过滤

    /// <summary>
    /// 为NULL过滤
    /// </summary>
    /// <param name="attributeName">属性</param>
    public void AddEqualNull(string attributeName)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IS NULL");
    }

    /// <summary>
    /// 值为不为NULL过滤
    /// </summary>
    /// <param name="attributeName">属性</param>
    public void AddNotEqualNull(string attributeName)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IS NOT NULL");
    }

    #endregion

    #region 比较条件过滤

    /// <summary>
    /// 清除过滤条件
    /// </summary>
    public void Clear()
    {
        Selections.Clear();
        Parameters.Clear();
    }

    #region AddEqualTo(等于条件过滤)

    /// <summary>
    /// 等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddEqualTo(string attributeName, string attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.Equal);

    /// <summary>
    /// 等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddEqualTo(string attributeName, bool attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.Equal);

    /// <summary>
    /// 等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddEqualTo(string attributeName, short attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.Equal);

    /// <summary>
    /// 等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddEqualTo(string attributeName, int attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.Equal);

    /// <summary>
    /// 等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddEqualTo(string attributeName, long attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.Equal);

    /// <summary>
    /// 等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddEqualTo(string attributeName, double attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.Equal);

    /// <summary>
    /// 等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddEqualTo(string attributeName, decimal attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.Equal);

    /// <summary>
    /// 等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddEqualTo(string attributeName, float attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.Equal);

    #endregion

    #region AddNotEqualTo(不等于条件过滤)

    /// <summary>
    /// 不等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddNotEqualTo(string attributeName, string attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.NotEqual);

    /// <summary>
    /// 不等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddNotEqualTo(string attributeName, bool attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.NotEqual);

    /// <summary>
    /// 不等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddNotEqualTo(string attributeName, short attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.NotEqual);

    /// <summary>
    /// 不等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddNotEqualTo(string attributeName, int attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.NotEqual);

    /// <summary>
    /// 不等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddNotEqualTo(string attributeName, long attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.NotEqual);

    /// <summary>
    /// 不等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddNotEqualTo(string attributeName, double attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.NotEqual);

    /// <summary>
    /// 不等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddNotEqualTo(string attributeName, decimal attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.NotEqual);

    /// <summary>
    /// 不等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddNotEqualTo(string attributeName, float attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.NotEqual);

    #endregion

    #region AddGreaterThan(大于条件过滤)

    /// <summary>
    /// 大于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThan(string attributeName, string attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThan);

    /// <summary>
    /// 大于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThan(string attributeName, short attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThan);

    /// <summary>
    /// 大于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThan(string attributeName, int attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThan);

    /// <summary>
    /// 大于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThan(string attributeName, long attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThan);

    /// <summary>
    /// 大于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThan(string attributeName, double attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThan);

    /// <summary>
    /// 大于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThan(string attributeName, decimal attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThan);

    /// <summary>
    /// 大于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThan(string attributeName, float attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThan);

    #endregion

    #region AddGreaterThanOrEqualTo(大于等于条件过滤)

    /// <summary>
    /// 大于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThanOrEqualTo(string attributeName, string attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThanOrEqual);

    /// <summary>
    /// 大于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThanOrEqualTo(string attributeName, short attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThanOrEqual);

    /// <summary>
    /// 大于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThanOrEqualTo(string attributeName, int attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThanOrEqual);

    /// <summary>
    /// 大于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThanOrEqualTo(string attributeName, long attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThanOrEqual);

    /// <summary>
    /// 大于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThanOrEqualTo(string attributeName, double attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThanOrEqual);

    /// <summary>
    /// 大于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThanOrEqualTo(string attributeName, decimal attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThanOrEqual);

    /// <summary>
    /// 大于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddGreaterThanOrEqualTo(string attributeName, float attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.GreaterThanOrEqual);

    #endregion

    #region AddLessThan(小于条件过滤)

    /// <summary>
    /// 小于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThan(string attributeName, string attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThan);

    /// <summary>
    /// 小于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThan(string attributeName, short attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThan);

    /// <summary>
    /// 小于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThan(string attributeName, int attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThan);

    /// <summary>
    /// 小于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThan(string attributeName, long attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThan);

    /// <summary>
    /// 小于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThan(string attributeName, double attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThan);

    /// <summary>
    /// 小于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThan(string attributeName, decimal attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThan);

    /// <summary>
    /// 小于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThan(string attributeName, float attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThan);

    #endregion

    #region AddLessThanOrEqualTo(小于等于条件过滤)

    /// <summary>
    /// 小于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThanOrEqualTo(string attributeName, string attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThanOrEqual);

    /// <summary>
    /// 小于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThanOrEqualTo(string attributeName, short attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThanOrEqual);

    /// <summary>
    /// 小于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThanOrEqualTo(string attributeName, int attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThanOrEqual);

    /// <summary>
    /// 小于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThanOrEqualTo(string attributeName, long attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThanOrEqual);

    /// <summary>
    /// 小于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThanOrEqualTo(string attributeName, double attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThanOrEqual);

    /// <summary>
    /// 小于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThanOrEqualTo(string attributeName, decimal attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThanOrEqual);

    /// <summary>
    /// 小于等于条件过滤
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddLessThanOrEqualTo(string attributeName, float attributeValue)
        => AddCompareParameter(attributeName, attributeValue, Operator.LessThanOrEqual);

    #endregion

    #region 私有方法

    /// <summary>
    /// 添加比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    /// <param name="compareOperator"></param>
    private void AddCompareParameter(string attributeName, string attributeValue, Operator compareOperator)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        var paraName = $"{columnName}_{NewCode.Mark}";
        Selections.Add($"[{columnName}] {GetOperatorString(compareOperator)} @{paraName}");
        Parameters.Add(new Parameter(paraName, attributeValue));
    }

    /// <summary>
    /// 添加比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    /// <param name="compareOperator"></param>
    private void AddCompareParameter(string attributeName, bool attributeValue, Operator compareOperator)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] {GetOperatorString(compareOperator)} {(GetBoolean.FromObject(attributeValue) ? "1" : "0")}");
    }

    /// <summary>
    /// 添加比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    /// <param name="compareOperator"></param>
    private void AddCompareParameter(string attributeName, short attributeValue, Operator compareOperator)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] {GetOperatorString(compareOperator)} {attributeValue}");
    }

    /// <summary>
    /// 添加比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    /// <param name="compareOperator"></param>
    private void AddCompareParameter(string attributeName, int attributeValue, Operator compareOperator)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] {GetOperatorString(compareOperator)} {attributeValue}");
    }

    /// <summary>
    /// 添加比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    /// <param name="compareOperator"></param>
    private void AddCompareParameter(string attributeName, long attributeValue, Operator compareOperator)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] {GetOperatorString(compareOperator)} {attributeValue}");
    }

    /// <summary>
    /// 添加比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    /// <param name="compareOperator"></param>
    private void AddCompareParameter(string attributeName, double attributeValue, Operator compareOperator)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] {GetOperatorString(compareOperator)} {attributeValue}");
    }

    /// <summary>
    /// 添加比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    /// <param name="compareOperator"></param>
    private void AddCompareParameter(string attributeName, decimal attributeValue, Operator compareOperator)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] {GetOperatorString(compareOperator)} {attributeValue}");
    }

    /// <summary>
    /// 添加比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    /// <param name="compareOperator"></param>
    private void AddCompareParameter(string attributeName, float attributeValue, Operator compareOperator)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] {GetOperatorString(compareOperator)} {attributeValue}");
    }

    #endregion

    #endregion

    #region 匹配条件过滤

    /// <summary>
    /// 指定子字符串与指定属匹配
    /// </summary>
    /// <param name="attributeName">实体属性</param>
    /// <param name="attributeValue">指定字符串</param>
    public void AddMatch(string attributeName, string attributeValue)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        var paraName = $"{columnName}_{NewCode.Mark}";
        Selections.Add($"[{columnName}] LIKE '%' + @{paraName} + '%'");
        Parameters.Add(new Parameter(paraName, attributeValue));
    }

    /// <summary>
    /// 指定子字符串与指定属不匹配
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue"></param>
    public void AddNotMatch(string attributeName, string attributeValue)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        var paraName = $"{columnName}_{NewCode.Mark}";
        Selections.Add($"[{columnName}] NOT LIKE '%' + @{paraName} + '%'");
        Parameters.Add(new Parameter(paraName, attributeValue));
    }

    /// <summary>
    /// 前缀匹配
    /// </summary>
    /// <param name="attributeName">属性</param>
    /// <param name="attributeValue">匹配值</param>
    public void AddMatchPrefix(string attributeName, string attributeValue)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        var paraName = $"{columnName}_{NewCode.Mark}";
        Selections.Add($"[{columnName}] LIKE @{paraName} + '%'");
        Parameters.Add(new Parameter(paraName, attributeValue));
    }

    #endregion

    #region 范围匹配

    #region AddIn(字段IN比较)

    /// <summary>
    /// 字段IN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="customerSql"></param>
    /// <param name="parameter"></param>
    public void AddIn(string attributeName, string customerSql, Parameter parameter)
        => AddIn(attributeName, customerSql, [parameter]);

    /// <summary>
    /// 字段IN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="customerSql"></param>
    /// <param name="parameters"></param>
    public void AddIn(string attributeName, string customerSql, List<Parameter>? parameters = null)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IN ({customerSql})");

        if (parameters != null)
        {
            Parameters.AddRange(parameters);
        }
    }

    /// <summary>
    /// 字段IN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValues"></param>
    public void AddIn(string attributeName, IEnumerable<string> attributeValues)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IN ('{string.Join("', '", attributeValues.Distinct())}')");
    }

    /// <summary>
    /// 字段IN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValues"></param>
    public void AddIn(string attributeName, List<short> attributeValues)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IN ({string.Join(", ", attributeValues)})");
    }

    /// <summary>
    /// 字段IN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValues"></param>
    public void AddIn(string attributeName, List<int> attributeValues)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IN ({string.Join(", ", attributeValues)})");
    }

    /// <summary>
    /// 字段IN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValues"></param>
    public void AddIn(string attributeName, List<long> attributeValues)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IN ({string.Join(", ", attributeValues)})");
    }

    /// <summary>
    /// 字段IN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValues"></param>
    public void AddIn(string attributeName, List<double> attributeValues)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IN ({string.Join(", ", attributeValues)})");
    }

    /// <summary>
    /// 字段IN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValues"></param>
    public void AddIn(string attributeName, List<decimal> attributeValues)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IN ({string.Join(", ", attributeValues)})");
    }

    /// <summary>
    /// 字段IN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValues"></param>
    public void AddIn(string attributeName, List<float> attributeValues)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] IN ({string.Join(", ", attributeValues)})");
    }

    #endregion

    #region AddBetween(字段BETWEEN比较)

    /// <summary>
    /// 字段BETWEEN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue1"></param>
    /// <param name="attributeValue2"></param>
    public void AddBetween(string attributeName, string attributeValue1, string attributeValue2)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        var paraName1 = $"{columnName}_{NewCode.Mark}";
        var paraName2 = $"{columnName}_{NewCode.Mark}";
        Selections.Add($"[{columnName}] BETWEEN @{paraName1} AND @{paraName2}");
        Parameters.Add(new Parameter(paraName1, attributeValue1));
        Parameters.Add(new Parameter(paraName2, attributeValue2));
    }

    /// <summary>
    /// 字段BETWEEN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue1"></param>
    /// <param name="attributeValue2"></param>
    public void AddBetween(string attributeName, short attributeValue1, short attributeValue2)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] BETWEEN {attributeValue1} AND {attributeValue2}");
    }

    /// <summary>
    /// 字段BETWEEN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue1"></param>
    /// <param name="attributeValue2"></param>
    public void AddBetween(string attributeName, int attributeValue1, int attributeValue2)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] BETWEEN {attributeValue1} AND {attributeValue2}");
    }

    /// <summary>
    /// 字段BETWEEN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue1"></param>
    /// <param name="attributeValue2"></param>
    public void AddBetween(string attributeName, long attributeValue1, long attributeValue2)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] BETWEEN {attributeValue1} AND {attributeValue2}");
    }

    /// <summary>
    /// 字段BETWEEN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue1"></param>
    /// <param name="attributeValue2"></param>
    public void AddBetween(string attributeName, double attributeValue1, double attributeValue2)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] BETWEEN {attributeValue1} AND {attributeValue2}");
    }

    /// <summary>
    /// 字段BETWEEN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue1"></param>
    /// <param name="attributeValue2"></param>
    public void AddBetween(string attributeName, decimal attributeValue1, decimal attributeValue2)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] BETWEEN {attributeValue1} AND {attributeValue2}");
    }

    /// <summary>
    /// 字段BETWEEN比较
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="attributeValue1"></param>
    /// <param name="attributeValue2"></param>
    public void AddBetween(string attributeName, float attributeValue1, float attributeValue2)
    {
        var attributeMap = ThisClassMap.GetAttributeMap(attributeName);
        var columnName = attributeMap.Column.ColumnName;
        Selections.Add($"[{columnName}] BETWEEN {attributeValue1} AND {attributeValue2}");
    }

    #endregion

    #endregion

    #region 字段比较

    /// <summary>
    /// 添加字段比较
    /// </summary>
    /// <param name="attributeName1"></param>
    /// <param name="attributeName2"></param>
    /// <param name="compareOperator"></param>
    public void AddFieldCompare(string attributeName1, string attributeName2, Operator compareOperator)
    {
        var columnName1 = ThisClassMap.GetAttributeMap(attributeName1).Column.ColumnName;
        var columnName2 = ThisClassMap.GetAttributeMap(attributeName2).Column.ColumnName;
        if (compareOperator is Operator.Equal or Operator.NotEqual
            or Operator.GreaterThan or Operator.GreaterThanOrEqual
            or Operator.LessThan or Operator.LessThanOrEqual)
        {
            Selections.Add($"[{columnName1}] {GetOperatorString(compareOperator)} [{columnName2}]");
        }
        else
        {
            throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Develop,
                $"字段比较不支持{compareOperator}操作", new
                {
                    attributeName1,
                    attributeName2,
                    compareOperator
                });
        }
    }

    #endregion

    #region 自定义比较

    /// <summary>
    /// 自定义比较(对于特殊或复杂的比较，通过该方法实现)
    /// </summary>
    /// <param name="customerSql"></param>
    public void AddCustomerCompare(string customerSql)
    {
        Selections.Add(customerSql);
    }

    /// <summary>
    /// 自定义比较(对于特殊或复杂的比较，通过该方法实现)
    /// </summary>
    /// <param name="customerSql"></param>
    /// <param name="parameter"></param>
    public void AddCustomerCompare(string customerSql, Parameter parameter)
    {
        Selections.Add(customerSql);
        Parameters.Add(parameter);
    }

    /// <summary>
    /// 自定义比较(对于特殊或复杂的比较，通过该方法实现)
    /// </summary>
    /// <param name="customerSql"></param>
    /// <param name="parameters"></param>
    public void AddCustomerCompare(string customerSql, List<Parameter> parameters)
    {
        Selections.Add(customerSql);
        Parameters.AddRange(parameters);
    }

    #endregion

    #endregion


    #region 私有静态方法

    /// <summary>
    /// 解析过滤条件字符串
    /// </summary>
    /// <param name="operatorType"></param>
    /// <returns></returns>
    private static string GetOperatorString(Operator operatorType) => operatorType switch
    {
        Operator.Equal => "=",
        Operator.NotEqual => "<>",
        Operator.GreaterThan => ">",
        Operator.GreaterThanOrEqual => ">=",
        Operator.LessThan => "<",
        Operator.LessThanOrEqual => "<=",
        Operator.Match => "LIKE",
        Operator.NotMatch => "NOT LIKE",
        Operator.MatchPrefix => "LIKE",
        Operator.IN => "IN",
        Operator.BETWEEN => "BETWEEN",
        _ => throw BusinessException.Get(MethodBase.GetCurrentMethod(), ExceptionInfoType.Develop, $"不支持的操作符{operatorType}", new
        {
            operatorType
        })
    };

    #endregion
}
