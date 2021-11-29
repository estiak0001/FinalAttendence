using PXLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogic.Repository
{
    
    public class Crud_Core_UserInfo
    {
        PXlibrary Pxlib = new PXlibrary();
        public List<Model_Core_UserInfo> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from mo in context.Core_UserInfo
                              from em in context.HRM_Employee.Where(hrm => hrm.EmployeeID == mo.EmployeeID.ToString()).DefaultIfEmpty()
                              select new
                              {
                                  id = mo.ID,
                                  username = mo.username,
                                  EmployeeID = mo.EmployeeID,
                                  EmployeeName = em.FirstName+" "+em.LastName,
                                  AccessCode = mo.AccessCode,
                                  Role = mo.Role,
                                  Active = mo.Status == "1" ? "Active" : "Inactive",
                                  entryDate = mo.EntryDate
                              }).AsEnumerable().Select(a => new Model_Core_UserInfo()
                              {
                                  ID = a.id.ToString(),
                                  username = a.username.ToString(),
                                  EmployeeID = a.EmployeeID,
                                  EmployeeName=a.EmployeeName,
                                  AccessCode = a.AccessCode,
                                  Role = a.Role,
                                  Status = a.Active,
                                  EntryDate = a.entryDate == null ? "" : string.Format("{0:MM/dd/yyyy}", a.entryDate)
                              }).ToList();
                return result;
            }
        }


        public string SaveInfo(Model_Core_UserInfo model, string LoginEmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            Core_UserInfo coreCom = new Core_UserInfo();
            coreCom.username = model.username;
            coreCom.AccessCode= model.AccessCode;
            coreCom.EmployeeID =model.EmployeeID;
            coreCom.Role =model.Role;

            string Userpassword = "";
            //Pxlib.(ref Userpassword, txtPassword.Text.Trim());
            Pxlib.PXEncode(ref Userpassword, model.password);

            coreCom.password= Userpassword;
            coreCom.EntryDate = DateTime.Now;
            coreCom.LUser =LoginEmployeeID;
            coreCom.EntryDate = DateTime.Now;
            coreCom.Status = model.Status;
            context.Core_UserInfo.Add(coreCom);
            context.SaveChanges();
            return coreCom.EmployeeID;
        }
        public Model_Core_UserInfo GetInfo(string Id)
        {
            var ii = Int32.Parse(Id);
            var db = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from psi in db.Core_UserInfo
                          .Where(psi => psi.ID == ii).DefaultIfEmpty().AsEnumerable()
                          from em in db.HRM_Employee.Where(hrm => hrm.EmployeeID == psi.EmployeeID.ToString()).DefaultIfEmpty()
                          select new
                          {
                              username = psi.username,
                              EmployeeID = psi.EmployeeID,
                              AccessCode = psi.AccessCode,
                              password = psi.password,
                              Role = psi.Role,
                              EmployeName = psi.FirstName + " " + psi.LastName,
                              entryDate = psi.EntryDate,
                              modifyDate = psi.ModifyDate,
                          }).AsEnumerable().Select(a => new Model_Core_UserInfo()
                          {
                              username = a.username,
                              EmployeeID =a.EmployeeID,
                              AccessCode = a.AccessCode,
                              password = a.password,
                              Role = a.Role,
                              EntryDate = a.entryDate == null ? "" : string.Format("{0:MM/dd/yyyy}", a.entryDate),
                              ModifiedDate = a.modifyDate == null ? "" : string.Format("{0:MM/dd/yyyy}", a.modifyDate),
                          }).FirstOrDefault();
            return result;
        }

        public Model_EmployeeBasicInfo GetEmployeeInfo(string EmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from hrm in context.HRM_EmployeeOfficialInfo.Where(gnn => gnn.EmployeeID == EmployeeID).DefaultIfEmpty()
                          from gn in context.HRM_Employee.Where(gn => gn.EmployeeID == hrm.EmployeeID).DefaultIfEmpty()
                          from depart in context.HRM_Def_Department.Where(depart => depart.DepartmentCode == hrm.DepartmentCode)
                         .DefaultIfEmpty()
                          from Desig in context.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == hrm.DesignationCode)
                          .DefaultIfEmpty()
                          from com in context.Core_Company.Where(c => c.CompanyCode == hrm.CompanyCode)
                          .DefaultIfEmpty()
                          from br in context.Core_Branch.Where(c => c.BranchCode == hrm.BranchCode)
                          .DefaultIfEmpty()
                          from et in context.HRM_Def_EmpType.Where(c => c.EmpTypeCode == hrm.EmpTypeCode)
                          .DefaultIfEmpty()
                          from ent in context.HRM_EIS_Def_EmploymentNature.Where(c => c.EmploymentNatureId == hrm.EmploymentNatureId)
                          .DefaultIfEmpty()
                          from user in context.Core_UserInfo.Where(c => c.EmployeeID == hrm.EmployeeID)
                          .DefaultIfEmpty()
                          select new
                          {
                              EmployeeID = hrm.EmployeeID,
                              EmployeeName = gn.FirstName + " " + gn.LastName,
                              MobileNo = hrm.MobileNo,
                              Email = hrm.Email,
                              DepartmentCode = depart.DepartmentName,
                              DesignationCode = Desig.DesignationName,
                              nationalID = gn.NationalIDNO,
                              company = com.CompanyName,
                              branch = br.BranchName,
                              Department = depart.DepartmentName,
                              designation = Desig.DesignationName,
                              empType = et.EmpTypeName,
                              empNature = ent.EmploymentNature,
                              offPhone = hrm.MobileNo,
                              offEmail = hrm.Email,
                              joining = hrm.JoiningDate,
                              active = user.Status,
                              session = user.SingleSession,
                          }).AsEnumerable().Select(a => new Model_EmployeeBasicInfo()
                          {
                              EmployeeID = a.EmployeeID,
                              EmployeeName = a.EmployeeName,
                              DepartmentCode = a.DepartmentCode,
                              DesignationCode = a.DesignationCode,
                              JoiningDate = string.Format("{0:MM/dd/yyyy}", a.joining),
                              NationalID = a.nationalID,
                              Company = a.company,
                              Branch = a.branch,
                              Department = a.Department,
                              EmployeeType = a.empType,
                              EmployeeNature = a.empNature,
                              OfficePhone = a.offPhone,
                              OfficeEmail = a.Email,
                              IsActive = a.active == "1"? true : false,
                              Session = a.session
                          }).FirstOrDefault(a => a.EmployeeID == EmployeeID);
                return result;
            
        }


        public bool UpdateInfo(string id, Model_Core_UserInfo model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.Core_UserInfo.FirstOrDefault(x => x.EmployeeID == model.EmployeeID);
            if (result != null)
            {               
                result.username = model.username;
                result.AccessCode = model.AccessCode;
                result.Role = model.Role;
                string Userpassword = "";                
                Pxlib.PXEncode(ref Userpassword, model.password);
                result.password = Userpassword;
                result.ModifyDate = DateTime.Now;
            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string EmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.Core_UserInfo.FirstOrDefault(x => x.EmployeeID == EmployeeID);
            if (result != null)
            {
                context.Core_UserInfo.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
