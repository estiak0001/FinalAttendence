using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Repository;
using System.Data.Entity;


namespace MVCPosApp.Controllers
{

    public class CompanyController : Controller
    {

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crd_Core_Company crud = new Crd_Core_Company();

        ClsCommon common = new ClsCommon();
        string strMaxNO = "";

        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult View_Company(string id)
        {
            if (id == null)
            {
                common.FindMaxNo(ref strMaxNO, "CompanyCode", "Core_Company", 3);
                ViewBag.MaxComID = strMaxNO.ToString();
                return View();
            }
            else
            {
                var result = crud.GetInfo(id);
                ViewBag.MaxComID = result.CompanyCode.ToString();
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult View_Company(Model_Core_Company Model)
        {

             var Item = db.Core_Company.FirstOrDefault(x => x.CompanyCode == Model.CompanyCode);
            if (Item == null)
            {
                var cou = db.Core_Company.SqlQuery(" SELECT * FROM Core_Company").Count();

                if (db.Core_Company.Any(k => k.CompanyName == Model.CompanyName))
                {
                    return Json(new { success = false, message = "Already Exists!" }, JsonRequestBehavior.AllowGet);
                }

                //else if(cou == 1)
                //{
                //    return Json(new { success = false, message = "You can add only one company!" }, JsonRequestBehavior.AllowGet);
                //}

                else
                {
                    crud.SaveInfo(Model);
                    return Json(new { success = true, message = "Data saved successfully!" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                if (db.Core_Company.Any(x => x.CompanyName == Model.CompanyName && x.CompanyCode != Model.CompanyCode))
                {
                    return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    crud.UpdateInfo(Model.CompanyCode, Model);

                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }


            }
        }

        public JsonResult getSingleData(string CompanyCode)
        {
            var result = crud.GetInfo(CompanyCode);
            ViewBag.MaxComID = result.CompanyCode.ToString();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult showInfo()
        {
            var resutl = crud.GetAllInfo();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MaxID()
        {
            common.FindMaxNo(ref strMaxNO, "CompanyCode", "Core_Company", 3);
            string MaxID = strMaxNO.ToString();
            return Json(MaxID);
        }

        //[HttpPost]
        //public ActionResult Delete(string id)
        //{
        //    crud.DeleteInfo(id);
        //    return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult DeleteMultiData(Model_Core_Company Model)
        {
            var data = Json("");

            foreach (var item2 in Model.AllID)
            {

                var Item = db.Core_Company.FirstOrDefault(x => x.CompanyCode == item2.CompanyCode);
                {
                    if (Item == null)
                    {
                        data = Json(new { success = false, message = "No Valid data selected!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        crud.DeleteInfo(item2.CompanyCode);
                        data = Json(new { success = true, message = "Data deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return data;
        }

        [HttpPost]
        public JsonResult CheckUsername(string CompanyCode, string CompanyName)
        {
            var ExistUserName = db.Core_Company.Where(x => x.CompanyName == CompanyName).FirstOrDefault();
            if (ExistUserName != null)
            {
                var ExistUserName1 = db.Core_Company.Where(x => x.CompanyName == CompanyName && x.CompanyCode == CompanyCode).FirstOrDefault();
                if (ExistUserName1 != null)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            else
            {
                return Json(1);

            }
        }
    }
}