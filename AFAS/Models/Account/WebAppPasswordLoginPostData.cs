namespace AFAS.Models.Account
{
    /// <summary>
    /// 登录表单
    /// </summary>
    public class WebAppPasswordLoginPostData
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; } = "";

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "";

    }
}
