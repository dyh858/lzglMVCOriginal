using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models;
namespace DAL
{
    public class DepartmentService
    {
        public List<Department> list()
        {
            string sql = "SELECT depid,depname,upperid,deptype,line FROM department WHERE valid='1' ORDER BY upperid,depid";
            sql = string.Format(sql);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<Department> all = new List<Department>();
                while (reader.Read())
                {
                    all.Add(new Department()
                    {
                        DepId = Convert.ToInt32(reader["depid"]),
                        DepName = reader["depname"].ToString(),
                        UpperId = Convert.ToInt32(reader["upperid"]),
                        DepType =reader["deptype"].ToString(),
                        Line = reader["line"].ToString()
                    });
                }
                reader.Close();
                reader.Dispose();
                return all;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }

        public Department FindById(Int32 id)
        {
            string sql = string.Format(@"SELECT depname,upperid,deptype,line FROM department WHERE depid=@depid");
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@depid",id),
            };
            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connString, CommandType.Text, sql, paras);
                Department vo = null;
                if (reader.Read())
                {
                    vo = new Department();
                    vo.DepName = reader["depname"].ToString();
                    vo.Upper = new DepartmentService().FindById(Convert.ToInt32(reader["upperid"]));
                    vo.DepType=reader["deptype"].ToString();
                    vo.Line = reader["line"].ToString();

                }
                reader.Close();
                reader.Dispose();
                return vo;
            }
            catch (SqlException ex)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + ex.Message);
            }
        }
    }
}
