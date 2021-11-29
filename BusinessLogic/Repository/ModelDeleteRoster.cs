using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class ModelDeleteRoster
    {
        public List<RosterIDlist> ListRosterScheduleId { get; set; }
    }

    public class RosterIDlist
    {
        public string RosterScheduleID { get; set; }
    }
}
