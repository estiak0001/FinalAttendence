using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class ShiftController : Controller
    {

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_ATD_Shift crud = new Crud_HRM_ATD_Shift();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Vw_Shift(string id)
        {
            if (id == null)
            {
                common.FindMaxNoAuto(ref strMaxNO, "ShiftCode", "HRM_ATD_Shift");
                ViewBag.MaxComID = strMaxNO.ToString();
                return View();
            }
            else
            {
                var result = crud.GetInfo(id);
                ViewBag.MaxComID = result.ShiftCode.ToString();
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult Vw_Shift(Model_HRM_ATD_Shift Model)
        {

            var Item = db.HRM_ATD_Shift.FirstOrDefault(x => x.ShiftCode == Model.ShiftCode);
            if (Item == null)
            {
                if (db.HRM_ATD_Shift.Any(k => k.ShiftName == Model.ShiftName))
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
                if (db.HRM_ATD_Shift.Any(x => x.ShiftName == Model.ShiftName && x.ShiftCode != Model.ShiftCode))
                {
                    return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    crud.UpdateInfo(Model.ShiftCode, Model);
                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getSingleData(string ShiftCode)
        {

            var result = crud.GetInfo(ShiftCode);
            ViewBag.MaxComID = result.ShiftCode.ToString();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult showInfo()
        {
            var resutl = crud.GetAllInfo();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MaxID()
        {
            common.FindMaxNoAuto(ref strMaxNO, "ShiftCode", "HRM_ATD_Shift");
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
        public ActionResult DeleteMultiData(Model_HRM_ATD_Shift Model)
        {
            var data = Json("");

            foreach (var item2 in Model.AllID)
            {

                var Item = db.HRM_ATD_Shift.FirstOrDefault(x => x.ShiftCode == item2.ShiftCode);
                {
                    if (Item == null)
                    {
                        data = Json(new { success = false, message = "No Valid data selected!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        crud.DeleteInfo(item2.ShiftCode);
                        data = Json(new { success = true, message = "Data deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return data;
        }
        [HttpPost]
        public JsonResult CheckUsername(string ShiftCode, string ShiftName)
        {
            var ExistUserName = db.HRM_ATD_Shift.Where(x => x.ShiftName == ShiftName).FirstOrDefault();
            if (ExistUserName != null)
            {
                var ExistUserName1 = db.HRM_ATD_Shift.Where(x => x.ShiftName == ShiftName && x.ShiftCode == ShiftCode).FirstOrDefault();
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