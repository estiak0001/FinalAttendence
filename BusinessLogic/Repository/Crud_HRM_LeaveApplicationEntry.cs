using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crud_HRM_LeaveApplicationEntry
    {
        public List<Model_HRM_LeaveApplicationEntry> GetAllInfo(string empID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (
                    from mo in context.HRM_LeaveApplicationEntry
                    from em in context.HRM_Employee.Where(em => em.EmployeeID == mo.EmployeeID).DefaultIfEmpty()
                    from emleaveType in context.HRM_ATD_LeaveType.Where(eml => eml.LeaveTypeId == mo.LeaveTypeId).DefaultIfEmpty()
                                                  .DefaultIfEmpty()
                              select new
                              {
                                  LeaveAppEntryId = mo.LeaveAppEntryId,
                                  EmployeeID = mo.EmployeeID,
                                  ApplyDate = mo.LDate,
                                  EmployeeName = em.FirstName+" "+em.LastName,
                                  StartDate = mo.StartDate,
                                  EndDate = mo.EndDate,
                                  NoOfDay = mo.NoOfDay,
                                  Reason = mo.Reason,
                                  Stattus = mo.HRApprovalStatus,
                                  LeaveType = emleaveType.Name
                              }).Where(em=> (em.EmployeeID == empID)||(empID == "")).AsEnumerable().Select(a => new Model_HRM_LeaveApplicationEntry()
                              {
                                  LeaveAppEntryId = a.LeaveAppEntryId.ToString(),
                                  EmployeeID = a.EmployeeID,
                                  EmployeeName = a.EmployeeName,
                                  ApplyDate = ((DateTime)a.ApplyDate).ToString("dd/MM/yyyy"),
                                  StartDate = ((DateTime)a.StartDate).ToString("dd/MM/yyyy"),
                                  EndDate = ((DateTime)a.EndDate).ToString("dd/MM/yyyy"),
                                  NoOfDay = a.NoOfDay,
                                  Reason=a.Reason,
                                  HRApprovalStatus = a.Stattus,
                                  LeaveType = a.LeaveType
                              }).ToList();
                return result;
            }
        }


        public List<Model_HRM_LeaveApplicationEntry> GetEmployeeApprrovedstatus(string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (
                    from mo in context.HRM_LeaveApplicationEntry
                    from em in context.HRM_Employee.Where(em => em.EmployeeID == mo.EmployeeID.ToString()).DefaultIfEmpty()
                              .DefaultIfEmpty()
                    select new
                    {
                        LeaveAppEntryId = mo.LeaveAppEntryId,
                        EmployeeID = mo.EmployeeID,
                        ApplyDate = mo.LDate,
                        EmployeeName = em.FirstName + " " + em.LastName + " ("+ mo.EmployeeID+")",
                        StartDate = mo.StartDate,
                        EndDate = mo.EndDate,
                        NoOfDay = mo.NoOfDay,
                        NoOfHour = mo.ShortLeaveTime,
                        Reason = mo.Reason,
                        HRApprovalStatus=mo.HRApprovalStatus
                    }).Where(t => t.HRApprovalStatus != "Pending" && t.EmployeeID== EmployeeID).AsEnumerable().Select(a => new Model_HRM_LeaveApplicationEntry()
                    {
                        LeaveAppEntryId = a.LeaveAppEntryId.ToString(),
                        EmployeeID = a.EmployeeID,
                        EmployeeName = a.EmployeeName,
                        ApplyDate = ((DateTime)a.ApplyDate).ToString("dd/MM/yyyy"),
                        StartDate = ((DateTime)a.StartDate).ToString("dd/MM/yyyy"),
                        EndDate = ((DateTime)a.EndDate).ToString("dd/MM/yyyy"),
                        NoOfDay = a.NoOfDay,
                        Reason = a.Reason,
                        HRApprovalStatus = a.HRApprovalStatus,
                        ShortLeaveTime = a.NoOfHour.ToString(),
                    }).ToList();
                return result;
            }
        }

        

        public List<Model_LeaveBalanceStatus> GetEmployeeLeaveStatus(string EmployeeID)
        {
            var returnModel = new List<Model_LeaveBalanceStatus>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Prc_EmployeeLeaveBalaceStatus";

                var sParam = cmd.CreateParameter();
                sParam.DbType = DbType.String;
                sParam.ParameterName = "@EmployeeID";
                sParam.Value = EmployeeID;
                sParam.IsNullable = false;
                cmd.Parameters.Add(sParam);

                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_LeaveBalanceStatus>(reader);
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

        public IsLeaveExist ISExistLeave(string EmployeeID, string leaveType, string NoOfDay)
        {
            var returnModel = new IsLeaveExist();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Prc_SingelEmployeeLeaveBalaceStatus";

                var sParam1 = cmd.CreateParameter();
                sParam1.DbType = DbType.String;
                sParam1.ParameterName = "@EmployeeID";
                sParam1.Value = EmployeeID;
                sParam1.IsNullable = false;
                cmd.Parameters.Add(sParam1);

                var sParam2 = cmd.CreateParameter();
                sParam2.DbType = DbType.String;
                sParam2.ParameterName = "@LeaveType";
                sParam2.Value = leaveType;
                sParam2.IsNullable = false;
                cmd.Parameters.Add(sParam2);

                var sParam3 = cmd.CreateParameter();
                sParam3.DbType = DbType.String;
                sParam3.ParameterName = "@NoOfDay";
                sParam3.Value = NoOfDay;
                sParam3.IsNullable = false;
                cmd.Parameters.Add(sParam3);
                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<IsLeaveExist>(reader);
                    returnModel = (from s in results select s).FirstOrDefault();
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


        public string SaveInfo(Model_HRM_LeaveApplicationEntry model, string LoginEmployeeID)
        {
            string TodayDate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime StartDate = new DateTime();
            StartDate = DateTime.ParseExact(model.StartDate, "dd/MM/yyyy", null);

            DateTime EndDate = new DateTime();
            EndDate = DateTime.ParseExact(model.EndDate, "dd/MM/yyyy", null);

            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_LeaveApplicationEntry coreCom = new HRM_LeaveApplicationEntry();
            coreCom.LeaveAppEntryId= model.LeaveAppEntryId;
            coreCom.EmployeeID = model.EmployeeID;
            coreCom.LeaveTypeId = model.LeaveTypeId;
            coreCom.StartDate = StartDate;
            coreCom.EndDate =EndDate;
            coreCom.NoOfDay = Convert.ToDecimal(model.NoOfDay);
            coreCom.HalfDay = "N";
            coreCom.FirstOrSecondHalf = "";
            coreCom.Reason = model.Reason;
            coreCom.BossEmpAutoId = model.BossEmpAutoId;
            coreCom.HOD = model.HOD;
            coreCom.IsApproved = "";
            coreCom.ConfirmationRemarks = "";
            coreCom.CompanyCode = "001";
            coreCom.HODApprovalStatus = "";
            coreCom.HODApprovalRemarks = "";
            coreCom.HRApprovalStatus = "Pending";
            coreCom.HRApprovalRemarks = "";
            coreCom.ApplyLeaveFormat = model.ApplyLeaveFormat;
            coreCom.LeaveApplyProcess = "Manual";
            coreCom.LUser = LoginEmployeeID;
            coreCom.LDate = DateTime.Now;
            if (model.ApplyLeaveFormat == "ShortLeave")
            {
                TimeSpan spanEndTIme = DateTime.ParseExact(model.ShortLeaveFrom,
                                    "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                DateTime fromtime = Convert.ToDateTime(TodayDate + " " + spanEndTIme);

                TimeSpan spanEndTIme2 = DateTime.ParseExact(model.ShortLeaveTime,
                                        "hh:mm", CultureInfo.InvariantCulture).TimeOfDay;
                coreCom.ShortLeaveFrom = fromtime;
                coreCom.ShortLeaveTo = fromtime;
                coreCom.ShortLeaveTime = spanEndTIme2;
            }
            
            context.HRM_LeaveApplicationEntry.Add(coreCom);
            context.SaveChanges();
            var data =  context.Entry(coreCom).GetDatabaseValues();
            return coreCom.LeaveAppEntryId;
        }
        public void SaveLeaveDaysInfo(HRM_LeaveApplicationDays model, string LoginEmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            context.HRM_LeaveApplicationDays.Add(model);
            context.SaveChanges();
        }
        public Model_HRM_LeaveApplicationEntry GetInfo(string LeaveAppEntryId)
        {
            var db = new GCTL_ERP_DB_MVC_06_27Entities();
            
            var result = (from psi in db.HRM_LeaveApplicationEntry
                          .Where(psi => psi.LeaveAppEntryId == LeaveAppEntryId).DefaultIfEmpty().AsEnumerable()
                          select new
                          {
                              LeaveAppEntryId = psi.LeaveAppEntryId,
                              EmployeeID = psi.EmployeeID,
                              BossEmpAutoId = psi.BossEmpAutoId,
                              HOD = psi.HOD,
                              ApplyLeaveFormat = psi.ApplyLeaveFormat,
                              LeaveTypeId = psi.LeaveTypeId,
                              StartDate = psi.StartDate,
                              NoOfDay = psi.NoOfDay,
                              EndDate = psi.EndDate,
                              ShortLeaveFrom = psi.ShortLeaveFrom,
                              ShortLeaveTime =  psi.ShortLeaveTime==null? null : DateTime.ParseExact(psi.ShortLeaveTime.ToString(),"hh:mm:ss", CultureInfo.InvariantCulture).TimeOfDay.ToString(),
                              ShortLeaveTO = psi.ShortLeaveTo,
                              Reason = psi.Reason
                          }).AsEnumerable().Select(a => new Model_HRM_LeaveApplicationEntry()
                          {
                              LeaveAppEntryId = a.LeaveAppEntryId.ToString(),
                              EmployeeID = a.EmployeeID,
                              BossEmpAutoId = a.BossEmpAutoId,
                              HOD = a.HOD,
                              
                              ApplyLeaveFormat = a.ApplyLeaveFormat,
                              LeaveTypeId = a.LeaveTypeId,
                              StartDate = ((DateTime)a.StartDate).ToString("dd/MM/yyyy"),
                              NoOfDay = a.NoOfDay,
                              EndDate = ((DateTime)a.EndDate).ToString("dd/MM/yyyy"),       
                              ShortLeaveFrom = a.ShortLeaveFrom == null ? null : ((DateTime)(a.ShortLeaveFrom)).ToString("hh:mm:ss tt"),
                              ShortLeaveTime = a.ShortLeaveTime,
                              ShortLeaveTo = a.ShortLeaveTO == null ? null : ((DateTime)(a.ShortLeaveTO)).ToString("hh:mm:ss tt"),
                              Reason =a.Reason
                          }).FirstOrDefault();
            
            return result;
        }

        public void SendMailAsync(LeaveInfoMail lmodel)
        {
            //LeaveInfoMail models = new LeaveInfoMail();

             // ToInvoice is a model, you can pass parameters if needed


            var message = new MailMessage();
            //message.To.Add(new MailAddress("tawfiq_islam@yahoo.com"));  
            message.To.Add(new MailAddress("eng.estiakahmed@gmail.com"));
            message.From = new MailAddress("gctlproject@gmail.com");
            message.Subject = "Leave Application From " + lmodel.EmployeeNAme;
            message.Body = lmodel.FormatString;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "gctlproject@gmail.com",
                    Password = "##Gctl12345##"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }

        public Model_EmployeeBasicInfo GetEmployeeInfo(string EmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from hrm in context.HRM_EmployeeOfficialInfo
                          from gn in context.HRM_Employee.Where(gn=>gn.EmployeeID==hrm.EmployeeID).DefaultIfEmpty()
                          from depart in context.HRM_Def_Department.Where(depart => depart.DepartmentCode == hrm.DepartmentCode)
       .DefaultIfEmpty()
                          from Desig in context.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == hrm.DesignationCode)
     .DefaultIfEmpty()
                          select new
                          {
                              EmployeeID = hrm.EmployeeID,
                              EmployeeName = gn.FirstName+" "+gn.LastName,                         
                              MobileNo = hrm.MobileNo,
                              Email = hrm.Email,
                              DepartmentCode = depart.DepartmentName,
                              DesignationCode = Desig.DesignationName,
                              ReportingTo = hrm.ReportingTo,
                              HeadofDep = hrm.HOD,
                          }).AsEnumerable().Select(a => new Model_EmployeeBasicInfo()
                          {
                              EmployeeID = a.EmployeeID,
                              EmployeeName=a.EmployeeName,
                              DepartmentCode=a.DepartmentCode,
                              DesignationCode=a.DesignationCode,
                              ReportingTo = a.ReportingTo,
                              HeadOfDep = a.HeadofDep
                              }).Where(a => a.EmployeeID == EmployeeID).FirstOrDefault();
                return result;
            
        }


        public bool UpdateInfo(string id, Model_HRM_LeaveApplicationEntry model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_LeaveApplicationEntry.FirstOrDefault(x => x.LeaveAppEntryId == model.LeaveAppEntryId);
            if (result != null)
            {
                DateTime StartDate = new DateTime();
                StartDate = DateTime.ParseExact(model.StartDate, "dd/MM/yyyy", null);

                DateTime EndDate = new DateTime();
                EndDate = DateTime.ParseExact(model.EndDate, "dd/MM/yyyy", null);

                result.EmployeeID = model.EmployeeID;
                result.BossEmpAutoId = model.BossEmpAutoId;
                result.HOD= model.HOD;
                result.ApplyLeaveFormat = model.ApplyLeaveFormat;
                result.LeaveTypeId = model.LeaveTypeId;
                result.StartDate=StartDate;
                result.EndDate = EndDate;
                result.NoOfDay = Convert.ToDecimal(model.NoOfDay);
                result.Reason = model.Reason;
                result.ModifyDate = DateTime.Now;
                if (model.ApplyLeaveFormat == "ShortLeave")
                {
                    string TodayDate = DateTime.Now.ToString("yyyy-MM-dd");
                    TimeSpan spanEndTIme = DateTime.ParseExact(model.ShortLeaveFrom,
                                        "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    DateTime fromtime = Convert.ToDateTime(TodayDate + " " + spanEndTIme);

                    TimeSpan spanEndTIme2 = DateTime.ParseExact(model.ShortLeaveTime,
                                            "hh:mm", CultureInfo.InvariantCulture).TimeOfDay;
                    result.ShortLeaveFrom = fromtime;
                    result.ShortLeaveTo = fromtime;
                    result.ShortLeaveTime = spanEndTIme2;
                }

            }
            context.SaveChanges();
            return true;
        }

        public bool DeleteExistInfo(string LeaveID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_LeaveApplicationDays.Where(x => x.LeaveAppEntryId == LeaveID).ToList();
            if (result != null)
            {context.HRM_LeaveApplicationDays.RemoveRange(context.HRM_LeaveApplicationDays.Where(c => c.LeaveAppEntryId == LeaveID));
                //context.HRM_LeaveApplicationDays.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteInfo(string LeaveAppEntryId)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_LeaveApplicationEntry.FirstOrDefault(x => x.LeaveAppEntryId == LeaveAppEntryId);
            if (result != null)
            {
                context.HRM_LeaveApplicationEntry.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Model_LeaveDateList>  GetEmployeeLeaveDays(string EmployeeID)
        {
            var returnModel = new List<Model_LeaveDateList>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetLeaveDays";

                var sParam = cmd.CreateParameter();
                sParam.DbType = DbType.String;
                sParam.ParameterName = "@EmployeeID";
                sParam.Value = EmployeeID;
                sParam.IsNullable = false;
                cmd.Parameters.Add(sParam);
                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_LeaveDateList>(reader);
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
