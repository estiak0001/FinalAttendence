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
    
    public partial class HRM_PAY_LateDeduction
    {
        public decimal LateDeductionCode { get; set; }
        public string LateDeductionId { get; set; }
        public decimal Days { get; set; }
        public decimal Amount { get; set; }
        public decimal PercentageValue { get; set; }
        public string PayHeadNameId { get; set; }
        public System.DateTime WEF { get; set; }
        public string Remark { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
