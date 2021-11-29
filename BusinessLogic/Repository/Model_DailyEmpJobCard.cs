using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
  public  class Model_DailyEmployeePresentList
    {

        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string AttendenceDate { get; set; }
        public string InTime { get; set; }
        public string Outtime { get; set; }

        public string LoginEmpoyeeID { get; set; }
        public string LateTime { get; set; }
        public string AbsentTime { get; set; }
        public string EmpStatus { get; set; }
        public string LateCount { get; set; }
       
    }
}
