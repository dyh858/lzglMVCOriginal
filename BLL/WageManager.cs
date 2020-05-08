using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data ;

namespace BLL
{
    public class WageManager
    {
        public string GetTable(string empid, string StartDate, string EndDate)
        {
            return new WageService().GetTable(empid,StartDate,EndDate);
        }
        public List<string> GetYearMonthList()
        {
            return new WageService().GetYearMonthList();
        }
    }
}
