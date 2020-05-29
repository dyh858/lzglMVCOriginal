using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    [Serializable]
    public class Employee
    {
        public string Empid { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public String Birthdate { get; set; }
        public string Idcard { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public string BankCard { get; set; }

        public int getSex()
        {
            if (Name == "男")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
