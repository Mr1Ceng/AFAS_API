namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目S5答案模型
    /// </summary>
    public class AnswerS5Model
    {
        /// <summary>
        /// 答案编码
        /// </summary>
        public string AnswerId { get; set; } = "";

        /// <summary>
        /// 题目编码;QS开头的6位字母数字组合
        /// </summary>
        public string QuestionId { get; set; } = "";

        /// <summary>
        /// 耗时（秒）
        /// </summary>
        public int TimeConsume { get; set; } = 0;

        /// <summary>
        /// 问题图片Base64
        /// </summary>
        public string QuestionImage { get; set; } = "";

        /// <summary>
        /// 答案图片Base64
        /// </summary>
        public string AnswerImage { get; set; } = "";

        /// <summary>
        /// 图形数
        /// </summary>
        public int ShapeNumber { get; set; } = 0;

        /// <summary>
        /// 错误数
        /// </summary>
        public int ErrorNumber { get; set; } = 0;

        /// <summary>
        /// 标准分
        /// </summary>
        public int StandardScore { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = "";

    }
}
