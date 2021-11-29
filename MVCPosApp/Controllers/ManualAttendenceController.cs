using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class ManualAttendenceController : Controller
    {
        // GET: ManualAttendence

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_ATD_Manual crud = new Crud_HRM_ATD_Manual();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MaxNo()
        {
            common.FindMaxNoAuto(ref strMaxNO, "ManualCode", "HRM_ATD_Manual");
            string MaxID = strMaxNO.ToString();
            return Json(MaxID);

        }
        public ActionResult View_ManualAttendence(string id)
        {
            ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                          => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                  "EmployeeID", "FirstName");
            ViewBag.LoadAttendenceType = new SelectList(db.HRM_ATD_AttendanceType, "AttendanceTypeCode", "AttendanceTypeName");
            common.FindMaxNoAuto(ref strMaxNO, "ManualCode", "HRM_ATD_Manual");
            ViewBag.MaxComID =strMaxNO.ToString();
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
        public ActionResult View_ManualAttendence(Model_HRM_ATD_Manual Model)
        {                
                var Item = db.HRM_ATD_Manual.FirstOrDefault(x => x.ManualCode == Model.ManualCode);
                if (Item == null)
                {
                    string LoginEmployeeID = Session["EmployeeID"].ToString();
                    crud.SaveInfo(Model, LoginEmployeeID);
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    crud.UpdateInfo(Model.ManualCode, Model);
                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }            
        }

        public JsonResult getSingleData(string ManualCode)
        {
            var result = crud.GetInfo(ManualCode);
            
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
        public ActionResult Delete(string ManualInOutID)
        {
            crud.DeleteInfo(ManualInOutID);
            return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteMultiData(Model_HRM_ATD_Manual Model)
        {
            var data = Json("");

            foreach (var item2 in Model.AllID)
            {

                var Item = db.HRM_ATD_Manual.FirstOrDefault(x => x.ManualCode == item2.ManualCode);
                {
                    if (Item == null)
                    {
                        data = Json(new { success = false, message = "No Valid data selected!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        crud.DeleteInfo(item2.ManualCode);
                        data = Json(new { success = true, message = "Data deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return data;
        }
    }
}