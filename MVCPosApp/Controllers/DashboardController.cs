using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class DashboardController : Controller
    {
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_Dashboard Crud = new Crud_Dashboard();
        Crud_HRM_LeaveApplicationEntry crud = new Crud_HRM_LeaveApplicationEntry();
        // GET: Dashboard
        public ActionResult Index(string empID)
        {
            string AccessCode= Session["AccessCode"].ToString();
            if (AccessCode == "001" && empID == null)
            {
                return RedirectToAction("AdminIndex", "Dashboard");
            }
            else
            {
                if (empID != null)
                {
                    ViewBag.LoadEmpID = empID;
                }
                else
                {
                    ViewBag.LoadEmpID = Session["EmployeeID"].ToString();
                }
                
                ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                         => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
                 "EmployeeID", "FirstName");
                ViewBag.Department = new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
                return View();
            }  
            
        }
        public ActionResult AdminIndex()
        {
            return View();
        }
        public ActionResult showEmployeeBasiciInfo(string EmployeeID)
        {
            var result = Crud.GetInfo(EmployeeID);
            return Json(result, JsonRequestBehavior.AllowGet);         
        }
        public ActionResult ShowEmployeeInList(string AttendenceDate)
        {
            //var datetimepresent = DateTime.Now.ToString("yyyy-MM-dd");
            var datetimepresent = AttendenceDate;
            var result = Crud.GetEmployeeInTimeList(datetimepresent);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowEmpDateTimeStatisitic(string AttendenceDate)
        {
            var EmployeeID = Session["EmployeeID"].ToString();
            var DepartmentCode = "";
            var result = Crud.GetEmployeeDailyStatistics(AttendenceDate, DepartmentCode, EmployeeID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowEmployeeLeaveBalance(string EmployeeID)
        {
            string AccessCode = Session["AccessCode"].ToString();
            if (AccessCode == "001")
            {
                var result = crud.GetEmployeeLeaveStatus(EmployeeID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var empID = Session["EmployeeID"].ToString();
            var resutl2 = crud.GetEmployeeLeaveStatus(empID);
            return Json(resutl2, JsonRequestBehavior.AllowGet);
        }
    }
}