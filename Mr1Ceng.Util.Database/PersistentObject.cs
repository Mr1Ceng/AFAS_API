using System.Reflection;
using Newtonsoft.Json;
using Mr1Ceng.Util.Database.Internal.Core;

namespace Mr1Ceng.Util.Database;

/// <summary>
/// 实体类的基类
/// </summary>
/// <remarks>
/// 实体基类，封装了单个实例持久化行为，所需要持久化的实体对象必须从该类派生。
/// </remarks>
public class PersistentObject
{
    #region 变量

    /// <summary>
    /// Broker对象
    /// </summary>
    private readonly Broker broker = Broker.Instance;

    #endregion


    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public PersistentObject()
    {
    }

    #endregion


    #region 公共方法

    /// <summary>
    /// 根据对象标识，装载实体信息
    /// </summary>
    /// <returns>成功 True,失败 False</returns>
    public bool Retrieve() => broker.RetrieveObject(this);

    /// <summary>
    /// 新增实体对象
    /// </summary>
    public void Insert()
    {
        IsPersistent = false;
        broker.SaveObject(this);
        IsPersistent = true;
    }

    /// <summary>
    /// 更新实体对象
    /// </summary>
    public void Update()
    {
        IsPersistent = true;
        broker.SaveObject(this);
    }

    /// <summary>
    /// 删除实体对象
    /// </summary>
    /// <returns>成功 True,失败 False</returns>
    public void Delete() => broker.DeleteObject(this);

    #endregion


    #region 公共方法(事务中)

    /// <summary>
    /// 根据对象标识，装载实体信息(事务中)
    /// </summary>
    /// <param name="ts">事务</param>
    /// <returns>成功 True,失败 False</returns>
    public bool Retrieve(Transaction ts) => broker.RetrieveObject(this, ts);

    /// <summary>
    /// 新增实体对象(事务中)
    /// </summary>
    /// <param name="ts">事务</param>
    public void Insert(Transaction ts)
    {
        IsPersistent = false;
        broker.SaveObject(this, ts);
        IsPersistent = true;
    }

    /// <summary>
    /// 更新实体对象(事务中)
    /// </summary>
    /// <param name="ts">事务</param>
    public void Update(Transaction ts)
    {
        IsPersistent = true;
        broker.SaveObject(this, ts);
    }

    /// <summary>
    /// 删除实体对象(事务中)
    /// </summary>
    /// <param name="ts">事务</param>
    public void Delete(Transaction ts) => broker.DeleteObject(this, ts);

    #endregion


    #region 方法

    /// <summary>
    /// 取属性值
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    internal object? GetAttributeValue(string name)
    {
        var attributeType = GetType();
        try
        {
            return attributeType.InvokeMember(name, BindingFlags.GetProperty, null, this, null);
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config, "属性字段不存在", new
            {
                name
            });
        }
    }

    /// <summary>
    /// 设置属性值
    /// </summary>
    /// <param name="name"></param>
    /// <param name="objValue"></param>
    internal void SetAttributeValue(string name, object objValue)
    {
        var attributeType = GetType();
        var objectsValue = new object[1];
        objectsValue[0] = objValue;
        try
        {
            attributeType.InvokeMember(name, BindingFlags.SetProperty, null, this, objectsValue);
        }
        catch (Exception ex)
        {
            throw BusinessException.Get(ex).Add(MethodBase.GetCurrentMethod(), ExceptionInfoType.Config, "属性字段不存在", new
            {
                name,
                objValue
            });
        }
    }

    /// <summary>
    /// 返回Type对象名
    /// </summary>
    /// <param name="classType">Type</param>
    /// <returns>对象类名</returns>
    public static string GetClassName(Type classType)
    {
        var name = classType.Name;
        return name[(name.LastIndexOf('.') + 1)..];
    }

    /// <summary>
    /// 使用统一接口，返回对象的类名，并且允许被重载
    /// </summary>
    /// <returns>对象类名</returns>
    public virtual string GetClassName()
    {
        var name = GetType().Name;
        return name[(name.LastIndexOf('.') + 1)..];
    }

    #endregion


    #region 属性

    /// <summary>
    /// 判断是否为PersistenceLayer对象
    /// </summary>
    /// <returns>True 持久对象，False 对象</returns>
    [JsonIgnore]
    public bool IsPersistent { get; set; }

    #endregion


    #region ICloneable 成员

    /// <summary>
    /// 克隆对象
    /// </summary>
    /// <returns></returns>
    public object Clone() => MemberwiseClone();

    #endregion
}
