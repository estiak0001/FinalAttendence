using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Model_HRM_ATD_AttendanceType
    {
       
         public string AttendanceTypeCode { get; set; }
        public string AttendanceTypeName { get; set; }
        public string ShortName { get; set; }
        public List<AllIDList> AllID { get; set; }
        public class AllIDList
        {
            public string AttendanceTypeCode { get; set; }
        }

    }
}
