using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
  public  class Model_HRM_ATD_Manual
    {
        [Key]
        public decimal autoId { get; set; }
        public string ManualCode { get; set; }
        public string BulkEntryId { get; set; }
        public string AttdEntryType { get; set; }
        public string EmployeeId { get; set; }
        public string AttendanceTypeCode { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Remarks { get; set; }
        public string LUser { get; set; }
        public string LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public string ModifyDate { get; set; }
        public string CompanyCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AttendanceLocation { get; set; }
        public string MapUrl { get; set; }

        public List<AllIDList> AllID { get; set; }
        public class AllIDList
        {
            public string ManualCode { get; set; }
        }
    }
}
