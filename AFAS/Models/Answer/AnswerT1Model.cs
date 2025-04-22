using AFAS.Models.Answer;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目T1答案模型
    /// </summary>
    public class AnswerT1Model
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
        /// 数字题1
        /// </summary>
        public int Number1 { get; set; } = 0;

        /// <summary>
        /// 数字题2
        /// </summary>
        public int Number2 { get; set; } = 0;

        /// <summary>
        /// 故事题
        /// </summary>
        public int Number3 { get; set; } = 0;

        /// <summary>
        /// 语义理解错误数
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
        public List<AnswerT1A> answerList { get; set; } = new List<AnswerT1A>();



    }
}
