using AFAS.Entity;
using AFAS.Models.Answer;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目S1答案模型
    /// </summary>
    public class AnswerS1Model
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
        /// 原始分
        /// </summary>
        public int OriginScore { get; set; } = 0;

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
        public List<AnswerS1A> answerList { get; set; } = new List<AnswerS1A>();



    }
}
