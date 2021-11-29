using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    class Model_EmployeeWeekend
    {
        public string CompanyWeekEndCode { get; set; }
        public string EmployeeCode { get; set; }
        public string Weekend { get; set; }
        public string EffectiveDate { get; set; }
        public string LUser { get; set; }
        public string LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public string ModifyDate { get; set; }
        public List<WeekendList> WeekendLists { get; set; }
        public class WeekendList
        {
            public string CompanyWeekEndCode { get; set; }
        }
    }
}
