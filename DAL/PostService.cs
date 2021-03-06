﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Models;
using Utils;

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
                    vo = new Post();
                    
                    vo.PostName=reader["postname"].ToString();
                    vo.Dept=new DepartmentService().FindById(Convert.ToInt32(reader["post_dep_id"]));
                    vo.Team = new DepartmentService().FindById(Convert.ToInt32(reader["post_team_id"]));
                
                }
                return vo;
            }
            catch (SqlException ex)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + ex.Message);
            }
        }
        public List<Post> FindAll()
        {
            string sql = string.Format(@"SELECT postname,post_dep_id,post_team_id,property," +
             "accountid,accountgradeid FROM post");

            try 
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<Post> list = new List<Post>();
                while(reader.Read())
                {
                    list.Add(new Post()
                    {
                        PostName=reader["postname"].ToString(),
                        Dept=new DepartmentService().FindById(Convert.ToInt32(reader["post_dep_id"])),
                        Team = new DepartmentService().FindById(Convert.ToInt32(reader["post_team_id"])),
                    });
                }
                reader.Close();
                reader.Dispose();
                return list;
            }
            catch (SqlException ex)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }
        public String getTable()
        {
            string sql = string.Format(@"SELECT p.PostId,p.postname,isnull(d.DepName,'') depname,isnull(dt.DepName,'') team, " +
                    "ISNULL(pp.PP_Name,'') prop,ISNULL(ps.accountname,'') sequence,ISNULL(pg.GradeName,'') grade FROM post p " +
                    "left join Department d on p.Post_Dep_Id=d.DepId " +
                    "left join Department dt on p.Post_Team_Id =dt.DepId " + 
                    "left join PostProp pp on p.Property=pp.Pp_id " +
                    "left join post_account_sequence ps on p.AccountId=ps.aid " +
                    "left join Post_Account_Grade pg on p.AccountGradeId=pg.GradeId");

            try
            {
                SqlDataAdapter adapter = SQLHelper.GetAdapter(sql);
                adapter.TableMappings.Add("Table", "post");
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                DataTable dt = dataset.Tables["post"];
                
                return new DataTableToJson().ToJson(dt);

            }
            catch (SqlException ex)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
