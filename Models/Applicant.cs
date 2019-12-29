using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Models
{
    [Serializable]
    public class Applicant
    {
        public int AId{ get; set;}
        [DisplayName("姓名")]
        [Required(ErrorMessage ="{0}不能为空")]
        public string AName{ get; set;}
        public int Gender{ get; set;}
        [DisplayName("出生日期")]
        [Required(ErrorMessage = "{0}不能为空")]
        public DateTime Birthdate{ get; set;}
        [DisplayName("身份证号码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [RegularExpression("^[0-9]{17}[0-9,X]$",ErrorMessage="{0}位数必须是18位")]
        public string IdCard{ get; set;}
        [DisplayName("地址")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Address{ get; set;}
        [DisplayName("电话号码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Mobilephone{ get; set;}
        [DisplayName("民族")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Nationality{ get; set;}
        [DisplayName("学历")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Education{ get; set;}
        public string dispGender()
        { 
            if(Gender==1)
            {
                return "男";
            }
            else if (Gender == 0)
            {
                return "女";
            }
            else
            {
                return null;
            }
        }
    }
}
