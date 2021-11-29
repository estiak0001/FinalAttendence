using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Model_Core_UserInfo
    {
        public string ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }        
        public string Status { get; set; }
        public string AccessCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }
        public string EntryDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
