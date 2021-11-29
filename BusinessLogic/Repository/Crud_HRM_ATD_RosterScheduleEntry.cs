using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crud_HRM_ATD_RosterScheduleEntry
    {
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";

        public List<LoadRosterInfo> GetAllInfo(string CompanyCode, string BranchCode , string DepartmentCode , string DesignationCode)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from mo in context.HRM_RosterScheduleEntry
                              from em in context.HRM_Employee.Where(em => em.EmployeeID == mo.EmployeeID.ToString()).DefaultIfEmpty()
                              from off in context.HRM_EmployeeOfficialInfo.Where(em => em.EmployeeID == mo.EmployeeID.ToString()).DefaultIfEmpty()
                              from en in context.HRM_ATD_Shift.Where(en => en.ShiftCode == mo.ShiftCode.ToString()).DefaultIfEmpty()
                              .DefaultIfEmpty()
                              select new
                              {
                                  RosterScheduleId = mo.RosterScheduleId,
                                  EmployeeID = mo.EmployeeID,
                                  EmployeeName = em.FirstName,
                                  Date = mo.Date,
                                  ShiftName = en.ShiftName,
                                  Remark = mo.Remark,
                                  companycode = em.CompanyCode,
                                  department = off.DepartmentCode,
                                  DesignationCode = off.DesignationCode,
                                  branchCode = off.BranchCode
                                  
                              }).AsEnumerable().Select(a => new LoadRosterInfo()
                              {
                                  RosterScheduleId = a.RosterScheduleId.ToString(),
                                  EmployeeID = a.EmployeeID,
                                  EmployeeName = a.EmployeeName,
                                  //Date = ((DateTime)a.Date).ToString("dd/MM/yyyy"),
                                  Date = ((DateTime)a.Date).ToString("dd/MM/yyyy"),
                                  ShiftName = a.ShiftName,
                                  Remark = a.Remark,
                                  CompanyCode = a.companycode,
                                  BranchCode = a.branchCode,
                                  Department = a.department,
                                  DesignationCode = a.DesignationCode
                              }).Where(s=> (CompanyCode == "" || s.CompanyCode == CompanyCode) && (BranchCode == "" || s.BranchCode == BranchCode) && (DepartmentCode == "" || s.Department == DepartmentCode) && (DesignationCode == "" || s.DesignationCode == DesignationCode)).ToList();
                return result;
            }
        }
        public List<Model_HRM_EmployeeForRoster> GetRosterEmoloyee(string CompanyCode, string DepartmentCode, string DesignationCode, string branchcode)
        {
            var returnModel = new List<Model_HRM_EmployeeForRoster>();

            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                //returnModel = (from off in db.HRM_EmployeeOfficialInfo.AsEnumerable()
                //               from em in db.HRM_Employee.Where(em => em.EmployeeID == off.EmployeeID.ToString()).DefaultIfEmpty()
                //              from de in db.HRM_Def_Department.Where(en => en.DepartmentCode == off.DepartmentCode.ToString()).DefaultIfEmpty()
                //              .DefaultIfEmpty()
                //              select new
                //              {
                //                  EmployeeID = off.EmployeeID,
                //                  CompanyCode = off.CompanyCode,
                //                  FirstName = em.FirstName,
                //                  DepartmentName = de.DepartmentName,
                //                  designationCode = off.DesignationCode,
                //                  branchCode = off.BranchCode
                //              }).AsEnumerable().Select(a => new Model_HRM_EmployeeForRoster()
                //              {
                //                  EmployeeID = a.EmployeeID,
                //                  FirstName = a.FirstName,
                //                  DepartmentName = a.DepartmentName,
                //                  DesignationName = "",
                //                  CompanyCode = a.CompanyCode,
                //                  DesignationCode = a.designationCode,
                //                  BranchCode = a.branchCode
                //              }).Where(d=> (CompanyCode == "" || d.CompanyCode == CompanyCode) && (DesignationCode == "" || d.DesignationCode == DesignationCode) && (branchcode == "" || d.BranchCode == branchcode)).ToList();

                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Prc_RosterEmployeeInfoLoad";
                cmd.CommandTimeout = 0;
                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@CompanyID";
                sParam1.Value = CompanyCode;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                var sParam2 = cmd.CreateParameter();
                sParam2.DbType = DbType.String;
                sParam2.ParameterName = "@DepartmentID";
                sParam2.Value = DepartmentCode;
                sParam2.IsNullable = false;
                cmd.Parameters.Add(sParam2);

                var sParam3 = cmd.CreateParameter();
                sParam3.DbType = DbType.String;
                sParam3.ParameterName = "@DesignationCode";
                sParam3.Value = DesignationCode;
                sParam3.IsNullable = false;
                cmd.Parameters.Add(sParam3);

                var sParam4 = cmd.CreateParameter();
                sParam4.DbType = DbType.String;
                sParam4.ParameterName = "@BranchCode";
                sParam4.Value = branchcode;
                sParam4.IsNullable = false;
                cmd.Parameters.Add(sParam4);

                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_HRM_EmployeeForRoster>(reader);
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


        public string SaveInfo(Model_HRM_RosterScheduleEntry2 model, string LoginEmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_RosterScheduleEntry coreCom2 = new HRM_RosterScheduleEntry();
            coreCom2.RosterScheduleId = model.RosterScheduleId;
            coreCom2.EmployeeID = model.EmployeeID;
            coreCom2.Date = model.Date;
            coreCom2.ShiftCode = model.ShiftCode;
            coreCom2.Weekend = model.Weekend;
            coreCom2.Remark = model.Remark;
            coreCom2.LUser = LoginEmployeeID;
            coreCom2.LDate = DateTime.Now;
            coreCom2.LIP = model.LIP;
            coreCom2.LMAC = model.LMAC;
            coreCom2.ModifyDate = model.ModifyDate;
            coreCom2.CompanyCode = model.CompanyCode;
            coreCom2.EmployeeID_SAO = model.EmployeeID_SAO;
            context.HRM_RosterScheduleEntry.Add(coreCom2);
            context.SaveChanges();
            return coreCom2.RosterScheduleId;
        }

        public bool DeleteExistInfo(string EmployeeID, DateTime Date )
        {
            //string[] spl = Date.Split('/');
            //DateTime fromdate = Convert.ToDateTime(spl[2] + "-" + spl[1] + "-" + spl[0]);


            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_RosterScheduleEntry.FirstOrDefault(x => x.EmployeeID == EmployeeID && x.Date == Date);
            if (result != null)
            {
                context.HRM_RosterScheduleEntry.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        //public bool DeleteExistInfo(string Fromdate, string Todate)
        //{
        //    string[] spl = Fromdate.Split('/');
        //    DateTime fromdate = Convert.ToDateTime(spl[2] + "-" + spl[1] + "-" + spl[0]);
        //    string[] spl2 = Todate.Split('/');
        //    DateTime todate = Convert.ToDateTime(spl2[2] + "-" + spl2[1] + "-" + spl2[0]);

        //    var context = new GCTL_ERP_DB_MVC_06_27Entities();
        //    var result = context.HRM_RosterScheduleEntry.Where(x => x.Date >= fromdate && x.Date <= todate).ToList();
        //    if (result != null)
        //    {
        //        context.HRM_RosterScheduleEntry.RemoveRange(result);
        //        context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}
        public bool DeleteInfo(string Fromdate, string Todate)
        {

            string[] spl = Fromdate.Split('/');
            DateTime fromdate = Convert.ToDateTime(spl[2] + "-" + spl[1] + "-" + spl[0]);
            string[] spl2 = Todate.Split('/');
            DateTime todate = Convert.ToDateTime(spl2[2] + "-" + spl2[1] + "-" + spl2[0]);

            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_RosterScheduleEntry.Where(x => x.Date >= fromdate && x.Date <= todate).ToList();
            if (result != null)
            {
                context.HRM_RosterScheduleEntry.RemoveRange(result);
                context.SaveChanges();
                return true;
            }
            return false;


        }
    }
}
