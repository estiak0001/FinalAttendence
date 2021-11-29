using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Model_LeaveBalanceStatus
    {
        public string ShortName { get; set; }
        public decimal? NoOfDay { get; set; }
        public decimal? Taken { get; set; }
        public decimal? Balance { get; set; }

    }
}
