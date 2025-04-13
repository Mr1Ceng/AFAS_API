using AFAS.Entitys;

namespace AFAS.Models
{
    /// <summary>
    /// 试卷题目T2模型
    /// </summary>
    public class QuestionT2Model
    {
        /// <summary>
        /// 题目编码;QS开头的6位字母数字组合
        /// </summary>
        public string QuestionId { get; set; } = "";

        /// <summary>
        /// 题目
        /// </summary>
        public string Question { get; set; } = "";

        /// <summary>
        /// 听相同答案
        /// </summary>
        public int Number1 { get; set; } = 0;

        /// <summary>
        /// 听不同答案
        /// </summary>
        public int Number2 { get; set; } = 0;

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
