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
    
    public partial class HRM_Separation
    {
        public decimal SeparationCode { get; set; }
        public string SeparationId { get; set; }
        public string EmployeeID { get; set; }
        public System.DateTime SeparationDate { get; set; }
        public string SeparationTypeId { get; set; }
        public decimal FinalPayment { get; set; }
        public string IsPaid { get; set; }
        public string Remark { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string CompanyCode { get; set; }
    }
}
