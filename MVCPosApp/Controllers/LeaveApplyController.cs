using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class LeaveApplyController : Controller
    {
        // GET: ManualAttendence

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_LeaveApplicationEntry crud = new Crud_HRM_LeaveApplicationEntry();
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MaxNo()
        {
            common.FindMaxNoAuto(ref strMaxNO, "LeaveAppEntryId", "HRM_LeaveApplicationEntry");
            string MaxID = strMaxNO.ToString();
            return Json(MaxID);
        }

        public ActionResult View_Leave(string id)
        {
            //var LeaveFormatType = new List<Model_SelectType>();
            //LeaveFormatType.Add(new Model_SelectType() { Value = "FullLeave", Text = "Full Leave" });            
            //ViewBag.LoadLeaveFormat = new SelectList(LeaveFormatType, "Value", "Text");

            string AccessCode = Session["AccessCode"].ToString();
            ViewBag.AccessCode = Session["AccessCode"].ToString();
            if (AccessCode == "001")
            {
                ViewBag.LoadLeaveFormat = new SelectList(
                                    new List<Model_SelectType>
                                    {
                                        new Model_SelectType { Text = "Full Leave", Value = "FullLeave" },
                                        new Model_SelectType { Text = "Short Leave", Value = "ShortLeave"}, 
                                    }, "Value", "Text");

                ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                              => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                      "EmployeeID", "FirstName");

                var weekend = common.GetCompanyWeekend();
                ViewBag.LoadWeekend = weekend.weekendday;

                ViewBag.LoadIS = new SelectList(db.HRM_Employee.ToList().Select(u
                               => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                      "EmployeeID", "FirstName");

                ViewBag.LoadHOD = new SelectList(db.HRM_Employee.ToList().Select(u
                               => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                      "EmployeeID", "FirstName");
                ViewBag.LoadLeaveType = new SelectList(db.HRM_ATD_LeaveType, "LeaveTypeId", "Name");
                common.FindMaxNoAuto(ref strMaxNO, "LeaveAppEntryId", "HRM_LeaveApplicationEntry");
                ViewBag.MaxComID = strMaxNO.ToString();
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
            else
            {
                var empID = Session["EmployeeID"].ToString();
                var employeedetails = db.HRM_EmployeeOfficialInfo.Where(d => d.EmployeeID == empID).FirstOrDefault();
                ViewBag.LoadLeaveFormat = new SelectList(
                                    new List<Model_SelectType>
                                    {
                                        new Model_SelectType { Text = "Full Leave", Value = "FullLeave" },
                                        new Model_SelectType { Text = "Short Leave", Value = "ShortLeave"},
                                    }, "Value", "Text");
                ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.Where(a=> a.EmployeeID == employeedetails.EmployeeID).ToList().Select(u
                              => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                      "EmployeeID", "FirstName");

                var empweekend = common.GetEmployeeWeekend(empID);
                var weekend = common.GetCompanyWeekend();
                if (empweekend.weekendday != null)
                {
                    ViewBag.LoadWeekend = empweekend.weekendday;
                }
                else
                {
                    ViewBag.LoadWeekend = weekend.weekendday;
                }

                ViewBag.LoadIS = new SelectList(db.HRM_Employee.Where(w=> w.EmployeeID == employeedetails.ReportingTo).ToList().Select(u
                               => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                      "EmployeeID", "FirstName");

                ViewBag.LoadHOD = new SelectList(db.HRM_Employee.Where(e=> e.EmployeeID == employeedetails.HOD).ToList().Select(u
                               => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                      "EmployeeID", "FirstName");
                ViewBag.LoadLeaveType = new SelectList(db.HRM_ATD_LeaveType, "LeaveTypeId", "Name");
                common.FindMaxNoAuto(ref strMaxNO, "LeaveAppEntryId", "HRM_LeaveApplicationEntry");
                ViewBag.MaxComID = strMaxNO.ToString();
                
                return View();                
            }
        }
       

        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpPost]
        public JsonResult View_Leave(Model_HRM_LeaveApplicationEntry Model)
        {
            var Item = db.HRM_LeaveApplicationEntry.FirstOrDefault(x => x.LeaveAppEntryId == Model.LeaveAppEntryId);
            //var balance = db.Prc_EmployeeLeaveBalaceStatus
            if (Item == null)
            {
                var IsApplicable = crud.ISExistLeave(Model.EmployeeID, Model.LeaveTypeId, Model.NoOfDay.ToString());
                if (IsApplicable.status == "Yes")
                {
                    string LoginEmployeeID = Session["EmployeeID"].ToString();
                    var Leaveid = crud.SaveInfo(Model, LoginEmployeeID);

                    var empInfo = crud.GetEmployeeInfo(Model.EmployeeID);

                    foreach (var day in Model.LeaveDaysList)
                    {
                        DateTime dateCon = new DateTime();
                        dateCon = DateTime.ParseExact(day, "MM/dd/yyyy", null);

                        HRM_LeaveApplicationDays leaveDays = new HRM_LeaveApplicationDays();
                        leaveDays.LeaveAppEntryId = Model.LeaveAppEntryId;
                        leaveDays.days = dateCon;
                        crud.SaveLeaveDaysInfo(leaveDays, LoginEmployeeID);
                    }
                    var t = db.HRM_ATD_LeaveType.Where(x => x.LeaveTypeId == Model.LeaveTypeId).FirstOrDefault();
                    LeaveInfoMail Leavemodels = new LeaveInfoMail();
                    Leavemodels.EmployeeNAme = empInfo.EmployeeName;
                    Leavemodels.Messege = "This is " + empInfo.EmployeeName + " I want to take leave from " + Model.StartDate + " to " + Model.EndDate;
                    Leavemodels.TotalDayes = Model.NoOfDay.ToString();
                    Leavemodels.DateFrom = Model.StartDate;
                    Leavemodels.DateTo = Model.EndDate;
                    Leavemodels.LeaveFormat = Model.ApplyLeaveFormat == "FullLeave" ? "Full Leave" : "Short Leave";
                    Leavemodels.LeaveType = t.Name;
                    Leavemodels.Reason = Model.Reason;
                    Leavemodels.LinkID = Leaveid;
                    Leavemodels.ShortLeaveFrom = Model.ShortLeaveFrom;
                    Leavemodels.ShortLeaveTo = Model.ShortLeaveTo;
                    Leavemodels.TotalTime = Model.ShortLeaveTime;
                    Leavemodels.FormatString = RenderPartialViewToString(this, "_emailTemplate", Leavemodels);

                    SendMail(Leavemodels);

                    return Json(new { success = true, message = "Your application placed successfully." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = IsApplicable.leavetype + " application balance is over! You can take " + IsApplicable.Balance + " days leave." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (Item.HRApprovalStatus == "Pending")
                {
                    string LoginEmployeeID = Session["EmployeeID"].ToString();
                    crud.UpdateInfo(Model.LeaveAppEntryId, Model);
                    crud.DeleteExistInfo(Model.LeaveAppEntryId);
                    foreach (var day in Model.LeaveDaysList)
                    {
                        DateTime dateCon = new DateTime();
                        dateCon = DateTime.ParseExact(day, "MM/dd/yyyy", null);

                        HRM_LeaveApplicationDays leaveDays = new HRM_LeaveApplicationDays();
                        leaveDays.LeaveAppEntryId = Model.LeaveAppEntryId;
                        leaveDays.days = dateCon;
                        crud.SaveLeaveDaysInfo(leaveDays, LoginEmployeeID);
                    }
                    return Json(new { success = true, message = "   Update Successfully!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "You can't update this application! This application already " + Item.HRApprovalStatus }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public void SendMail(LeaveInfoMail model)
        {
            if (model != null)
            {
                var BodyHtml = RenderPartialViewToString(this, "_emailTemplate", model); // ToInvoice is a model, you can pass parameters if needed


                //var message = new MailMessage();
                //message.To.Add(new MailAddress("eng.estiakahmed@gmail.com"));
                //message.From = new MailAddress("estiak.eng@gmail.com");
                //message.Subject = "Leave Application From " + model.EmployeeNAme;
                //message.Body = InvoiceHtml;
                //message.IsBodyHtml = true;

                //using (var smtp = new SmtpClient())
                //{
                //    var credential = new NetworkCredential
                //    {
                //        UserName = "gctlproject@gmail.com",
                //        Password = "##Gctl12345##"
                //    };
                //    smtp.Credentials = credential;
                //    smtp.Host = "smtp.gmail.com";
                //    smtp.Port = 587;
                //    smtp.EnableSsl = true;
                //    await smtp.SendMailAsync(message);
                //}
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress("tawfiq_islam@yahoo.com"));
                mail.From = new MailAddress("gctlproject@gmail.com");
                mail.Subject = "Leave Application From " + model.EmployeeNAme;
                string Body = BodyHtml;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("gctlproject@gmail.com", "##Gctl12345##"); // Enter seders User name and password       
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            else
            {

            }
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/LeaveApplicationFile/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public JsonResult getSingleData(string LeaveAppEntryId)
        {
            var result = crud.GetInfo(LeaveAppEntryId);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeeInfo(string EmployeeID)
        {
            var result = crud.GetEmployeeInfo(EmployeeID); 
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult showInfo(string EmployeeID)
        {
            string AccessCode = Session["AccessCode"].ToString();
            if (AccessCode == "001")
            {
                var resutl = crud.GetAllInfo(EmployeeID);
                return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
            }
            var empID = Session["EmployeeID"].ToString();
            var resutl2 = crud.GetAllInfo(empID);
            return Json(new { data = resutl2 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowEmployeeLeaveBalance(string EmployeeID)
        {
            string AccessCode = Session["AccessCode"].ToString();
            if (AccessCode == "001")
            {
                var resutl = crud.GetEmployeeLeaveStatus(EmployeeID);
                return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
            }
            var empID = Session["EmployeeID"].ToString();
            var resutl2 = crud.GetEmployeeLeaveStatus(empID);
            return Json(new { data = resutl2 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowEmployeeApprovedLeaveInfo(string EmployeeID)
        {
            string AccessCode = Session["AccessCode"].ToString();
            if (AccessCode == "001")
            {
                var resutl = crud.GetEmployeeApprrovedstatus(EmployeeID);
                return Json(new { data = resutl }, JsonRequestBehavior.AllowGet);
            }
            var empID = Session["EmployeeID"].ToString();
            var resutl2 = crud.GetEmployeeApprrovedstatus(empID);
            return Json(new { data = resutl2 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(string LeaveAppEntryId)
        {
            crud.DeleteInfo(LeaveAppEntryId);
            return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowLevaeDatesForEmployee(string EmployeeID)
        {
            string AccessCode = Session["AccessCode"].ToString();
            if (AccessCode == "001")
            {
                var resutl = crud.GetEmployeeLeaveDays(EmployeeID);
                return Json(resutl, JsonRequestBehavior.AllowGet);
            }
            var empID = Session["EmployeeID"].ToString();
            var resutl2 = crud.GetEmployeeLeaveDays(empID);
            return Json(resutl2, JsonRequestBehavior.AllowGet);
        }

    }
}