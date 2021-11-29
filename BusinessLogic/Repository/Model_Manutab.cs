using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class Model_Manutab
    {
        public string title { get; set; }
        public string ParentID { get; set; }
        public string MenuId { get; set; }
        public int OrderBy { get; set; }
        public string ControllerName { get; set; }
        public string ViewName { get; set; }
        public string chkAdd { get; set; }
        public string chkEdit { get; set; }
        public string chkDelete { get; set; }
        public string chkPrint { get; set; }
        public string TitleCheck { get; set; }
        public string Icon { get; set; }

    }
}
