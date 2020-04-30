using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Utils;
namespace DAL
{
    public class WageService
    {

        public String GetTable(string empid,string StartDate,string EndDate)
        {
            string sql = "select 月份,职工号,姓名,发放项目名称,应发合计 金额 from vw_WageWhole " +
                        "where 职工号='{0}' and 月份>='{1}' and 月份<='{2}'";
            sql = string.Format(sql, empid, StartDate, EndDate);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = SQLHelper.GetAdatper(sql);
            adapter.TableMappings.Add("Table", "Wage2013");
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            dt = dataSet.Tables["Wage2013"];
            return new DataTableToJson().ToJson(dt);
        }
    }

}
