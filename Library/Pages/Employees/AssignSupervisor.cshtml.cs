using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Employees
{
    public class AssignSupervisorModel : PageModel
    {
        public Employee Employee { get; set; } = default!;

        public IList<Employee> Employees { get; set; } = default!;

        private readonly LibraryContext _context;

        public AssignSupervisorModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet(int employeeID)
        {
            //Employee? e = _context.
        }
    }
}
