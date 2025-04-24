namespace AFAS.Models.Answer
{
    /// <summary>
    /// T3答案明细
    /// </summary>
    public class AnswerT3A
    {
        /// <summary>
        /// 顺逆类型
        /// </summary>
        public bool QuestionType { get; set; } = false;

        /// <summary>
        /// 题目序号
        /// </summary>
        public int QuestionSort { get; set; } = 0;

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; } = 0;

        /// <summary>
        /// 答案
        /// </summary>
        public string Value { get; set; } = "";

    }
}
