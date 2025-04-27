using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Mr1Ceng.Util
{
    /// <summary>
    /// EF拓展数据库帮助类
    /// </summary>
    public class EFCoreExtentions
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// 构造函数，初始化EFCoreExtentions并注入DbContext实例。
        /// </summary>
        /// <param name="dbContext">EF Core的数据库上下文。</param>
        public EFCoreExtentions(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 执行一条SQL查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句。</param>
        /// <param name="parameters">SQL语句中的参数列表。</param>
        /// <returns>包含查询结果的DataTable。</returns>
        public DataTable ExecuteSqlQuery(string sql, params object[] parameters)
        {
            // 获取数据库连接
            using var connection = _dbContext.Database.GetDbConnection();
            connection.Open();

            // 创建命令并设置SQL语句
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            // 添加参数到命令
            if (parameters != null && parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    var dbParameter = command.CreateParameter();
                    dbParameter.Value = parameter;
                    command.Parameters.Add(dbParameter);
                }
            }

            // 执行查询并加载结果到DataTable
            using var reader = command.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(reader);

            return dataTable;
        }

        /// <summary>
        /// 执行一条SQL非查询语句（如INSERT、UPDATE、DELETE），返回受影响的行数。
        /// </summary>
        /// <param name="sql">SQL非查询语句。</param>
        /// <param name="parameters">SQL语句中的参数列表。</param>
        /// <returns>受影响的行数。</returns>
        public int ExecuteSqlNonQuery(string sql, params object[] parameters)
        {
            // 获取数据库连接
            using var connection = _dbContext.Database.GetDbConnection();
            connection.Open();

            // 创建命令并设置SQL语句
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            // 添加参数到命令
            if (parameters != null && parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    var dbParameter = command.CreateParameter();
                    dbParameter.Value = parameter;
                    command.Parameters.Add(dbParameter);
                }
            }

            // 执行非查询操作
            return command.ExecuteNonQuery();
        }
    }
}
