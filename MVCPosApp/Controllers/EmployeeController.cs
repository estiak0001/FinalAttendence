using BusinessLogic;

using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class EmployeeController : Controller
    {

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crd_HRM_Employee empModel = new Crd_HRM_Employee();
        Crd_HRM_EmployeeOfficialInfo empModelOfficial = new Crd_HRM_EmployeeOfficialInfo();

        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index(string page)
        {
            ViewBag.LoadCompany = new SelectList(db.Core_Company, "CompanyCode", "CompanyName");
            ViewBag.LoadDepartment = new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
            ViewBag.LoadDesignation = new SelectList(db.HRM_Def_Designation, "DesignationCode", "DesignationName");
            ViewBag.Branch = new SelectList(db.Core_Branch, "BranchCode", "BranchName");

            int pages = 1;
            if (page != null)
            {
                pages = Convert.ToInt32(page);
            }
            
            ProductIndexView employeeView = new ProductIndexView();
            employeeView.employeePerPage = 18;
            employeeView.employees = empModel.GetAllEmployee("", "", "", "", "");
            employeeView.CurrentPage = pages;
            employeeView.Pager = new Pager(employeeView.employees.Count(), pages);
            return View(employeeView);
        }
        public ActionResult SearchResult(string search, string company, string branch, string department, string designation)
        {
            if(company == null)
            {
                company = "";
            }
            if (branch == null)
            {
                branch = "";
            }
            if (department == null)
            {
                department = "";
            }
            if (designation == null)
            {
                designation = "";
            }

            int page = 1;
            ProductIndexView employeeView = new ProductIndexView();
            employeeView.employeePerPage = 18;
            employeeView.employees = empModel.GetAllEmployee(search, company, branch, department, designation);
            employeeView.CurrentPage = page;
            employeeView.Pager = new Pager(employeeView.employees.Count(), page);
            //return Pertia(employeeView);
            return PartialView("_EmployeeList", employeeView);
        }
        public ActionResult ShowEmployeInfo()
        {
            string search = null;
            string company = "";
            string branch = "";
            string department = "";
            string designation = "";

            var result = empModel.GetAllEmployee(search, company, branch, department, designation);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Search(string serchtext)
        //{
        //}

        [HttpGet]
        public ActionResult AddOrEditEmployee(string empID)
        {
            //ViewBag.ActiveTab = "custom-tabs-one-messages";
            ViewBag.BloodGroup = new SelectList(db.HRM_Def_BloodGroup, "BloodGroupCode", "BloodGroup");
            ViewBag.Sex = new SelectList(db.HRM_Def_Sex, "SexCode", "Sex");
            ViewBag.Religion = new SelectList(db.HRM_Def_Religion, "ReligionCode", "Religion");
            ViewBag.LoadNationality = new SelectList(db.HRM_Def_Nationality, "nationalitycode", "Nationality");

            //official
            string empIDLastInserted = "";
            if (TempData.ContainsKey("InsertedEmployeeID"))
            {
                empIDLastInserted = TempData["InsertedEmployeeID"].ToString();
                empID = empIDLastInserted;
            }
            
            if (empIDLastInserted != "")
            {
                ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.Where(x=>x.EmployeeID == empIDLastInserted).ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            }
            else
            {
                if (empID != null)
                {
                    ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
                }
                else
                {
                    //ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.Where(c => !db.HRM_EmployeeOfficialInfo.Select(b => b.EmployeeID).Contains(c.EmployeeID)).ToList().Select(u
                    //         => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                    // "EmployeeID", "FirstName");

                    ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
                }
            }
            
            ViewBag.LoadCompany = new SelectList(db.Core_Company, "CompanyCode", "CompanyName");
            ViewBag.LoadBranch = new SelectList(db.Core_Branch, "BranchCode", "BranchName");
            ViewBag.LoadDepartment = new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
            ViewBag.LoadDesignation = new SelectList(db.HRM_Def_Designation, "DesignationCode", "DesignationName");
            ViewBag.LoadShift = new SelectList(db.HRM_ATD_Shift, "ShiftCode", "ShiftName");
            ViewBag.LoadEmpType = new SelectList(db.HRM_Def_EmpType, "EmpTypeCode", "EmpTypeName");
            ViewBag.LoadEmpNature = new SelectList(db.HRM_EIS_Def_EmploymentNature, "EmploymentNatureId", "EmploymentNature");
            ViewBag.Relation = new SelectList(db.HRM_Def_Relationship, "RelationshipCode", "Relationship");
            ViewBag.PeriodList = new SelectList(db.Core_PeriodInfo, "PeriodInfoId", "PeriodName");
            ViewBag.Currency = new SelectList(db.CA_Def_Currency, "CurrencyId", "ShortName");
            ViewBag.PaymentMood = new SelectList(db.Sales_Def_PaymentMode, "PaymentModeID", "PaymentModeName");
            ViewBag.ProbationType = new SelectList(db.Core_PeriodInfo, "PeriodInfoId", "PeriodName");
            ViewBag.MaritalStatus = new SelectList(db.HRM_Def_MaritalStatus, "MaritalStatusCode", "MaritalStatus");
            ViewBag.Grade = new SelectList(db.HRM_Def_Grade, "GradeCode", "GradeName");
            ViewBag.District = new SelectList(db.HRM_Def_District, "DistrictID", "District");

            ViewBag.LoadReportingTo = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            ViewBag.LoadHOD = new SelectList(db.HRM_Employee.ToList().Select(u
                      => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            ViewBag.LoadEmpStatus = new SelectList(db.HRM_Def_EmployeeStatus, "EmployeeStatusCode", "EmployeeStatus");
            if (empID == null)
            {
                //common.FindMaxNoAuto(ref strMaxNO, "EmployeeID", "HRM_Employee");
                common.FindMaxNo(ref strMaxNO, "EmployeeID", "HRM_Employee", 4);
                ViewBag.MaxComID = strMaxNO.ToString();
                Model_GaneranAndOfficialEmployee All = new Model_GaneranAndOfficialEmployee();
                All.general = new Model_HRM_Employee();
                All.contact = new Model_HRM_EmployeeContactInfo();
                All.Model_HRM_EmployeeReferenceInfos = new List<Model_HRM_EmployeeReferenceInfo>();

                All.EmployeeID = strMaxNO.ToString();
                return View(All);
            }
            else
            {
                //string empIDLastInserted3 = "";
                //if (TempData.ContainsKey("InsertedEmployeeID"))
                //{
                //    empIDLastInserted3 = TempData["InsertedEmployeeID"].ToString();
                //    string RID = TempData["rreffID"].ToString();
                //    var result2 = empModel.GetEmployee2(empID);
                //    result2.Model_HRM_EmployeeReferenceInfos = empModel.GetEmployeeRefecenceInfo(empID);

                //if (RID != "")
                //{
                //    var RefData = empModel.GetEmployeeRefecenceInfo2(RID);
                //    List<Model_HRM_EmployeeReferenceInfo> rre = new List<Model_HRM_EmployeeReferenceInfo>();
                //    result2.EmpReferenceID = RefData.Id;
                //    result2.refName = RefData.refName;
                //    result2.REOrganization = RefData.REOrganization;
                //    result2.REaddress = RefData.REaddress;
                //    result2.RelationID = RefData.RelationID;
                //    result2.ReMobile = RefData.ReMobile;
                //    result2.REemail = RefData.REemail;
                //    rre = empModel.GetEmployeeRefecenceInfo(RefData.EmployeeID);
                //    result2.Model_HRM_EmployeeReferenceInfos = rre;
                //}
                //return View(result2);
                //}
                Model_GaneranAndOfficialEmployee All = new Model_GaneranAndOfficialEmployee();
                All.general = new Model_HRM_Employee();
                All.contact = new Model_HRM_EmployeeContactInfo();
                All.Model_HRM_EmployeeReferenceInfos = new List<Model_HRM_EmployeeReferenceInfo>();
                var result = empModel.GetEmployee(empID);
                if(result!=null)
                {
                    All = result;
                }
                var general = empModel.GetEmployeeGeneral(empID);
                if(general != null)
                {
                    All.general = general;
                    All.EmployeeID = general.EmployeeID;
                }
                var contactInfo = empModel.contactinfodetail(empID);
                if (contactInfo != null)
                {
                    All.contact = contactInfo;
                }
                var refe = empModel.GetEmployeeRefecenceInfo(empID);
                if (refe.Count != 0)
                {
                    All.Model_HRM_EmployeeReferenceInfos = refe;
                }
                return View(All);
            }
        }

        [HttpPost]
        public ActionResult AddOrEditEmployee(Model_GaneranAndOfficialEmployee Emp)
        {
            string LoginEmployeeID = Session["EmployeeID"].ToString();
            var Item = db.HRM_Employee.FirstOrDefault(x => x.EmployeeID == Emp.EmployeeID);
            if (Item == null)
            {
                string directory = @"~\EmpImage\";
                string directory2 = @"~\SignatureImage\";
                HttpPostedFileBase ProfileImageUpload = Emp.general.Photo; // Request.Files["ProfileImageUpload"];
                HttpPostedFileBase ProfileImageUpload2 = Emp.general.Photo2;
                string fileName = null;
                if (ProfileImageUpload != null && ProfileImageUpload.ContentLength > 0)
                {
                    string extension = Path.GetExtension(ProfileImageUpload.FileName);
                    fileName = Emp.EmployeeID + extension;
                    string imgPath = Path.Combine(directory, fileName);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    ProfileImageUpload.SaveAs(Server.MapPath(Path.Combine(directory, fileName)));
                }
                else
                {
                    fileName = "default.png";
                }

                string fileName2 = null;
                if (ProfileImageUpload2 != null && ProfileImageUpload2.ContentLength > 0)
                {
                    string extension = Path.GetExtension(ProfileImageUpload2.FileName);
                    fileName2 = Emp.EmployeeID + extension;
                    string imgPath = Path.Combine(directory2, fileName2);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    ProfileImageUpload2.SaveAs(Server.MapPath(Path.Combine(directory2, fileName2)));
                }
                else
                {
                    fileName2 = "";
                }

                string NewDateString = string.Empty;
                if (Emp.general.DateOfBirthOrginal != null)
                {
                    string DateString = Emp.general.DateOfBirthOrginal.ToString();
                    DateTime date = new DateTime();
                    date = DateTime.ParseExact(DateString, "dd/MM/yyyy", null);
                    NewDateString = date.ToString("yyyy/MM/dd");
                }
                else
                {
                    NewDateString = DBNull.Value.ToString();
                }

                HRM_Employee coreCom = new HRM_Employee()
                {
                    EmployeeID = Emp.EmployeeID,
                    FirstName = Emp.general.FirstName,
                    LastName = Emp.general.LastName,
                    FirstNameBangla = Emp.general.FirstNameBangla,
                    LastNameBangla = Emp.general.LastNameBangla,
                    FatherName = Emp.general.FatherName,
                    MotherName = Emp.general.MotherName,
                    DateOfBirthOrginal = Convert.ToDateTime(NewDateString),
                    BirthCertificateNo = "",
                    PlaceOfBirth = Emp.general.PlaceOfBirth,
                    SexCode = Emp.general.SexCode,
                    BloodGroupCode = Emp.general.BloodGroupCode,
                    NationalityCode = Emp.general.NationalityCode,
                    NationalIDNO = Emp.general.NationalIDNO,
                    ReligionCode = Emp.general.ReligionCode,
                    MaritalStatusCode = Emp.general.MaritalStatusCode,
                    NoOfSon = "",
                    NoOfDaughters = "",
                    CardNo = "",
                    PersonalEmail = Emp.general.PersonalEmail,
                    Telephone = Emp.general.Telephone,
                    LUser = LoginEmployeeID,
                    CompanyCode = "001",
                    UserInfoEmployeeID = LoginEmployeeID,
                    PhotoUrl = "~/EmpImage/" + fileName,
                    SignatureImageUrl = "~/SignatureImage/" + fileName2,
                    
                };
                TempData["ActiveTab"] = "custom-tabs-one-contact";
                TempData["InsertedEmployeeID"] = Emp.EmployeeID;
                db.HRM_Employee.Add(coreCom);
                db.SaveChanges();


                return RedirectToAction("AddOrEditEmployee", "Employee", Emp.EmployeeID);
            }
            else
            {
                string directory = @"~\EmpImage\";
                string directory2 = @"~\SignatureImage\";
                HttpPostedFileBase ProfileImageUpload = Emp.general.Photo; // Request.Files["ProfileImageUpload"];
                HttpPostedFileBase ProfileImageUpload2 = Emp.general.Photo2;
                string fileName = null;
                
                if (ProfileImageUpload != null && ProfileImageUpload.ContentLength > 0)
                {
                    string extension = Path.GetExtension(ProfileImageUpload.FileName);
                    fileName = Emp.EmployeeID + extension;
                    string imgPath = Path.Combine(directory, fileName);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    ProfileImageUpload.SaveAs(Server.MapPath(Path.Combine(directory, fileName)));
                    fileName = "~/EmpImage/" + fileName;
                }
                else
                {
                    fileName = Item.PhotoUrl;
                }
                string fileName2 = null;
                if (ProfileImageUpload2 != null && ProfileImageUpload2.ContentLength > 0)
                {
                    string extension = Path.GetExtension(ProfileImageUpload2.FileName);
                    fileName2 = Emp.EmployeeID + extension;
                    string imgPath = Path.Combine(directory2, fileName2);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    ProfileImageUpload2.SaveAs(Server.MapPath(Path.Combine(directory2, fileName2)));
                    fileName2 = "~/SignatureImage/" + fileName2;
                }
                else
                {
                    fileName2 = Item.SignatureImageUrl;
                }
                Item.EmployeeID = Emp.EmployeeID;
                Item.FirstName = Emp.general.FirstName;
                Item.LastName = Emp.general.LastName;
                Item.FirstNameBangla = Emp.general.FirstNameBangla;
                Item.LastNameBangla = Emp.general.LastNameBangla;
                Item.FatherName = Emp.general.FatherName;
                Item.MotherName = Emp.general.MotherName;

                string DateString = Emp.general.DateOfBirthOrginal;
                DateTime date = new DateTime();
                date = DateTime.ParseExact(DateString, "dd/MM/yyyy", null);
                string NewDateString = date.ToString("yyyy/MM/dd");
                Item.DateOfBirthOrginal = Convert.ToDateTime(NewDateString);

                Item.BirthCertificateNo = "";
                Item.PlaceOfBirth = Emp.general.PlaceOfBirth;
                Item.SexCode = Emp.general.SexCode;
                Item.BloodGroupCode = Emp.general.BloodGroupCode;
                Item.NationalityCode = Emp.general.NationalityCode;
                Item.NationalIDNO = Emp.general.NationalIDNO;
                Item.ReligionCode = Emp.general.ReligionCode;
                Item.MaritalStatusCode = Emp.general.MaritalStatusCode;
                Item.NoOfSon = "";
                Item.NoOfDaughters = "";
                Item.CardNo = "";
                Item.PersonalEmail = Emp.general.PersonalEmail;
                Item.Telephone = Emp.general.Telephone;
                Item.UserInfoEmployeeID = LoginEmployeeID;
                Item.PhotoUrl = fileName;
                Item.SignatureImageUrl = fileName2;
                db.SaveChanges();

                TempData["ActiveTab"] = "custom-tabs-one-contact";
                TempData["InsertedEmployeeID"] = Emp.EmployeeID;

                
                return RedirectToAction("AddOrEditEmployee", "Employee", Emp.EmployeeID);
            }
        }
        [HttpPost]
        public ActionResult AddOrEditEmployeeOfficialInfo(Model_GaneranAndOfficialEmployee Emp)
        {
            string LoginEmployeeID = Session["EmployeeID"].ToString();
            var Item = db.HRM_EmployeeOfficialInfo.FirstOrDefault(x => x.EmployeeID == Emp.EmployeeID);
            if (Item == null)
            {
                string DateString = Emp.JoiningDate.ToString();
                DateTime date = new DateTime();
                date = DateTime.ParseExact(DateString, "dd/MM/yyyy", null);
                string JoiningDate = date.ToString("MM/dd/yyyy");

                HRM_EmployeeOfficialInfo coreCom = new HRM_EmployeeOfficialInfo();
                coreCom.EmployeeID = Emp.EmployeeID;
                coreCom.CompanyCode = Emp.CompanyCode;
                if (Emp.BranchCode != null)
                {
                    coreCom.BranchCode = Emp.BranchCode;
                }
                else
                {
                    coreCom.BranchCode = "";
                }
                if (Emp.DivisionCode != null)
                {
                    coreCom.DivisionCode = Emp.DivisionCode;
                }
                else
                {
                    coreCom.DivisionCode = "";
                }
                coreCom.DepartmentCode = Emp.DepartmentCode;
                coreCom.DesignationCode = Emp.DesignationCode;

                if (Emp.EmpTypeCode != null)
                {
                    coreCom.EmpTypeCode = Emp.EmpTypeCode;
                }
                else
                {
                    coreCom.EmpTypeCode = "";
                }
                if (Emp.GradeCode != null)
                {
                    coreCom.GradeCode = Emp.GradeCode;
                }
                else
                {
                    coreCom.GradeCode = "";
                }
                coreCom.EmploymentNatureId = Emp.EmploymentNatureId;
                if (Emp.GrossSalary != null)
                {
                    coreCom.GrossSalary = Convert.ToDecimal(Emp.GrossSalary);
                }
                else
                {
                    coreCom.GrossSalary = 0;
                }
                coreCom.CurrencyCode = Emp.CurrencyCode;
                coreCom.PaymentPeriodID = Emp.PaymentPeriodID;
                coreCom.ProbationPeriod = Emp.ProbationPeriod;
                coreCom.ProbationPeriodType = Emp.ProbationPeriodType;
                coreCom.DisbursementMethodId = "";
                
                coreCom.ShiftCode = Emp.ShiftCode;
                coreCom.EmployeeStatus = Emp.EmployeeStatus;
                if (Emp.ReportingTo != null)
                {
                    coreCom.ReportingTo = Emp.ReportingTo;
                }
                else
                {
                    coreCom.ReportingTo = "";
                }
                if (Emp.HOD != null)
                {
                    coreCom.HOD = Emp.HOD;
                }
                else
                {
                    coreCom.HOD = "";
                }
                if (Emp.MobileNo != null)
                {
                    coreCom.MobileNo = Emp.MobileNo;
                }
                else
                {
                    coreCom.MobileNo = "";
                }
                if (Emp.Email != null)
                {
                    coreCom.Email = Emp.Email;
                }
                else
                {
                    coreCom.Email = "";
                }
                coreCom.AppointmentLetterNo = "";
                coreCom.JoiningDate = DateTime.Parse(JoiningDate);
                if (Emp.JoiningSalary != null)
                {
                    coreCom.JoiningSalary = Convert.ToDecimal(Emp.JoiningSalary);
                }
                else
                {
                    coreCom.JoiningSalary = 0;
                }
                coreCom.ProbationPeriodType = "";
                coreCom.ProbationPeriod = "";
                coreCom.LUser = LoginEmployeeID;
                coreCom.LDate = DateTime.Now;

                coreCom.CompanyCode_Session = "001";
                coreCom.UserInfoEmployeeID = LoginEmployeeID;
                db.HRM_EmployeeOfficialInfo.Add(coreCom);
                db.SaveChanges();

                TempData["ActiveTab"] = "custom-tabs-one-settings";
                TempData["InsertedEmployeeID"] = Emp.EmployeeID;

                return RedirectToAction("AddOrEditEmployee", "Employee", Emp.EmployeeID);
            }
            else
            {
                string DateString = Emp.JoiningDate.ToString();
                DateTime date = new DateTime();
                date = DateTime.ParseExact(DateString, "dd/MM/yyyy", null);
                string JoiningDate = date.ToString("MM/dd/yyyy");

                Item.EmployeeID = Emp.EmployeeID;
                Item.CompanyCode = Emp.CompanyCode;
                if (Emp.BranchCode != null)
                {
                    Item.BranchCode = Emp.BranchCode;
                }
                else
                {
                    Item.BranchCode = "";
                }
                if (Emp.DivisionCode != null)
                {
                    Item.DivisionCode = Emp.DivisionCode;
                }
                else
                {
                    Item.DivisionCode = "";
                }
                Item.DepartmentCode = Emp.DepartmentCode;
                Item.DesignationCode = Emp.DesignationCode;
                if (Emp.EmpTypeCode != null)
                {
                    Item.EmpTypeCode = Emp.EmpTypeCode;
                }
                else
                {
                    Item.EmpTypeCode = "";
                }
                if (Emp.GradeCode != null)
                {
                    Item.GradeCode = Emp.GradeCode;
                }
                else
                {
                    Item.GradeCode = "";
                }
                Item.EmploymentNatureId = Emp.EmploymentNatureId;
                if (Emp.GrossSalary != null)
                {
                    Item.GrossSalary = Convert.ToDecimal(Emp.GrossSalary);
                }
                else
                {
                    Item.GrossSalary = 0;
                }
                Item.CurrencyCode = Emp.CurrencyCode;
                Item.PaymentPeriodID = Emp.PaymentPeriodID;
                Item.ProbationPeriod = Emp.ProbationPeriod;
                Item.ProbationPeriodType = Emp.ProbationPeriodType;
                Item.DisbursementMethodId = "";
                Item.ShiftCode = Emp.ShiftCode;
                Item.EmployeeStatus = Emp.EmployeeStatus;
                if (Emp.ReportingTo != null)
                {
                    Item.ReportingTo = Emp.ReportingTo;
                }
                else
                {
                    Item.ReportingTo = "";
                }
                if (Emp.HOD != null)
                {
                    Item.HOD = Emp.HOD;
                }
                else
                {
                    Item.HOD = "";
                }
                if (Emp.MobileNo != null)
                {
                    Item.MobileNo = Emp.MobileNo;
                }
                else
                {
                    Item.MobileNo = "";
                }
                if (Emp.Email != null)
                {
                    Item.Email = Emp.Email;
                }
                else
                {
                    Item.Email = "";
                }
                Item.AppointmentLetterNo = "";
                Item.JoiningDate = DateTime.Parse(JoiningDate);
                if (Emp.JoiningSalary != null)
                {
                    Item.JoiningSalary = Convert.ToDecimal(Emp.JoiningSalary);
                }
                else
                {
                    Item.JoiningSalary = 0;
                }
                Item.ProbationPeriodType = Emp.ProbationPeriodType;
                Item.ProbationPeriod = Emp.ProbationPeriod;
                Item.LUser = LoginEmployeeID;
                Item.LDate = DateTime.Now;

                Item.CompanyCode_Session = "001";
                Item.UserInfoEmployeeID = LoginEmployeeID;
                db.SaveChanges();

                TempData["ActiveTab"] = "custom-tabs-one-settings";
                TempData["InsertedEmployeeID"] = Emp.EmployeeID;

                return RedirectToAction("AddOrEditEmployee", "Employee", Emp.EmployeeID);
            }
        }
        [HttpPost]
        public ActionResult AddOrEditEmployeeRefInfo(Model_GaneranAndOfficialEmployee Emp)
        {
            string LoginEmployeeID = Session["EmployeeID"].ToString();
            var Item = db.HRM_EmployeeReferenceInfo.FirstOrDefault(x => x.EmpReferenceID == Emp.EmpReferenceID);
            if (Item == null)
            {
                common.FindMaxNo(ref strMaxNO, "EmpReferenceID", "HRM_EmployeeReferenceInfo", 3);

                HRM_EmployeeReferenceInfo coreCom = new HRM_EmployeeReferenceInfo();

                coreCom.EmpReferenceID = strMaxNO.ToString();
                coreCom.EmployeeID = Emp.EmployeeID;
                coreCom.ReferenceName = Emp.refName;
                coreCom.OrganizationName = Emp.REOrganization;
                coreCom.RefAddress = Emp.REaddress;
                coreCom.RelationID = Emp.RelationID;
                coreCom.MobileNumber = Emp.ReMobile;
                coreCom.Email = Emp.REemail;
                coreCom.LUser = LoginEmployeeID;
                coreCom.LDate = DateTime.Now;
                coreCom.UserInfoEmployeeID = LoginEmployeeID;
                coreCom.CompanyCode = "";
                //TempData["ActiveTab"] = "custom-tabs-one-settings";
                //TempData["InsertedEmployeeID"] = Emp.EmployeeID;

                db.HRM_EmployeeReferenceInfo.Add(coreCom);
                db.SaveChanges();
                //TempData["rreffID"] = coreCom.EmpReferenceID;
                //return RedirectToAction("AddOrEditEmployee", "Employee");

                return Json(new { success = true, message = "Data Added Successfully!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Item.ReferenceName = Emp.refName;
                Item.OrganizationName = Emp.REOrganization;
                Item.RefAddress = Emp.REaddress;
                Item.RelationID = Emp.RelationID;
                Item.MobileNumber = Emp.ReMobile;
                Item.Email = Emp.REemail;
                Item.LUser = LoginEmployeeID;
                Item.ModifyDate = DateTime.Now;
                //TempData["ActiveTab"] = "custom-tabs-one-settings";
                //TempData["InsertedEmployeeID"] = Emp.EmployeeID;
                //Item.UserInfoEmployeeID = LoginEmployeeID;
                db.SaveChanges();
                //return RedirectToAction("AddOrEditEmployee", "Employee");

                return Json(new { success = true, message = "Data Updated Successfully!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEditEmployeeContactInfo(Model_GaneranAndOfficialEmployee Emp)
        {
            string LoginEmployeeID = Session["EmployeeID"].ToString();
            var Item = db.HRM_EmployeeContactInfo.FirstOrDefault(x => x.EmployeeID == Emp.EmployeeID);
            if (Item == null)
            {
                HRM_EmployeeContactInfo coreCom = new HRM_EmployeeContactInfo();

                coreCom.EmployeeID = Emp.EmployeeID;
                coreCom.ParmanentAddress = Emp.contact.ParmanentAddress;
                coreCom.ParmanentPostOffice = Emp.contact.ParmanentPostOffice;
                coreCom.ParmanentThana = Emp.contact.ParmanentThana;
                coreCom.ParmanentPostCode = Emp.contact.ParmanentPostCode;
                coreCom.ParmanentDistrict = Emp.contact.ParmanentDistrict;
                coreCom.ParmanentPhone = Emp.contact.ParmanentPhone;

                coreCom.PresentAddress = Emp.contact.PresentAddress;
                coreCom.PresentPostOffice = Emp.contact.PresentPostOffice;
                coreCom.PresentThana = Emp.contact.PresentThana;
                coreCom.PresentPostCode = Emp.contact.PresentPostCode;
                coreCom.PresentDistrict = Emp.contact.PresentDistrict;
                coreCom.PresentPhone = Emp.contact.PresentPhone;

                coreCom.EmContactName1 = Emp.contact.EmContactName1;
                coreCom.EmContactRelation1 = Emp.contact.EmContactRelation1;
                coreCom.EmContactAddress1 = Emp.contact.EmContactAddress1;
                coreCom.EmContactPhone1 = Emp.contact.EmContactPhone1;
                coreCom.EmContactEmail = Emp.contact.EmContactEmail;

                coreCom.EmContactName2 = Emp.contact.EmContactName2;
                coreCom.EmContactRelation2 = Emp.contact.EmContactRelation2;
                coreCom.EmContactAddress2 = Emp.contact.EmContactAddress2;
                coreCom.EmContactPhone2 = Emp.contact.EmContactPhone2;
                coreCom.EmContactEmai2 = Emp.contact.EmContactEmai2;

                coreCom.LUser = LoginEmployeeID;
                coreCom.LDate = DateTime.Now;
                coreCom.UserInfoEmployeeID = LoginEmployeeID;
                coreCom.CompanyCode = "001";

                db.HRM_EmployeeContactInfo.Add(coreCom);
                db.SaveChanges();

                TempData["ActiveTab"] = "custom-tabs-one-profile";
                TempData["InsertedEmployeeID"] = Emp.EmployeeID;

                
                return RedirectToAction("AddOrEditEmployee", "Employee", Emp.EmployeeID);
            }
            else
            {
                Item.EmployeeID = Emp.EmployeeID;
                Item.ParmanentAddress = Emp.contact.ParmanentAddress;
                Item.ParmanentPostOffice = Emp.contact.ParmanentPostOffice;
                Item.ParmanentThana = Emp.contact.ParmanentThana;
                Item.ParmanentPostCode = Emp.contact.ParmanentPostCode;
                Item.ParmanentDistrict = Emp.contact.ParmanentDistrict;
                Item.ParmanentPhone = Emp.contact.ParmanentPhone;

                Item.PresentAddress = Emp.contact.PresentAddress;
                Item.PresentPostOffice = Emp.contact.PresentPostOffice;
                Item.PresentThana = Emp.contact.PresentThana;
                Item.PresentPostCode = Emp.contact.PresentPostCode;
                Item.PresentDistrict = Emp.contact.PresentDistrict;
                Item.PresentPhone = Emp.contact.PresentPhone;

                Item.EmContactName1 = Emp.contact.EmContactName1;
                Item.EmContactRelation1 = Emp.contact.EmContactRelation1;
                Item.EmContactAddress1 = Emp.contact.EmContactAddress1;
                Item.EmContactPhone1 = Emp.contact.EmContactPhone1;
                Item.EmContactEmail = Emp.contact.EmContactEmail;

                Item.EmContactName2 = Emp.contact.EmContactName2;
                Item.EmContactRelation2 = Emp.contact.EmContactRelation2;
                Item.EmContactAddress2 = Emp.contact.EmContactAddress2;
                Item.EmContactPhone2 = Emp.contact.EmContactPhone2;
                Item.EmContactEmai2 = Emp.contact.EmContactEmai2;

                Item.LUser = LoginEmployeeID;
                Item.ModifyDate = DateTime.Now;

                
                db.SaveChanges();
                TempData["ActiveTab"] = "custom-tabs-one-profile";
                TempData["InsertedEmployeeID"] = Emp.EmployeeID;


                return RedirectToAction("AddOrEditEmployee", "Employee", Emp.EmployeeID);
            }
        }

        [HttpPost]
        public ActionResult DeleteData(string EmpReferenceID)
        {
            var itemToRemove = db.HRM_EmployeeReferenceInfo.SingleOrDefault(x => x.EmpReferenceID == EmpReferenceID); //returns a single item.

            if (itemToRemove != null)
            {
                db.HRM_EmployeeReferenceInfo.Remove(itemToRemove);
                db.SaveChanges();
                return Json(new { success = true, message = "Data Delete Successfully!" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, message = "Something went wrong!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditEmployeeRefInfo(string id)
        {
            Model_GaneranAndOfficialEmployee Emp = new Model_GaneranAndOfficialEmployee();
            var RefData = empModel.GetEmployeeRefecenceInfo2(id);
            TempData["ActiveTab"] = "custom-tabs-one-settings";
            TempData["InsertedEmployeeID"] = RefData.EmployeeID;
            TempData["rreffID"] = id;
            return RedirectToAction("AddOrEditEmployee", "Employee", Emp);
        }
        public JsonResult GetEmployeeInfo(string EmployeeID)
        {
            var result = empModel.GetEmployee2(EmployeeID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SingelData(string refID)
        {
            var result = empModel.GetEmployeeRefecenceInfo2(refID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeReef(string id)
        {
            var result = empModel.GetEmployeeRefecenceInfo(id);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            empModel.DeleteCompany(id);
            return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }

    }
}