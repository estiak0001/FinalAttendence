using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Model_HRM_ATD_Shift
    {
   
         public string ShiftCode { get; set; }
        public string ShiftName { get; set; }
        public string ShiftShortName { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
        public string LateTime { get; set; }
        public string AbsentTime { get; set; }

        public string WEF { get; set; }
        public string Remarks { get; set;}
        public string ShiftTypeID { get; set; }

        public List<AllIDList> AllID { get; set; }
        public class AllIDList
        {
            public string ShiftCode { get; set; }
        }
    }
}
