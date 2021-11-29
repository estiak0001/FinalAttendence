using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public  class Model_HRM_RosterScheduleEntry2
    {
        public decimal TC { get; set; }
        public string RosterScheduleId { get; set; }
        public string EmployeeID { get; set; }
        public System.DateTime Date { get; set; }
        public string ShiftCode { get; set; }
        public string Weekend { get; set; }
        public string Remark { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string CompanyCode { get; set; }
        public string EmployeeID_SAO { get; set; }       
    }
}
