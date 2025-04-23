using AFAS.Models.Answer;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目T2答案模型
    /// </summary>
    public class AnswerT2Model
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
        /// 听不同
        /// </summary>
        public int Number1 { get; set; } = 0;

        /// <summary>
        /// 听相同
        /// </summary>
        public int Number2 { get; set; } = 0;

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
        public List<AnswerT2A> answerList { get; set; } = new List<AnswerT2A>();

    }
}
