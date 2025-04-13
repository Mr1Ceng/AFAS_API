using AFAS.Entitys;

namespace AFAS.Models
{
    /// <summary>
    /// 试卷题目T1模型
    /// </summary>
    public class QuestionT1Model
    {
        /// <summary>
        /// 题目编码;QS开头的6位字母数字组合
        /// </summary>
        public string QuestionId { get; set; } = "";

        /// <summary>
        /// 数字题
        /// </summary>
        public string NumberQuestion { get; set; } = "";

        /// <summary>
        /// 故事题
        /// </summary>
        public string StoryQuestion { get; set; } = "";

        /// <summary>
        /// 数字题1答案
        /// </summary>
        public int Number1 { get; set; } = 0;

        /// <summary>
        /// 数字题2答案
        /// </summary>
        public int Number2 { get; set; } = 0;

        /// <summary>
        /// 故事题答案
        /// </summary>
        public int Number3 { get; set; } = 0;

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
