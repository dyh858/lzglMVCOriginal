using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Models;
using Models.RBAC;

namespace DAL.RBAC
{
    public class ActionService
    {
        public bool insert(Actions vo)
        {
            string sql = "insert into action(title,url)values('{0}','{1}')";
            sql = string.Format(sql,vo.Title,vo.Url);
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
        public Actions FindById(int actid)
        {
            string sql = "select actid,title,url from action where actid={0}";
            sql = string.Format(sql, actid);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                Actions vo = null;
                while (reader.Read())
                {
                    vo = new Models.RBAC.Actions()
                    {
                        Actid = Convert.ToInt32(reader["actid"]),
                        Title = reader["title"].ToString(),
                        Url = reader["url"].ToString()
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
        public bool Modify(Actions vo)
        {
            string sql = "UPDATE action set title='{0}',url='{1}' WHERE actid={2}";
            sql = string.Format(sql, vo.Title, vo.Url, vo.Actid);
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
        public bool delete(int actid)
        {
            string sql = "DELETE FROM action WHERE actid={0}";
            sql = string.Format(sql, actid);
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
        /// 根据权限组ID获得权限列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public List<Actions> FindByGroupsId(int gid)
        {
            string sql = "select a.actid,title,url from action a left join groups_action " +
                    "g on a.actid=g.actid where gid={0}";
            sql = string.Format(sql, gid);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<Actions> all = new List<Actions>();
                while (reader.Read())
                {
                    all.Add(new Models.RBAC.Actions()
                    {
                        Actid=Convert.ToInt32(reader["actid"]),
                        Title=reader["title"].ToString(),
                        Url=reader["url"].ToString(),
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
        /// 获得所有的权限
        /// </summary>
        /// <returns></returns>
        public List<Actions> list()
        {
            string sql = "select actid,title,url from action";
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<Actions> all = new List<Actions>();
                while (reader.Read())
                {
                    all.Add(new Models.RBAC.Actions()
                    {
                        Actid = Convert.ToInt32(reader["actid"]),
                        Title = reader["title"].ToString(),
                        Url = reader["url"].ToString(),
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
    }
}
