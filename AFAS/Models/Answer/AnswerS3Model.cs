using AFAS.Models.Answer;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目S3答案模型
    /// </summary>
    public class AnswerS3Model
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
        /// 划消数量
        /// </summary>
        public int RightNumber { get; set; } = 0;

        /// <summary>
        /// 错误数（不含未完成）
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

        /// <summary>
        /// 答案明细
        /// </summary>
        public List<AnswerS3A> answerList { get; set; } = new List<AnswerS3A>();



    }
}
