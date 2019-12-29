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
    public class SysAdmin
    {

        [DisplayName("账号")]
        [Required(ErrorMessage="{0}不得为空")]
        public string LoginId { get; set; }
        //[DisplayName("用户名")]
        //[Required(ErrorMessage = "{0}不得为空")]
        public string AdminName { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "{0}不得为空")]
        public string LoginPwd { get; set; }
    }
}
