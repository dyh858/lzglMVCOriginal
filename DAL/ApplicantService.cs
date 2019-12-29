using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models;
using DAL;

namespace DAL
{
    public class ApplicantService
    {
        public bool insert(Applicant SysApplicant)
        { 
            string sql = "INSERT INTO applicant(a_name,gender,birthdate,id_card," +
                " address,mobilephone,nationality,education)VALUES('{0}',{1},"+
                " '{2}','{3}','{4}','{5}','{6}','{7}')";
            sql=string.Format(sql,SysApplicant.AName,SysApplicant.Gender,SysApplicant.Birthdate,
                SysApplicant.IdCard,SysApplicant.Address,SysApplicant.Mobilephone,SysApplicant.Nationality,
                SysApplicant.Education);
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
        /// 返回所有行
        /// </summary>
        /// <returns></returns>
        public List<Applicant> list()
        {
            string sql = "select a_id,a_name,gender,birthdate,id_card," +
                " address,mobilephone,nationality,education from applicant ";
            sql = string.Format(sql);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<Applicant> all = new List<Applicant>();
                while (reader.Read())
                {
                    all.Add(new Applicant() 
                    {
                        AId=Convert.ToInt32(reader["a_id"]),
                        AName = reader["a_name"].ToString(),
                        Gender = Convert.ToInt32(reader["gender"]),
                        Birthdate = Convert.ToDateTime(reader["birthdate"].ToString()),
                        IdCard = reader["id_card"].ToString(),
                        Address = reader["address"].ToString(),
                        Mobilephone = reader["mobilephone"].ToString(),
                        Nationality = reader["nationality"].ToString(),
                        Education = reader["education"].ToString(),
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
        /// 根据记录号查找应聘人员信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public Applicant findById(int aid)
        {
            string sql = "SELECT a_id,a_name,gender,birthdate,id_card," +
                " address,mobilephone,nationality,education FROM applicant " +
                " WHERE a_id={0}";
            sql = string.Format(sql,aid);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                Applicant vo = null;
                if (reader.Read())
                {
                    vo = new Applicant()
                    {
                        AId = Convert.ToInt32(reader["a_id"]),
                        AName = reader["a_name"].ToString(),
                        Gender = Convert.ToInt32(reader["gender"]),
                        Birthdate = Convert.ToDateTime(reader["birthdate"].ToString()),
                        IdCard = reader["id_card"].ToString(),
                        Address = reader["address"].ToString(),
                        Mobilephone = reader["mobilephone"].ToString(),
                        Nationality = reader["nationality"].ToString(),
                        Education = reader["education"].ToString(),
                    };
                }
                return vo;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        public bool Modify(Applicant vo)
        {
            string sql = "UPDATE applicant set a_name='{0}',gender={1},birthdate='{2}'," +
            " id_card='{3}',address='{4}',mobilephone='{5}',nationality='{6}',education='{7}'" +
            " WHERE a_id={8}";
            sql = string.Format(sql,vo.AName,vo.Gender,vo.Birthdate,vo.IdCard,vo.Address,vo.Mobilephone,
                vo.Nationality,vo.Education,vo.AId);
            int result  = SQLHelper.Update(sql);
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
        public bool delete(int aid)
        {
            string sql = "DELETE FROM applicant WHERE a_id={0}";
            sql = string.Format(sql, aid);
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
    }
}
