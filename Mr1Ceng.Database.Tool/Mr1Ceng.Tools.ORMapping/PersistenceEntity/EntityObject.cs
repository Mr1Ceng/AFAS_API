using System.Collections;
using Mr1Ceng.Util;

namespace Mr1Ceng.Tools.ORMapping.V7.PersistenceEntity;

/// <summary>
/// EntityObject 的摘要说明。
/// </summary>
public class EntityObject
{
    /// <summary>
    /// EntityName
    /// </summary>
    public string EntityName { get; set; } = "";

    /// <summary>
    /// EntityType
    /// </summary>
    public string EntityType { get; set; } = "";

    /// <summary>
    /// EntityDescription
    /// </summary>
    public string EntityDescription { get; set; } = "";

    /// <summary>
    /// 关系列表
    /// </summary>
    public RelationArrayList Relationships { get; set; }

    /// <summary>
    /// 字段列表
    /// </summary>
    public FieldArrayList FieldArrayLists { get; set; }

    /// <summary>
    /// 备注列表
    /// </summary>
    public IList<KeyValueText> FieldRemarkLists { get; set; }
}

public class Field
{
    public Field() {}

    public Field(string name, string dataType, int length, bool isKey)
    {
        Name = name;
        DataType = dataType;
        Length = length;
        IsKey = isKey;
    }

    public string Name { get; set; } = "";

    public string DataType { get; set; } = "";

    public int Length { get; set; }

    public bool IsIdentity { get; set; }

    public bool IsKey { get; set; }
}

public class Relation(string parent, string primaryKey, string child, string foreignKey)
{
    public string Parent { get; set; } = parent;

    public string PrimaryKey { get; set; } = primaryKey;

    public string Child { get; set; } = child;

    public string ForeignKey { get; set; } = foreignKey;
}

public class FieldArrayList : ArrayList
{
    public int Add(Field value) => base.Add(value);

    public new Field this[int index]
    {
        get => (Field)base[index];
        set => base[index] = value;
    }

    public override int Count => base.Count;
}

public class RelationArrayList : ArrayList
{
    public int Add(Relation value) => base.Add(value);

    public new Relation this[int index]
    {
        get => (Relation)base[index];
        set => base[index] = value;
    }

    public override int Count => base.Count;
}
