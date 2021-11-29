
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crd_HRM_Employee
    {
        //public List<Model_HRM_Employee> GetAllEmployee()
        //{
        //    using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
        //    {
        //        var result = (from hrm in context.HRM_Employee
        //                      select new Model_HRM_Employee()
        //                      {
        //                          EmployeeID = hrm.EmployeeID,
        //                          FirstName = hrm.FirstName + hrm.LastName,
        //                          FatherName = hrm.FatherName,
        //                          MotherName = hrm.MotherName,
        //                          Telephone = hrm.Telephone,
        //                          PersonalEmail = hrm.PersonalEmail,
        //                          PhotoUrl = hrm.PhotoUrl
        //                      }).ToList();
        //        return result;
        //    }

        //}

        public List<Model_GaneranAndOfficialEmployee> GetAllEmployee(string search, string company, string branch, string department, string designation)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from offic in context.HRM_EmployeeOfficialInfo.Where(p=> (company == "" || p.CompanyCode == company) && (branch == "" || p.BranchCode == branch) && (department == "" || p.DepartmentCode == department) && (designation == "" || p.DesignationCode == designation))
                              from gen in context.HRM_Employee.Where(gen => gen.EmployeeID == offic.EmployeeID)
           .DefaultIfEmpty()
                              from depart in context.HRM_Def_Department.Where(depart => depart.DepartmentCode == offic.DepartmentCode)
           .DefaultIfEmpty()
                              from Desig in context.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == offic.DesignationCode)
         .DefaultIfEmpty()
                              from shi in context.HRM_ATD_Shift.Where(shi => shi.ShiftCode == offic.ShiftCode)
             .DefaultIfEmpty()
                              select new Model_GaneranAndOfficialEmployee()
                              {
                                  EmployeeID = offic.EmployeeID,
                                  EmployeeName = gen.FirstName + " " + gen.LastName,
                                  DepartmentCode = depart.DepartmentName,
                                  DesignationCode = Desig.DesignationName,
                                  ShiftCode = shi.ShiftName,
                                  PhotoUrl = gen.PhotoUrl
                              }).Where(x => x.EmployeeName.Contains(search) || search == null).ToList();
                return result;
            }
        }

        public Model_GaneranAndOfficialEmployee GetEmployee(string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from offic in context.HRM_EmployeeOfficialInfo.Where(x => x.EmployeeID == EmployeeID).AsEnumerable()
                              join emm in context.HRM_Employee
                                on new { X1 = offic.EmployeeID } equals new { X1 = emm.EmployeeID }
                                into rmp
                              from gen in rmp.DefaultIfEmpty()
                              join dess in context.HRM_Def_Designation
                                on new { X1 = offic.DesignationCode } equals new { X1 = dess.DesignationCode }
                                into rmpdes
                              from des in rmpdes.DefaultIfEmpty()
                              join depp in context.HRM_Def_Department
                                on new { X1 = offic.DepartmentCode } equals new { X1 = depp.DepartmentCode }
                                into rmpdep
                              from dep in rmpdep.DefaultIfEmpty()

                              join gg in context.HRM_Def_Grade
                                on new { X1 = offic.GradeCode } equals new { X1 = gg.GradeCode }
                                into rmpgg
                              from gr in rmpgg.DefaultIfEmpty()

                              select new
                              {
                                  EmployeeID = offic.EmployeeID,
                                  FirstName = gen.FirstName,
                                  LastName = gen.LastName,
                                  FatherName = gen.FatherName,
                                  MotherName = gen.MotherName,
                                  //FirstNameBangla = gen.FirstNameBangla,
                                  //LastNameBangla = gen.LastNameBangla,
                                  DateOfBirthOrginal = ((DateTime)gen.DateOfBirthOrginal).ToString("dd/MM/yyyy"),
                                  BirthCertificateNo = gen.BirthCertificateNo,
                                  SexCode = gen.SexCode,
                                  BloodGroupCode = gen.BloodGroupCode,
                                  NationalityCode = gen.NationalityCode,
                                  NationalIDNO = gen.NationalIDNO,
                                  ReligionCode = gen.ReligionCode,
                                  MaritalStatusCode = gen.MaritalStatusCode,
                                  Telephone = gen.Telephone,
                                  PersonalEmail = gen.PersonalEmail,
                                  PhotoUrl = gen.PhotoUrl,
                                  SignatureImageUrl = gen.SignatureImageUrl,
                                  CompanyCode = offic.CompanyCode,
                                  BranchCode = offic.BranchCode,
                                  DepartmentCode = offic.DepartmentCode,
                                  DesignationCode = offic.DesignationCode,
                                  ShiftCode = offic.ShiftCode,
                                  EmpTypeCode = offic.EmpTypeCode,
                                  EmploymentNatureId = offic.EmploymentNatureId,
                                  GrossSalary = offic.GrossSalary,
                                  ReportingTo = offic.ReportingTo,
                                  HOD = offic.HOD,
                                  JoiningDate = ((DateTime)offic.JoiningDate).ToString("dd/MM/yyyy"),
                                  EmployeeStatus = offic.EmployeeStatus,
                                  MobileNo = offic.MobileNo,
                                  Email = offic.Email,
                                  EmployeeFullName = gen.FirstName + " " + gen.LastName,
                                  designationNAme = des.DesignationName,
                                  DepartmentName = dep.DepartmentName,
                                  PlaceOfBirth = gen.PlaceOfBirth,
                                  gradeName = gr.GradeName,
                                  gradeCode = offic.GradeCode,
                                  curr = offic.CurrencyCode,
                                  paymentPeriod = offic.PaymentPeriodID,
                                  prbationPeriod = offic.ProbationPeriod,
                                  proabType = offic.ProbationPeriodType
                              }).AsEnumerable().Select(a => new Model_GaneranAndOfficialEmployee()
                              {
                                  EmployeeID = a.EmployeeID,
                                  FirstName = a.FirstName,
                                  LastName = a.LastName,
                                  FatherName = a.FatherName,
                                  MotherName = a.MotherName,
                                  //FirstNameBangla = gen.FirstNameBangla,
                                  //LastNameBangla = gen.LastNameBangla,
                                  DateOfBirthOrginal = a.DateOfBirthOrginal,
                                  BirthCertificateNo = a.BirthCertificateNo,
                                  SexCode = a.SexCode,
                                  BloodGroupCode = a.BloodGroupCode,
                                  NationalityCode = a.NationalityCode,
                                  NationalIDNO = a.NationalIDNO,
                                  ReligionCode = a.ReligionCode,
                                  MaritalStatusCode = a.MaritalStatusCode,
                                  Telephone = a.Telephone,
                                  PersonalEmail = a.PersonalEmail,
                                  PhotoUrl = a.PhotoUrl,
                                  SignatureImageUrl = a.SignatureImageUrl,
                                  CompanyCode = a.CompanyCode,
                                  BranchCode = a.BranchCode,
                                  DepartmentCode = a.DepartmentCode,
                                  DesignationCode = a.DesignationCode,
                                  ShiftCode = a.ShiftCode,
                                  EmpTypeCode = a.EmpTypeCode,
                                  EmploymentNatureId = a.EmploymentNatureId,
                                  GrossSalary = a.GrossSalary,
                                  ReportingTo = a.ReportingTo,
                                  HOD = a.HOD,
                                  JoiningDate = a.JoiningDate,
                                  EmployeeStatus = a.EmployeeStatus,
                                  MobileNo = a.MobileNo,
                                  Email = a.Email,
                                  EmployeeFullName = a.FirstName + " " + a.LastName,
                                  designationNAme = a.designationNAme,
                                  DepartmentName = a.DepartmentName,
                                  PlaceOfBirth = a.PlaceOfBirth,
                                  GradeCode = a.gradeCode,
                                  CurrencyCode = a.curr,
                                  PaymentPeriodID = a.paymentPeriod,
                                  ProbationPeriod = a.prbationPeriod,
                                  ProbationPeriodType = a.proabType
                              }).FirstOrDefault();
                return result;
            }

        }

        public Model_HRM_Employee GetEmployeeGeneral(string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from gen in context.HRM_Employee.Where(x => x.EmployeeID == EmployeeID).AsEnumerable()
                              
                              select new
                              {
                                  EmployeeID = gen.EmployeeID,
                                  FirstName = gen.FirstName,
                                  LastName = gen.LastName,
                                  FatherName = gen.FatherName,
                                  MotherName = gen.MotherName,
                                  //FirstNameBangla = gen.FirstNameBangla,
                                  //LastNameBangla = gen.LastNameBangla,
                                  DateOfBirthOrginal = ((DateTime)gen.DateOfBirthOrginal).ToString("dd/MM/yyyy"),
                                  BirthCertificateNo = gen.BirthCertificateNo,
                                  SexCode = gen.SexCode,
                                  BloodGroupCode = gen.BloodGroupCode,
                                  NationalityCode = gen.NationalityCode,
                                  NationalIDNO = gen.NationalIDNO,
                                  ReligionCode = gen.ReligionCode,
                                  MaritalStatusCode = gen.MaritalStatusCode,
                                  Telephone = gen.Telephone,
                                  PersonalEmail = gen.PersonalEmail,
                                  PhotoUrl = gen.PhotoUrl,
                                  SignatureImageUrl = gen.SignatureImageUrl,
                                  //CompanyCode = offic.CompanyCode,
                                  //BranchCode = offic.BranchCode,
                                  //DepartmentCode = offic.DepartmentCode,
                                  //DesignationCode = offic.DesignationCode,
                                  //ShiftCode = offic.ShiftCode,
                                  //EmpTypeCode = offic.EmpTypeCode,
                                  //EmploymentNatureId = offic.EmploymentNatureId,
                                  //GrossSalary = offic.GrossSalary,
                                  //ReportingTo = offic.ReportingTo,
                                  //HOD = offic.HOD,
                                  //JoiningDate = ((DateTime)offic.JoiningDate).ToString("dd/MM/yyyy"),
                                  //EmployeeStatus = offic.EmployeeStatus,
                                  //MobileNo = offic.MobileNo,
                                  //Email = offic.Email,
                                  EmployeeFullName = gen.FirstName + " " + gen.LastName,
                                  //designationNAme = des.DesignationName,
                                  //DepartmentName = dep.DepartmentName,
                                  PlaceOfBirth = gen.PlaceOfBirth,
                                  //gradeName = gr.GradeName,
                                  //gradeCode = offic.GradeCode,
                                  //curr = offic.CurrencyCode,
                                  //paymentPeriod = offic.PaymentPeriodID,
                                  //prbationPeriod = offic.ProbationPeriod,
                                  //proabType = offic.ProbationPeriodType
                              }).AsEnumerable().Select(a => new Model_HRM_Employee()
                              {
                                  EmployeeID = a.EmployeeID,
                                  FirstName = a.FirstName,
                                  LastName = a.LastName,
                                  FatherName = a.FatherName,
                                  MotherName = a.MotherName,
                                  //FirstNameBangla = gen.FirstNameBangla,
                                  //LastNameBangla = gen.LastNameBangla,
                                  DateOfBirthOrginal = a.DateOfBirthOrginal,
                                  BirthCertificateNo = a.BirthCertificateNo,
                                  SexCode = a.SexCode,
                                  BloodGroupCode = a.BloodGroupCode,
                                  NationalityCode = a.NationalityCode,
                                  NationalIDNO = a.NationalIDNO,
                                  ReligionCode = a.ReligionCode,
                                  MaritalStatusCode = a.MaritalStatusCode,
                                  Telephone = a.Telephone,
                                  PersonalEmail = a.PersonalEmail,
                                  PhotoUrl = a.PhotoUrl,
                                  SignatureImageUrl = a.SignatureImageUrl,
                                  //CompanyCode = a.CompanyCode,
                                  //BranchCode = a.BranchCode,
                                  //DepartmentCode = a.DepartmentCode,
                                  //DesignationCode = a.DesignationCode,
                                  //ShiftCode = a.ShiftCode,
                                  //EmpTypeCode = a.EmpTypeCode,
                                  //EmploymentNatureId = a.EmploymentNatureId,
                                  //GrossSalary = a.GrossSalary,
                                  //ReportingTo = a.ReportingTo,
                                  //HOD = a.HOD,
                                  //JoiningDate = a.JoiningDate,
                                  //EmployeeStatus = a.EmployeeStatus,
                                  //MobileNo = a.MobileNo,
                                  //Email = a.Email,
                                  EmployeeFullName = a.FirstName + " " + a.LastName,
                                  //designationNAme = a.designationNAme,
                                  //DepartmentName = a.DepartmentName,
                                  //PlaceOfBirth = a.PlaceOfBirth,
                                  //GradeCode = a.gradeCode,
                                  //CurrencyCode = a.curr,
                                  //PaymentPeriodID = a.paymentPeriod,
                                  //ProbationPeriod = a.prbationPeriod,
                                  //ProbationPeriodType = a.proabType
                              }).FirstOrDefault();
                return result;
            }

        }

        public Model_HRM_EmployeeContactInfo contactinfodetail(string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from cont in context.HRM_EmployeeContactInfo.Where(x => x.EmployeeID == EmployeeID).AsEnumerable()
                              
                              select new
                              {
                                  ParmanentAddress = cont.ParmanentAddress == null ? "" : cont.ParmanentAddress,
                                  ParmanentPostOffice = cont.ParmanentPostOffice,
                                  ParmanentThana = cont.ParmanentThana,
                                  ParmanentPostCode = cont.ParmanentPostCode,
                                  ParmanentDistrict = cont.ParmanentDistrict,
                                  ParmanentPhone = cont.ParmanentPhone,

                                  PresentAddress = cont.PresentAddress,
                                  PresentPostOffice = cont.PresentPostOffice,
                                  PresentThana = cont.PresentThana,
                                  PresentPostCode = cont.PresentPostCode,
                                  PresentDistrict = cont.PresentDistrict,
                                  PresentMobile = cont.PresentMobile,
                                  PresentPhone = cont.PresentPhone,

                                  EmContactName1 = cont.EmContactName1,
                                  EmContactRelation1 = cont.EmContactRelation1,
                                  EmContactAddress1 = cont.EmContactAddress1,
                                  EmContactPhone1 = cont.EmContactPhone1,
                                  EmContactEmail = cont.EmContactEmail,

                                  EmContactName2 = cont.EmContactName2,
                                  EmContactRelation2 = cont.EmContactRelation2,
                                  EmContactAddress2 = cont.EmContactAddress2,
                                  EmContactPhone2 = cont.EmContactPhone2,
                                  EmContactEmai2 = cont.EmContactEmai2

                              }).AsEnumerable().Select(a => new Model_HRM_EmployeeContactInfo()
                              {

                                  ParmanentAddress = a.ParmanentAddress == null ? "" : a.ParmanentAddress,
                                  ParmanentPostOffice = a.ParmanentPostOffice,
                                  ParmanentThana = a.ParmanentThana,
                                  ParmanentPostCode = a.ParmanentPostCode,
                                  ParmanentDistrict = a.ParmanentDistrict,
                                  ParmanentPhone = a.ParmanentPhone,

                                  PresentAddress = a.PresentAddress,
                                  PresentPostOffice = a.PresentPostOffice,
                                  PresentThana = a.PresentThana,
                                  PresentPostCode = a.PresentPostCode,
                                  PresentDistrict = a.PresentDistrict,
                                  PresentMobile = a.PresentMobile,
                                  PresentPhone = a.PresentPhone,

                                  EmContactName1 = a.EmContactName1,
                                  EmContactRelation1 = a.EmContactRelation1,
                                  EmContactAddress1 = a.EmContactAddress1,
                                  EmContactPhone1 = a.EmContactPhone1,
                                  EmContactEmail = a.EmContactEmail,

                                  EmContactName2 = a.EmContactName2,
                                  EmContactRelation2 = a.EmContactRelation2,
                                  EmContactAddress2 = a.EmContactAddress2,
                                  EmContactPhone2 = a.EmContactPhone2,
                                  EmContactEmai2 = a.EmContactEmai2
                              }).FirstOrDefault();
                return result;
            }

        }

        public List<Model_HRM_EmployeeReferenceInfo> GetEmployeeRefecenceInfo(string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var tt = context.HRM_EmployeeReferenceInfo.Where(x => x.EmployeeID == EmployeeID).ToList();
                List<Model_HRM_EmployeeReferenceInfo> data = new List<Model_HRM_EmployeeReferenceInfo>();
                if (tt.Count > 0)
                {
                    data = (from refe in context.HRM_EmployeeReferenceInfo.Where(x => x.EmployeeID == EmployeeID)
                            join rel in context.HRM_Def_Relationship
                                on new { X1 = refe.RelationID } equals new { X1 = rel.RelationshipCode }
                                into rmp
                            from re in rmp.DefaultIfEmpty()
                            select new Model_HRM_EmployeeReferenceInfo()
                            {
                                EmpReferenceID = refe.EmpReferenceID,
                                EmployeeID = refe.EmployeeID,
                                refName = refe.ReferenceName,
                                REOrganization = refe.OrganizationName,
                                REaddress = refe.RefAddress,
                                ReMobile = refe.MobileNumber ,
                                REemail = refe.Email ,
                                Relation = re.Relationship
                            }).ToList();
                }
                 
                return data;
            }

        }

        public Model_HRM_EmployeeReferenceInfo GetEmployeeRefecenceInfo2(string refId)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from refe in context.HRM_EmployeeReferenceInfo.Where(x => x.EmpReferenceID == refId)
                              .DefaultIfEmpty()
                              from re in context.HRM_Def_Relationship.Where(gen => gen.RelationshipCode == refe.RelationID)
                                .DefaultIfEmpty()
                              select new Model_HRM_EmployeeReferenceInfo()
                              {
                                  EmpReferenceID = refe.EmpReferenceID,
                                  EmployeeID = refe.EmployeeID,
                                  refName = refe.ReferenceName,
                                  REOrganization = refe.OrganizationName,
                                  REaddress = refe.RefAddress,
                                  ReMobile = refe.MobileNumber,
                                  REemail = refe.Email,
                                  Relation = re.Relationship,
                                  RelationID = refe.RelationID,
                              }).FirstOrDefault();
                return result;
            }

        }

        public Model_GaneranAndOfficialEmployee GetEmployee2(string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from gen in context.HRM_Employee.Where(gen => gen.EmployeeID == EmployeeID).AsEnumerable()
                              from gn in context.HRM_EmployeeOfficialInfo.Where(f => gen.EmployeeID == f.EmployeeID)
                              from de in context.HRM_Def_Designation.Where(g => g.DesignationCode == gn.DesignationCode)
                              from dep in context.HRM_Def_Department.Where(h => h.DepartmentCode == gn.DepartmentCode)
                              select new
                              {
                                  EmployeeID = gen.EmployeeID,
                                  FirstName = gen.FirstName,
                                  LastName = gen.LastName,
                                  FatherName = gen.FatherName,
                                  MotherName = gen.MotherName,
                                  //FirstNameBangla = gen.FirstNameBangla,
                                  //LastNameBangla = gen.LastNameBangla,
                                  DateOfBirthOrginal = gen.DateOfBirthOrginal,
                                  BirthCertificateNo = gen.BirthCertificateNo,
                                  SexCode = gen.SexCode,
                                  BloodGroupCode = gen.BloodGroupCode,
                                  NationalityCode = gen.NationalityCode,
                                  NationalIDNO = gen.NationalIDNO,
                                  ReligionCode = gen.ReligionCode,
                                  MaritalStatusCode = gen.MaritalStatusCode,
                                  Telephone = gen.Telephone,
                                  PersonalEmail = gen.PersonalEmail,
                                  PhotoUrl = gen.PhotoUrl,
                                  sig = gen.SignatureImageUrl,
                                  EmployeeFullName = gen.FirstName + " " + gen.LastName,
                                  department = dep.DepartmentName,
                                  designation = de.DesignationName,
                              }).Select(a => new Model_GaneranAndOfficialEmployee()
                              {
                                  EmployeeID = a.EmployeeID,
                                  FirstName = a.FirstName,
                                  LastName = a.LastName,
                                  FatherName = a.FatherName,
                                  MotherName = a.MotherName,
                                  //FirstNameBangla = gen.FirstNameBangla,
                                  //LastNameBangla = gen.LastNameBangla,
                                  DateOfBirthOrginal = a.DateOfBirthOrginal == null ? null : ((DateTime)a.DateOfBirthOrginal).ToString("dd/MM/yyyy"),
                                  BirthCertificateNo = a.BirthCertificateNo,
                                  SexCode = a.SexCode,
                                  BloodGroupCode = a.BloodGroupCode,
                                  NationalityCode = a.NationalityCode,
                                  NationalIDNO = a.NationalIDNO,
                                  ReligionCode = a.ReligionCode,
                                  MaritalStatusCode = a.MaritalStatusCode,
                                  Telephone = a.Telephone,
                                  PersonalEmail = a.PersonalEmail,
                                  PhotoUrl = a.PhotoUrl,
                                  EmployeeFullName = a.FirstName + " " + a.LastName,
                                  DepartmentName = a.department,
                                  designationNAme = a.designation,
                                  SignatureImageUrl = a.sig
                              }).FirstOrDefault();
                return result;
            }

        }

        public Model_HRM_EmployeeContactInfo ContactInfo(string EmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from gen in context.HRM_EmployeeContactInfo.Where(gen => gen.EmployeeID == EmployeeID)
                          from gn in context.HRM_EmployeeOfficialInfo.Where(f => gen.EmployeeID == f.EmployeeID)
                          from de in context.HRM_Def_Designation.Where(g => g.DesignationCode == gn.DesignationCode)
                          from dep in context.HRM_Def_Department.Where(h => h.DepartmentCode == gn.DepartmentCode)
                          select new
                          {
                              EmployeeID = gen.EmployeeID,
                              department = dep.DepartmentName,
                              designation = de.DesignationName,

                          }).Select(a => new Model_HRM_EmployeeContactInfo()
                          {
                              EmployeeID = a.EmployeeID,

                          }).FirstOrDefault();
            return result;

        }

        public Model_EmployeeBasicInfo GetEmployeeInfo(string EmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from gn in context.HRM_Employee.DefaultIfEmpty()
                          from gen in context.HRM_EmployeeOfficialInfo.Where(gen => gen.EmployeeID == gn.EmployeeID)
                              .DefaultIfEmpty()
                          from de in context.HRM_Def_Designation.Where(gen => gen.DesignationCode == gen.DesignationCode)
                          .DefaultIfEmpty()
                          from dep in context.HRM_Def_Department.Where(gen => gen.DepartmentCode == gen.DepartmentCode)
                          .DefaultIfEmpty()
                          select new
                          {
                              EmployeeID = gn.EmployeeID,
                              EmployeeName = gn.FirstName + " " +gn.LastName,
                              MobileNo = gn.Telephone,
                              Email = gn.PersonalEmail,
                              designation = de.DesignationName,
                              department = dep.DepartmentName
                          }).AsEnumerable().Select(a => new Model_EmployeeBasicInfo()
                          {
                              EmployeeID = a.EmployeeID,
                              EmployeeName = a.EmployeeName,
                              Designation = a.designation,
                              Department = a.department
                          }).Where(a => a.EmployeeID == EmployeeID).FirstOrDefault();
            return result;
        }

        public bool DeleteCompany(string id)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var EmployeeID = context.HRM_Employee.FirstOrDefault(x => x.EmployeeID == id);
                if (EmployeeID != null)
                {
                    context.HRM_Employee.Remove(EmployeeID);
                    context.SaveChanges();
                    return true;
                }
                return false;


            }

        }
    }
}
