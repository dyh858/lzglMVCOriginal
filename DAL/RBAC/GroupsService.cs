using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Models;
using Models.RBAC;
namespace DAL.RBAC
{
    public class GroupsService
    {
        public bool insert(Groups vo)
        {
            string sql = "insert into groups(title,upperid,note)values('{0}','{1}','{2}')";
            sql = string.Format(sql, vo.Title, vo.Upperid,vo.Note);
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
        /// 根据权限组ID查找权限组
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public Groups FindById(int gid)
        {
            string sql = "select gid,title,note from Groups where gid={0}";
            sql = string.Format(sql, gid);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                Groups vo = null;
                while (reader.Read())
                {
                    vo = new Models.RBAC.Groups()
                    {
                        Gid = Convert.ToInt32(reader["gid"]),
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
        /// 修改
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public bool Modify(Groups vo)
        {
            string sql = "UPDATE Groups set title='{0}',note='{1}' WHERE gid={2}";
            sql = string.Format(sql, vo.Title, vo.Note, vo.Gid);
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
        /// 删除一条记录
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public bool delete(int gid)
        {
            string sql = "DELETE FROM Groups WHERE gid={0}";
            sql = string.Format(sql, gid);
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
        /// 根据角色ID返回所有权限组
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>权限组集合</returns>
        public List<Groups> FindByRoleId(int id)
        {
            string sql = "select g.gid,g.title,upperid,note from groups g left join role_groups r on  g.gid=r.gid where rid={0}";
            sql = string.Format(sql, id);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<Groups> all = new List<Groups>();
                while (reader.Read())
                {
                    all.Add(new Groups()
                    {
                        Gid=Convert.ToInt32(reader["gid"]),
                        Title=reader["title"].ToString(),
                        Upperid=Convert.ToInt32(reader["upperid"]),
                        Note=reader["note"].ToString(),
                        AllActions = new ActionService().FindByGroupsId(Convert.ToInt32(reader["gid"]))
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

        /// <summary>
        ///返回所有的权限组 
        /// </summary>
        /// <returns></returns>
        public List<Groups> list()
        {
            string sql = "select gid,title,note from groups";
            sql = string.Format(sql);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<Groups> all = new List<Groups>();
                while (reader.Read())
                {
                    all.Add(new Groups()
                    {
                        Gid = Convert.ToInt32(reader["gid"]),
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
        /// <summary>
        /// 通过权限组ID、权限ID确定权限是否已经分配给权限组
        /// </summary>
        /// <param name="gid">权限组ID</param>
        /// <param name="actid">权限ID</param>
        /// <returns>已分配返回true，否则返回false</returns>
        public bool FindActionsByGidActid(int gid, int actid)
        {
            string sql = "SELECT gid,actid FROM groups_action WHERE gid={0} and actid={1}";
            sql = string.Format(sql, gid, actid);
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
        /// 给权限组分配权限
        /// </summary>
        /// <param name="gid">权限组ID</param>
        /// <param name="actid">权限ID</param>
        /// <returns>成功返回true</returns>
        public bool AllotActions(int gid, int actid)
        {
            string sql = "INSERT INTO groups_action(gid,actid)VALUES({0},{1})";
            sql = string.Format(sql, gid, actid);
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
        /// 移除权限组的权限
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="actid"></param>
        /// <returns></returns>
        public bool RemoveActions(int gid, int actid)
        {
            string sql = "DELETE groups_action WHERE gid={0} and actid={1}";
            sql = string.Format(sql, gid, actid);
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
