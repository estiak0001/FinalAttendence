using BusinessLogic;
using BusinessLogic.Repository;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class AllAttenReController : Controller
    {
        // GET: DaillyAttendence
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crud_HRM_AllReport Crud = new Crud_HRM_AllReport();
        public ActionResult View_AllAttenReport()
        {
            var ReportType = new List<Model_SelectType>();
            //ReportType.Add(new Model_SelectType() { Value = "FullLeave", Text = "Full Leave" });
            ViewBag.LoadReportType = new SelectList(ReportType, "Value", "Text");

            ViewBag.Department = new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
            ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");

            ViewBag.LoadReportFormat = new SelectList(
                    new List<Model_SelectType>
                    {
                                        new Model_SelectType { Text = "PDF", Value = "PDF" },
                                        new Model_SelectType { Text = "Excel", Value = "Excel"},
                                        new Model_SelectType { Text = "Word", Value = "Word"},
                    }, "Value", "Text");

            ViewBag.LoadReportTypes = new SelectList(
                    new List<Model_SelectType>
                    {
                                        new Model_SelectType { Text = "Present Report", Value = "1" },
                                        new Model_SelectType { Text = "Absent Report", Value = "2"},
                                        new Model_SelectType { Text = "Late Report", Value = "3"},
                                        new Model_SelectType { Text = "Combine Report", Value = "4"},
                                        new Model_SelectType { Text = "Movement Report", Value = "5"},
                                        new Model_SelectType { Text = "Employee Job Card", Value = "6"},
                    }, "Value", "Text");

            return View();                        
        }

        public ActionResult ShowEmpPresentList(string AttendenceDate,string DepartmentCode)
        {
            var LoginEmployeeID= "29";
            var resutl = Crud.GetEmployeePrresentList (AttendenceDate,DepartmentCode, LoginEmployeeID);
            return Json( resutl, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowEmpLateList(string AttendenceDate, string DepartmentCode)
        {
            var LoginEmployeeID = Session["EmployeeID"].ToString();
            var resutl = Crud.GetEmployeeLateList(AttendenceDate, DepartmentCode, LoginEmployeeID);
            return Json(resutl, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowEmpAbsentdList(string AttendenceDate, string DepartmentCode)
        {
            var LoginEmployeeID = Session["EmployeeID"].ToString();
            var resutl = Crud.GetEmployeeAbsentList(AttendenceDate, DepartmentCode, LoginEmployeeID);                      
            return Json(resutl, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowEmpAllList(string AttendenceDate, string DepartmentCode)
        {
            var LoginEmployeeID = Session["EmployeeID"].ToString();
            var resutl = Crud.GetEmployeeAllList(AttendenceDate, DepartmentCode, LoginEmployeeID);
            return Json(resutl, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowEmpJobCard(string FromDate, string ToDate, string EmployeeID)
        {
            //var LoginEmployeeID = Session["EmployeeID"].ToString();

            var resutl = Crud.GetEmployeeJobCard(FromDate, ToDate, EmployeeID);
            return Json(resutl, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowEmpMovementList(string EmployeeID, string FromDate, string ToDate)
        {
            var resutl = Crud.GetEmployeeMovementReport(EmployeeID,FromDate,ToDate);
            return Json(resutl, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ShowEmpDateTimeStatisitic(string FromDate, string ToDate, string EmployeeID, string EmoStatus)
        {

            var resutl = Crud.GetEmployeeDatetimeStatistics(FromDate, ToDate, EmployeeID, EmoStatus);
            return Json(resutl, JsonRequestBehavior.AllowGet);
        }
        //string EmployeeID, string FromDate, string ToDate, string DepartmentCode, string ReportType, string formats
        public ActionResult Report(string EmployeeID, string formats, string DepartmentCode, string ReportType, string FromDate, string ToDate)
        {
            LocalReport lr = new LocalReport();
            //Present Report
            if(ReportType == "1")
            {
                if(EmployeeID != "")
                {
                    string path = Path.Combine(Server.MapPath("~/RDLCReport"), "EmployeeAttendenceySummeryEmpDatatimeStatistics.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        lr.ReportPath = path;
                    }
                    else
                    {
                        return View();
                    }
                    string EmoStatus = "P";
                    var resutl = Crud.GetEmployeeDatetimeStatistics(FromDate, ToDate, EmployeeID, EmoStatus);
                    ReportDataSource rd1 = new ReportDataSource("DataSet2", resutl);
                    lr.DisplayName = EmployeeID + "PresentReport";
                    lr.DataSources.Add(rd1);
                }
                else
                {
                    string path = Path.Combine(Server.MapPath("~/RDLCReport"), "AllPresentReport.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        lr.ReportPath = path;
                    }
                    else
                    {
                        return View();
                    }
                    var LoginEmployeeID = Session["EmployeeID"].ToString();
                    var resut = Crud.GetEmployeePrresentList(FromDate, DepartmentCode, LoginEmployeeID);
                    ReportDataSource rd = new ReportDataSource("DataSetAllPresent", resut);
                    lr.DisplayName = "AllAbsentReport";
                    lr.DataSources.Add(rd);
                }
            }
            //Absent Report
            if(ReportType == "2")
            {
                if(EmployeeID != "")
                {
                    string path = Path.Combine(Server.MapPath("~/RDLCReport"), "EmployeeAttendenceySummeryEmpDatatimeStatistics.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        lr.ReportPath = path;
                    }
                    else
                    {
                        return View();
                    }
                    string EmoStatus = "A";
                    var resutl = Crud.GetEmployeeDatetimeStatistics(FromDate, ToDate, EmployeeID, EmoStatus);
                    ReportDataSource rd1 = new ReportDataSource("DataSet2", resutl);
                    lr.DisplayName =  EmployeeID+"AbsentReport";
                    lr.DataSources.Add(rd1);
                }
                else
                {
                    string path = Path.Combine(Server.MapPath("~/RDLCReport"), "AbsentReport.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        lr.ReportPath = path;
                    }
                    else
                    {
                        return View();
                    }
                    var LoginEmployeeID = Session["EmployeeID"].ToString();
                    var resut = Crud.GetEmployeeAbsentList(FromDate, DepartmentCode, LoginEmployeeID);
                    ReportDataSource rd = new ReportDataSource("DataSet1", resut);
                    lr.DisplayName = "AllAbsentReport";
                    lr.DataSources.Add(rd);
                }

            }
            //Late Report
            if (ReportType == "3")
            {
                if (EmployeeID != "")
                {
                    string path = Path.Combine(Server.MapPath("~/RDLCReport"), "EmployeeAttendenceySummeryEmpDatatimeStatistics.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        lr.ReportPath = path;
                    }
                    else
                    {
                        return View();
                    }
                    string EmoStatus = "L";
                    var resutl = Crud.GetEmployeeDatetimeStatistics(FromDate, ToDate, EmployeeID, EmoStatus);
                    ReportDataSource rd1 = new ReportDataSource("DataSet2", resutl);
                    lr.DisplayName = EmployeeID + "AbsentReport";
                    lr.DataSources.Add(rd1);
                }
                else
                {
                    string path = Path.Combine(Server.MapPath("~/RDLCReport"), "AllLateReport.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        lr.ReportPath = path;
                    }
                    else
                    {
                        return View();
                    }
                    var LoginEmployeeID = Session["EmployeeID"].ToString();
                    var resut = Crud.GetEmployeeLateList(FromDate, DepartmentCode, LoginEmployeeID);
                    ReportDataSource rd = new ReportDataSource("DataSetAllLate", resut);
                    lr.DisplayName = "AllLateReport";
                    lr.DataSources.Add(rd);
                }

            }

            if (ReportType == "4")
            {
                    string path = Path.Combine(Server.MapPath("~/RDLCReport"), "CombineReport.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        lr.ReportPath = path;
                    }
                    else
                    {
                        return View();
                    }
                    var LoginEmployeeID = Session["EmployeeID"].ToString();
                    var resut = Crud.GetEmployeeAllList(FromDate, DepartmentCode, LoginEmployeeID);
                    ReportDataSource rd = new ReportDataSource("DataSetCombine", resut);
                    lr.DisplayName = "AllLateReport";
                    lr.DataSources.Add(rd);               
            }
            if (ReportType == "5")
            {
                string path = Path.Combine(Server.MapPath("~/RDLCReport"), "AllMovementReport.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View();
                }
                var resut = Crud.GetEmployeeMovementReport(EmployeeID, FromDate, ToDate);
                ReportDataSource rd = new ReportDataSource("DataSetMovement", resut);
                lr.DisplayName = "AllLateReport";
                lr.DataSources.Add(rd);
            }
            if (ReportType == "6")
            {
                string path = Path.Combine(Server.MapPath("~/RDLCReport"), "EmployeeJobCard.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View();
                }

                var resutl = Crud.GetEmployeeJobCard(FromDate, ToDate, EmployeeID);
                ReportDataSource rd1 = new ReportDataSource("DataSetJobCard", resutl);
                lr.DisplayName = EmployeeID + "PresentReport";
                lr.DataSources.Add(rd1);
            }

            string reportType = formats;
            string mimeType;
            string encoding;
            string fileNameExtention;

            string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>" + formats + "</OutputFormat>" +
                "<PageWidth> 8.5in</PageWidth>" +
                "<PageHeight> 11in </PageHeight>" +
                "<MarginTop>0.25in</MarginTop>" +
                "<MarginLeft> 0.25in </MarginLeft>" +
                "<MarginRight> 0.25in </MarginRight>" +
                "<MarginBottom> 0.25in </MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtention,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }
    }
}