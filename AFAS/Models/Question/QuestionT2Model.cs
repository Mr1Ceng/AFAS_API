using AFAS.Entitys;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目T2模型
    /// </summary>
    public class QuestionT2Model
    {
        /// <summary>
        /// 题目基本信息
        /// </summary>
        public BQuestion QuestionInfo { get; set; } = new();

        /// <summary>
        /// 题目标准答案信息
        /// </summary>
        public BQuestionT2 AnswerInfo { get; set; } = new();
        /// <summary>
        /// 题目T2问题列表
        /// </summary>
        public List<BQuestionT2Q> bQuestionT2QList { get; set; } = new();

        /// <summary>
        /// 题目T2答案列表
        /// </summary>
        public List<BQuestionT2A> bQuestionT2AList { get; set; } = new();

    }
}
