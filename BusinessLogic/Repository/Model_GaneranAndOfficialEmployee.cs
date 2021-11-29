using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogic.Repository
{
    public class Model_GaneranAndOfficialEmployee
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
        [Required(ErrorMessage = "Enter Date of Birth")]
        public string DateOfBirthOrginal { get; set; }
        public string BirthCertificateNo { get; set; }
        [Required(ErrorMessage = "Select Gender")]
        public string SexCode { get; set; }
        [Required(ErrorMessage = "Select Blood Group")]
        public string BloodGroupCode { get; set; }
        //[Required(ErrorMessage = "Select National ID")]
        public string NationalityCode { get; set; }
        [Required(ErrorMessage = "Enter National ID")]
        public string NationalIDNO { get; set; }
        [Required(ErrorMessage = "Select Relgion")]
        public string ReligionCode { get; set; }
        public string PlaceOfBirth { get; set; }
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

        //Contact Info
        //public string ParmanentAddress { get; set; }
        //public string ParmanentPostOffice { get; set; }
        //public string ParmanentThana { get; set; }
        //public string ParmanentPostCode { get; set; }
        //public string ParmanentDistrict { get; set; }
        //public string ParmanentPhone { get; set; }
        //public string PresentAddress { get; set; }
        //public string PresentPostOffice { get; set; }
        //public string PresentThana { get; set; }
        //public string PresentPostCode { get; set; }
        //public string PresentDistrict { get; set; }
        //public string PresentMobile { get; set; }
        //public string PresentPhone { get; set; }
        //public string PresentFax { get; set; }
        //public string PresentEmail { get; set; }
        //public string EmContactName1 { get; set; }
        //public string EmContactRelation1 { get; set; }
        //public string EmContactAddress1 { get; set; }
        //public string EmContactPhone1 { get; set; }
        //public string EmContactMobile1 { get; set; }
        //public string EmContactFax1 { get; set; }
        //public string EmContactEmail { get; set; }
        //public string EmContactName2 { get; set; }
        //public string EmContactRelation2 { get; set; }
        //public string EmContactAddress2 { get; set; }
        //public string EmContactPhone2 { get; set; }
        //public string EmContactMobile2 { get; set; }
        //public string EmContactFax2 { get; set; }
        //public string EmContactEmai2 { get; set; }

        // Oficial info

        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Select Company")]
        public string CompanyCode { get; set; }
        [Required(ErrorMessage = "Enter Branch")]
        public string BranchCode { get; set; }

        //[Required(ErrorMessage = "Enter Divition")]
        public string DivisionCode { get; set; }
       [Required(ErrorMessage = "Select Department")]
        public string DepartmentCode { get; set; }
        [Required(ErrorMessage = "Select Designation")]

        public string DesignationCode { get; set; }
        [Required(ErrorMessage = "Enter Employee Type")]
        public string EmpTypeCode { get; set; }
        [Required(ErrorMessage = "Enter Grade Code")]
        public string GradeCode { get; set; }
        [Required(ErrorMessage = "Select Employee Nature")]
        public string EmploymentNatureId { get; set; }
        [Required(ErrorMessage = "Enter Gross Salary")]
        public decimal? GrossSalary { get; set; }
        [Required(ErrorMessage = "Enter Currency Code")]
        public string CurrencyCode { get; set; }
        [Required(ErrorMessage = "Enter Payment Period")]
        public string PaymentPeriodID { get; set; }

        public string DisbursementMethodId { get; set; }
        [Required(ErrorMessage = "Select Shift")]
        public string ShiftCode { get; set; }
        [Required(ErrorMessage = "Select Status")]
        public string EmployeeStatus { get; set; }
        [Required(ErrorMessage = "Enter Reporting To")]
        public string ReportingTo { get; set; }
        [Required(ErrorMessage = "Enter HOD")]
        public string HOD { get; set; }
        [Required(ErrorMessage = "Enter Mobile")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Enter HOD")]
        public string Email { get; set; }

        public string AppointmentLetterNo { get; set; }

        public string AppointmentLetterDate { get; set; }
        [Required(ErrorMessage = "Select Joining Date")]

        public string JoiningDate { get; set; }
        public string JoiningSalary { get; set; }
        [Required(ErrorMessage = "Enter Probation Type")]
        public string ProbationPeriodType { get; set; }
        [Required(ErrorMessage = "Enter Probation Period")]
        public string ProbationPeriod { get; set; }

        public string ConfirmeDate { get; set; }
        public string StepNoId { get; set; }


        public string SectionCode { get; set; }

        public string LineCode { get; set; }
        [Required(ErrorMessage = "Enter Probation Period")]
        public string AttendenceID { get; set; }

        public string EmployeeFullName { get; set; }

        public string DepartmentName { get; set; }

        public string designationNAme { get; set; }

        public string RelationshipCode { get; set; }
        public string Relationship { get; set; }

        public string refName { get; set; }
        public string REOrganization { get; set; }
        public string REaddress { get; set; }
        public string Relation { get; set; }
        public string RelationID { get; set; }
        public string ReMobile { get; set; }
        public string REemail { get; set; }
        public string EmpReferenceID { get; set; }

        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ParmanentAddressBangla { get; set; }
        public string PresentAddressBangla { get; set; }

        public Model_HRM_Employee general { get; set; }
        public Model_HRM_EmployeeContactInfo contact { get; set; }
        public List<Model_HRM_EmployeeReferenceInfo> Model_HRM_EmployeeReferenceInfos { get; set; }
    }
}
