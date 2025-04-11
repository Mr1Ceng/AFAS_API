namespace Mr1Ceng.Util.Swagger;

/// <summary>
/// WebApi的分组项
/// </summary>
public class SwaggerGroupItem(string groupId, string groupName)
{
    /// <summary>
    /// 分组ID
    /// </summary>
    public string GroupId { get; set; } = groupId;

    /// <summary>
    /// 分组名称
    /// </summary>
    public string GroupName { get; set; } = groupName;
}
