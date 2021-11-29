using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class ProductIndexView
    {
        public IEnumerable<Model_GaneranAndOfficialEmployee> employees { get; set; }
        public int employeePerPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(employees.Count() / (double)employeePerPage));
        }
        public IEnumerable<Model_GaneranAndOfficialEmployee> PaginatedEmployee()
        {
            int start = (CurrentPage - 1) * employeePerPage;
            return employees.OrderBy(p => p.EmployeeID).Skip(start).Take(employeePerPage);
        }
        public Pager Pager { get; set; }
    }
}
