
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crd_HRM_EmployeeOfficialInfo
    {

        public List<Model_HRM_EmployeeOfficialInfo> GetAllEmployee()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from offic in context.HRM_EmployeeOfficialInfo
                              from gen in context.HRM_Employee.Where(gen => gen.EmployeeID == offic.EmployeeID)
           .DefaultIfEmpty()
                              from depart in context.HRM_Def_Department.Where(depart => depart.DepartmentCode ==offic.DepartmentCode)
           .DefaultIfEmpty()
                              from Desig in context.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == offic.DesignationCode)
         .DefaultIfEmpty()
                              from shi in context.HRM_ATD_Shift.Where(shi=> shi.ShiftCode == offic.ShiftCode)
             .DefaultIfEmpty()
                              select new Model_HRM_EmployeeOfficialInfo()
                              {
                                  EmployeeID = offic.EmployeeID,
                                  EmployeeName = gen.FirstName+" "+gen.LastName,
                                  DepartmentCode = depart.DepartmentName,
                                  DesignationCode = Desig.DesignationName,
                                  ShiftCode=shi.ShiftName

                              }).ToList();
                return result;
            }

        }
        public Model_HRM_EmployeeOfficialInfo GetEmployee(string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = context.HRM_EmployeeOfficialInfo.Where(x => x.EmployeeID == EmployeeID).AsEnumerable().Select(x => new Model_HRM_EmployeeOfficialInfo()
                {
                    EmployeeID = x.EmployeeID,
                    CompanyCode=x.CompanyCode,
                    BranchCode=x.BranchCode,
                    DepartmentCode = x.DepartmentCode,
                    DesignationCode = x.DesignationCode,
                    ShiftCode = x.ShiftCode,
                    EmpTypeCode= x.EmpTypeCode,
                    EmploymentNatureId=x.EmploymentNatureId,
                    GrossSalary=x.GrossSalary.ToString(),
                    ReportingTo = x.ReportingTo,
                    HOD = x.HOD,                    
                    JoiningDate = ((DateTime)x.JoiningDate).ToString("dd/MM/yyyy"),
                    EmployeeStatus = x.EmployeeStatus,
                    MobileNo = x.MobileNo,
                    Email = x.Email                    
                }).FirstOrDefault();

                return result;
            }

        }


        public bool DeleteCompany(string id)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var EmployeeID = context.HRM_EmployeeOfficialInfo.FirstOrDefault(x => x.EmployeeID == id);
                if (EmployeeID != null)
                {
                    context.HRM_EmployeeOfficialInfo.Remove(EmployeeID);
                    context.SaveChanges();
                    return true;
                }
                return false;


            }

        }
    }
}
