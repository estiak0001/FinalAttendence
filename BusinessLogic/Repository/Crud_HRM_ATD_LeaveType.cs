using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crud_HRM_ATD_LeaveType
    {
        public List<Model_HRM_ATD_LeaveType> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from mo in context.HRM_ATD_LeaveType
                              from em in context.Acc_Duration_Type.Where(em => em.Duration_TypeCode == mo.YMWD.ToString()).DefaultIfEmpty()
                              .DefaultIfEmpty()
                              select new
                              {
                                  LeaveTypeId = mo.LeaveTypeId,
                                  Name = mo.Name,
                                  ShortName = mo.ShortName,
                                  RulePolicy = mo.RulePolicy,
                                  NoOfDay = mo.NoOfDay,
                                  For = mo.For,
                                  YMWD = em.durationType,
                                  WEF = mo.WEF
                              }).AsEnumerable().Select(a => new Model_HRM_ATD_LeaveType()
                              {
                                  LeaveTypeId = a.LeaveTypeId.ToString(),
                                  Name=a.Name,
                                  ShortName=a.ShortName,
                                  RulePolicy=a.RulePolicy,
                                  NoOfDay=a.NoOfDay,
                                  For=a.For,
                                  WEF = ((DateTime)a.WEF).ToString("dd/MM/yyyy")                                  
                              }).ToList();
                return result;
            }
        }
        public string SaveInfo(Model_HRM_ATD_LeaveType model, string LoginEmployeeID)
        {
            DateTime WEF = new DateTime();
            WEF = DateTime.ParseExact(model.WEF, "dd/MM/yyyy", null);            
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_ATD_LeaveType coreCom = new HRM_ATD_LeaveType();
            coreCom.LeaveTypeId = model.LeaveTypeId;
            coreCom.Name = model.Name;
            coreCom.ShortName = model.ShortName;
            coreCom.RulePolicy = model.RulePolicy;
            decimal Nod = 0;
            if (model.NoOfDay.ToString() !="")
            {
                Nod = Convert.ToDecimal(model.NoOfDay);
            }
            decimal Fo= 0;
            if (model.For.ToString() != "")
            {
                Fo = Convert.ToDecimal(model.For);
            }

            coreCom.NoOfDay =Nod;
            coreCom.For = Fo;
            coreCom.YMWD =model.YMWD;
            coreCom.WEF = WEF;
            coreCom.LUser =LoginEmployeeID;
            coreCom.LDate = DateTime.Now;           
            context.HRM_ATD_LeaveType.Add(coreCom);
            context.SaveChanges();
            return coreCom.LeaveTypeId;
        }
        public Model_HRM_ATD_LeaveType GetInfo(string LeaveTypeId)
        {
            
            var db = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from psi in db.HRM_ATD_LeaveType
                          .Where(psi => psi.LeaveTypeId == LeaveTypeId).DefaultIfEmpty().AsEnumerable()
                          select new
                          {
                              LeaveTypeId = psi.LeaveTypeId,
                              Name = psi.Name,
                              ShortName = psi.ShortName,
                              RulePolicy = psi.RulePolicy,
                              NoOfDay = psi.NoOfDay,
                              For = psi.For,
                              YMWD=psi.YMWD,
                              WEF = psi.WEF
                          }).AsEnumerable().Select(a => new Model_HRM_ATD_LeaveType()
                          {
                              LeaveTypeId = a.LeaveTypeId.ToString(),
                              Name=a.Name,
                              ShortName=a.ShortName,
                              RulePolicy=a.RulePolicy,
                              NoOfDay=a.NoOfDay,
                              For=a.For,
                              YMWD=a.YMWD,
                              WEF = ((DateTime)a.WEF).ToString("dd/MM/yyyy")                              
                          }).FirstOrDefault();
            return result;
        }

     //   public Model_EmployeeBasicInfo GetEmployeeInfo(string EmployeeID)
     //   {
     //       var context = new GCTL_ERP_DB_MVC_06_27Entities();
     //       var result = (from hrm in context.HRM_EmployeeOfficialInfo
     //                     from gn in context.HRM_Employee.Where(gn=>gn.EmployeeID==hrm.EmployeeID).DefaultIfEmpty()
     //                     from depart in context.HRM_Def_Department.Where(depart => depart.DepartmentCode == hrm.DepartmentCode)
     //  .DefaultIfEmpty()
     //                     from Desig in context.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == hrm.DesignationCode)
     //.DefaultIfEmpty()
     //                     select new
     //                     {
     //                         EmployeeID = hrm.EmployeeID,
     //                         EmployeeName = gn.FirstName+gn.LastName,
                         
     //                         MobileNo = hrm.MobileNo,
     //                         Email = hrm.Email,
     //                         DepartmentCode = depart.DepartmentName,
     //                         DesignationCode = Desig.DesignationName,
     //                     }).AsEnumerable().Select(a => new Model_EmployeeBasicInfo()
     //                     {
     //                         EmployeeID = a.EmployeeID,
     //                         EmployeeName=a.EmployeeName,
     //                         DepartmentCode=a.DepartmentCode,
     //                         DesignationCode=a.DesignationCode
     //                         }).Where(a => a.EmployeeID == EmployeeID).FirstOrDefault();
     //           return result;
            
     //   }


        public bool UpdateInfo(string id, Model_HRM_ATD_LeaveType model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_LeaveType.FirstOrDefault(x => x.LeaveTypeId == model.LeaveTypeId);
            if (result != null)
            {

                DateTime WEF = new DateTime();
                WEF = DateTime.ParseExact(model.WEF, "dd/MM/yyyy", null);                
                result.Name =model.Name;
                result.ShortName =model.ShortName;
                result.RulePolicy =model.RulePolicy;
                decimal Nod = 0;
                if (model.NoOfDay.ToString() != "")
                {
                    Nod = Convert.ToDecimal(model.NoOfDay);
                }
                decimal Fo = 0;
                if (model.For.ToString() != "")
                {
                    Fo = Convert.ToDecimal(model.For);
                }
                result.NoOfDay = Nod;
                result.For = Fo;                
                result.ModifyDate = DateTime.Now;       
                result.WEF = WEF;
                result.YMWD = model.YMWD;
            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string LeaveTypeId)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_LeaveType.FirstOrDefault(x => x.LeaveTypeId == LeaveTypeId);
            if (result != null)
            {
                context.HRM_ATD_LeaveType.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
