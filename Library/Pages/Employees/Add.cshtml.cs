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
        public Employee Employee { get; set; } = default!;

        [BindProperty]
        public LoginUser Login { get; set; } = default!;

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
            if (_context.Logins.Where(l => l.Username == Login.Username).Any())
            {
                Error = 1;
                return false;
            }

            if (Employee.BirthDate > DateTime.Today)
            {
                Error = 2;
                return false;
            }

            // Check that Hire Date occurs after Birth Date
            if (Employee.BirthDate.AddYears(-18) >= Employee.HireDate)
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
                _context.Logins.Add(Login);
                _context.SaveChanges();

                Employee.LoginID = Login.ID;
                _context.Employees.Add(Employee);
                _context.SaveChanges();

                return RedirectToPage("Add");
            }

            return Page();
        }
    }
}
