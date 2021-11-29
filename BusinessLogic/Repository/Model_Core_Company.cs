using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Repository
{
    public class Model_Core_Company
    {

        public string CompanyCode { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string CompanyName { get; set; }
        public string CompanyShortName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string HotLine { get; set; }
        public string URL { get; set; }
        public string Email { get; set; }

        public List<AllIDList> AllID { get; set; }
        public class AllIDList
        {
            public string CompanyCode { get; set; }
        }
    }
}
