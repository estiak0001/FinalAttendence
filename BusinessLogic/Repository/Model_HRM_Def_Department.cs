using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Repository
{
    public class Model_HRM_Def_Department
    {
 
      
        public string DepartmentCode { get; set; }       
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }
        public string LUser { get; set; }
        public DateTime LDate { get; set; }

        public List<AllIDList> AllID { get; set; }
        public class AllIDList
        {
            public string DepartmentCode { get; set; }
        }



    }
}
