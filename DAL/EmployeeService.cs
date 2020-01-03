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
    public class EmployeeService
    {
        public List<Employee> GetEmpByDept(string deptName)
        {
            string sql = "SELECT 职工号,姓名,性别,出生日期,身份证号码,"+
                "手机长号,住址 FROM 员工 WHERE ltrim(rtrim(部门名称))= '{0}' " +
                "OR ltrim(rtrim(班组))= '{0}'";
            
            sql=string.Format(sql,deptName.Trim());
            SqlDataReader objReader=SQLHelper.GetReader(sql);
            List<Employee> list = new List<Employee>();
            while(objReader.Read())
            {
                list.Add(new Employee()
                {
                    Empid=objReader["职工号"].ToString(),
                    Name=objReader["姓名"].ToString(),
                    Birthdate=Convert.ToDateTime(objReader["出生日期"]).ToString("yyyy-MM-dd"),
                    Idcard=objReader["身份证号码"].ToString(),
                    Gender = objReader["性别"].ToString(),
                    MobilePhone=objReader["手机长号"].ToString(),
                    Address = objReader["住址"].ToString()

                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据职工号查询职工
        /// </summary>
        /// <param name="empid">职工号</param>
        /// <returns></returns>
        public Employee getEmpById(string empid)
        {
            string sql = "SELECT 职工号,姓名,性别,出生日期,身份证号码,"+
                "手机长号,住址 FROM 员工 WHERE 职工号='{0}'";
            sql = string.Format(sql, empid);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Employee vo = null;

            if(objReader.Read())
            {
                vo = new Employee()  //类的实例初始化器
                { 
                    Empid=objReader["职工号"].ToString(),
                    Name = objReader["姓名"].ToString(),
                    Gender = objReader["性别"].ToString(),
                    Birthdate = Convert.ToDateTime(objReader["出生日期"]).ToString("yyyy-MM-dd"),
                    Idcard = objReader["身份证号码"].ToString(),
                    MobilePhone = objReader["手机长号"].ToString(),
                    Address = objReader["住址"].ToString(),
                 };
            }

            objReader.Close();
            return vo;
        }
        /// <summary>
        /// 修改雇员
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public int ModifyEmployee(Employee vo)
        {
            string sql = "update employees set name='{0}',sex={1},birthdate='{2}'," +
                "idcard='{3}',mobilephone={4},住址='{5}' where empid='{6}'";
            sql=string.Format(sql,vo.Name,vo.getSex(),vo.Birthdate,vo.Idcard,
                vo.MobilePhone,vo.Address,vo.Empid);
            try
            {
                return Convert.ToInt32(SQLHelper.Update(sql));
            }
            catch (SqlException ex)
            {

                throw new Exception("数据操作出现异常！具体信息：\r\n" + ex.Message);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
