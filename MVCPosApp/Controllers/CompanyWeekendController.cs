using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class CompanyWeekendController : Controller
    {
        
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_CompanyWeekend crud = new Crud_CompanyWeekend();

        ClsCommon common = new ClsCommon();
        string strMaxNO = "";


        // GET: CompanyWeekend
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult MaxID()
        {
            common.FindMaxNo(ref strMaxNO, "CompanyWeekEndCode", "HRM_ATD_CompanyWeekEnd", 0);
            string MaxID = strMaxNO.ToString();
            return Json(MaxID);
        }

        [HttpGet]
        public ActionResult View_CompanyWeekend(string id)
        {
            
            if (id == null)
            {
                common.FindMaxNo(ref strMaxNO, "CompanyWeekEndCode", "HRM_ATD_CompanyWeekEnd", 0);
                ViewBag.MaxWeekendID = strMaxNO.ToString();
                return View();
            }
            else
            {
                var result = crud.GetInfo(id);
                ViewBag.MaxComID = result.CompanyWeekEndCode.ToString();
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult View_CompanyWeekend(Model_CompannyWeekend Model)
        {            
            var str = string.Join(",", Model.WeekendLists.Select(x => x.CompanyWeekEndCode).ToArray());
            Model.Weekend = str;
            
            var Item = db.HRM_ATD_CompanyWeekEnd.FirstOrDefault(x => x.CompanyWeekEndCode == Model.CompanyWeekEndCode);
            if (Item == null)
            {
                common.FindMaxNo(ref strMaxNO, "CompanyWeekEndCode", "HRM_ATD_CompanyWeekEnd", 0);
                Model.CompanyWeekEndCode = strMaxNO.ToString();

                if (db.HRM_ATD_CompanyWeekEnd.Any(k => k.Weekend == Model.Weekend))
                {
                    return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    crud.SaveInfo(Model);
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                if (db.HRM_ATD_CompanyWeekEnd.Any(x => x.Weekend == Model.Weekend && x.CompanyWeekEndCode != Model.CompanyWeekEndCode))
                {
                    return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    crud.UpdateInfo(Model.CompanyWeekEndCode, Model);

                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }


            }
        }
        public JsonResult getSingleData(string CompanyWeekendCode)
        {

            var result = crud.GetInfo(CompanyWeekendCode);
            ViewBag.MaxComID = result.CompanyWeekEndCode.ToString();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult showInfo()
        {
            var resutl = crud.GetAllInfo();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMultiData(Model_CompannyWeekend Model)
        {
            var data = Json("");

            foreach (var item2 in Model.WeekendLists)
            {

                var Item = db.HRM_ATD_CompanyWeekEnd.FirstOrDefault(x => x.CompanyWeekEndCode == item2.CompanyWeekEndCode);
                {
                    if (Item == null)
                    {
                        data = Json(new { success = false, message = "No Valid data selected!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        crud.DeleteInfo(item2.CompanyWeekEndCode);
                        data = Json(new { success = true, message = "Data deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return data;
        }
    }
}