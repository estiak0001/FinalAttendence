using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Crud_HRM_Def_Designation
    {
        public List<Model_HRM_Def_Designation> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = context.HRM_Def_Designation.Select(x => new Model_HRM_Def_Designation()
                {
                    DesignationCode = x.DesignationCode,
                    DesignationName = x.DesignationName,
                    DesignationShortName = x.DesignationShortName
                }).ToList();
                return result;
            }
        }


        public string SaveInfo(Model_HRM_Def_Designation model,string LoginEmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_Def_Designation coreCom = new HRM_Def_Designation();
            coreCom.DesignationCode = model.DesignationCode;
            coreCom.DesignationName = model.DesignationName;
            coreCom.DesignationShortName = model.DesignationShortName;
            if(model.BanglaDesignation !=null)
            {
                coreCom.BanglaDesignation = model.BanglaDesignation;
            }
            else
            {
                coreCom.BanglaDesignation = "";
            }
            if (model.BanglaShortName != null)
            {
                coreCom.BanglaShortName = model.BanglaShortName;
            }
            else
            {
                coreCom.BanglaShortName = "";
            }
            coreCom.LUser = LoginEmployeeID;
            coreCom.LDate = DateTime.Now;
            
            context.HRM_Def_Designation.Add(coreCom);
            context.SaveChanges();
            return coreCom.DesignationCode;
        }
        public Model_HRM_Def_Designation GetInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_Def_Designation.Where(x => x.DesignationCode == id).Select(x => new Model_HRM_Def_Designation()
            {
                DesignationCode = x.DesignationCode,
                DesignationName = x.DesignationName,
                DesignationShortName = x.DesignationShortName
            }).FirstOrDefault();

            return result;
        }



        public bool UpdateInfo(string id, Model_HRM_Def_Designation model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_Def_Designation.FirstOrDefault(x => x.DesignationCode == id);
            if (result != null)
            {
                result.DesignationName = model.DesignationName;
                result.DesignationShortName = model.DesignationShortName;
                if (model.BanglaDesignation != null)
                {
                    result.BanglaDesignation = model.BanglaDesignation;
                }
                else
                {
                    result.BanglaDesignation = "";
                }
                if (model.BanglaShortName != null)
                {
                    result.BanglaShortName = model.BanglaShortName;
                }
                else
                {
                    result.BanglaShortName = "";
                }
            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_Def_Designation.FirstOrDefault(x => x.DesignationCode == id);
            if (result != null)
            {
                context.HRM_Def_Designation.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
