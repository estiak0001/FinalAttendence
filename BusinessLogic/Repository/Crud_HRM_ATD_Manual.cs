using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crud_HRM_ATD_Manual
    {
        ClsCommon common = new ClsCommon();
        public List<Model_HRM_ATD_Manual> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from mo in context.HRM_ATD_Manual
                              from em in context.HRM_Employee.Where(hrm => hrm.EmployeeID == mo.EmployeeId.ToString()).DefaultIfEmpty()
                              from attType in context.HRM_ATD_AttendanceType.Where(attType => attType.AttendanceTypeCode == mo.AttendanceTypeCode).DefaultIfEmpty()
                              select new
                              {
                                  ManualCode = mo.ManualCode,
                                  Date = mo.Date,
                                  EmployeeID = em.FirstName+em.LastName,
                                  Time = mo.Time,                                 
                                  AttendenceType = attType.AttendanceTypeName,
                                  Remarks = mo.Remarks

                              }).AsEnumerable().Select(a => new Model_HRM_ATD_Manual()
                              {
                                  ManualCode = a.ManualCode.ToString(),
                                  Date = ((DateTime)a.Date).ToString("dd/MM/yyyy"),
                                  EmployeeId = a.EmployeeID,
                                  Time = ((DateTime)a.Time).ToString("hh:mm:ss tt"),                                  
                                  AttendanceTypeCode=a.AttendenceType,
                                  Remarks=a.Remarks
                              }).ToList();



                return result;
            }
        }


        public string SaveInfo(Model_HRM_ATD_Manual model, string LoginEmployeeID)
        {

            DateTime CheckDate = new DateTime();
            CheckDate = DateTime.ParseExact(model.Date, "dd/MM/yyyy", null);

            string ToDayDate = DateTime.Now.ToString("yyyy-MM-dd");
            TimeSpan spanInTime = DateTime.ParseExact(model.Time,
                                    "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;            
            DateTime InTime= Convert.ToDateTime(ToDayDate + " " + spanInTime);
            string strMaxNO = "";
            common.FindMaxNoAuto(ref strMaxNO, "ManualCode", "HRM_ATD_Manual");
            

            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            HRM_ATD_Manual coreCom = new HRM_ATD_Manual();
            coreCom.ManualCode = strMaxNO.ToString();
            coreCom.BulkEntryId = "";
            coreCom.AttdEntryType = "From Web";
            coreCom.Date =CheckDate;
            coreCom.EmployeeId = model.EmployeeId;
            coreCom.Time = InTime;
            coreCom.AttendanceTypeCode =model.AttendanceTypeCode;
            coreCom.Remarks = model.Remarks;

            coreCom.LUser =LoginEmployeeID;
            coreCom.LDate = DateTime.Now;
            coreCom.LIP = "";
            coreCom.LMAC = "";
            //coreCom.ModifyDate = DateTime.Now;
            coreCom.CompanyCode = "001";
            coreCom.Latitude = model.Latitude;
            coreCom.Longitude = model.Longitude;
            context.HRM_ATD_Manual.Add(coreCom);
            context.SaveChanges();
            return coreCom.ManualCode;
        }
        public Model_HRM_ATD_Manual GetInfo(string ManualCode)
        {
            
            var db = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from psi in db.HRM_ATD_Manual
                          .Where(psi => psi.ManualCode == ManualCode).DefaultIfEmpty().AsEnumerable()
                          select new
                          {
                              ManualCode = psi.ManualCode,
                              Date = psi.Date,
                              Time = psi.Time,
                              EmployeeId = psi.EmployeeId,
                              AttendanceTypeCode = psi.AttendanceTypeCode,
                              Remarks = psi.Remarks,
                              ldate = psi.LDate,
                              modifiedDate = psi.ModifyDate,
                          }).AsEnumerable().Select(a => new Model_HRM_ATD_Manual()
                          {
                              ManualCode = a.ManualCode.ToString(),
                              Date = ((DateTime)a.Date).ToString("dd/MM/yyyy"),
                              EmployeeId = a.EmployeeId.ToString(),
                              Time = ((DateTime)a.Time).ToString("hh:mm:ss tt"),
                              AttendanceTypeCode = a.AttendanceTypeCode,
                              Remarks = a.Remarks,
                              LDate =  ((DateTime)a.ldate).ToString("dd/MM/yyyy"),
                              ModifyDate = ((DateTime)a.modifiedDate).ToString("dd/MM/yyyy"),
                          }).FirstOrDefault();
            return result;
        }

        public Model_EmployeeBasicInfo GetEmployeeInfo(string EmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from hrm in context.HRM_EmployeeOfficialInfo
                          from gn in context.HRM_Employee.Where(gn=>gn.EmployeeID==hrm.EmployeeID).DefaultIfEmpty()
                          from depart in context.HRM_Def_Department.Where(depart => depart.DepartmentCode == hrm.DepartmentCode)
       .DefaultIfEmpty()
                          from Desig in context.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == hrm.DesignationCode)
     .DefaultIfEmpty()
                          select new
                          {
                              EmployeeID = hrm.EmployeeID,
                              EmployeeName = gn.FirstName+gn.LastName,                         
                              MobileNo = hrm.MobileNo,
                              Email = hrm.Email,
                              DepartmentCode = depart.DepartmentName,
                              DesignationCode = Desig.DesignationName,
                          }).AsEnumerable().Select(a => new Model_EmployeeBasicInfo()
                          {
                              EmployeeID = a.EmployeeID,
                              EmployeeName=a.EmployeeName,
                              DepartmentCode=a.DepartmentCode,
                              DesignationCode=a.DesignationCode
                              }).Where(a => a.EmployeeID == EmployeeID).FirstOrDefault();
                return result;
        }


        public bool UpdateInfo(string id, Model_HRM_ATD_Manual model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_Manual.FirstOrDefault(x => x.ManualCode == model.ManualCode);
            if (result != null)
            {

                DateTime CheckDate = new DateTime();
                CheckDate = DateTime.ParseExact(model.Date, "dd/MM/yyyy", null);

                string ToDayDate = DateTime.Now.ToString("yyyy-MM-dd");
                TimeSpan spanInTime = DateTime.ParseExact(model.Time,
                                        "hh:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
                string ConcatTime = ToDayDate +" "+ spanInTime;
                DateTime InTime = new DateTime();
                InTime = Convert.ToDateTime(ConcatTime);

                result.EmployeeId =model.EmployeeId;
                result.BulkEntryId = "";
                result.AttdEntryType = "";
                result.Date= CheckDate;
                result.Time = InTime;
                result.ModifyDate = DateTime.Now;       
                result.AttendanceTypeCode = model.AttendanceTypeCode;
                result.Remarks = model.Remarks;
            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string ManualCode)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.HRM_ATD_Manual.FirstOrDefault(x => x.ManualCode == ManualCode);
            if (result != null)
            {
                context.HRM_ATD_Manual.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
