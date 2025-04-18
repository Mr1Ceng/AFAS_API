using AFAS.Entitys;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目S1模型
    /// </summary>
    public class QuestionS1Model
    {
        /// <summary>
        /// 题目基本信息
        /// </summary>
        public BQuestion QuestionInfo { get; set; } = new();

        /// <summary>
        /// 题目问题列表
        /// </summary>
        public List<BQuestionS1> QuestionList { get; set; } = new();

    }
}
