using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class LeaveTypeController : Controller
    {
        // GET: ManualAttendence

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_ATD_LeaveType crud = new Crud_HRM_ATD_LeaveType();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MaxNo()
        {
            common.FindMaxNoAuto(ref strMaxNO, "LeaveTypeId", "HRM_ATD_LeaveType");
            string MaxID = strMaxNO.ToString();
            return Json(MaxID); 

        }
        public ActionResult Vew_LeaveType(string id)
        {
            
            ViewBag.LoaddurationType = new SelectList(db.Acc_Duration_Type, "Duration_TypeCode", "durationType");

            common.FindMaxNoAuto(ref strMaxNO, "LeaveTypeId", "HRM_ATD_LeaveType");
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
        public ActionResult Vew_LeaveType(Model_HRM_ATD_LeaveType Model)
        {                
                var Item = db.HRM_ATD_LeaveType.FirstOrDefault(x => x.LeaveTypeId == Model.LeaveTypeId);
                if (Item == null)
                {
                    string LoginEmployeeID = Session["EmployeeID"].ToString();
                    crud.SaveInfo(Model, LoginEmployeeID);
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    crud.UpdateInfo(Model.LeaveTypeId, Model);
                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }            
        }

        public JsonResult getSingleData(string LeaveTypeId)
        {
            var result = crud.GetInfo(LeaveTypeId);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetEmployeeInfo(string EmployeeID)
        //{
        //    var result = crud.GetEmployeeInfo(EmployeeID); 
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult showInfo()
        {
            var resutl = crud.GetAllInfo();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult Delete(string LeaveTypeId)
        {
            crud.DeleteInfo(LeaveTypeId);
            return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMultiData(Model_HRM_ATD_LeaveType Model)
        {
            var data = Json("");

            foreach (var item2 in Model.AllID)
            {

                var Item = db.HRM_ATD_LeaveType.FirstOrDefault(x => x.LeaveTypeId == item2.LeaveTypeId);
                {
                    if (Item == null)
                    {
                        data = Json(new { success = false, message = "No Valid data selected!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        crud.DeleteInfo(item2.LeaveTypeId);
                        data = Json(new { success = true, message = "Data deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return data;
        }
    }
}                                