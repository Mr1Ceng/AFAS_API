﻿using Microsoft.Data.Sqlite;
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
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameter">参数</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecuteSqlQuery(string sql, Parameter parameter)
        {
            return ExecuteSqlQuery(sql, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecuteSqlQuery(string sql, IList<Parameter> parameters)
        {
            // 获取数据库连接
            using var connection = _dbContext.Database.GetDbConnection();
            connection.Open();

            // 创建命令并设置SQL语句
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            // 添加参数到命令
            foreach (Parameter parameter in parameters)
            {
                command.Parameters.Add(new SqliteParameter("@" + parameter.Name, parameter.Value));
            }

            // 执行查询并加载结果到DataTable
            using var reader = command.ExecuteReader();
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataSet.EnforceConstraints = false; // 关闭约束检查
            dataSet.Tables.Add(dataTable);
            dataTable.Load(reader);

            return dataTable;
        }

        #region 分页查询

        /// <summary>
        /// 执行一条SQL分页查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKey">排序字段</param>
        /// <param name="pageNumber">当前页码（从 0 开始）</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="parameter">参数</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecutePagedQuery(string sql, KeySorterValue sortKey, int pageNumber, int pageSize, Parameter parameter)
        {
            return ExecutePagedQuery(sql, sortKey, pageNumber, pageSize, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL分页查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKey">排序字段</param>
        /// <param name="pageNumber">当前页码（从 0 开始）</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecutePagedQuery(string sql, KeySorterValue sortKey, int pageNumber, int pageSize, IList<Parameter> parameters)
        {
            return ExecutePagedQuery(sql, new List<KeySorterValue>() { sortKey } , pageNumber, pageSize, parameters);
        }

        /// <summary>
        /// 执行一条SQL分页查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKeys">排序字段列表</param>
        /// <param name="pageNumber">当前页码（从 0 开始）</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="parameter">参数</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecutePagedQuery(string sql, List<KeySorterValue> sortKeys, int pageNumber, int pageSize, Parameter parameter)
        {
            return ExecutePagedQuery(sql, sortKeys, pageNumber, pageSize, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL分页查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKeys">排序字段列表</param>
        /// <param name="pageNumber">当前页码（从 0 开始）</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecutePagedQuery(string sql, List<KeySorterValue> sortKeys, int pageNumber, int pageSize, IList<Parameter> parameters)
        {
            int offset = pageNumber * pageSize;

            // 生成排序 SQL 语句
            string orderByClause = "ORDER BY ";
            if (sortKeys != null && sortKeys.Count > 0)
            {
                var orderList = new List<string>();

                foreach (var sort in sortKeys)
                {
                    orderList.Add($"{sort.Key} {sort.Value.ToString()}");
                }

                orderByClause += string.Join(", ", orderList);
            }
            else
            {
                orderByClause = ""; // 默认不排序
            }

            // 拼接最终 SQL
            string pagedSql = (pageSize == 0)
                 ? $"{sql} {orderByClause}"  // 不加 LIMIT / OFFSET
                 : $"{sql} {orderByClause} LIMIT {pageSize} OFFSET {offset}";

            using var connection = _dbContext.Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = pagedSql;

            // 添加参数到命令
            foreach (Parameter parameter in parameters)
            {
                command.Parameters.Add(new SqliteParameter("@" + parameter.Name, parameter.Value));
            }

            using var reader = command.ExecuteReader();
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataSet.EnforceConstraints = false; // 关闭约束检查
            dataSet.Tables.Add(dataTable);
            dataTable.Load(reader);

            return dataTable;
        }

        /// <summary>
        /// 执行一条SQL分页查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKey">排序字段</param>
        /// <param name="pageNumber">当前页码（从 0 开始）</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="parameter">参数</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataList<T> ExecutePagedQuery<T>(string sql, KeySorterValue sortKey, int pageNumber, int pageSize, Parameter parameter)
        {
            return ExecutePagedQuery<T>(sql, new List<KeySorterValue>() { sortKey }, pageNumber, pageSize, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL分页查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKey">排序字段</param>
        /// <param name="pageNumber">当前页码（从 0 开始）</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataList<T> ExecutePagedQuery<T>(string sql, KeySorterValue sortKey, int pageNumber, int pageSize, IList<Parameter> parameters)
        {
            return ExecutePagedQuery<T>(sql, new List<KeySorterValue>() { sortKey }, pageNumber, pageSize, parameters);
        }


        /// <summary>
        /// 执行一条SQL分页查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKeys">排序字段列表</param>
        /// <param name="pageNumber">当前页码（从 0 开始）</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="parameter">参数</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataList<T> ExecutePagedQuery<T>(string sql, List<KeySorterValue> sortKeys, int pageNumber, int pageSize, Parameter parameter)
        {
            return ExecutePagedQuery<T>(sql, sortKeys, pageNumber, pageSize, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL分页查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKeys">排序字段列表</param>
        /// <param name="pageNumber">当前页码（从 0 开始）</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataList<T> ExecutePagedQuery<T>(string sql, List<KeySorterValue> sortKeys, int pageNumber, int pageSize, IList<Parameter> parameters)
        {
            using var connection = _dbContext.Database.GetDbConnection();
            connection.Open();

            int totalRowCount = 0;
            // 获取总行数
            using var countCommand = connection.CreateCommand();
            countCommand.CommandText = $"SELECT COUNT(*) FROM ({sql}) AS TotalCountQuery";  // 计算总行数
            foreach (Parameter parameter in parameters)
            {
                countCommand.Parameters.Add(new SqliteParameter("@" + parameter.Name, parameter.Value));
            }

            totalRowCount = GetInt.FromObject(countCommand.ExecuteScalar()); // 获取总行数

            int offset = pageNumber * pageSize;
            // 生成排序 SQL 语句
            string orderByClause = "ORDER BY ";
            if (sortKeys != null && sortKeys.Count > 0)
            {
                var orderList = new List<string>();

                foreach (var sort in sortKeys)
                {
                    orderList.Add($"{sort.Key} {sort.Value.ToString()}");
                }

                orderByClause += string.Join(", ", orderList);
            }
            else
            {
                orderByClause = ""; // 默认不排序
            }

            // 拼接最终 SQL
            string pagedSql = (pageSize == 0)
                 ? $"{sql} {orderByClause}"  // 不加 LIMIT / OFFSET
                 : $"{sql} {orderByClause} LIMIT {pageSize} OFFSET {offset}";

            using var command = connection.CreateCommand();
            command.CommandText = pagedSql;

            // 添加参数到命令
            foreach (Parameter parameter in parameters)
            {
                command.Parameters.Add(new SqliteParameter("@" + parameter.Name, parameter.Value));
            }

            using var reader = command.ExecuteReader();
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataSet.EnforceConstraints = false; // 关闭约束检查
            dataSet.Tables.Add(dataTable);
            dataTable.Load(reader);


            return new DataList<T>()
            {
                Data = GetList.FromDataTable<T>(dataTable),
                Count = totalRowCount,
            };
        }

        #endregion

        #region 排序查询
        /// <summary>
        /// 执行一条SQL排序查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKey">排序字段</param>
        /// <param name="parameter">参数</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecuteSortedQuery(string sql, KeySorterValue sortKey, Parameter parameter)
        {
            return ExecuteSortedQuery(sql, sortKey, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL排序查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKey">排序字段</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecuteSortedQuery(string sql, KeySorterValue sortKey, IList<Parameter> parameters)
        {
            return ExecuteSortedQuery(sql, new List<KeySorterValue>() { sortKey }, parameters);
        }

        /// <summary>
        /// 执行一条SQL排序查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKeys">排序字段列表</param>
        /// <param name="parameter">参数</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecuteSortedQuery(string sql, List<KeySorterValue> sortKeys, Parameter parameter)
        {
            return ExecuteSortedQuery(sql, sortKeys, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL排序查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKeys">排序字段列表</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecuteSortedQuery(string sql, List<KeySorterValue> sortKeys, IList<Parameter> parameters)
        {
            // 生成排序 SQL 语句
            string orderByClause = "ORDER BY ";
            if (sortKeys != null && sortKeys.Count > 0)
            {
                var orderList = new List<string>();

                foreach (var sort in sortKeys)
                {
                    orderList.Add($"{sort.Key} {sort.Value.ToString()}");
                }

                orderByClause += string.Join(", ", orderList);
            }
            else
            {
                orderByClause = ""; // 默认不排序
            }

            // 拼接最终 SQL
            string pagedSql = $"{sql} {orderByClause}";

            using var connection = _dbContext.Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = pagedSql;

            // 添加参数到命令
            foreach (Parameter parameter in parameters)
            {
                command.Parameters.Add(new SqliteParameter("@" + parameter.Name, parameter.Value));
            }

            using var reader = command.ExecuteReader();
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataSet.EnforceConstraints = false; // 关闭约束检查
            dataSet.Tables.Add(dataTable);
            dataTable.Load(reader);

            return dataTable;
        }

        /// <summary>
        /// 执行一条SQL排序查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKey">排序字段</param>
        /// <param name="parameter">参数</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataList<T> ExecuteSortedQuery<T>(string sql, KeySorterValue sortKey, Parameter parameter)
        {
            return ExecuteSortedQuery<T>(sql, sortKey, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL排序查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKey">排序字段</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataList<T> ExecuteSortedQuery<T>(string sql, KeySorterValue sortKey, IList<Parameter> parameters)
        {
            return ExecuteSortedQuery<T>(sql, new List<KeySorterValue>() { sortKey }, parameters);
        }

        /// <summary>
        /// 执行一条SQL排序查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKeys">排序字段列表</param>
        /// <param name="parameter">参数</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataList<T> ExecuteSortedQuery<T>(string sql, List<KeySorterValue> sortKeys, Parameter parameter)
        {
            return ExecuteSortedQuery<T>(sql, sortKeys, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL排序查询语句，并返回结果集作为DataTable。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="sortKeys">排序字段列表</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataList<T> ExecuteSortedQuery<T>(string sql, List<KeySorterValue> sortKeys, IList<Parameter> parameters)
        {
            // 生成排序 SQL 语句
            string orderByClause = "ORDER BY ";
            if (sortKeys != null && sortKeys.Count > 0)
            {
                var orderList = new List<string>();

                foreach (var sort in sortKeys)
                {
                    orderList.Add($"{sort.Key} {sort.Value.ToString()}");
                }

                orderByClause += string.Join(", ", orderList);
            }
            else
            {
                orderByClause = ""; // 默认不排序
            }

            // 拼接最终 SQL
            string pagedSql = $"{sql} {orderByClause}";

            using var connection = _dbContext.Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = pagedSql;

            // 添加参数到命令
            foreach (Parameter parameter in parameters)
            {
                command.Parameters.Add(new SqliteParameter("@" + parameter.Name, parameter.Value));
            }

            using var reader = command.ExecuteReader();
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataSet.EnforceConstraints = false; // 关闭约束检查
            dataSet.Tables.Add(dataTable);
            dataTable.Load(reader);

            return new DataList<T>()
            {
                Data = GetList.FromDataTable<T>(dataTable),
                Count = dataTable.Rows.Count,
            };
        }

        #endregion

        /// <summary>
        /// 执行一条SQL非查询语句（如INSERT、UPDATE、DELETE），返回受影响的行数。
        /// </summary>
        /// <param name="sql">SQL非查询语句。</param>
        /// <param name="parameter">SQL语句中的参数</param>
        /// <returns>受影响的行数。</returns>
        public int ExecuteSqlNonQuery(string sql, Parameter parameter)
        {
            return ExecuteSqlNonQuery(sql, new List<Parameter> { parameter });
        }

        /// <summary>
        /// 执行一条SQL非查询语句（如INSERT、UPDATE、DELETE），返回受影响的行数。
        /// </summary>
        /// <param name="sql">SQL非查询语句。</param>
        /// <param name="parameters">SQL语句中的参数列表。</param>
        /// <returns>受影响的行数。</returns>
        public int ExecuteSqlNonQuery(string sql, IList<Parameter> parameters)
        {
            // 获取数据库连接
            using var connection = _dbContext.Database.GetDbConnection();
            connection.Open();

            // 创建命令并设置SQL语句
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            // 添加参数到命令
            foreach (Parameter parameter in parameters)
            {
                command.Parameters.Add(new SqliteParameter("@" + parameter.Name, parameter.Value));
            }

            // 执行非查询操作
            return command.ExecuteNonQuery();
        }
    }
}
