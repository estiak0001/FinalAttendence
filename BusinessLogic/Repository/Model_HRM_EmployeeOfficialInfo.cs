using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogic.Repository
{
    public class Model_HRM_EmployeeOfficialInfo
    {

        [Required(ErrorMessage = "Enter Employee ID")]
        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Select Company")]
        public string CompanyCode { get; set; }

        public string BranchCode { get; set; }


        public string DivisionCode { get; set; }
        [Required(ErrorMessage = "Select Department")]
        public string DepartmentCode { get; set; }
        [Required(ErrorMessage = "Select Designation")]

        public string DesignationCode { get; set; }
        

        public string EmpTypeCode { get; set; }

        public string GradeCode { get; set; }
        [Required(ErrorMessage = "Select Employee Nature")]

        public string EmploymentNatureId { get; set; }

        public string GrossSalary { get; set; }

        public string CurrencyCode { get; set; }

        public string PaymentPeriodID { get; set; }

        public string DisbursementMethodId { get; set; }
        [Required(ErrorMessage = "Select Shift")]
        public string ShiftCode { get; set; }
        [Required(ErrorMessage = "Select Status")]

        public string EmployeeStatus { get; set; }

        public string ReportingTo { get; set; }

        public string HOD { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }

        public string AppointmentLetterNo { get; set; }

        public string AppointmentLetterDate { get; set; }
        [Required(ErrorMessage = "Select Joining Date")]

        public string JoiningDate { get; set; }
        public string JoiningSalary { get; set; }

        public string ProbationPeriodType { get; set; }

        public string ProbationPeriod { get; set; }

        public string ConfirmeDate { get; set; }
        public string StepNoId { get; set; }


        public string SectionCode { get; set; }

        public string LineCode { get; set; }

        public string AttendenceID { get; set; }


    }
}
