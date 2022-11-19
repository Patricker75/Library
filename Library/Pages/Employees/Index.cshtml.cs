using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging;

namespace Library.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public IList<Employee> Employees { get; set; } = default!;

        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
            {
                return RedirectToPage("/Index");
            }

            if (_context.Employees != null)
            {
                Employees = _context.Employees.ToList();
            }

            return Page();
        }
    }
}
