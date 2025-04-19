namespace AFAS.Models.Answer
{
    /// <summary>
    /// S3答案明细
    /// </summary>
    public class AnswerS3A
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int GridRow { get; set; } = 0;

        /// <summary>
        /// 列号
        /// </summary>
        public int GridColumn { get; set; } = 0;

        /// <summary>
        /// 答案值
        /// </summary>
        public int Value { get; set; } = 0;

    }
}
