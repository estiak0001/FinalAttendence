//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    
    public partial class HRM_Attd_Temp_DailyInOut_Br_2
    {
        public decimal AutoId { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string DepartmentCode { get; set; }
        public string LineName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> InTime { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public Nullable<System.DateTime> SDIntime { get; set; }
        public Nullable<System.DateTime> SDOutTime { get; set; }
        public Nullable<System.DateTime> OutTime_br { get; set; }
        public string AttStatus { get; set; }
        public string OTTimeBr { get; set; }
        public string AttendanceTypeCode { get; set; }
    }
}
