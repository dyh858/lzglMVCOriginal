using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using Models.RBAC;


namespace DAL.RBAC
{
    public class RoleService
    {
        public bool insert(Role vo)
        {
            string sql = "insert into role(title,note)values('{0}','{1}')";
            sql = string.Format(sql, vo.Title, vo.Note);
            try
            {
                if (SQLHelper.Update(sql) >= 1)
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
        //
        public Role FindById(int rid)
        {
            string sql = "select rid,title,note from role where rid={0}";
            sql = string.Format(sql, rid);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                Role vo = null;
                while (reader.Read())
                {
                    vo = new Models.RBAC.Role()
                    {
                        Rid = Convert.ToInt32(reader["rid"]),
                        Title = reader["title"].ToString(),
                        Note = reader["note"].ToString()
                    };
                }
                reader.Close();
                return vo;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }
        /// <summary>
        /// 返回所有的角色
        /// </summary>
        /// <returns></returns>
        public List<Role> list()
        {
            string sql = "select rid,title,note from role";
            sql = string.Format(sql);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<Role> all = new List<Role>();
                while (reader.Read())
                {
                    all.Add(new Role()
                    {
                        Rid = Convert.ToInt32(reader["rid"]),
                        Title = reader["title"].ToString(),
                        Note = reader["note"].ToString(),
                    });
                }
                reader.Close();
                return all;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        //
        public bool Modify(Role vo)
        {
            string sql = string.Format(@"UPDATE Role set title=@title,note=@note WHERE rid=@rid");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@title",vo.Title),
                new SqlParameter("@note",vo.Note),
                new SqlParameter("@rid",vo.Rid),
            };

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.connString, CommandType.Text, sql, para);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public bool delete(int rid)
        {
            string sql = "DELETE FROM Role WHERE rid={0}";
            sql = string.Format(sql, rid);
            int result = SQLHelper.Update(sql);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 通过角色ID和权限组ID确定权限组是否已经分配给角色
        /// </summary>
        /// <param name="rid">角色ID</param>
        /// <param name="gid">权限组ID</param>
        /// <returns>已分配返回true，否则返回false</returns>
        public bool FindGroupsByRidGid(int rid, int gid)
        {
            string sql = "SELECT rid,gid FROM role_groups WHERE rid={0} and gid={1}";
            sql = string.Format(sql, rid, gid);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);

                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 给角色分配权限组
        /// </summary>
        /// <param name="rid">角色ID</param>
        /// <param name="gid">权限组ID</param>
        /// <returns>成功返回true</returns>
        public bool AllotGroups(int rid, int gid)
        {
            string sql = "INSERT INTO role_groups(rid,gid)VALUES({0},{1})";
            sql = string.Format(sql, rid, gid);
            try
            {
                if (SQLHelper.Update(sql) >= 1)
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
        /// <summary>
        /// 移除角色的权限组
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public bool RemoveGroups(int rid, int gid)
        {
            string sql = "DELETE role_groups WHERE rid={0} and gid={1}";
            sql = string.Format(sql, rid, gid);
            try
            {
                if (SQLHelper.Update(sql) >= 1)
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
    }
}
