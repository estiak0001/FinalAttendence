using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crud_HRM_AllReport
    {
       
        public List<Model_DailyEmployeePresentList> GetEmployeePrresentList(string AttendenceDate,string DepartmentCode,string LoginEmployeeID)
        {
            var returnModel = new List<Model_DailyEmployeePresentList>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                string[] SplitString2 = AttendenceDate.Split('/');
                string ConvertDate = SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];

                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Rpt_DailyAttendencePrententSummery2";
                cmd.CommandTimeout = 0;
                var sParam1= cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@AttendenceDate";
                sParam1.Value = ConvertDate;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                var sParam2 = cmd.CreateParameter();
                sParam2.DbType = DbType.String;
                sParam2.ParameterName = "@DepartmentCode";
                sParam2.Value = DepartmentCode;
                sParam2.IsNullable = false;
                cmd.Parameters.Add(sParam2);


                var sParam3 = cmd.CreateParameter();
                sParam3.DbType = DbType.String;
                sParam3.ParameterName = "@LoginEmpoyeeID";
                sParam3.Value = LoginEmployeeID;
                sParam3.IsNullable = false;
                cmd.Parameters.Add(sParam3);


                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_DailyEmployeePresentList>(reader);
                    returnModel = (from s in results select s).ToList();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }

        public List<Model_DailyEmployeePresentList> GetEmployeeAbsentList(string AttendenceDate, string DepartmentCode, string LoginEmployeeID)
        {
            var returnModel = new List<Model_DailyEmployeePresentList>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                string[] SplitString2 = AttendenceDate.Split('/');
                string ConvertDate = SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];

                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Rpt_DailyAttendenceAbsentSummery2";
                cmd.CommandTimeout = 0;
                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@AttendenceDate";
                sParam1.Value = ConvertDate;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                var sParam2 = cmd.CreateParameter();
                sParam2.DbType = DbType.String;
                sParam2.ParameterName = "@DepartmentCode";
                sParam2.Value = DepartmentCode;
                sParam2.IsNullable = false;
                cmd.Parameters.Add(sParam2);


                var sParam3 = cmd.CreateParameter();
                sParam3.DbType = DbType.String;
                sParam3.ParameterName = "@LoginEmpoyeeID";
                sParam3.Value = LoginEmployeeID;
                sParam3.IsNullable = false;
                cmd.Parameters.Add(sParam3);


                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_DailyEmployeePresentList>(reader);
                    returnModel = (from s in results select s).ToList();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }

        public List<Model_DailyEmployeePresentList> GetEmployeeLateList(string AttendenceDate, string DepartmentCode, string LoginEmployeeID)
        {
            var returnModel = new List<Model_DailyEmployeePresentList>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                string[] SplitString2 = AttendenceDate.Split('/');
                string ConvertDate = SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];

                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Rpt_DailyAttendenceLateSummery2";
                cmd.CommandTimeout = 0;
                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@AttendenceDate";
                sParam1.Value = ConvertDate;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                var sParam2 = cmd.CreateParameter();
                sParam2.DbType = DbType.String;
                sParam2.ParameterName = "@DepartmentCode";
                sParam2.Value = DepartmentCode;
                sParam2.IsNullable = false;
                cmd.Parameters.Add(sParam2);


                var sParam3 = cmd.CreateParameter();
                sParam3.DbType = DbType.String;
                sParam3.ParameterName = "@LoginEmpoyeeID";
                sParam3.Value = LoginEmployeeID;
                sParam3.IsNullable = false;
                cmd.Parameters.Add(sParam3);


                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_DailyEmployeePresentList>(reader);
                    returnModel = (from s in results select s).ToList();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }
  

        public List<Model_DailyEmployeePresentList> GetEmployeeAllList(string AttendenceDate, string DepartmentCode, string LoginEmployeeID)
        {
            var returnModel = new List<Model_DailyEmployeePresentList>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                string[] SplitString2 = AttendenceDate.Split('/');
                string ConvertDate = SplitString2[2] + "-" + SplitString2[1] + "-" + SplitString2[0];

                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Rpt_DailyAttendenceAllSummery2";
                cmd.CommandTimeout = 0;
                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@AttendenceDate";
                sParam1.Value = ConvertDate;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                var sParam2 = cmd.CreateParameter();
                sParam2.DbType = DbType.String;
                sParam2.ParameterName = "@DepartmentCode";
                sParam2.Value = DepartmentCode;
                sParam2.IsNullable = false;
                cmd.Parameters.Add(sParam2);


                var sParam3 = cmd.CreateParameter();
                sParam3.DbType = DbType.String;
                sParam3.ParameterName = "@LoginEmpoyeeID";
                sParam3.Value = LoginEmployeeID;
                sParam3.IsNullable = false;
                cmd.Parameters.Add(sParam3);


                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_DailyEmployeePresentList>(reader);
                    returnModel = (from s in results select s).ToList();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }

        public List<Model_DailyEmpJobCards> GetEmployeeJobCard(string FromDate, string ToDate, string EmployeeID)
        {
            var returnModel = new List<Model_DailyEmpJobCards>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                string[] SplitFromDate = FromDate.Split('/');
                string ConvertFromDate = SplitFromDate[2] + "-" + SplitFromDate[1] + "-" + SplitFromDate[0];

                string[] SplitToDate = ToDate.Split('/');
                string ConvertToDate = SplitToDate[2] + "-" + SplitToDate[1] + "-" + SplitToDate[0];

                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.prc_EmployeeAttendenceySummery";
                cmd.CommandTimeout = 0;
                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@StartDateTime";
                sParam1.Value = ConvertFromDate;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                var sParam2 = cmd.CreateParameter();
                sParam2.DbType = DbType.String;
                sParam2.ParameterName = "@EndDateTime";
                sParam2.Value = ConvertToDate;
                sParam2.IsNullable = false;
                cmd.Parameters.Add(sParam2);

                var sParam3 = cmd.CreateParameter();
                sParam3.DbType = DbType.String;
                sParam3.ParameterName = "@EmployeeID";
                sParam3.Value = EmployeeID;
                sParam3.IsNullable = false;
                cmd.Parameters.Add(sParam3);
                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_DailyEmpJobCards>(reader);
                    returnModel = (from s in results select s).ToList();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }


        public List<Model_DailyEmpJobCards> GetEmployeeDatetimeStatistics(string FromDate, string ToDate, string EmployeeID,string EmoStatus)
        {
            var returnModel = new List<Model_DailyEmpJobCards>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                string[] SplitFromDate = FromDate.Split('/');
                string ConvertFromDate = SplitFromDate[2] + "-" + SplitFromDate[1] + "-" + SplitFromDate[0];

                string[] SplitToDate = ToDate.Split('/');
                string ConvertToDate = SplitToDate[2] + "-" + SplitToDate[1] + "-" + SplitToDate[0];

                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.prc_EmployeeAttendenceySummeryEmpDatatimeStatistics";
                cmd.CommandTimeout = 0;
                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@StartDateTime";
                sParam1.Value = ConvertFromDate;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                var sParam2 = cmd.CreateParameter();
                sParam2.DbType = DbType.String;
                sParam2.ParameterName = "@EndDateTime";
                sParam2.Value = ConvertToDate;
                sParam2.IsNullable = false;
                cmd.Parameters.Add(sParam2);

                var sParam3 = cmd.CreateParameter();
                sParam3.DbType = DbType.String;
                sParam3.ParameterName = "@EmployeeID";
                sParam3.Value = EmployeeID;
                sParam3.IsNullable = false;
                cmd.Parameters.Add(sParam3);


                var sParam4 = cmd.CreateParameter();
                sParam4.DbType = DbType.String;
                sParam4.ParameterName = "@EmoStatus";
                sParam4.Value = EmoStatus;
                sParam4.IsNullable = false;
                cmd.Parameters.Add(sParam4);
                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_DailyEmpJobCards>(reader);
                    returnModel = (from s in results select s).ToList();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }

        public List<Model_EmpMovementReport> GetEmployeeMovementReport(string EmployeeID, string FromDate, string ToDate)
        {
            var returnModel = new List<Model_EmpMovementReport>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                string[] SplitFromDate = FromDate.Split('/');
                string ConvertFromDate = SplitFromDate[2] + "-" + SplitFromDate[1] + "-" + SplitFromDate[0];

                string[] SplitToDate = ToDate.Split('/');
                string ConvertToDate = SplitToDate[2] + "-" + SplitToDate[1] + "-" + SplitToDate[0];

                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Rpt_Prc_EmployeeMovement";
                cmd.CommandTimeout = 0;
                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@EmployeeID";
                sParam1.Value = EmployeeID;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                var sParam2 = cmd.CreateParameter();
                sParam2.DbType = DbType.String;
                sParam2.ParameterName = "@FromDate";
                sParam2.Value = ConvertFromDate;
                sParam2.IsNullable = false;
                cmd.Parameters.Add(sParam2);

                var sParam3 = cmd.CreateParameter();
                sParam3.DbType = DbType.String;
                sParam3.ParameterName = "@ToDate";
                sParam3.Value = ConvertToDate;
                sParam3.IsNullable = false;
                cmd.Parameters.Add(sParam3);
                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_EmpMovementReport>(reader);
                    returnModel = (from s in results select s).ToList();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }
    }
}
