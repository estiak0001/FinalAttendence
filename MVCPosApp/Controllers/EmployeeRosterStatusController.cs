using BusinessLogic;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class EmployeeRosterStatusController : Controller
    {
        private EmployeeRosterStatusCrud _DA { get; set; }
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        // GET: EmployeeRosterStatus
        public ActionResult Index()
        {
            ViewBag.Department = new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
            ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}", u.FirstName, "-", u.EmployeeID), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            ViewBag.LoadreortTypeFormat = new SelectList(
                                    new List<Model_SelectType>
                                    {
                                        new Model_SelectType { Text = "All", Value = "All" },
                                        new Model_SelectType { Text = "Roster", Value = "Roster"}, //....
                                        new Model_SelectType { Text = "Leave", Value = "Leave"},
                                        new Model_SelectType { Text = "General shift", Value = "General"},
                                    }, "Value", "Text");
            return View();
        }


        public ActionResult GetEvents(string FromDate, string ToDate, string DepartmentID, string EmployeeID, string Type)
        {
            using (GCTL_ERP_DB_MVC_06_27Entities dc = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                //List<EventModel> ev;
                //var events = dc.EventTables.ToList();
                
                //foreach(var item in events2)
                //{
                //    ev.EventID = (int)item.LeaveAppEntryCode;
                //    ev.Subject = item.EmployeeID + " (Le)";
                //    ev.Description = item.ConfirmationRemarks;
                //    ev.StartTime = item.StartDate;
                //    ev.EndTime = item.EndDate;
                //    ev.ThemeColor = "skyblue";
                //    ev.IsFullDay = false;
                //}
                
                var resultLeave = (from item in dc.HRM_LeaveApplicationEntry
                                   join empoff in dc.HRM_EmployeeOfficialInfo on item.EmployeeID equals empoff.EmployeeID
                                   into gemoff
                                   from demoff in gemoff.DefaultIfEmpty()

                                  join emp in dc.HRM_Employee on item.EmployeeID equals emp.EmployeeID
                                  into gem
                                  from dem in gem.DefaultIfEmpty()
                                   where (DepartmentID == "" || demoff.DepartmentCode == DepartmentID) && (EmployeeID == "" || item.EmployeeID == EmployeeID)
                                   select new
                              {
                                  ID = item.LeaveAppEntryCode,
                                  Name = dem.FirstName,
                                  Subject = dem.FirstName + " (Le) " + item.ApplyLeaveFormat,
                                  Descrip = item.Reason,
                                  stratTime = item.StartDate,
                                  endTime = item.EndDate, 
                                  leaveFormat = item.ApplyLeaveFormat,
                                  shortLeStart = item.ShortLeaveFrom,
                                  shortLeEnd = item.ShortLeaveTo,
                                  shortLeTotalTime = item.ShortLeaveTime,
                                  
                              }).Distinct().AsEnumerable().Select(a => new EventModel()
                              {
                                  EventID = a.ID.ToString(),
                                  Subject = a.Subject,
                                  Description = a.leaveFormat == "Full Leave"? (a.Name+ " Applied for "+ a.leaveFormat+" From "+a.stratTime.ToString("yyyy-MM-dd")+ " to "+a.endTime.ToString("yyyy-MM-dd")) : (a.Name + " Applied for " + a.leaveFormat + " For "+a.stratTime.ToString("yyyy-MM-dd")+ " from " + (a.shortLeStart == null ? null : ((DateTime)(a.shortLeStart)).ToString("hh:mm:ss tt")) + " to " + (a.shortLeEnd == null ? null : ((DateTime)(a.shortLeEnd)).ToString("hh:mm:ss tt")) +". And Total hour is: "+(a.shortLeTotalTime == null ? null : DateTime.ParseExact(a.shortLeTotalTime.ToString(), "hh:mm:ss", CultureInfo.InvariantCulture).TimeOfDay.ToString())),
                                  StartTime = ((DateTime)a.stratTime).ToString("yyyy-MM-dd"),
                                  EndTime = ((DateTime)a.endTime).ToString("yyyy-MM-dd") + " 23:59:59",
                                  ThemeColor = "coral",
                                  IsFullDay = false
                              }).ToList();


                var resultRoster = (from item in dc.HRM_RosterScheduleEntry
                                    join empoff in dc.HRM_EmployeeOfficialInfo on item.EmployeeID equals empoff.EmployeeID
                                    into gemoff
                                    from demoff in gemoff.DefaultIfEmpty()
                                    join emp in dc.HRM_Employee on demoff.EmployeeID equals emp.EmployeeID
                                    into gem
                                    from dem in gem.DefaultIfEmpty()
                                    join shift in dc.HRM_ATD_Shift on item.ShiftCode equals shift.ShiftCode
                                    into g
                                    from d in g.DefaultIfEmpty()
                                    where (DepartmentID == "" || demoff.DepartmentCode == DepartmentID) && (EmployeeID == "" || item.EmployeeID == EmployeeID)
                                    select new
                                  {
                                      ID = item.RosterScheduleId,
                                      Subject = dem.FirstName + " (R)",
                                      Descrip = "This employee is in roster",
                                      stratTime = item.Date,
                                      endTime = item.Date,
                                      shifstart = d.ShiftStartTime,
                                      shiftEnd = d.ShiftEndTime,
                                      shiftcode = d.ShiftCode
                                  }).Distinct().AsEnumerable().Select(a => new EventModel()
                                  {
                                      EventID = a.ID,
                                      Subject = a.Subject+ " "+ ((DateTime)a.shifstart).ToString("HH:mm tt")+"-"+ ((DateTime)a.shiftEnd).ToString("HH:mm tt"),
                                      Description = a.Descrip,
                                      StartTime = ((DateTime)a.stratTime).ToString("yyyy-MM-dd") + " " + ((DateTime)a.shifstart).ToString("hh:mm:ss tt"),
                                      EndTime =a.shiftcode== "5"?(((DateTime)a.endTime.AddDays(1)).ToString("yyyy-MM-dd") +" "+ ((DateTime)a.shiftEnd).ToString("hh:mm:ss tt")): (((DateTime)a.endTime).ToString("yyyy-MM-dd") + " " + ((DateTime)a.shiftEnd).ToString("hh:mm:ss tt")),
                                      ThemeColor = "palegreen",
                                      IsFullDay = false
                                  }).ToList();
                List<EventModel> evList = new List<EventModel>();
                if (EmployeeID != "")
                {
                    DateTime now = DateTime.Now;
                    DateTime firstdayofyear = new DateTime(now.Year, now.Month, 1);
                    DateTime lastdayofyear = firstdayofyear.AddMonths(1).AddDays(-1);

                    //int year = DateTime.Now.Year;
                    //DateTime firstdayofyear = new DateTime(year, 1, 1);
                    //DateTime lastdayofyear = new DateTime(year, 12, 31);
                    if (FromDate != "" && ToDate != "")
                    {
                        string cv1 = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.CurrentCulture).ToString("dd/MM/yyyy");
                        string cv2 = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.CurrentCulture).ToString("dd/MM/yyyy");
                        firstdayofyear = Convert.ToDateTime(cv1);
                        lastdayofyear = Convert.ToDateTime(cv2);
                        //lastdayofyear = firstdayofyear.AddMonths(1).AddDays(-1);

                    }
                    var employeeShift = (from empoff in dc.HRM_EmployeeOfficialInfo
                                         join emp in dc.HRM_Employee on empoff.EmployeeID equals emp.EmployeeID
                                         into gem
                                         from dem in gem.DefaultIfEmpty()
                                         join shift in dc.HRM_ATD_Shift on empoff.ShiftCode equals shift.ShiftCode
                                         into g
                                         from d in g.DefaultIfEmpty()
                                         where (DepartmentID == "" || empoff.DepartmentCode == DepartmentID) && (EmployeeID == "" || empoff.EmployeeID == EmployeeID)
                                         select new
                                         {
                                             ID = empoff.EmployeeID,
                                             Subject = dem.FirstName + " (G)",
                                             Descrip = "This employee is in General shift",
                                             stratTime = firstdayofyear,
                                             endTime = lastdayofyear,
                                             shifstart = d.ShiftStartTime,
                                             shiftEnd = d.ShiftEndTime,
                                             shiftcode = d.ShiftCode
                                         }).Distinct().AsEnumerable().Select(a => new EventModel()
                                         {
                                             EventID = a.ID,
                                             Subject = a.Subject + " " + a.shifstart.ToString("HH:mm tt") + "-" + a.shiftEnd.ToString("HH:mm tt"),
                                             Description = a.Descrip,
                                             StartTime = a.shifstart.ToString("hh:mm:ss tt"),
                                             EndTime = a.shiftEnd.ToString("hh:mm:ss tt"),
                                             ThemeColor = "skyblue",
                                             IsFullDay = false
                                         }).ToList().Take(10);

                    for (DateTime datess = firstdayofyear; datess.Date <= lastdayofyear.Date; datess = datess.AddDays(1))
                    {
                        foreach (var it in employeeShift)
                        {
                            var rss = dc.HRM_RosterScheduleEntry.Any(x => x.EmployeeID == it.EventID && datess == x.Date);
                            var leave = dc.HRM_LeaveApplicationEntry.Any(x => x.EmployeeID == it.EventID && (datess >= x.StartDate && datess <= x.EndDate));
                            if (rss == false && leave == false)
                            {
                                EventModel ev = new EventModel();
                                ev.EventID = it.EventID;
                                ev.Subject = it.Subject;
                                ev.Description = it.Description;
                                ev.StartTime = datess.ToString("yyyy-MM-dd") + " " + it.StartTime;
                                ev.EndTime = datess.ToString("yyyy-MM-dd") + " " + it.EndTime;
                                ev.IsFullDay = it.IsFullDay;
                                ev.ThemeColor = it.ThemeColor;

                                evList.Add(ev);
                            }
                        }
                    }
                }
                var tt = "";
                if (Type == "" || Type == "All")
                {
                    return new JsonResult { Data = new { resultRoster, resultLeave, evList }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                if (Type == "Roster")
                {
                    return new JsonResult { Data = new { resultRoster }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                if (Type == "Leave")
                {
                    return new JsonResult { Data = new { resultLeave }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                if (Type == "General")
                {
                    return new JsonResult { Data = new { evList }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = new { tt }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        //public ActionResult GetGeneralShiftEvents(string FromDate, string ToDate)
        //{
        //    using (GCTL_ERP_DB_MVC_06_27Entities dc = new GCTL_ERP_DB_MVC_06_27Entities())
        //    {
                

        //            DateTime now = DateTime.Now;
        //            DateTime firstdayofyear = new DateTime(now.Year, now.Month, 1);
        //            DateTime lastdayofyear = firstdayofyear.AddMonths(1).AddDays(-1);

        //        if (FromDate != "" && ToDate != "")
        //        {
        //            firstdayofyear = Convert.ToDateTime(FromDate);
        //            lastdayofyear = Convert.ToDateTime(ToDate);
        //        }

        //        //int year = DateTime.Now.Year;
        //        //DateTime firstdayofyear = new DateTime(year, 1, 1);
        //        //DateTime lastdayofyear = new DateTime(year, 12, 31);

        //        string iDate = "05/05/2005";
        //        DateTime oDate = Convert.ToDateTime(iDate);

        //        List<EventModel> evList = new List<EventModel>();

        //        var employeeShift = (from empoff in dc.HRM_EmployeeOfficialInfo
        //                             join emp in dc.HRM_Employee on empoff.EmployeeID equals emp.EmployeeID
        //                             into gem
        //                             from dem in gem.DefaultIfEmpty()
        //                             join shift in dc.HRM_ATD_Shift on empoff.ShiftCode equals shift.ShiftCode
        //                             into g
        //                             from d in g.DefaultIfEmpty()

        //                             select new
        //                             {
        //                                 ID = empoff.EmployeeID,
        //                                 Subject = dem.FirstName + " (G)",
        //                                 Descrip = "This employee is in General shift",
        //                                 stratTime = firstdayofyear,
        //                                 endTime = lastdayofyear,
        //                                 shifstart = d.ShiftStartTime,
        //                                 shiftEnd = d.ShiftEndTime,
        //                                 shiftcode = d.ShiftCode
        //                             }).Distinct().AsEnumerable().Select(a => new EventModel()
        //                             {
        //                                 EventID = a.ID,
        //                                 Subject = a.Subject + " " + a.shifstart.ToString("HH:mm tt") + "-" + a.shiftEnd.ToString("HH:mm tt"),
        //                                 Description = a.Descrip,
        //                                 StartTime = a.shifstart.ToString("hh:mm:ss tt"),
        //                                 EndTime = a.shiftEnd.ToString("hh:mm:ss tt"),
        //                                 ThemeColor = "skyblue",
        //                                 IsFullDay = false
        //                             }).ToList();

        //        for (DateTime datess = firstdayofyear; datess.Date <= lastdayofyear.Date; datess = datess.AddDays(1))
        //        {
        //            foreach (var it in employeeShift)
        //            {
        //                var rss = dc.HRM_RosterScheduleEntry.Any(x => x.EmployeeID == it.EventID && datess == x.Date);
        //                var leave = dc.HRM_LeaveApplicationEntry.Any(x => x.EmployeeID == it.EventID && (datess >= x.StartDate && datess <= x.EndDate));
        //                if (rss == false && leave == false)
        //                {
        //                    EventModel ev = new EventModel();
        //                    ev.EventID = it.EventID;
        //                    ev.Subject = it.Subject;
        //                    ev.Description = it.Description;
        //                    ev.StartTime = datess.ToString("yyyy-MM-dd") + " " + it.StartTime;
        //                    ev.EndTime = datess.ToString("yyyy-MM-dd") + " " + it.EndTime;
        //                    ev.IsFullDay = it.IsFullDay;
        //                    ev.ThemeColor = it.ThemeColor;

        //                    evList.Add(ev);
        //                }
        //            }
        //        }

        //        return new JsonResult { Data = new { evList }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //}

        [HttpPost]
        public JsonResult SaveEvent(EventTable e)
        {
            var status = false;
            using (GCTL_ERP_DB_MVC_06_27Entities dc = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.EventTable.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.StartTime = e.StartTime;
                        v.EndTime = e.EndTime;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.EventTable.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (GCTL_ERP_DB_MVC_06_27Entities dc = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var v = dc.EventTable.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.EventTable.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        //[HttpGet]
        //public ActionResult GetCalendarEvents(string start, string end)
        //{
        //    List<EventModel> events = _DA.GetCalendarEvents(start, end);

        //    return Json(events);
        //}

        //[HttpPost]
        //public ActionResult UpdateEvent( EventModel evt)
        //{
        //    string message = String.Empty;

        //    message = _DA.UpdateEvent(evt);

        //    return Json(new { message });
        //}

        //[HttpPost]
        //public ActionResult AddEvent(EventModel evt)
        //{
        //    string message = String.Empty;
        //    int eventId = 0;

        //    message = _DA.AddEvent(evt, out eventId);

        //    return Json(new { message, eventId });
        //}

        //[HttpPost]
        //public ActionResult DeleteEvent(EventModel evt)
        //{
        //    string message = String.Empty;

        //    message = _DA.DeleteEvent(evt.EventId);

        //    return Json(new { message });
        //}
    }
}