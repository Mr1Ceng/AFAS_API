namespace AFAS.Models.EvaluationStandard
{
    /// <summary>
    /// 测评标准配置
    /// </summary>
    public class EvaluationStandardForm
    {
        /// <summary>
        /// 等级编码
        /// </summary>
        public string LevelCode { get; set; } = "";

        /// <summary>
        /// 等级名称
        /// </summary>
        public string LevelName { get; set; } = "";

        /// <summary>
        /// 视觉广度
        /// </summary>
        public int S1 { get; set; } = 0;

        /// <summary>
        /// 视觉稳定性
        /// </summary>
        public int S2 { get; set; } = 0;

        /// <summary>
        /// 视觉转移
        /// </summary>
        public int S3 { get; set; } = 0;

        /// <summary>
        /// 手眼协调
        /// </summary>
        public int S4 { get; set; } = 0;

        /// <summary>
        /// 视觉工作记忆
        /// </summary>
        public int S5 { get; set; } = 0;

        /// <summary>
        /// 听觉集中
        /// </summary>
        public int T1 { get; set; } = 0;

        /// <summary>
        /// 听觉分辨
        /// </summary>
        public int T2 { get; set; } = 0;

        /// <summary>
        /// 听觉记忆
        /// </summary>
        public int T3 { get; set; } = 0;
    }
}
