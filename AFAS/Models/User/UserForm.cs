namespace AFAS.Models.User
{
    /// <summary>
    /// 用户表单
    /// </summary>
    public class UserForm
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserId { get; set; } = "";

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = "";

        /// <summary>
        /// 用户头像
        /// </summary>
        public string AvatarUrl { get; set; } = "";

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; } = "";

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; } = 0;

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; } = "";

        /// <summary>
        /// 用户角色;枚举：UserRole
        /// </summary>
        public string Role { get; set; } = "";

    }
}
