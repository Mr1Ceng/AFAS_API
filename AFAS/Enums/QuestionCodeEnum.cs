using System.ComponentModel;

namespace AFAS.Enums;

/// <summary>
/// 问题类型枚举
/// </summary>
internal enum QuestionCodeEnum
{
    [Description("视觉广度")] S1,
    [Description("视觉稳定性")] S2,
    [Description("视觉转移")] S3,
    [Description("手眼协调")] S4,
    [Description("视觉工作记忆")] S5,
    [Description("听觉集中")] T1,
    [Description("听觉分辨")] T2,
    [Description("听觉记忆")] T3,
}
