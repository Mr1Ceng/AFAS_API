namespace AFAS.Models
{
    /// <summary>
    /// 试卷题目模型
    /// </summary>
    public class QuestionModel
    {
        /// <summary>
        /// 题目编码;QS开头的6位字母数字组合
        /// </summary>
        public string QuestionId { get; set; } = "";

        /// <summary>
        /// 题目名称
        /// </summary>
        public string QuestionName { get; set; } = "";

        /// <summary>
        /// 试卷编码;QN开头的6位字母数字
        /// </summary>
        public string QuestionnaireId { get; set; } = "";

        /// <summary>
        /// 注意事项
        /// </summary>
        public string Precautions { get; set; } = "";

        /// <summary>
        /// 指导语
        /// </summary>
        public string Instruction { get; set; } = "";

        /// <summary>
        /// 指导语
        /// </summary>
        public string Instruction2 { get; set; } = "";

        /// <summary>
        /// 指导语
        /// </summary>
        public string Instruction3 { get; set; } = "";

        /// <summary>
        /// 指导语
        /// </summary>
        public string Instruction4 { get; set; } = "";

    }
}
