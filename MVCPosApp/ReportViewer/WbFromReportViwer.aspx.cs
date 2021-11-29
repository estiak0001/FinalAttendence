using BusinessLogic.Repository;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using FJS;
using MVCPosApp.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVCPosApp.ReportViewer
{
    public partial class WbFromReportViwer : System.Web.UI.Page
    {

        ProjectConnection con = new ProjectConnection();
        SrvGeneral srv = new SrvGeneral();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SalesInvoice"] = null;
               
                if (Request.QueryString["DAP"] != null)
                {
                   
                        var queryStrings = HttpUtility.UrlDecode(Request.QueryString.ToString());
                        var arrQueryStrings = queryStrings.Split('&');
                        //var length = arrQueryStrings.Length;
                        var part1 = arrQueryStrings[0];//x=1,2
                        var part2 = arrQueryStrings[1];
                        string Date = part1.Trim().Substring(part1.Trim().LastIndexOf('=') + 1);
                         string Department = part2.Trim().Substring(part2.Trim().LastIndexOf('=') + 1);
                    showDailyAttendencePresentList(Date, Department);
                }
                
                else if (Request.QueryString["DAA"] != null)
                {

                    var queryStrings = HttpUtility.UrlDecode(Request.QueryString.ToString());
                    var arrQueryStrings = queryStrings.Split('&');
                    //var length = arrQueryStrings.Length;
                    var part1 = arrQueryStrings[0];//x=1,2
                    var part2 = arrQueryStrings[1];
                    string Date = part1.Trim().Substring(part1.Trim().LastIndexOf('=') + 1);
                    string Department = part2.Trim().Substring(part2.Trim().LastIndexOf('=') + 1);
                    showDailyAttendenceAbsentList(Date, Department);
                }
                else if (Request.QueryString["DAL"] != null)
                {

                    var queryStrings = HttpUtility.UrlDecode(Request.QueryString.ToString());
                    var arrQueryStrings = queryStrings.Split('&');
                    //var length = arrQueryStrings.Length;
                    var part1 = arrQueryStrings[0];//x=1,2
                    var part2 = arrQueryStrings[1];
                    string Date = part1.Trim().Substring(part1.Trim().LastIndexOf('=') + 1);
                    string Department = part2.Trim().Substring(part2.Trim().LastIndexOf('=') + 1);
                    showDailyAttendenceLateList(Date, Department);
                }

                else if (Request.QueryString["DALL"] != null)
                {

                    var queryStrings = HttpUtility.UrlDecode(Request.QueryString.ToString());
                    var arrQueryStrings = queryStrings.Split('&');
                    //var length = arrQueryStrings.Length;
                    var part1 = arrQueryStrings[0];//x=1,2
                    var part2 = arrQueryStrings[1];
                    string Date = part1.Trim().Substring(part1.Trim().LastIndexOf('=') + 1);
                    string Department = part2.Trim().Substring(part2.Trim().LastIndexOf('=') + 1);
                    showDailyAttendenceAll(Date, Department);
                }
                else if (Request.QueryString["DARE"] != null)
                {

                    var queryStrings = HttpUtility.UrlDecode(Request.QueryString.ToString());
                    var arrQueryStrings = queryStrings.Split('&');
                    //var length = arrQueryStrings.Length;
                    var part1 = arrQueryStrings[0];//x=1,2
                    var part2 = arrQueryStrings[1];
                    var Part3 = arrQueryStrings[2];
                    string EmployeeID = part1.Trim().Substring(part1.Trim().LastIndexOf('=') + 1);
                    string FromDate = part2.Trim().Substring(part2.Trim().LastIndexOf('=') + 1);
                    string ToDate = Part3.Trim().Substring(Part3.Trim().LastIndexOf('=') + 1);
                    showEmployeeDateWaysDetails(EmployeeID,FromDate,ToDate);
                }
                else if (Request.QueryString["EMR"] != null)
                {

                    var queryStrings = HttpUtility.UrlDecode(Request.QueryString.ToString());
                    var arrQueryStrings = queryStrings.Split('&');
                    //var length = arrQueryStrings.Length;
                    var part1 = arrQueryStrings[0];//x=1,2
                    var part2 = arrQueryStrings[1];
                    var Part3 = arrQueryStrings[2];
                    string EmployeeID = part1.Trim().Substring(part1.Trim().LastIndexOf('=') + 1);
                    string FromDate = part2.Trim().Substring(part2.Trim().LastIndexOf('=') + 1);
                    string ToDate = Part3.Trim().Substring(Part3.Trim().LastIndexOf('=') + 1);
                    showEmployeeMovementReport(EmployeeID, FromDate, ToDate);
                }
                else
                {
                    
                }
            }


        }
        public void showEmployeeMovementReport(string EmployeeID, string FromDate, string ToDate)
        {
            string[] SplitString2 = FromDate.Split('/');
            string ConvertFromDate = SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];

            string[] SplitToDate = ToDate.Split('/');
            string ConvertTodate = SplitToDate[2] + "-" + SplitToDate[1] + "-" + SplitToDate[0];

            string query = "exec Rpt_Prc_EmployeeMovement '" + EmployeeID + "','" + ConvertFromDate + "','" + ConvertTodate + "'";
            DataTable dt = new DataTable();
            srv.GetData(dt, query);
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Reports/Rpt_EmpMovement.rpt"));
            crystalReport.SetDataSource(dt);
            CrystalReportViewer1.PDFOneClickPrinting = false;
            CrystalReportViewer1.HasExportButton = true;
            CrystalReportViewer1.HasPrintButton = true;
            CrystalReportViewer1.HasGotoPageButton = true;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer1.ReportSource = crystalReport;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
            Session["SalesInvoice"] = crystalReport;
            CrystalReportViewer1.ReportSource = crystalReport;

        }
        public void showEmployeeDateWaysDetails(string EmployeeID, string FromDate,string ToDate)
        {
            string[] SplitString2 = FromDate.Split('/');
            string ConvertFromDate= SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];

            string[] SplitToDate = ToDate.Split('/');
            string ConvertTodate = SplitToDate[2] + "-" + SplitToDate[1] + "-" + SplitToDate[0];

            string query = "exec prc_EmployeeAttendenceySummery '" + ConvertFromDate + "','" + ConvertTodate + "','" + EmployeeID + "'";
            DataTable dt = new DataTable();
            srv.GetData(dt, query);
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Reports/Rpt_EmployeeAttendenceSummery.rpt"));
            crystalReport.SetDataSource(dt);
            CrystalReportViewer1.PDFOneClickPrinting = false;
            CrystalReportViewer1.HasExportButton = true;
            CrystalReportViewer1.HasPrintButton = true;
            CrystalReportViewer1.HasGotoPageButton = true;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer1.ReportSource = crystalReport;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
            Session["SalesInvoice"] = crystalReport;
            CrystalReportViewer1.ReportSource = crystalReport;

        }
        public void showDailyAttendenceAll(string Date, string Department)
        {
            
            string[] SplitString2 = Date.Split('/');
            string ConvertDate = SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];
            string query = "exec Rpt_DailyAttendenceAllSummery2 '" + ConvertDate + "','" + Department + "','" + Session["EmployeeID"].ToString() + "'";

            DataTable dt = new DataTable();
            srv.GetData(dt, query);
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Reports/Rpt_DailAttendenceAll.rpt"));
            crystalReport.SetDataSource(dt);
            CrystalReportViewer1.PDFOneClickPrinting = false;
            CrystalReportViewer1.HasExportButton = true;
            CrystalReportViewer1.HasPrintButton = true;
            CrystalReportViewer1.HasGotoPageButton = true;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer1.ReportSource = crystalReport;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
            Session["SalesInvoice"] = crystalReport;
            CrystalReportViewer1.ReportSource = crystalReport;
        }
        public void showDailyAttendenceLateList(string Date, string Department)
        {
            
            string[] SplitString2 = Date.Split('/');
            string ConvertDate = SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];
            string query = "exec Rpt_DailyAttendenceLateSummery2 '" + ConvertDate + "','" + Department + "','" + Session["EmployeeID"].ToString() + "'";
            DataTable dt = new DataTable();
            srv.GetData(dt, query);
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Reports/Rpt_DailyLateList.rpt"));
            crystalReport.SetDataSource(dt);
            CrystalReportViewer1.PDFOneClickPrinting = false;
            CrystalReportViewer1.HasExportButton = true;
            CrystalReportViewer1.HasPrintButton = true;
            CrystalReportViewer1.HasGotoPageButton = true;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer1.ReportSource = crystalReport;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
            Session["SalesInvoice"] = crystalReport;
            CrystalReportViewer1.ReportSource = crystalReport;
        }
        public void showDailyAttendenceAbsentList(string Date, string Department)
        {
            
            string[] SplitString2 = Date.Split('/');
            string ConvertDate = SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];
            string query = "exec Rpt_DailyAttendenceAbsentSummery2 '" + ConvertDate + "','" + Department + "','" + Session["EmployeeID"].ToString() + "'";
            DataTable dt = new DataTable();
            srv.GetData(dt, query);
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Reports/Rpt_DailyAbsentList.rpt"));
            crystalReport.SetDataSource(dt);
            CrystalReportViewer1.PDFOneClickPrinting = false;
            CrystalReportViewer1.HasExportButton = true;
            CrystalReportViewer1.HasPrintButton = true;
            CrystalReportViewer1.HasGotoPageButton = true;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer1.ReportSource = crystalReport;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
            Session["SalesInvoice"] = crystalReport;
            CrystalReportViewer1.ReportSource = crystalReport;
        }

        public void showDailyAttendencePresentList(string Date,string Department)
        {
            string[] SplitString2 = Date.Split('/');
            string ConvertDate = SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];
            string query = "exec Rpt_DailyAttendencePrententSummery2 '" + ConvertDate + "','" + Department + "','"+ Session["EmployeeID"].ToString() + "'";
            DataTable dt = new DataTable();
            srv.GetData(dt, query);
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Reports/Rpt_DailyPresentList.rpt"));
            crystalReport.SetDataSource(dt);
            CrystalReportViewer1.PDFOneClickPrinting = false;
            CrystalReportViewer1.HasExportButton = true;
            CrystalReportViewer1.HasPrintButton = true;
            CrystalReportViewer1.HasGotoPageButton = true;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
            CrystalReportViewer1.ReportSource = crystalReport;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
            Session["SalesInvoice"] = crystalReport;
            CrystalReportViewer1.ReportSource = crystalReport;

        }

        protected void Page_Init(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
            }
            else
            {
                if (Session["SalesInvoice"] != null)
                {
                    ReportDocument doc = (ReportDocument)Session["SalesInvoice"];
                    CrystalReportViewer1.ReportSource = doc;
                }
            }
        }


        protected void btnPdf_Click(object sender, EventArgs e)
        {

        }
    }
}