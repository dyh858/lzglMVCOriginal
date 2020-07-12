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
        public string InvitationCode { get; set; }
        public SysAdmin Admin { get; set; }
        public Post Position { get; set; }
        public int getSex()
        {
            if (Gender == "男")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public void setSex(int val)
        {
            if (val == 1)
            {
                Gender = "男";
            }
            else
            {
                Gender = "女";
            }
        }
    }
}
