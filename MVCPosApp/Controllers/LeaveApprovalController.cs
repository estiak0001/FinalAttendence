using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class LeaveApprovalController : Controller
    {
        // GET: ManualAttendence

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_LeaveApprovalEntry crud = new Crud_HRM_LeaveApprovalEntry();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult View_LeaveApproval(string id)
        {
            var HRApprovalStatus = new List<Model_SelectType>();
            HRApprovalStatus.Add(new Model_SelectType() { Value = "Approved", Text = "Approved" });
            HRApprovalStatus.Add(new Model_SelectType() { Value = "Canceled", Text = "Canceled" });

            ViewBag.LoadHRApprovalStatus = new SelectList(HRApprovalStatus, "Value", "Text");

            var LeaveFormatType = new List<Model_SelectType>();
            LeaveFormatType.Add(new Model_SelectType() { Value = "FullLeave", Text = "Full Leave" });

            ViewBag.LoadLeaveFormat = new SelectList(LeaveFormatType, "Value", "Text");


            ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                          => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
                  "EmployeeID", "FirstName");

            ViewBag.LoadDepartment = new SelectList(db.HRM_Def_Department.ToList().Select(u
                          => new { DepartmentName = u.DepartmentName, DepartmentCode = u.DepartmentCode }),
                  "DepartmentCode", "DepartmentName");

            ViewBag.LoadCompany = new SelectList(db.Core_Company.ToList().Select(u
                          => new { CompanyCode = u.CompanyCode, CompanyName = u.CompanyName }),
                  "CompanyCode", "CompanyName");

            ViewBag.LoadIS = new SelectList(db.HRM_Employee.ToList().Select(u
                           => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
                  "EmployeeID", "FirstName");

            ViewBag.LoadHOD = new SelectList(db.HRM_Employee.ToList().Select(u
                           => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
                  "EmployeeID", "FirstName");
            ViewBag.LoadLeaveType = new SelectList(db.HRM_ATD_LeaveType, "LeaveTypeId", "Name");


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
        public ActionResult View_LeaveApproval(Model_HRM_LeaveApplicationEntry Model)
        {
            var Item = db.HRM_LeaveApplicationEntry.FirstOrDefault(x => x.LeaveAppEntryId == Model.LeaveAppEntryId);
            if (Item != null)
            {
                crud.UpdateInfo(Model.LeaveAppEntryId, Model);
                return Json(new { success = true, message = "Approve Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Something wrong!" }, JsonRequestBehavior.AllowGet);

            }
        }
       
        public ActionResult ApproveByMail(string id)
        {
            var Item = db.HRM_LeaveApplicationEntry.FirstOrDefault(x => x.LeaveAppEntryId == id);
            if (Item != null)
            {
                if(Item.HRApprovalStatus == "Pending")
                {
                    Model_HRM_LeaveApplicationEntry mm = new Model_HRM_LeaveApplicationEntry();
                    mm.LeaveAppEntryId = id;
                    mm.HRApprovalStatus = "Approved";
                    mm.HRApprovalRemarks = "Your Leave Approved.";
                    crud.UpdateInfo(id, mm);
                    return PartialView("_AlertView");
                }
                else
                {
                    return PartialView("_AlertView2");
                }
                
            }
            else
            {
                return PartialView("_AlertView2");

            }
        }
        
        public ActionResult RejectByMail(string id)
        {
            var Item = db.HRM_LeaveApplicationEntry.FirstOrDefault(x => x.LeaveAppEntryId == id);
            if (Item != null)
            {
                if (Item.HRApprovalStatus == "Pending")
                {
                    Model_HRM_LeaveApplicationEntry mm = new Model_HRM_LeaveApplicationEntry();
                    mm.LeaveAppEntryId = id;
                    mm.HRApprovalStatus = "Cancelled";
                    mm.HRApprovalRemarks = "Your Leave Rejected.";
                    crud.UpdateInfo(id, mm);
                    return PartialView("_AlertView");
                }
                else
                {
                    return PartialView("_AlertView2");
                }
                
            }
            else
            {
                return PartialView("_AlertView");

            }
        }
        
        public JsonResult getSingleData(string LeaveAppEntryId)
        {
            var result = crud.GetInfo(LeaveAppEntryId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSingleDataApproved(string LeaveAppEntryId)
        {
            var result = crud.GetApprovedInfo(LeaveAppEntryId);
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

        public ActionResult ShowApproveInfo(string CompanyCode, string DepartmentCode, string EmployeeID)
        {
            var resutl = crud.GetAllApproveInfo(CompanyCode, DepartmentCode, EmployeeID);
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ViewPartialPopup(string LeaveAppEntryId)
        {
           
            Model_HRM_LeaveApplicationEntry result = crud.GetPopupLoadInfo(LeaveAppEntryId);
            HRM_LeaveApplicationEntry leaveInfo = (HRM_LeaveApplicationEntry)db.HRM_LeaveApplicationEntry.Where(x=> x.LeaveAppEntryId == LeaveAppEntryId).FirstOrDefault();
            if (leaveInfo == null)
            {
                return HttpNotFound();
            }            
            return PartialView("_ViewPartialPopup", result);
        }
        [HttpGet]
        public ActionResult ViewPartialPopup2(string LeaveAppEntryId, string status)
        {
            Model_HRM_LeaveApplicationEntry result = crud.GetPopupLoadInfo(LeaveAppEntryId);
            result.HRApprovalStatus = status;
            
            HRM_LeaveApplicationEntry leaveInfo = (HRM_LeaveApplicationEntry)db.HRM_LeaveApplicationEntry.Where(x => x.LeaveAppEntryId == LeaveAppEntryId).FirstOrDefault();
            if (leaveInfo == null)
            {
                return HttpNotFound();
            }
            if (leaveInfo.HRApprovalStatus != "Pending")
            {
                return Json(new { success = false, message = "Action submitted you cant change it now!" }, JsonRequestBehavior.AllowGet);
            }
            return PartialView("_ViewPartialPopup2", result);
        }

    }
}