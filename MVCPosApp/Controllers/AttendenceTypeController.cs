using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class AttendenceTypeController : Controller
    {

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_ATD_AttendanceType crud = new Crud_HRM_ATD_AttendanceType();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AddOrEdit(string id)
        {
            if (id == null)
            {
                common.FindMaxNoAuto(ref strMaxNO, "AttendanceTypeCode", "HRM_ATD_AttendanceType");
                ViewBag.MaxComID = strMaxNO.ToString();
                return View();
            }
            else
            {

                var result = crud.GetInfo(id);
                ViewBag.MaxComID = result.AttendanceTypeCode.ToString();
                return View(result);

            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(Model_HRM_ATD_AttendanceType Model)
        {

            var Item = db.HRM_ATD_AttendanceType.FirstOrDefault(x => x.AttendanceTypeCode == Model.AttendanceTypeCode);
            if (Item == null)
            {
                if (db.HRM_ATD_AttendanceType.Any(k => k.AttendanceTypeName == Model.AttendanceTypeName))
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
                if (db.HRM_ATD_AttendanceType.Any(x => x.AttendanceTypeName == Model.AttendanceTypeName && x.AttendanceTypeCode != Model.AttendanceTypeCode))
                {
                    return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    crud.UpdateInfo(Model.AttendanceTypeCode, Model);
                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getSingleData(string AttendanceTypeCode)
        {

            var result = crud.GetInfo(AttendanceTypeCode);
            ViewBag.MaxComID = result.AttendanceTypeCode.ToString();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult showInfo()
        {
            var resutl = crud.GetAllInfo();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MaxID()
        {
            common.FindMaxNoAuto(ref strMaxNO, "AttendanceTypeCode", "HRM_ATD_AttendanceType");
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
        public ActionResult DeleteMultiData(Model_HRM_ATD_AttendanceType Model)
        {
            var data = Json("");

            foreach (var item2 in Model.AllID)
            {

                var Item = db.HRM_ATD_AttendanceType.FirstOrDefault(x => x.AttendanceTypeCode == item2.AttendanceTypeCode);
                {
                    if (Item == null)
                    {
                        data = Json(new { success = false, message = "No Valid data selected!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        crud.DeleteInfo(item2.AttendanceTypeCode);
                        data = Json(new { success = true, message = "Data deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return data;
        }

        [HttpPost]
        public JsonResult CheckUsername(string AttendanceTypeCode, string AttendanceTypeName)
        {
            var ExistUserName = db.HRM_ATD_AttendanceType.Where(x => x.AttendanceTypeName == AttendanceTypeName).FirstOrDefault();
            if (ExistUserName != null)
            {
                var ExistUserName1 = db.HRM_ATD_AttendanceType.Where(x => x.AttendanceTypeName == AttendanceTypeName && x.AttendanceTypeCode == AttendanceTypeCode).FirstOrDefault();
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