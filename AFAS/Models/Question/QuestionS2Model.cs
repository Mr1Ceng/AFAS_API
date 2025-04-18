using AFAS.Entitys;

namespace AFAS.Models.Question
{
    /// <summary>
    /// 试卷题目S2模型
    /// </summary>
    public class QuestionS2Model
    {
        /// <summary>
        /// 题目基本信息
        /// </summary>
        public BQuestion QuestionInfo { get; set; } = new();

        /// <summary>
        /// 题目问题列表
        /// </summary>
        public List<BQuestionS2> QuestionList { get; set; } = new();

    }
}
