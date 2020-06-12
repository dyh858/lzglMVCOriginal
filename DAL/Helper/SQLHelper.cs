using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    /// <summary>
    /// 通用数据访问类
    /// </summary>
    class SQLHelper
    {
        private static readonly string connString =
            ConfigurationManager.ConnectionStrings["connString"].ToString();
        /// <summary>
        /// 执行增删改方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        { 
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行一个结果集的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
        public static SqlDataAdapter GetAdapter(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                return adapter;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataset(string sql,params SqlParameter[] cmdParameters)
        {
            //利用using使用资源后，自动释放
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //打开连接，不必使用close关闭
                conn.Open();

                //设置cmd
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                                
                if(cmdParameters!=null)
                {
                    foreach (SqlParameter para in cmdParameters)
                    {
                        cmd.Parameters.Add(para);

                    }
                }
                //取数据
                using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }
    }
}
