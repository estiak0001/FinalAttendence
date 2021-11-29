using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogic.Repository
{
   public class Model_HRM_Employee
    {
        [Required(ErrorMessage = "Enter Employee ID")]
        public string EmployeeID { get; set; }
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Father Name")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Enter Mother Name")]
        public string MotherName { get; set; }
        public string FirstNameBangla { get; set; }
        public string LastNameBangla { get; set; }
        [Required(ErrorMessage = "Select Date of Birth")]
        public string DateOfBirthOrginal { get; set; }
        public string BirthCertificateNo { get; set; }
        [Required(ErrorMessage = "Select Gender")]
        public string SexCode { get; set; }
        [Required(ErrorMessage = "Select Blood Group")]
        public string BloodGroupCode { get; set; }
        [Required(ErrorMessage = "Select National ID")]
        public string NationalityCode { get; set; }
        [Required(ErrorMessage = "Select Nationality")]
        public string NationalIDNO { get; set; }
        [Required(ErrorMessage = "Select Relgion")]
        public string ReligionCode { get; set; }

        [Required(ErrorMessage = "Enter Marital Status")]
        public string MaritalStatusCode { get; set; }
        [Required(ErrorMessage = "Select Email")]
        public string PersonalEmail { get; set; }
        [Required(ErrorMessage = "Select Phone")]
        public string Telephone { get; set; }
        public string UserInfoEmployeeID { get; set; }
        public string PhotoUrl { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public HttpPostedFileBase Photo2 { get; set; }
        public string SignatureImageUrl { get; set; }
        public string PlaceOfBirth { get; set; }
        public string EmployeeFullName { get; set; }

    }
}
