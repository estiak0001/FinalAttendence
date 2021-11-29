using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crud_HRM_LeaveApprovalEntry
    {
        public List<Model_HRM_LeaveApplicationEntry> GetAllInfo()
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
                                  EmployeeName = em.FirstName+" "+em.LastName,
                                  StartDate = mo.StartDate,
                                  EndDate = mo.EndDate,
                                  NoOfDay = mo.NoOfDay,
                                  Reason = mo.Reason,
                        HRApprovalStatus = mo.HRApprovalStatus

                              }).Where(t => t.HRApprovalStatus == "").AsEnumerable().Select(a => new Model_HRM_LeaveApplicationEntry()
                              {
                                  LeaveAppEntryId = a.LeaveAppEntryId.ToString(),
                                  EmployeeID = a.EmployeeID,
                                  EmployeeName = a.EmployeeName,
                                  ApplyDate = ((DateTime)a.ApplyDate).ToString("dd/MM/yyyy"),
                                  StartDate = ((DateTime)a.StartDate).ToString("dd/MM/yyyy"),
                                  EndDate = ((DateTime)a.EndDate).ToString("dd/MM/yyyy"),
                                  NoOfDay = a.NoOfDay,
                                  Reason=a.Reason
                              }).ToList();
                return result;
            }
        }
        public List<Model_HRM_LeaveApplicationEntry> GetAllApproveInfo(string CompanyCode, string DepartmentCode, string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (
                    from mo in context.HRM_LeaveApplicationEntry
                    from emof in context.HRM_EmployeeOfficialInfo.Where(emof => emof.EmployeeID == mo.EmployeeID.ToString()).DefaultIfEmpty()
                    from em in context.HRM_Employee.Where(em => em.EmployeeID == emof.EmployeeID.ToString()).DefaultIfEmpty()
                    from emdep in context.HRM_Def_Department.Where(emdep => emdep.DepartmentCode == emof.DepartmentCode.ToString()).DefaultIfEmpty()
                             .DefaultIfEmpty()
                    select new
                    {
                        LeaveAppEntryId = mo.LeaveAppEntryId,
                        EmployeeID = mo.EmployeeID,
                        ApplyDate = mo.LDate,
                        EmployeeName = em.FirstName + " " + em.LastName,
                        StartDate = mo.StartDate,
                        EndDate = mo.EndDate,
                        NoOfDay = mo.NoOfDay,
                        Reason = mo.Reason,
                        HRApprovalStatus = mo.HRApprovalStatus,
                        departmentcode = emdep.DepartmentCode

                    }).Where(m=> (m.departmentcode == DepartmentCode || DepartmentCode == "") && (m.EmployeeID == EmployeeID || EmployeeID == "")).AsEnumerable().Select(a => new Model_HRM_LeaveApplicationEntry()
                    {
                        LeaveAppEntryId = a.LeaveAppEntryId.ToString(),
                        EmployeeID = a.EmployeeID,
                        EmployeeName = a.EmployeeName,
                        ApplyDate = ((DateTime)a.ApplyDate).ToString("dd/MM/yyyy"),
                        StartDate = ((DateTime)a.StartDate).ToString("dd/MM/yyyy"),
                        EndDate = ((DateTime)a.EndDate).ToString("dd/MM/yyyy"),
                        NoOfDay = a.NoOfDay,
                        Reason = a.Reason,
                        HRApprovalStatus = a.HRApprovalStatus
                    }).ToList();
                return result;
            }
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
                             Reason=a.Reason
                          }).FirstOrDefault();
            return result;
        }
        public Model_HRM_LeaveApplicationEntry GetPopupLoadInfo(string LeaveAppEntryId)
        {
            var db = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from psi in db.HRM_LeaveApplicationEntry
                          .Where(psi => psi.LeaveAppEntryId == LeaveAppEntryId).DefaultIfEmpty()
                          from hrm in db.HRM_EmployeeOfficialInfo.Where(x => psi.EmployeeID == x.EmployeeID).DefaultIfEmpty()
                          from gn in db.HRM_Employee.Where(gn => gn.EmployeeID == hrm.EmployeeID).DefaultIfEmpty()
                          from boss in db.HRM_Employee.Where(bss => bss.EmployeeID == psi.BossEmpAutoId).DefaultIfEmpty()
                          from hod in db.HRM_Employee.Where(hod => hod.EmployeeID == psi.HOD).DefaultIfEmpty()
                          from lt in db.HRM_ATD_LeaveType.Where(lt => lt.LeaveTypeId == psi.LeaveTypeId).DefaultIfEmpty()
                          from depart in db.HRM_Def_Department.Where(depart => depart.DepartmentCode == hrm.DepartmentCode)
                                .DefaultIfEmpty()
                          from Desig in db.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == hrm.DesignationCode)
                                .DefaultIfEmpty()

                          select new
                          {
                              LeaveAppEntryId = psi.LeaveAppEntryId,
                              EmployeeIDwithName = gn.FirstName + gn.LastName + " (" + psi.EmployeeID + ")",
                              EmployeeName = gn.FirstName + " " + gn.LastName,
                              BossEmpAutoId = boss.FirstName + " " + boss.LastName,
                              HOD = hod.FirstName + " " + hod.LastName,
                              ApplyLeaveFormat = psi.ApplyLeaveFormat,
                              LeaveTypeId = lt.Name,
                              StartDate = psi.StartDate,
                              NoOfDay = psi.NoOfDay,
                              EndDate = psi.EndDate,
                              Reason = psi.Reason,
                              DepartmentCode = depart.DepartmentName,
                              DesignationCode = Desig.DesignationName,
                              ShortLeaveFrom = psi.ShortLeaveFrom,
                              ShortLeaveTime = psi.ShortLeaveTime == null ? null : psi.ShortLeaveTime.ToString().Substring(0, 5),
                              ShortLeaveTO = psi.ShortLeaveTo,
                              HRApprovalStatus = psi.HRApprovalStatus,

                          }).AsEnumerable().Select(a => new Model_HRM_LeaveApplicationEntry()
                          {
                              LeaveAppEntryId = a.LeaveAppEntryId.ToString(),
                              EmployeeID = a.EmployeeIDwithName,
                              EmployeeName = a.EmployeeName,
                              DesignationCode = a.DesignationCode,
                              DepartmentCode = a.DepartmentCode,
                              BossEmpAutoId = a.BossEmpAutoId,
                              HOD = a.HOD,
                              HRApprovalStatus = a.HRApprovalStatus,
                              ApplyLeaveFormat = a.ApplyLeaveFormat,
                              LeaveTypeId = a.LeaveTypeId,
                              StartDate = ((DateTime)a.StartDate).ToString("dd/MM/yyyy"),
                              NoOfDay = a.NoOfDay,
                              EndDate = ((DateTime)a.EndDate).ToString("dd/MM/yyyy"),
                              ShortLeaveFrom = a.ShortLeaveFrom == null ? null : ((DateTime)(a.ShortLeaveFrom)).ToString("hh:mm:ss tt"),
                              ShortLeaveTime = a.ShortLeaveTime,
                              ShortLeaveTo = a.ShortLeaveTO == null ? null : ((DateTime)(a.ShortLeaveTO)).ToString("hh:mm:ss tt"),
                              Reason = a.Reason
                          }).FirstOrDefault();
            return result;
        }
       
        public Model_HRM_LeaveApplicationEntry GetApprovedInfo(string LeaveAppEntryId)
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
                              Reason = psi.Reason,
                              HRApprovalStatus = psi.HRApprovalStatus,
                              HRApprovalRemarks = psi.HRApprovalRemarks
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
                              Reason = a.Reason,
                              HRApprovalStatus=a.HRApprovalStatus,
                              HRApprovalRemarks=a.HRApprovalRemarks
                          }).FirstOrDefault();
            return result;
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
                              EmployeeName = gn.FirstName+gn.LastName,                         
                              MobileNo = hrm.MobileNo,
                              Email = hrm.Email,
                              DepartmentCode = depart.DepartmentName,
                              DesignationCode = Desig.DesignationName,
                          }).AsEnumerable().Select(a => new Model_EmployeeBasicInfo()
                          {
                              EmployeeID = a.EmployeeID,
                              EmployeeName=a.EmployeeName,
                              DepartmentCode=a.DepartmentCode,
                              DesignationCode=a.DesignationCode
                              }).Where(a => a.EmployeeID == EmployeeID).FirstOrDefault();
                return result;
            
        }


        public bool UpdateInfo(string id, Model_HRM_LeaveApplicationEntry model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_LeaveApplicationEntry.FirstOrDefault(x => x.LeaveAppEntryId == model.LeaveAppEntryId);
            if (result != null)
            {
                if(model.HRApprovalRemarks == null)
                {
                    model.HRApprovalRemarks = "";
                }
                result.HRApprovalStatus = model.HRApprovalStatus;
                result.HRApprovalRemarks = model.HRApprovalRemarks;
                result.ModifyDate = DateTime.Now;
            
            }
            context.SaveChanges();
            return true;
        }
    

    }
}
