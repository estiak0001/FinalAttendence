using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Crud_HRM_ATD_AttendanceType
    {
        public List<Model_HRM_ATD_AttendanceType> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = context.HRM_ATD_AttendanceType.Select(x => new Model_HRM_ATD_AttendanceType()
                {
                    AttendanceTypeCode = x.AttendanceTypeCode,
                    AttendanceTypeName = x.AttendanceTypeName,
                    ShortName=x.ShortName
                }).ToList();
                return result;
            }
        }


        public string SaveInfo(Model_HRM_ATD_AttendanceType model,string LoginEmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_ATD_AttendanceType coreCom = new HRM_ATD_AttendanceType();
            coreCom.AttendanceTypeCode = model.AttendanceTypeCode;
            coreCom.AttendanceTypeName = model.AttendanceTypeName;
            if(model.ShortName !=null)
            {
                coreCom.ShortName = model.ShortName;
            }
            else
            {
                coreCom.ShortName = "";
            }
            coreCom.LUser = LoginEmployeeID;
            coreCom.LDate = DateTime.Now;
            
            context.HRM_ATD_AttendanceType.Add(coreCom);
            context.SaveChanges();
            return coreCom.AttendanceTypeCode;
        }
        public Model_HRM_ATD_AttendanceType GetInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_AttendanceType.Where(x => x.AttendanceTypeCode == id).Select(x => new Model_HRM_ATD_AttendanceType()
            {
                AttendanceTypeCode = x.AttendanceTypeCode,
                AttendanceTypeName = x.AttendanceTypeName,
                ShortName=x.ShortName
            }).FirstOrDefault();

            return result;
        }



        public bool UpdateInfo(string id, Model_HRM_ATD_AttendanceType model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_AttendanceType.FirstOrDefault(x => x.AttendanceTypeCode == id);
            if (result != null)
            {
                result.AttendanceTypeName = model.AttendanceTypeName;
                result.ShortName = model.ShortName;
                
            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_AttendanceType.FirstOrDefault(x => x.AttendanceTypeCode == id);
            if (result != null)
            {
                context.HRM_ATD_AttendanceType.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
