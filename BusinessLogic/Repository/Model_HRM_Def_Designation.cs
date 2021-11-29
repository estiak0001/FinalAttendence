using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Repository
{
    public class Model_HRM_Def_Designation
    {
 
      
        public string DesignationCode { get; set; }
        public List<DesgIDList> desgID { get; set; }
        public string DesignationName { get; set; }
        public string DesignationShortName { get; set; }

        public string BanglaDesignation { get; set; }
        public string BanglaShortName { get; set; }

        public string LUser { get; set; }
        public DateTime LDate { get; set; }

        public class DesgIDList
        {
            public string DesignationCode { get; set; }
        }

    }
}
