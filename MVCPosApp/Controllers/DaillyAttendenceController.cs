using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class DaillyAttendenceController : Controller
    {
        // GET: DaillyAttendence
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        public ActionResult View_DailyAttendence()
        {
            ViewBag.Department = new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
            return View();
        }
    }
}