using System.ComponentModel;

namespace AFAS.Authorization.Enums;

/// <summary>
/// 角色枚举
/// </summary>
public enum RoleEnum
{
    [Description("老师")] TEACHER,
    [Description("学生")] STUDENT,
}
