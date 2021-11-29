using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class AttendenceDetailsController : Controller
    {
        // GET: DaillyAttendence
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        public ActionResult View_AttendenceDetails()
        {
            ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            return View();
            

                
        }
    }
}