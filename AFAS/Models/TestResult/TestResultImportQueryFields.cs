namespace AFAS.Models.TestResult
{
    /// <summary> 
    /// 测试结果导入查询行数据 
    /// </summary>
    public class TestResultImportQueryFields
    {
        /// <summary> 
        /// 查询文本 
        /// </summary>
        public string QueryText { get; set; } = "";

        /// <summary>
        /// 状态
        /// </summary>
        public int Status{ get; set; } = -1;

        /// <summary> 
        /// 开始日期 
        /// </summary>
        public string StartDay { get; set; } = "";

        /// <summary> 
        /// 结束日期 
        /// </summary>
        public string EndDay { get; set; } = "";
    }
}
