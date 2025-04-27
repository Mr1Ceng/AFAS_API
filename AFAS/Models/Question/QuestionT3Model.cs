using AFAS.Entity;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目T3模型
    /// </summary>
    public class QuestionT3Model
    {
        /// <summary>
        /// 题目基本信息
        /// </summary>
        public BQuestion QuestionInfo { get; set; } = new();

        /// <summary>
        /// 题目问题列表
        /// </summary>
        public List<BQuestionT3> QuestionList { get; set; } = new();

    }
}
