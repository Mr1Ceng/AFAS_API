using AFAS.Entitys;

namespace AFAS.Models
{
    /// <summary>
    /// 试卷模型
    /// </summary>
    public class QuestionnaireModel : QuestionnaireForm
    {
        /// <summary>
        /// 题目列表
        /// </summary>
        public List<BQuestion> bQuestionList { get; set; } = new();

        /// <summary>
        /// S1题目
        /// </summary>
        public List<BQuestionS1> bQuestionS1List { get; set; } = new();

        /// <summary>
        /// S2题目
        /// </summary>
        public List<BQuestionS2> bQuestionS2List { get; set; } = new();

        /// <summary>
        /// S3题目
        /// </summary>
        public List<BQuestionS3> bQuestionS3List { get; set; } = new();

        /// <summary>
        /// S4题目
        /// </summary>
        public BQuestionS4 bQuestionS4 { get; set; } = new();

        /// <summary>
        /// S5题目
        /// </summary>
        public List<BQuestionS5> bQuestionS5List { get; set; } = new();

        /// <summary>
        /// T1题目
        /// </summary>
        public QuestionT1Model bQuestionT1 { get; set; } = new();

        /// <summary>
        /// T2题目
        /// </summary>
        public QuestionT2Model bQuestionT2 { get; set; } = new();

        /// <summary>
        /// T3题目
        /// </summary>
        public List<BQuestionT3> bQuestionT3List { get; set; } = new();
    }
}
