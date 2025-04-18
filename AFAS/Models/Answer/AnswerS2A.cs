namespace AFAS.Models.Answer
{
    /// <summary>
    /// S2答案明细
    /// </summary>
    public class AnswerS2A
    {
        /// <summary>
        /// 行号
        /// </summary>
        public string GridRow { get; set; } = "";

        /// <summary>
        /// 列号
        /// </summary>
        public int GridColumn { get; set; } = 0;

        /// <summary>
        /// 是否选择
        /// </summary>
        public bool Selected { get; set; } = false;

    }
}
