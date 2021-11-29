using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MVCPosApp.Controllers
{
    public class UserAccessController : Controller
    {
        
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_Core_UserInfo crud = new Crud_Core_UserInfo();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
       

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult View_Useracess(string id)
        {
            var RoleType= new List<Model_SelectType>();
            
            RoleType.Add(new Model_SelectType() { Value ="Admin", Text = "Admin" });
            RoleType.Add(new Model_SelectType() { Value = "User", Text = "User" });

            var SessionType = new List<Model_SelectType>();

            SessionType.Add(new Model_SelectType() { Value = "Multiple", Text = "Multiple" });
            SessionType.Add(new Model_SelectType() { Value = "Single", Text = "Single" });

            ViewBag.LoadRoleType = new SelectList(RoleType, "Value", "Text");
            ViewBag.LoadAccessCode = new SelectList(db.Core_AccessCode2.
                    Select(e => new { e.AccessCodeID, e.AccessCodeName })
                            .Distinct().AsEnumerable().Select(a => new Model_Core_AccessCode2()
                            {
                                AccessCodeID = a.AccessCodeID,
                                AccessCodeName = a.AccessCodeName
                            }).ToList(), "AccessCodeID", "AccessCodeName");

            ViewBag.SessionType = new SelectList(SessionType, "Value", "Text");

            ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                          => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                  "EmployeeID", "FirstName");

            ViewBag.Company = new SelectList(db.Core_Company.
                     Select(e => new { e.CompanyCode, e.CompanyName })
                             .Distinct().AsEnumerable().Select(a => new Model_SelectType()
                             {
                                 Value = a.CompanyCode,
                                 Text = a.CompanyName
                             }).ToList(), "Value", "Text");
            if (id == null)
            {
                return View();
            }
            else
            {
                var result = crud.GetInfo(id);
                return View(result);
            }

        }
        [HttpPost]
        public ActionResult View_Useracess(Model_Core_UserInfo Model)
        {
            var Item = db.Core_UserInfo.FirstOrDefault(x => x.EmployeeID == Model.EmployeeID);
            if (Item == null)
            {
                string LoginEmployeeID = Session["EmployeeID"].ToString();
                crud.SaveInfo(Model, LoginEmployeeID);
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                crud.UpdateInfo(Model.EmployeeID, Model);
                return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getSingleData(string Id)
        {
            var result = crud.GetInfo(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeeInfo(string EmployeeID)
        {
            var result = crud.GetEmployeeInfo(EmployeeID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult showInfo()
        {
            var resutl = crud.GetAllInfo();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string EmployeeID)
        {
            crud.DeleteInfo(EmployeeID);
            return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}