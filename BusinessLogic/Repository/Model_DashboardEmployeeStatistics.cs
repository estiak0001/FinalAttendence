using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Model_DashboardEmployeeStatistics
    {
        public int TotalEmp { get; set; }
        public int TodayPresent { get; set; }
        public int TodayLate { get; set; }
        public int TodayAbsent { get; set; }
    }
}
