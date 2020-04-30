using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.RBAC
{
    [Serializable]
    public class Actions
    {
        public int Actid { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
}
