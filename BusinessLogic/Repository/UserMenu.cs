using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
  public  class UserMenu
    {
        public int id { get; set; }
        public int MenuId { get; set; }
        public string MenuText { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string UserEmployeeID { get; set; }
    }
}
