using BusinessLogic;
using BusinessLogic.Repository;
using PXLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCPosApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        PXlibrary Pxlib = new PXlibrary();

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Model_UserInfo ms)
        {
            if (ModelState.IsValid)
            {
                Session["EmployeeID"] = null;
                Session["username"] = null;
                Session["AccessCode"] = null;

                string password = "";
                Pxlib.PXEncode(ref password, ms.UserPassword);
                //var v = db.Core_UserInfo.Where(a => a.username.Equals(ms.username) && a.uuser.Equals(ms.UserPassword)).FirstOrDefault();
                var v = db.Core_UserInfo.Where(a => a.username.Equals(ms.username) && a.password.Equals(password)).FirstOrDefault();
                if (v != null)
                {
                    Session["EmployeeID"] = v.EmployeeID;
                    Session["username"] = v.username;
                    Session["AccessCode"] = v.AccessCode;
                    if(v.AccessCode=="001")
                    {
                        return RedirectToAction("AdminIndex", "Dashboard");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid User Name or Password!");
                    //ViewBag.errorMessage = "Error: " + ex.Message + " - " + ex.InnerException;
                    return View();
                }
            }
            ModelState.AddModelError(string.Empty, "Please enter username and password!");

            return View();

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["EmployeeID"] = null;
            Session["username"] = null;
            Session["AccessCode"] = null;
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ErrorPage()
        { 
            return View();
        }
    }
}