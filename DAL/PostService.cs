using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Data.SqlClient;
using System.Data;
namespace DAL
{
    public class PostService
    {
        public bool insert(Post vo)
        {
            string sql = string.Format(@"INSERT INTO (postname,post_dep_id,post_team_id,property," +
             "accountid,accountgradeid)VALUES(@name,@dept,@team,@prop,@sequence,@grade)");
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@name",vo.PostName),
                new SqlParameter("@dept",vo.Dept.DepId),
                new SqlParameter("@team",vo.Team.DepId),
                new SqlParameter("@prop",vo.Property),
                new SqlParameter("@sequence",vo.Sequence.aid),
                new SqlParameter("@grade",vo.Grade.GradeId),
            };
            try
            {
                if (SqlHelper.ExecuteNonQuery(SqlHelper.connString, CommandType.Text, sql, paras) >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + ex.Message);
            }
        }

        public Post FindById(Int32 id)
        {
            string sql = string.Format(@"SELECT postname,post_dep_id,post_team_id,property," +
             "accountid,accountgradeid FROM post WHERE postid=@postid");
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@postid",id),
            };
            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connString, CommandType.Text, sql, paras);
                Post vo = null;
                if (reader.Read())
                {
                    vo = new Post
                    {
                        PostName=reader["postname"].ToString(),
                        Dept=new DepartmentService().FindById(Convert.ToInt32(reader["post_dep_id"])),
                        Team = new DepartmentService().FindById(Convert.ToInt32(reader["post_team_id"])),

                    };           
                }
                return vo;
            }
            catch (SqlException ex)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + ex.Message);
            }
        }
    }
}
