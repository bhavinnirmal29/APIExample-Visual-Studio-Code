using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIExample.Models
{
    public class EmpProjectModel
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string Dept { get; set; }
        public string Desg { get; set; }
        public double Salary { get; set; }
        public int projid { get; set; }
        public string password { get; set; }
    }
}