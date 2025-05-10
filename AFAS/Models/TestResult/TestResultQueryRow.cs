namespace AFAS.Models.TestResult
{

    /// <summary> 
    /// 测试结果查询行数据 
    /// </summary>
    public class TestResultQueryRow
    {

        /// <summary> 
        /// 答案编码 
        /// </summary>
        public string AnswerId { get; set; } = "";

        /// <summary> 
        /// 试卷编码;QN开头的6位字母数字 
        /// </summary>
        public string QuestionnaireId { get; set; } = "";

        /// <summary> 
        /// 试卷名称
        /// </summary>
        public string QuestionnaireName { get; set; } = "";

        /// <summary> 
        /// 试卷版本
        /// </summary>
        public string VersionName { get; set; } = "";

        /// <summary> 
        /// 用户编码 
        /// </summary>
        public string UserId { get; set; } = "";

        /// <summary> 
        /// 用户姓名 
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary> 
        /// 测评日期 
        /// </summary>
        public string QuestionnaireDate { get; set; } = "";

        /// <summary> 
        /// 测评老师 
        /// </summary>
        public string TeacherId { get; set; } = "";

        /// <summary> 
        /// 测评老师姓名 
        /// </summary>
        public string TeacherName { get; set; } = "";

        /// <summary> 
        /// 状态 
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary> 
        /// 雷达图Base64 
        /// </summary>
        public string RadarImage { get; set; } = "";

        /// <summary> 
        /// 视知觉结果图Base64 
        /// </summary>
        public string SImage { get; set; } = "";

        /// <summary> 
        /// 视知觉结果 
        /// </summary>
        public string SResult { get; set; } = "";

        /// <summary> 
        /// 听知觉结果图Base64 
        /// </summary>
        public string TImage { get; set; } = "";

        /// <summary> 
        /// 听知觉结果 
        /// </summary>
        public string TResult { get; set; } = "";

        /// <summary> 
        /// 弱势 
        /// </summary>
        public string Weak { get; set; } = "";

        /// <summary> 
        /// 优势 
        /// </summary>
        public string Advantage { get; set; } = "";

        /// <summary> 
        /// 备注 
        /// </summary>
        public string Remark { get; set; } = "";

        /// <summary> 
        /// 建议课程 
        /// </summary>
        public string SuggestedCourse { get; set; } = "";

        /// <summary> 
        /// 建议课程名称 
        /// </summary>
        public string SuggestedCourseName { get; set; } = "";

        /// <summary> 
        /// 评测标准 
        /// </summary>
        public string LevelCode { get; set; } = "";

        /// <summary> 
        /// 评测标准名称 
        /// </summary>
        public string LevelName { get; set; } = "";
    }
}
