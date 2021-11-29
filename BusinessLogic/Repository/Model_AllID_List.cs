using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Model_AllID_List
    {
        public List<AllIDList> AllID { get; set; }
        public class AllIDList
        {
            public string Codes { get; set; }
        }
    }
}
