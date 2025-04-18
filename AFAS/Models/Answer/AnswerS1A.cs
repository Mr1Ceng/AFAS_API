namespace AFAS.Models.Answer
{
    /// <summary>
    /// S1答案明细
    /// </summary>
    public class AnswerS1A
    {
        /// <summary>
        /// 格子类型;SMALL、MIDDLE、LARGE
        /// </summary>
        public string GridType { get; set; } = "";

        /// <summary>
        /// 耗时（秒）
        /// </summary>
        public int TimeConsume { get; set; } = 0;

    }
}
