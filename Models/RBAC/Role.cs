using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Models.RBAC
{
    [Serializable]
    public class Role
    {
        public int Rid { get; set; }
        [DisplayName("角色名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Title { get; set; }
        public string Note { get; set; }
    }
}
