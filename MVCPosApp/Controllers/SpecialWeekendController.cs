using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class SpecialWeekendController : Controller
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
            common.FindMaxNo(ref strMaxNO, "EmployeeWeekEndCode", "HRM_ATD_EmployeeWeekEnd", 0);
            string MaxID = strMaxNO.ToString();
            return Json(MaxID);
        }

        [HttpGet]
        public ActionResult View_SpecialWeekend(string id)
        {
            ViewBag.LoadCompany = new SelectList(db.Core_Company, "CompanyCode", "CompanyName");
            ViewBag.LoadDepartment = new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
            ViewBag.LoadDesignation = new SelectList(db.HRM_Def_Designation, "DesignationCode", "DesignationName");
            ViewBag.LoadBranch = new SelectList(db.Core_Branch, "BranchCode", "BranchName");

            if (id == null)
            {
                common.FindMaxNo(ref strMaxNO, "EmployeeWeekEndCode", "HRM_ATD_EmployeeWeekEnd", 0);
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
        public ActionResult View_SpecialWeekend(Model_CompannyWeekend Model)
        {
            var str = string.Join(",", Model.WeekendLists.Select(x => x.CompanyWeekEndCode).ToArray());
            Model.Weekend = str;

            foreach (var item in Model.Empids)
            {
                //var Itemm = db.HRM_ATD_EmployeeWeekEnd.FirstOrDefault(x => x.EmployeeCode == item.EmployeeID);
                //if (Itemm != null)
                //{
                //    crud.DeleteExistInfo(item.EmployeeID);
                //}
                //common.FindMaxNo(ref strMaxNO, "EmployeeWeekEndCode", "HRM_ATD_EmployeeWeekEnd", 0);
                //Model.CompanyWeekEndCode = strMaxNO.ToString();

                crud.SWeekSaveInfo(Model, item.EmployeeID);
                //if (db.HRM_ATD_CompanyWeekEnd.Any(k => k.Weekend == Model.Weekend))
                //{
                //    return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{

                //}
            }
            return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            //return Json(new { success = false, message = "Something Wrong!" }, JsonRequestBehavior.AllowGet);
            //    var Item = db.HRM_ATD_EmployeeWeekEnd.FirstOrDefault(x => x.EmployeeCode == Model.Empids);
            //if (Item == null)
            //{
            //    common.FindMaxNo(ref strMaxNO, "EmployeeWeekEndCode", "HRM_ATD_EmployeeWeekEnd", 0);
            //    Model.CompanyWeekEndCode = strMaxNO.ToString();

            //    if (db.HRM_ATD_CompanyWeekEnd.Any(k => k.Weekend == Model.Weekend))
            //    {
            //        return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            //        crud.SWeekSaveInfo(Model);
            //        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            //else
            //{

            //    if (db.HRM_ATD_EmployeeWeekEnd.Any(x => x.WeekendDay == Model.Weekend && x.EmployeeWeekEndCode != Model.CompanyWeekEndCode))
            //    {
            //        return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            //        crud.UpdateInfo(Model.CompanyWeekEndCode, Model);

            //        return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
            //    }


            //}
        }
        public JsonResult getSingleData(string CompanyWeekendCode)
        {

            var result = crud.GetInfo(CompanyWeekendCode);
            ViewBag.MaxComID = result.CompanyWeekEndCode.ToString();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult showInfo()
        {
            var resutl = crud.GetSAllInfo();
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