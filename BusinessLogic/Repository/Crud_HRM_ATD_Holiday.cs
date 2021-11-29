using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Crud_HRM_ATD_Holiday
    {
        public List<Model_HRM_ATD_Holiday> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from hol in context.HRM_ATD_Holiday
                              join holtype in context.HRM_ATD_HolidayType on hol.HolidayType equals holtype.HolidayType
                              into g
                              from d in g.DefaultIfEmpty()
                              select new
                              {
                                  HolidayCode = hol.HolidayCode,
                                  HolidayName = hol.HolidayName,
                                  FromDate = hol.FromDate,
                                  ToDate = hol.ToDate,
                                  HolidayType = d.HolidayTypeName

                              }).AsEnumerable().Select(a => new Model_HRM_ATD_Holiday()
                              {
                                  HolidayCode = a.HolidayCode,
                                  HolidayName = a.HolidayName,
                                  FromDate = ((DateTime)a.FromDate).ToString("dd/MM/yyyy"),
                                  ToDate = ((DateTime)a.ToDate).ToString("dd/MM/yyyy"),
                                  HolidayType = a.HolidayType
                              }).ToList();

              
                return result;
            }
        }


        public string SaveInfo(Model_HRM_ATD_Holiday model,string LoginEmployeeID)
        {

            DateTime SpanFromDate= new DateTime();
            SpanFromDate = DateTime.ParseExact(model.FromDate, "dd/MM/yyyy", null);

            DateTime SpanToDate = new DateTime();
            SpanToDate = DateTime.ParseExact(model.ToDate, "dd/MM/yyyy", null);

            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_ATD_Holiday coreCom = new HRM_ATD_Holiday();
            coreCom.HolidayCode = model.HolidayCode;
            coreCom.FromDate = SpanFromDate;
            coreCom.ToDate = SpanToDate;
            coreCom.NoOfDays = Convert.ToByte(model.NoOfDays);
            coreCom.HolidayName = model.HolidayName;
            coreCom.HolidayType = model.HolidayType;
           
            coreCom.LUser = LoginEmployeeID;
            coreCom.LDate = DateTime.Now;
            
            context.HRM_ATD_Holiday.Add(coreCom);
            context.SaveChanges();
            return coreCom.HolidayCode;
        }
        public Model_HRM_ATD_Holiday GetInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
          
            var result = (from hol in context.HRM_ATD_Holiday.Where(hol => hol.HolidayCode == id).DefaultIfEmpty().AsEnumerable()
                          select new
                          {
                              HolidayCode = hol.HolidayCode,                              
                              FromDate = hol.FromDate,
                              ToDate = hol.ToDate,
                              NoOfDays = hol.NoOfDays,
                              HolidayName = hol.HolidayName,
                              HolidayType = hol.HolidayType,
                          }).AsEnumerable().Select(a => new Model_HRM_ATD_Holiday()
                          {
                              HolidayCode = a.HolidayCode,                              
                              FromDate = ((DateTime)a.FromDate).ToString("dd/MM/yyyy"),
                              ToDate = ((DateTime)a.ToDate).ToString("dd/MM/yyyy"),
                              NoOfDays=a.NoOfDays.ToString(),
                              HolidayName = a.HolidayName,
                              HolidayType = a.HolidayType,
                          }).FirstOrDefault();
            return result;
        }



        public bool UpdateInfo(string id, Model_HRM_ATD_Holiday model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_Holiday.FirstOrDefault(x => x.HolidayCode == id);
            if (result != null)
            {

                DateTime holFromDate= new DateTime();
                holFromDate = DateTime.ParseExact(model.FromDate, "dd/MM/yyyy", null);

                DateTime holToDate = new DateTime();
                holToDate = DateTime.ParseExact(model.ToDate, "dd/MM/yyyy", null);

                result.FromDate = holFromDate;
                result.ToDate = holToDate;
                result.NoOfDays= Convert.ToByte(model.NoOfDays);
                result.HolidayName = model.HolidayName;
                result.HolidayType = model.HolidayType;

            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_Holiday.FirstOrDefault(x => x.HolidayCode == id);
            if (result != null)
            {
                context.HRM_ATD_Holiday.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
