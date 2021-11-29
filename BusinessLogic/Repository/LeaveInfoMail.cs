using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class LeaveInfoMail
    {
        public string EmployeeNAme { get; set; }
        public string TotalDayes { get; set; }
        public string LeaveType { get; set; }
        public string LeaveFormat { get; set; }
        public string Reason { get; set; }
        public string Messege { get; set; }
        public string  LinkID { get; set; }
        public string FormatString { get; set; }

        public string ShortLeaveFrom { get; set; }
        public string ShortLeaveTo { get; set; }
        public string TotalTime { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

    }
}
