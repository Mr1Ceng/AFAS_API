namespace AFAS.Models.TestResult
{
    /// <summary> 
    /// 测试结果查询行数据 
    /// </summary>
    public class TestResultQueryFields
    {
        /// <summary> 
        /// 查询文本 
        /// </summary>
        public string QueryText { get; set; } = "";

        /// <summary> 
        /// 状态 
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary> 
        /// 开始日期 
        /// </summary>
        public string StartDay { get; set; } = "";

        /// <summary> 
        /// 结束日期 
        /// </summary>
        public string EndDay { get; set; } = "";

        /// <summary> 
        /// 导入编码 
        /// </summary>
        public string ImportId { get; set; } = "";
    }
}
