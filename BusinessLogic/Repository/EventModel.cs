using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class EventModel
    {
        public string EventID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }

        public string DepartmentCode { get; set; }
        public string EmployeeID { get; set; }
    }
}
