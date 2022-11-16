using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Employees
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Employee NewEmployee { get; set; } = default!;

        public int Error { get; set; }
 
        private readonly LibraryContext _context;

        public AddModel(LibraryContext library)
        {
            _context = library;
        }

        public IActionResult OnGet()
        {
            string? roles = HttpContext.Session.GetString("roles");
            if (roles == null || !roles.Contains("admin"))
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        private bool VerifyForm()
        {
            // Check if username exists in member or employee table
            if (_context.Member.Where(m => m.Username == NewEmployee.Username).Any() || _context.Employee.Where(e => e.Username == NewEmployee.Username).Any())
            {
                Error = 1;
                return false;
            }

            if (NewEmployee.BirthDate > DateTime.Today)
            {
                Error = 2;
                return false;
            }

            // Check that Hire Date occurs after Birth Date
            if (NewEmployee.BirthDate.AddYears(-18) >= NewEmployee.HireDate)
            {
                Error = 3;
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid && VerifyForm())
            {
                _context.Employee.Add(NewEmployee);
                _context.SaveChanges();

                return RedirectToPage("Add");
            }

            return Page();
        }
    }
}
