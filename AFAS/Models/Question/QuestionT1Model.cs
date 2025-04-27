using AFAS.Entity;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目T1模型
    /// </summary>
    public class QuestionT1Model
    {
        /// <summary>
        /// 题目基本信息
        /// </summary>
        public BQuestion QuestionInfo { get; set; } = new();

        /// <summary>
        /// 题目标准答案信息
        /// </summary>
        public BQuestionT1 AnswerInfo { get; set; } = new();

        /// <summary>
        /// 题目T1问题列表
        /// </summary>
        public List<BQuestionT1Q> bQuestionT1QList { get; set; } = new();

        /// <summary>
        /// 题目T1答案列表
        /// </summary>
        public List<BQuestionT1A> bQuestionT1AList { get; set; } = new();

    }
}
