using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Employees
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public Employee Employee { get; set; } = default!;

        public string ErrorMessage { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public EditModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int employeeID)
        {
            Employee? e = _context.Employee.Find(employeeID);
            
            if (e == null)
            {
                return RedirectToPage("/Employee/Index");
            }

            Employee = e;

            return Page();
        }
    }
}
