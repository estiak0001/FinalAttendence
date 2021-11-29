using BusinessLogic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class PartialMenuController : Controller
    {
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        // GET: PartialMenu
        public ActionResult showMenu()
        {
            if (Session["EmployeeID"] != null)
            {
                string AccessCodeID = Session["AccessCode"].ToString();
                var result = db.Core_AccessCode2.Where(x => x.AccessCodeID == AccessCodeID && x.TitleCheck=="Y").OrderBy(x => x.MenuId).ToList();
                Session["MenuList"] = result;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}