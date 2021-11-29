using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crud_CompanyWeekend
    {
        ClsCommon common = new ClsCommon();
        string strMaxNO = "";

        //public List<Model_CompannyWeekend> GetAllInfo()
        //{
        //    using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
        //    {
        //        var result = context.HRM_ATD_CompanyWeekEnd.Select(x => new Model_CompannyWeekend()
        //        {
        //            CompanyWeekEndCode = x.CompanyWeekEndCode.ToString(),
        //            EffectiveDate = ((DateTime)x.EffectiveDate).ToString("dd/MM/yyyy"),
        //            Weekend = x.Weekend,

        //        }).ToList();
        //        return result;
        //    }
        //}

        public List<Model_CompannyWeekend> GetSAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from mo in context.HRM_ATD_EmployeeWeekEnd
                        .DefaultIfEmpty()
                              from em in context.HRM_Employee.Where(em => em.EmployeeID == mo.EmployeeCode.ToString()).DefaultIfEmpty()

                              select new
                              {
                                  CompanyWeekEndCode = mo.EmployeeWeekEndCode,
                                  EffectiveDate = mo.EffectiveDate,
                                  ApplyDate = mo.LDate,
                                  Weekend = mo.WeekendDay,
                                  emName = em.FirstName

                              }).AsEnumerable().Select(a => new Model_CompannyWeekend()
                              {
                                  CompanyWeekEndCode = a.CompanyWeekEndCode,
                                  Weekend = a.Weekend,
                                  EffectiveDate = a.EffectiveDate == null ? "" : ((DateTime)a.EffectiveDate).ToString("dd/MM/yyyy"),
                                  EmployeeName = a.emName
                              }).ToList();
                return result;
            }
        }

        public List<Model_CompannyWeekend> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from mo in context.HRM_ATD_CompanyWeekEnd
                        .DefaultIfEmpty()
                    select new
                    {
                        CompanyWeekEndCode = mo.CompanyWeekEndCode,
                        EffectiveDate = mo.EffectiveDate,
                        ApplyDate = mo.LDate,
                        Weekend = mo.Weekend

                    }).AsEnumerable().Select(a => new Model_CompannyWeekend()
                    {
                        CompanyWeekEndCode = a.CompanyWeekEndCode,
                        Weekend = a.Weekend,
                        EffectiveDate = ((DateTime)a.EffectiveDate).ToString("dd/MM/yyyy")
                    }).ToList();
                return result;
            }
        }
        public Model_CompannyWeekend GetInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from mo in context.HRM_ATD_CompanyWeekEnd.Where(x => x.CompanyWeekEndCode == id)
                        .DefaultIfEmpty()
                          select new
                          {
                              CompanyWeekEndCode = mo.CompanyWeekEndCode,
                              EffectiveDate = mo.EffectiveDate,
                              ApplyDate = mo.LDate,
                              Weekend = mo.Weekend

                          }).AsEnumerable().Select(a => new Model_CompannyWeekend()
                          {
                              CompanyWeekEndCode = a.CompanyWeekEndCode,
                              Weekend = a.Weekend,
                              EffectiveDate = ((DateTime)a.EffectiveDate).ToString("dd/MM/yyyy")
                          }).FirstOrDefault();
            return result;
        }

        public Model_CompannyWeekend GetSInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from mo in context.HRM_ATD_EmployeeWeekEnd.Where(x => x.EmployeeWeekEndCode == id)
                        .DefaultIfEmpty()
                          select new
                          {
                              CompanyWeekEndCode = mo.EmployeeWeekEndCode,
                              EffectiveDate = mo.EffectiveDate,
                              ApplyDate = mo.LDate,
                              Weekend = mo.WeekendDay,
                              empid = mo.EmployeeCode

                          }).AsEnumerable().Select(a => new Model_CompannyWeekend()
                          {
                              CompanyWeekEndCode = a.CompanyWeekEndCode,
                              Weekend = a.Weekend,
                              EffectiveDate = ((DateTime)a.EffectiveDate).ToString("dd/MM/yyyy"),
                              EmployeeID = a.empid
                          }).FirstOrDefault();
            return result;
        }
        public string SaveInfo(Model_CompannyWeekend model)
        {
            DateTime EffDate = new DateTime();
            EffDate = DateTime.ParseExact(model.EffectiveDate, "dd/MM/yyyy", null);

            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_ATD_CompanyWeekEnd coreCom = new HRM_ATD_CompanyWeekEnd();
            coreCom.CompanyWeekEndCode = model.CompanyWeekEndCode;
            coreCom.Weekend = model.Weekend;
            coreCom.EffectiveDate = EffDate;
            coreCom.LDate = DateTime.Now;
            coreCom.LIP = model.LIP;
            coreCom.LMAC = model.LMAC;
            coreCom.LUser = model.LUser;
            coreCom.ModifyDate = null;

            context.HRM_ATD_CompanyWeekEnd.Add(coreCom);
            context.SaveChanges();
            return coreCom.CompanyWeekEndCode;
        }
        public bool DeleteExistInfo(string EmployeeID)
        {
            //string[] spl = Date.Split('/');
            //DateTime fromdate = Convert.ToDateTime(spl[2] + "-" + spl[1] + "-" + spl[0]);


            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_EmployeeWeekEnd.FirstOrDefault(x => x.EmployeeCode == EmployeeID);
            if (result != null)
            {
                context.HRM_ATD_EmployeeWeekEnd.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public string SWeekSaveInfo(Model_CompannyWeekend model, string empid)
        {
            DateTime EffDate = new DateTime();
            EffDate = DateTime.ParseExact(model.EffectiveDate, "dd/MM/yyyy", null);
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_ATD_EmployeeWeekEnd coreCom = new HRM_ATD_EmployeeWeekEnd();

            common.FindMaxNo(ref strMaxNO, "EmployeeWeekEndCode", "HRM_ATD_EmployeeWeekEnd", 0);
            string MaxID = strMaxNO.ToString();

                coreCom.EmployeeWeekEndCode = MaxID;
                coreCom.EmployeeCode = empid;
                coreCom.WeekendDay = model.Weekend;
                coreCom.EffectiveDate = EffDate;
                coreCom.LDate = DateTime.Now;
                coreCom.LIP = model.LIP;
                coreCom.LMAC = model.LMAC;
                coreCom.LUser = model.LUser;
                coreCom.ModifyDate = null;

                context.HRM_ATD_EmployeeWeekEnd.Add(coreCom);
                context.SaveChanges();
        

            return coreCom.EmployeeWeekEndCode;
        }

        public bool UpdateInfo(string id, Model_CompannyWeekend model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_CompanyWeekEnd.FirstOrDefault(x => x.CompanyWeekEndCode == id);
            if (result != null)
            {
                DateTime EffDate = new DateTime();
                EffDate = DateTime.ParseExact(model.EffectiveDate, "dd/MM/yyyy", null);



                result.CompanyWeekEndCode = model.CompanyWeekEndCode;
                result.Weekend = model.Weekend;
                result.EffectiveDate = EffDate;
                result.LDate = DateTime.Now;
                result.LIP = model.LIP;
                result.LMAC = model.LMAC;
                result.LUser = model.LUser;
                result.ModifyDate = DateTime.Now;
            }
            context.SaveChanges();
            return true;
        }

        public bool DeleteInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_CompanyWeekEnd.FirstOrDefault(x => x.CompanyWeekEndCode == id);
            if (result != null)
            {
                context.HRM_ATD_CompanyWeekEnd.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
