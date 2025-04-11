using System.ComponentModel;

namespace Mr1Ceng.Util;

/// <summary>
/// 审批状态
/// </summary>
public enum ApproveStatus
{
    /// <summary>
    /// 未填写的
    /// </summary>
    [Description("未填写")] BLANK = 0,

    /// <summary>
    /// 未提交的
    /// </summary>
    [Description("未提交")] DRAFT = 1,

    /// <summary>
    /// 待审核的
    /// </summary>
    [Description("待审核")] SUBMITTED = 2,

    /// <summary>
    /// 审批通过的
    /// </summary>
    [Description("审批通过")] SUCCESSFUL = 3,

    /// <summary>
    /// 审批拒绝的
    /// </summary>
    [Description("审批拒绝")] REFUSED = 4,

    /// <summary>
    /// 取消的
    /// </summary>
    [Description("撤回")] CANCELED = 5
}
