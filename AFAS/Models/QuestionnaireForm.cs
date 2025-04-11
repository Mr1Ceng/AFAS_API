namespace AFAS.Models
{
    /// <summary>
    /// 试卷表单
    /// </summary>
    public class BQuestionnaireForm
    {
        /// <summary>
        /// 试卷编码;QN开头的6位字母数字
        /// </summary>
        public string QuestionnaireId { get; set; } = "";

        /// <summary>
        /// 试卷名称
        /// </summary>
        public string QuestionnaireName { get; set; } = "";

        /// <summary>
        /// 试卷版本名称
        /// </summary>
        public string VersionName { get; set; } = "";

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = "";

    }
}
