using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
  public  class Model_EmployeeBasicInfo
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentCode { get; set; }
        public string Designation { get; set; }
        public string DesignationCode { get; set; }
        public string PhotoUrl { get; set; }
        public string JoiningDate { get; set; }
        public string ReportingTo { get; set; }
        public string HeadOfDep { get; set; }
        public string NationalID { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string WorkStation { get; set; }
        public string EmployeeType { get; set; }
        public string EmployeeNature { get; set; }
        public string OfficePhone { get; set; }
        public string OfficeEmail { get; set; }
        public bool IsActive { get; set; }
        public string Session { get; set; }
    }
}
