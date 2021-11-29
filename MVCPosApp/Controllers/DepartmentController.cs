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

    public class DepartmentController : Controller
    {

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_Def_Department crud = new Crud_HRM_Def_Department();

        ClsCommon common = new ClsCommon();
        string strMaxNO = "";

        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult View_Department(string id)
        {
            if (id == null)
            {

                common.FindMaxNo(ref strMaxNO, "DepartmentCode", "HRM_Def_Department", 3);
                ViewBag.MaxComID = strMaxNO.ToString();


                return View();
            }
            else
            {

                var result = crud.GetInfo(id);
                ViewBag.MaxComID = result.DepartmentCode.ToString();
                return View(result);

            }
        }
        [HttpPost]
        public ActionResult View_Department(Model_HRM_Def_Department Model)
        {

            var Item = db.HRM_Def_Department.FirstOrDefault(x => x.DepartmentCode == Model.DepartmentCode);
            if (Item == null)
            {
                if (db.HRM_Def_Department.Any(k => k.DepartmentName == Model.DepartmentName))
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

                if (db.HRM_Def_Department.Any(x => x.DepartmentName == Model.DepartmentName && x.DepartmentCode != Model.DepartmentCode))
                {


                    return Json(new { success = false, message = "Already Exists" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    crud.UpdateInfo(Model.DepartmentCode, Model);

                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }


            }
        }

        public JsonResult getSingleData(string DepartmentCode)
        {

            var result = crud.GetInfo(DepartmentCode);
            ViewBag.MaxComID = result.DepartmentCode.ToString();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult showInfo()
        {
            var resutl = crud.GetAllInfo();
            return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MaxID()
        {
            common.FindMaxNo(ref strMaxNO, "DepartmentCode", "HRM_Def_Department", 3);
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
        public ActionResult DeleteMultiData(Model_HRM_Def_Department Model)
        {
            var data = Json("");

            foreach (var item2 in Model.AllID)
            {

                var Item = db.HRM_Def_Designation.FirstOrDefault(x => x.DesignationCode == item2.DepartmentCode);
                {
                    if (Item == null)
                    {
                        data = Json(new { success = false, message = "No Valid data selected!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        crud.DeleteInfo(item2.DepartmentCode);
                        data = Json(new { success = true, message = "Data deleted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return data;
        }

        [HttpPost]
        public JsonResult CheckUsername(string DepartmentCode, string DepartmentName)
        {
            var ExistUserName = db.HRM_Def_Department.Where(x => x.DepartmentName == DepartmentName).FirstOrDefault();
            if (ExistUserName != null)
            {
                var ExistUserName1 = db.HRM_Def_Department.Where(x => x.DepartmentName == DepartmentName && x.DepartmentCode == DepartmentCode).FirstOrDefault();
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