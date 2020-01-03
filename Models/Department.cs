using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    [Serializable]
    public class Department
    {
        public int DepId { get; set; }
        public string DepName { get; set; }
        public int UpperId { get;set; }
        public string DepType { get; set; }
        public string Line { get; set; }
        public byte Valid { get; set; }
    }
}
