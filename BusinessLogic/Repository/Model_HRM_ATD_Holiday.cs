using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Model_HRM_ATD_Holiday
    {

         public string HolidayCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string NoOfDays { get; set; }
        public string HolidayName { get; set; }
        public string HolidayType { get; set; }
        public List<AllIDList> AllID { get; set; }
        public class AllIDList
        {
            public string HolidayCode { get; set; }
        }
    }
}
