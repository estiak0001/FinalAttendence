using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class HolidayController : Controller
    {

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_ATD_Holiday crud = new Crud_HRM_ATD_Holiday();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Vew_Holiday(string id)
        {
            ViewBag.LoadHolidayType = new SelectList(db.HRM_ATD_HolidayType, "HolidayType", "HolidayTypeName");
            if (id == null)
            {                
                common.FindMaxNoAuto(ref strMaxNO, "HolidayCode", "HRM_ATD_Holiday");               
                ViewBag.MaxComID = strMaxNO.ToString();
                return View();
            }
            else
            {
                var result = crud.GetInfo(id);
                ViewBag.MaxComID = result.HolidayCode.ToString();                
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult Vew_Holiday(Model_HRM_ATD_Holiday Model)
        {

            var Item = db.HRM_ATD_Holiday.FirstOrDefault(x => x.HolidayCode == Model.HolidayCode);
            if (Item == null)
            {
                DateTime ModelHolidayDate = new DateTime();
                ModelHolidayDate = DateTime.ParseExact(Model.FromDate, "dd/MM/yyyy", null);
                if (db.HRM_ATD_Holiday.Any(k => k.FromDate == ModelHolidayDate))
                {
                    return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string LoginEmployeeID = Session["EmployeeID"].ToString();
                    crud.SaveInfo(Model, LoginEmployeeID);
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                DateTime ModelHolidayDate = new DateTime();
                ModelHolidayDate = DateTime.ParseExact(Model.FromDate, "dd/MM/yyyy", null);
                if (db.HRM_ATD_Holiday.Any(x => x.FromDate == ModelHolidayDate && x.HolidayCode != Model.HolidayCode))
                {
                    return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                  
                    crud.UpdateInfo(Model.HolidayCode, Model);
                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getSingleData(string HolidayCode)
        {

            var result = crud.GetInfo(HolidayCode);
            ViewBag.MaxComID = result.HolidayCode.ToString();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult showInfo()
        {
            var resutl = crud.GetAllInfo();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MaxID()
        {
            common.FindMaxNoAuto(ref strMaxNO, "HolidayCode", "HRM_ATD_Holiday");
            string MaxID = strMaxNO.ToString();
            return Json(MaxID);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {

            crud.DeleteInfo(id);

            return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult DeleteMultiData(Model_HRM_ATD_Holiday Model)
        {
            var data = Json("");

            foreach (var item2 in Model.AllID)
            {

                var Item = db.HRM_ATD_Holiday.FirstOrDefault(x => x.HolidayCode == item2.HolidayCode);
                {
                    if (Item == null)
                    {
                        data = Json(new { success = false, message = "No Valid data selected!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        crud.DeleteInfo(item2.HolidayCode);
                        data = Json(new { success = true, message = "Data deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return data;
        }

        [HttpPost]
        public JsonResult CheckUsername(string HolidayCode, string HolidayDate)
        {
            var ExistUserName = db.HRM_ATD_Holiday.Where(x => x.FromDate == Convert.ToDateTime(HolidayDate)).FirstOrDefault();
            if (ExistUserName != null)
            {
                var ExistUserName1 = db.HRM_ATD_Holiday.Where(x => x.FromDate == Convert.ToDateTime(HolidayDate) && x.HolidayCode == HolidayCode).FirstOrDefault();
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