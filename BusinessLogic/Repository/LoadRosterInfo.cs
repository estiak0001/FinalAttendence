using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class LoadRosterInfo
    {
        
        public string RosterScheduleId { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Date { get; set; }
        public string ShiftName { get; set; }
        public string Weekend { get; set; }
        public string Remark { get; set; }
        public string CompanyCode { get; set; }
        public string BranchCode { get; set; }
        public string Department { get; set; }
        public string DesignationCode { get; set; }

    }
}
