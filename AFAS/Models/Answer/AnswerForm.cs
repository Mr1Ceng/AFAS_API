namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目结果表单
    /// </summary>
    public class AnswerForm
    {
        /// <summary>
        /// 答案编码
        /// </summary>
        public string AnswerId { get; set; } = "";

        /// <summary>
        /// 试卷编码;QN开头的6位字母数字
        /// </summary>
        public string QuestionnaireId { get; set; } = "";

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserId { get; set; } = "";

        /// <summary>
        /// 测评日期
        /// </summary>
        public string QuestionnaireDate { get; set; } = "";

        /// <summary>
        /// 测评老师
        /// </summary>
        public string TeacherId { get; set; } = "";

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// 雷达图Base64
        /// </summary>
        public string RadarImage { get; set; } = "";

        /// <summary>
        /// 视知觉结果图Base64
        /// </summary>
        public string SImage { get; set; } = "";

        /// <summary>
        /// 视知觉结果
        /// </summary>
        public string SResult { get; set; } = "";

        /// <summary>
        /// 听知觉结果图Base64
        /// </summary>
        public string TImage { get; set; } = "";

        /// <summary>
        /// 听知觉结果
        /// </summary>
        public string TResult { get; set; } = "";

        /// <summary>
        /// 弱势
        /// </summary>
        public string Weak { get; set; } = "";

        /// <summary>
        /// 优势
        /// </summary>
        public string Advantage { get; set; } = "";

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = "";

        /// <summary>
        /// 建议课程
        /// </summary>
        public string SuggestedCourse { get; set; } = "";

        /// <summary>
        /// 评测标准
        /// </summary>
        public string LevelCode { get; set; } = "";

    }
}
