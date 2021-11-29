
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Crd_Core_Company
    {
        public List<Model_Core_Company> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = context.Core_Company.Select(x => new Model_Core_Company()
                {
                    CompanyCode = x.CompanyCode,
                    CompanyName = x.CompanyName,
                    CompanyShortName=x.CompanyShortName,
                    Address1 = x.Address1,
                    Address2=x.Address2,
                    Phone1 = x.Phone1,
                    Phone2=x.Phone2,
                    Fax=x.Fax,
                    HotLine=x.HotLine,
                    URL=x.URL,
                    Email = x.Email
                }).ToList();
                return result;
            }
        }


        public string SaveInfo(Model_Core_Company model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            Core_Company coreCom = new Core_Company();
            coreCom.CompanyCode = model.CompanyCode;
            coreCom.CompanyName = model.CompanyName;
            coreCom.CompanyShortName = model.CompanyShortName;
            coreCom.Address1 = model.Address1;
            coreCom.Address2 = model.Address2;
            coreCom.Phone1 = model.Phone1;
            coreCom.Phone2 = model.Phone2;
            coreCom.Fax = model.Fax;
            coreCom.HotLine = model.HotLine;
            coreCom.URL = model.URL;
            coreCom.Email = model.Email;
            coreCom.LUser = "LoginUser";
            coreCom.LDate = DateTime.Now;

            context.Core_Company.Add(coreCom);
            context.SaveChanges();
            return coreCom.CompanyCode;
        }


        public Model_Core_Company GetInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.Core_Company.Where(x => x.CompanyCode == id).Select(x => new Model_Core_Company()
            {
                CompanyCode = x.CompanyCode,
                CompanyName = x.CompanyName,
                CompanyShortName = x.CompanyShortName,
                Address1 = x.Address1,
                Address2 = x.Address2,
                Phone1 = x.Phone1,
                Phone2=x.Phone2,
                Fax=x.Fax,
                HotLine=x.HotLine,
                URL=x.URL,
                Email = x.Email

            }).FirstOrDefault();
            return result;
        }

        public bool UpdateInfo(string id, Model_Core_Company model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.Core_Company.FirstOrDefault(x => x.CompanyCode == id);
            if (result != null)
            {
                result.CompanyName = model.CompanyName;
                result.CompanyShortName = model.CompanyShortName;
                result.Address1 = model.Address1;
                result.Address2 = model.Address2;
                result.Phone1 = model.Phone1;
                result.Phone2 = model.Phone2;
                result.Fax = model.Fax;
                result.HotLine = model.HotLine;
                result.URL = model.URL;
                result.Email = model.Email;
            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.Core_Company.FirstOrDefault(x => x.CompanyCode == id);
            if (result != null)
            {
                context.Core_Company.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
