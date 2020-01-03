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
                return all;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }
    }
}
