using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class RosterScheduleEntryController : Controller
    {
        // GET: RosterScheduleEntry

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_ATD_RosterScheduleEntry crud = new Crud_HRM_ATD_RosterScheduleEntry();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MaxNo()
        {
            common.FindMaxNoAuto(ref strMaxNO, "RosterScheduleId", "HRM_RosterScheduleEntry");
            string MaxID = strMaxNO.ToString();
            return Json(MaxID);
        }
        public ActionResult View_RosterScheduleEntry(string id)
        {
            ViewBag.LoadCompany = new SelectList(db.Core_Company, "CompanyCode", "CompanyName");
            ViewBag.Branch = new SelectList(db.Core_Branch, "BranchCode", "BranchName");
            ViewBag.LoadDepartment = new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
            ViewBag.LoadDesignation = new SelectList(db.HRM_Def_Designation, "DesignationCode", "DesignationName");
            ViewBag.LoadShift = new SelectList(db.HRM_ATD_Shift, "ShiftCode", "ShiftName");
            ViewBag.LoadCompWeekend = new SelectList(db.HRM_ATD_CompanyWeekEnd, "CompanyWeekEndCode", "Weekend");


            common.FindMaxNoAuto(ref strMaxNO, "RosterScheduleId", "HRM_RosterScheduleEntry");
            ViewBag.MaxComID = strMaxNO.ToString();
            if (id == null)
            {
                return View();
            }
            else
            {
                var result = "";
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult View_RosterScheduleEntry(Model_HRM_ATD_RosterScheduleEntry Model)
        {
            string LoginEmployeeID = Session["EmployeeID"].ToString();
            string LoginCompanyCode = "001";
            string[] spl = Model.FromDate.Split('/');
            DateTime fromdate = Convert.ToDateTime(spl[2] + "-" + spl[1] + "-" + spl[0]);
            string[] spl2 = Model.ToDate.Split('/');
            DateTime todate = Convert.ToDateTime(spl2[2] + "-" + spl2[1] + "-" + spl2[0]);
            //List<DateTime> list = Enumerable.Range(1, DateTime.DaysInMonth(2020, 7))
            //      .Select(day => new DateTime(2020, 7, todate.Day))
            //      .ToList();

            List<DateTime> list = Enumerable.Range(0, todate.Subtract(fromdate).Days + 1)
                    .Select(d => fromdate.AddDays(d)).ToList();
            var data = Json("");

            string remark = "";
            if (Model.Remark == null)
            {
                remark = " ";
            }
            else
            {
                remark = Model.Remark;
            }


            foreach (var item2 in Model.RsemployeeID)
            {
                foreach (var item in list)
                {
                    var Item = db.HRM_RosterScheduleEntry.FirstOrDefault(x => x.EmployeeID == item2.EmployeeID && x.Date == item.Date);
                    if (Item == null)
                    {
                        try
                        {
                             common.FindMaxNoAuto(ref strMaxNO, "RosterScheduleId", "HRM_RosterScheduleEntry");
                            string MaxID = strMaxNO.ToString();
                            //var context = new GCTL_ERP_DB_MVC_06_27Entities();
                            Model_HRM_RosterScheduleEntry2 coreCom = new Model_HRM_RosterScheduleEntry2();
                            coreCom.RosterScheduleId = MaxID;
                            coreCom.EmployeeID = item2.EmployeeID;
                            coreCom.Date = item.Date;
                            coreCom.ShiftCode = Model.ShiftCode;
                            coreCom.Weekend = "01";
                            coreCom.Remark = remark;
                            coreCom.LUser = LoginEmployeeID;
                            coreCom.LDate = DateTime.Now;
                            coreCom.LIP = "";
                            coreCom.LMAC = "";
                            coreCom.ModifyDate = DateTime.Now;
                            coreCom.CompanyCode = LoginCompanyCode;
                            coreCom.EmployeeID_SAO = "0004";
                            crud.SaveInfo(coreCom, LoginEmployeeID);
                            data = Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        catch(Exception ex)
                        {
                            data = Json(new { success = true, message = "Data Not Saved Successfully" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //crud.DeleteExistInfo(Model.FromDate, Model.ToDate);
                        crud.DeleteExistInfo(item2.EmployeeID, item.Date);
                        try
                        {
                            common.FindMaxNoAuto(ref strMaxNO, "RosterScheduleId", "HRM_RosterScheduleEntry");
                            string MaxID = strMaxNO.ToString();
                            //var context = new GCTL_ERP_DB_MVC_06_27Entities();
                            Model_HRM_RosterScheduleEntry2 coreCom = new Model_HRM_RosterScheduleEntry2();
                            coreCom.RosterScheduleId = MaxID;
                            coreCom.EmployeeID = item2.EmployeeID;
                            coreCom.Date = item.Date;
                            coreCom.ShiftCode = Model.ShiftCode;
                            coreCom.Weekend = "01";
                            coreCom.Remark = Model.Remark;
                            coreCom.LUser = LoginEmployeeID;
                            coreCom.LDate = DateTime.Now;
                            coreCom.LIP = "";
                            coreCom.LMAC = "";
                            coreCom.ModifyDate = DateTime.Now;
                            coreCom.CompanyCode = LoginCompanyCode;
                            coreCom.EmployeeID_SAO = "0004";
                            crud.SaveInfo(coreCom, LoginEmployeeID);
                            data = Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            return data;
        }

        public ActionResult LoadInfo(string CompanyCode, string DepartmentCode, string DesignationCode, string BranchCode)
        {
            var resutl = crud.GetAllInfo(CompanyCode, BranchCode, DepartmentCode, DesignationCode);
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowRosterEmployee(string CompanyCode, string DepartmentCode, string DesignationCode, string BranchCode)
        {
            var resutl = crud.GetRosterEmoloyee(CompanyCode, DepartmentCode, DesignationCode, BranchCode);
            var test = Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
            return test;
        }

        [HttpPost]
        public ActionResult Delete(Model_HRM_ATD_RosterScheduleEntry Model)
        {
            string[] spl = Model.FromDate.Split('/');
            DateTime fromdate = Convert.ToDateTime(spl[2] + "-" + spl[1] + "-" + spl[0]);
            string[] spl2 = Model.ToDate.Split('/');
            DateTime todate = Convert.ToDateTime(spl2[2] + "-" + spl2[1] + "-" + spl2[0]);

            List<DateTime> list = Enumerable.Range(0, todate.Subtract(fromdate).Days + 1)
                   .Select(d => fromdate.AddDays(d)).ToList();
            var data = Json("");

            foreach (var item2 in Model.RsemployeeID)
            {
                foreach (var item in list)
                {
                    var Item = db.HRM_RosterScheduleEntry.FirstOrDefault(x => x.EmployeeID == item2.EmployeeID && x.Date == item.Date);
                    {
                        if (Item == null)
                        {
                            data = Json(new { success = true, message = "No Valid data selected!" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            crud.DeleteExistInfo(item2.EmployeeID, item.Date);
                            data = Json(new { success = true, message = "Data deleted Successfully" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }     
            return data;
        }
    }
}