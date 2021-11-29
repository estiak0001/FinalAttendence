using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class DayVM
    {
        public DateTime Date { get; set; }
        public int Day { get { return Date.Day; } }
        public bool IsCurrent { get; set; }
        public bool IsSelected { get; set; }
    }
}
