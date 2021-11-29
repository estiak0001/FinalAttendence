using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Crud_HRM_Def_Department
    {
        public List<Model_HRM_Def_Department> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = context.HRM_Def_Department.Select(x => new Model_HRM_Def_Department()
                {
                    DepartmentCode = x.DepartmentCode,
                    DepartmentName = x.DepartmentName,
                    DepartmentShortName=x.DepartmentShortName
                }).ToList();
                return result;
            }
        }


        public string SaveInfo(Model_HRM_Def_Department model,string LoginEmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_Def_Department coreCom = new HRM_Def_Department();
            coreCom.DepartmentCode = model.DepartmentCode;
            coreCom.DepartmentName = model.DepartmentName;
            coreCom.DepartmentShortName = model.DepartmentShortName;
            coreCom.LUser = LoginEmployeeID;
            coreCom.LDate = DateTime.Now;
            
            context.HRM_Def_Department.Add(coreCom);
            context.SaveChanges();
            return coreCom.DepartmentCode;
        }
        public Model_HRM_Def_Department GetInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_Def_Department.Where(x => x.DepartmentCode == id).Select(x => new Model_HRM_Def_Department
            ()
            {
                DepartmentCode = x.DepartmentCode,
                DepartmentName = x.DepartmentName,
                DepartmentShortName=x.DepartmentShortName
            }).FirstOrDefault();

            return result;
        }



        public bool UpdateInfo(string id, Model_HRM_Def_Department model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_Def_Department.FirstOrDefault(x => x.DepartmentCode == id);
            if (result != null)
            {
                result.DepartmentName = model.DepartmentName;
                result.DepartmentShortName = model.DepartmentShortName;
            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_Def_Department.FirstOrDefault(x => x.DepartmentCode == id);
            if (result != null)
            {
                context.HRM_Def_Department.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
