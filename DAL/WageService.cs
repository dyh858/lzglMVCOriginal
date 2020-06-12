using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Models;
using Utils;
namespace DAL
{
    public class WageService
    {

        public String GetTable(string empid,string StartDate,string EndDate)
        {
            string sql = "select 月份,职工号,姓名,发放项目名称,应发合计 金额 from vw_WageWhole " +
                        "where 职工号='{0}' and 月份>='{1}' and 月份<='{2}' order by 月份";
            sql = string.Format(sql, empid, StartDate, EndDate);
            //DataTable dt = new DataTable();
            SqlDataAdapter adapter = SQLHelper.GetAdapter(sql);
            adapter.TableMappings.Add("Table", "Wage2013");
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dt = dataSet.Tables["Wage2013"]; 
            return new DataTableToJson().ToJson(dt);
        }
        public List<string> GetYearMonthList() 
        {
            string sql = "select 月份 from vw_wagewhole group by 月份 order by 月份";
            sql = string.Format(sql);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<string> all = new List<string>();
                while (reader.Read())
                {
                    all.Add(reader["月份"].ToString());
                }
                reader.Close();
                return all;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
            }      
        }
        public List<Int32> GetPayYearList()
        {
            string sql = "select payyear from vw_wagewhole group by payyear order by payyear";
            sql = string.Format(sql);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                List<Int32> all = new List<Int32>();
                while (reader.Read())
                {
                    all.Add(Convert.ToInt32(reader["payyear"]));
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
