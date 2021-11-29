using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class AccessCodeController : Controller
    {
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_Core_AccessCode2 crud = new Crud_Core_AccessCode2();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult MaxID()
        {
            common.FindMaxNo(ref strMaxNO, "AccessCodeID", "Core_AccessCode2", 3);
            string MaxID = strMaxNO.ToString();
            return Json(MaxID);
        }
        public ActionResult View_AccessCode(string id)
        {
            common.FindMaxNo(ref strMaxNO, "AccessCodeID", "Core_AccessCode2", 3);
            ViewBag.MaxComID = strMaxNO.ToString();
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


        public JsonResult InsertAccessCode(Core_AccessCode2[] list)
        {
            var AccessCodeID = Session["AccessCode"].ToString();
            var check = db.Core_AccessCode2.FirstOrDefault(x => x.AccessCodeID == AccessCodeID && x.title == "User Access" && x.chkAdd == "Y");
            if (check != null)
            {
                var exisAccesscode = "";
                foreach (Core_AccessCode2 i in list)
                {
                    exisAccesscode = i.AccessCodeID;
                }
                var result = db.Core_AccessCode2.Where(x => x.AccessCodeID == exisAccesscode).ToList();
                if (result != null)
                {
                    db.Core_AccessCode2.RemoveRange(result);
                    db.SaveChanges();                 
                }
                foreach (Core_AccessCode2 i in list)
                {
                    db.Core_AccessCode2.Add(i);
                }
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, message = "You Have No Access" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getSingleData(string AccessCodeID)
        {
            var result = crud.GetInfo(AccessCodeID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingleDataGrid(string AccessCodeID)
        {
            var resutl = crud.GetAccessCodeGrid(AccessCodeID);           
            return Json(resutl, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult showInfo()
        {
            var resutl = crud.GetAllInfo();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetManuTabInfo()
        {
            var resutl = crud.GetAllMenutab();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string AccessCodeID)
        {
            var SessionAccess= Session["AccessCode"].ToString();
            var check = db.Core_AccessCode2.FirstOrDefault(x => x.AccessCodeID == SessionAccess && x.title == "User Access" && x.chkDelete == "Y");
            if (check != null)
            {
                crud.DeleteInfo(AccessCodeID);
                return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, message = "You Have No Access" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}