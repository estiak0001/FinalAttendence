using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Crud_HRM_ATD_Shift
    {
        public List<Model_HRM_ATD_Shift> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from shift in context.HRM_ATD_Shift
                              select new
                              {
                                  ShiftCode = shift.ShiftCode,
                                  ShiftName = shift.ShiftName,
                                  ShiftStartTime = shift.ShiftStartTime,
                                  ShiftEndTime = shift.ShiftEndTime,
                                  LateTime = shift.LateTime,
                                  AbsentTime = shift.AbsentTime,


                              }).AsEnumerable().Select(a => new Model_HRM_ATD_Shift()
                              {

                                  ShiftCode = a.ShiftCode,
                                  ShiftName = a.ShiftName,
                                  ShiftStartTime = ((DateTime)a.ShiftStartTime).ToString("hh:mm:ss tt"),
                                  ShiftEndTime = ((DateTime)a.ShiftEndTime).ToString("hh:mm:ss tt"),
                                  LateTime = ((DateTime)a.LateTime).ToString("hh:mm:ss tt"),
                                  AbsentTime = ((DateTime)a.AbsentTime).ToString("hh:mm:ss tt")

                              }).ToList();


            
                return result;
            }
        }


        public string SaveInfo(Model_HRM_ATD_Shift model,string LoginEmployeeID)
        {

            string TodayDate = DateTime.Now.ToString("yyyy-MM-dd");

            TimeSpan spanStartTime = DateTime.ParseExact(model.ShiftStartTime,
                                    "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
            DateTime StartTime = Convert.ToDateTime(TodayDate + " " + spanStartTime);

            TimeSpan spanEndTIme= DateTime.ParseExact(model.ShiftEndTime,
                                    "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
            DateTime EndTime= Convert.ToDateTime(TodayDate + " " + spanEndTIme);

            TimeSpan spanLateTime = DateTime.ParseExact(model.LateTime,
                                    "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
            DateTime LateTime = Convert.ToDateTime(TodayDate + " " + spanLateTime);

            TimeSpan spanAbsentTime = DateTime.ParseExact(model.AbsentTime,
                                    "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
            DateTime AbsentTime = Convert.ToDateTime(TodayDate + " " + spanAbsentTime);

            DateTime WTF= new DateTime();
            WTF = DateTime.ParseExact(model.WEF, "dd/MM/yyyy", null);



            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_ATD_Shift coreCom = new HRM_ATD_Shift();
            coreCom.ShiftCode = model.ShiftCode;
            coreCom.ShiftName = model.ShiftName;

            if (model.ShiftShortName !=null)
            {
                coreCom.ShiftShortName = model.ShiftShortName;
            }
            else
            {
                coreCom.ShiftShortName = "";
            }

            

            coreCom.ShiftStartTime = StartTime;
            coreCom.ShiftEndTime = EndTime;
            coreCom.LateTime = LateTime;
            coreCom.AbsentTime = AbsentTime;
            coreCom.Remarks = model.Remarks;
            coreCom.LUser = LoginEmployeeID;
            coreCom.LDate = DateTime.Now;
            coreCom.WEF = WTF;
            if (model.Remarks != null)
            {
                coreCom.Remarks = model.Remarks;
            }
            else
            {
                coreCom.Remarks = "";
            }
            coreCom.LUser = LoginEmployeeID;
            coreCom.LDate = DateTime.Now;
            coreCom.ShiftTypeID = model.ShiftTypeID;
            context.HRM_ATD_Shift.Add(coreCom);
            context.SaveChanges();
            return coreCom.ShiftCode;
        }
        public Model_HRM_ATD_Shift GetInfo(string id)
        {
            var db = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from psi in db.HRM_ATD_Shift.Where(psi => psi.ShiftCode == id).DefaultIfEmpty().AsEnumerable()
                          select new
                          {
                              ShiftCode = psi.ShiftCode,
                              ShiftName = psi.ShiftName,
                              ShiftShortName = psi.ShiftShortName,
                              ShiftStartTime = psi.ShiftStartTime,
                              ShiftEndTime = psi.ShiftEndTime,
                              LateTime = psi.LateTime,
                              AbsentTime = psi.AbsentTime,
                              WEF = psi.WEF,
                              Remarks = psi.Remarks,
                              ShiftTypeID = psi.ShiftTypeID
                          }).AsEnumerable().Select(a => new Model_HRM_ATD_Shift()
                          {
                              ShiftCode = a.ShiftCode,
                              ShiftName = a.ShiftName,
                              ShiftShortName=a.ShiftShortName,
                              ShiftStartTime = ((DateTime)a.ShiftStartTime).ToString("hh:mm:ss tt"),
                              ShiftEndTime = ((DateTime)a.ShiftEndTime).ToString("hh:mm:ss tt"),
                              LateTime = ((DateTime)a.LateTime).ToString("hh:mm:ss tt"),
                              AbsentTime = ((DateTime)a.AbsentTime).ToString("hh:mm:ss tt"),
                              WEF=((DateTime)a.WEF).ToString("dd/MM/yyyy"),                          
                              Remarks=a.Remarks,
                              ShiftTypeID=a.ShiftTypeID
                          }).FirstOrDefault();
                          return result;
        }



        public bool UpdateInfo(string id, Model_HRM_ATD_Shift model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_Shift.FirstOrDefault(x => x.ShiftCode == id);
            if (result != null)
            {
                string TodayDate = DateTime.Now.ToString("yyyy-MM-dd");

                TimeSpan spanStartTime = DateTime.ParseExact(model.ShiftStartTime,
                                        "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
                DateTime StartTime = Convert.ToDateTime(TodayDate + " " + spanStartTime);

                TimeSpan spanEndTIme = DateTime.ParseExact(model.ShiftEndTime,
                                        "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
                DateTime EndTime = Convert.ToDateTime(TodayDate + " " + spanEndTIme);

                TimeSpan spanLateTime = DateTime.ParseExact(model.LateTime,
                                        "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
                DateTime LateTime = Convert.ToDateTime(TodayDate + " " + spanLateTime);

                TimeSpan spanAbsentTime = DateTime.ParseExact(model.AbsentTime,
                                        "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
                DateTime AbsentTime = Convert.ToDateTime(TodayDate + " " + spanAbsentTime);

                DateTime WTF = new DateTime();
                WTF = DateTime.ParseExact(model.WEF, "dd/MM/yyyy", null);


                result.ShiftName = model.ShiftName;
                if (model.ShiftShortName != null)
                {
                    result.ShiftShortName = model.ShiftShortName;
                }
                else
                {
                    result.ShiftShortName = "";
                }
                result.ShiftStartTime = StartTime;
                result.ShiftEndTime = EndTime;
                result.LateTime = LateTime;
                result.AbsentTime = AbsentTime;
                if (model.Remarks !=null)
                {
                    result.Remarks = model.Remarks;
                }
                else
                {
                    result.Remarks = "";
                }
                result.WEF = WTF;

            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string id)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_Shift.FirstOrDefault(x => x.ShiftCode == id);
            if (result != null)
            {
                context.HRM_ATD_Shift.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
