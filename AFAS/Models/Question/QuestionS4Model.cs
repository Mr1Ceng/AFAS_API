using AFAS.Entity;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目S4模型
    /// </summary>
    public class QuestionS4Model
    {
        /// <summary>
        /// 题目基本信息
        /// </summary>
        public BQuestion QuestionInfo { get; set; } = new();

        /// <summary>
        /// 题目问题列表
        /// </summary>
        public BQuestionS4 QuestionList { get; set; } = new();

    }
}
