using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
  public  class Model_HRM_ATD_LeaveType
    {
       
        public string LeaveTypeId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string RulePolicy { get; set; }
        public decimal? NoOfDay { get; set; }
        public decimal? For { get; set; }
        public string YMWD { get; set; }
        public string WEF { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }

        public List<AllIDList> AllID { get; set; }
        public class AllIDList
        {
            public string LeaveTypeId { get; set; }
        }
    }
}
