using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Crud_Dashboard
    {
        public Model_EmployeeBasicInfo GetInfo(string EmployeeID)
        {

            var db = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from hrm in db.HRM_EmployeeOfficialInfo
                          from gn in db.HRM_Employee.Where(gn => gn.EmployeeID == hrm.EmployeeID).DefaultIfEmpty()
                          from depart in db.HRM_Def_Department.Where(depart => depart.DepartmentCode == hrm.DepartmentCode)
       .DefaultIfEmpty()
                          from Desig in db.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == hrm.DesignationCode)
     .DefaultIfEmpty()
                          select new
                          {
                              EmployeeID = hrm.EmployeeID,
                              EmployeeName = gn.FirstName+ " " + gn.LastName,                             
                              DepartmentCode = depart.DepartmentName,
                              DesignationCode = Desig.DesignationName,
                              PhotoUrl = gn.PhotoUrl.Substring(1, gn.PhotoUrl.Length - 1),
                              JoiningDate=hrm.JoiningDate
                          }).AsEnumerable().Select(a => new Model_EmployeeBasicInfo()
                          {
                              EmployeeID = a.EmployeeID,
                              EmployeeName = a.EmployeeName.ToString(),
                              DepartmentCode = a.DepartmentCode,
                              DesignationCode = a.DesignationCode,
                              PhotoUrl = a.PhotoUrl,                              
                              JoiningDate = ((DateTime)a.JoiningDate).ToString("dd/MM/yyyy")
                             
                          }).Where(a => a.EmployeeID == EmployeeID).FirstOrDefault();
            return result;
        }
        public List<Model_DashboardEmployeeInTimeList> GetEmployeeInTimeList(string AttendenceDate)
        {
            var returnModel = new List<Model_DashboardEmployeeInTimeList>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {                
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Prc_DashboardEmployeeInList";
                cmd.CommandTimeout = 0;
                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@AttendenceDate";
                sParam1.Value = AttendenceDate;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_DashboardEmployeeInTimeList>(reader);
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

        public List<Model_DashboardEmployeeStatistics> GetEmployeeDailyStatistics(string AttendenceDate, string DepartmentCode, string LoginEmployeeID)
        {
            var returnModel = new List<Model_DashboardEmployeeStatistics>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {               
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Rpt_DailyAttendenceDashboardSummery";
                cmd.CommandTimeout = 0;
                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@AttendenceDate";
                sParam1.Value = AttendenceDate;
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
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_DashboardEmployeeStatistics>(reader);
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
