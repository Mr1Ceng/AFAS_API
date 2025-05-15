namespace AFAS.Models.TestResult
{

    /// <summary> 
    /// 测试结果导入查询行数据 
    /// </summary>
    public class TestResultImportQueryRow
    {
        /// <summary>
        /// 导入编码
        /// </summary>
        public string ImportId { get; set; } = "";

        /// <summary>
        /// 导入时间
        /// </summary>
        public string ImportStamp { get; set; } = "";

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// 导入结果
        /// </summary>
        public string ImportResult { get; set; } = "";

        /// <summary>
        /// 导入数量
        /// </summary>
        public int ImportCount { get; set; } = 0;

        /// <summary>
        /// 导入者
        /// </summary>
        public string UserId { get; set; } = "";

        /// <summary> 
        /// 导入者姓名 
        /// </summary>
        public string UserName { get; set; } = "";
    }
}
