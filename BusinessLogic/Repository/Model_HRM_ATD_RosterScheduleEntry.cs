using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Model_HRM_ATD_RosterScheduleEntry
    {
        public decimal TC { get; set; }
        public string RosterScheduleId { get; set; }
        public List<RosterEmpID> RsemployeeID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ShiftCode { get; set; }
        public string Weekend { get; set; }
        public string Remark { get; set; }
        public string LUser { get; set; }
        public string LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public string ModifyDate { get; set; }
        public string CompanyCode { get; set; }
        public string EmployeeID_SAO { get; set; }
    }
    public class RosterEmpID
    {
        public string EmployeeID { get; set; }
    }
}
