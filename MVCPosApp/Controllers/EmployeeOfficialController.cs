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
    public class EmployeeOfficialController : Controller
    {

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crd_HRM_EmployeeOfficialInfo empModel = new Crd_HRM_EmployeeOfficialInfo();

        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowEmployeInfo()
        {
            var result = empModel.GetAllEmployee();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEditEmployee(string id)
        {
            ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            ViewBag.LoadCompany= new SelectList(db.Core_Company, "CompanyCode", "CompanyName");
            ViewBag.LoadBranch = new SelectList(db.Core_Branch, "BranchCode", "BranchName");
            ViewBag.LoadDepartment= new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
            ViewBag.LoadDesignation = new SelectList(db.HRM_Def_Designation, "DesignationCode", "DesignationName");
            ViewBag.LoadShift= new SelectList(db.HRM_ATD_Shift, "ShiftCode", "ShiftName");
            ViewBag.LoadEmpType = new SelectList(db.HRM_Def_EmpType, "EmpTypeCode", "EmpTypeName");
            ViewBag.LoadEmpNature = new SelectList(db.HRM_EIS_Def_EmploymentNature, "EmploymentNatureId", "EmploymentNature");

            ViewBag.LoadReportingTo = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            ViewBag.LoadHOD= new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            ViewBag.LoadEmpStatus = new SelectList(db.HRM_Def_EmployeeStatus, "EmployeeStatusCode", "EmployeeStatus");

            if (id == null)
            {
                return View(new Model_HRM_EmployeeOfficialInfo());
            }
            else
            {
                var result = empModel.GetEmployee(id);
                
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEditEmployee(Model_HRM_EmployeeOfficialInfo Emp)
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
                if(Emp.BranchCode != null)
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
                    coreCom.DivisionCode= "";
                }
                coreCom.DepartmentCode= Emp.DepartmentCode;
                coreCom.DesignationCode= Emp.DesignationCode;
                
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
                    coreCom.GradeCode= "";
                }
                coreCom.EmploymentNatureId= Emp.EmploymentNatureId;
                if (Emp.GrossSalary != null)
                {
                    coreCom.GrossSalary = Convert.ToDecimal(Emp.GrossSalary);
                }
                else
                {
                    coreCom.GrossSalary= 0;
                }
                coreCom.CurrencyCode="" ;
                coreCom.PaymentPeriodID="";
                coreCom.DisbursementMethodId= "";
                coreCom.ShiftCode=Emp.ShiftCode;
                coreCom.EmployeeStatus = Emp.EmployeeStatus;
                if (Emp.ReportingTo != null)
                {
                    coreCom.ReportingTo= Emp.ReportingTo;
                }
                else
                {
                    coreCom.ReportingTo= "";
                }
                if (Emp.HOD != null)
                {
                    coreCom.HOD= Emp.HOD;
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
                    coreCom.MobileNo= "";
                }
                if (Emp.Email != null)
                {
                    coreCom.Email = Emp.Email;
                }
                else
                {
                    coreCom.Email = "";
                }
                coreCom.AppointmentLetterNo= "";                
                coreCom.JoiningDate =Convert.ToDateTime(JoiningDate);
                if (Emp.JoiningSalary != null)
                {
                    coreCom.JoiningSalary= Convert.ToDecimal(Emp.JoiningSalary);
                }
                else
                {
                    coreCom.JoiningSalary= 0;
                }
                coreCom.ProbationPeriodType = "";
                coreCom.ProbationPeriod = "";               
                coreCom.LUser= LoginEmployeeID;
                coreCom.LDate = DateTime.Now;
                
                coreCom.CompanyCode_Session = "001";
                coreCom.UserInfoEmployeeID = LoginEmployeeID;                
                db.HRM_EmployeeOfficialInfo.Add(coreCom);
                db.SaveChanges();
                return RedirectToAction("Index", "EmployeeOfficial");

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
                Item.CurrencyCode = "";
                Item.PaymentPeriodID = "";
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
                Item.JoiningDate = Convert.ToDateTime(JoiningDate);
                if (Emp.JoiningSalary != null)
                {
                    Item.JoiningSalary = Convert.ToDecimal(Emp.JoiningSalary);
                }
                else
                {
                    Item.JoiningSalary = 0;
                }
                Item.ProbationPeriodType = "";
                Item.ProbationPeriod = "";
                Item.LUser = LoginEmployeeID;
                Item.LDate = DateTime.Now;
                
                Item.CompanyCode_Session = "001";
                Item.UserInfoEmployeeID = LoginEmployeeID;
                db.SaveChanges();

                return RedirectToAction("Index", "EmployeeOfficial");
            }

        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            empModel.DeleteCompany(id);
            return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);

        }

    }
}