using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
  public  class Model_DailyEmpJobCards
    {

        public string DateData { get; set; }
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string ShiftName { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }

        public string LateTime { get; set; }
        public string AbsentTime { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string DaysName { get; set; }
        public string EmoStatus { get; set; }
        public string WorkingHour { get; set; }
        public string LateCount { get; set; }

    }
}
