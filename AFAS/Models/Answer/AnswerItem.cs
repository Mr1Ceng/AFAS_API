namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目答案明细
    /// </summary>
    public class AnswerItem
    {
        /// <summary>
        /// 题目类型
        /// </summary>
        public string QuestionCode { get; set; } = "";

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
