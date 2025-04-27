using System.ComponentModel;

namespace AFAS.Authorization.Enums;

/// <summary>
/// 角色枚举
/// </summary>
internal enum RoleEnum
{
    [Description("老师")] TEACHER,
    [Description("学生")] STUDENT,
}
