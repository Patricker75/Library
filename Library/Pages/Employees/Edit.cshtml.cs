using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Library.Pages.Employees
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public int Error { get; set; }

        public string Roles { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public EditModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int employeeID)
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
            {
                return RedirectToPage("/Index");
            }

            Roles = HttpContext.Session.GetString("roles") ?? string.Empty;
            
            Employee? e = _context.Employee.Find(employeeID);
            
            if (e == null)
            {
                return RedirectToPage("/Employees/Index");
            }

            Employee = e;

            return Page();
        }

        private bool VerifyForm()
        {
            if (Employee.BirthDate > DateTime.Today)
            {
                Error = 1;
                return false;
            }

            // Check that Hire Date occurs after Birth Date
            if (Employee.BirthDate.AddYears(-18) >= Employee.HireDate)
            {
                Error = 2;
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            Employee? e = _context.Employee.Find(Employee.ID);
            
            if (e == null)
            {
                return Page();
            }

            Employee.Username = e.Username;
            Employee.Password = e.Password;

            //if (Employee.HireDate == null)
            //{
            //    Employee.HireDate = e.HireDate;
            //}

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid && VerifyForm())
            {
                _context.Employee.Update(Employee);
                _context.SaveChanges();

                int? loginID = HttpContext.Session.GetInt32("loginID");
                if (loginID != null && e.ID == loginID)
                {
                    HttpContext.Session.SetString("userFullName", $"{e.FirstName} {e.LastName}");
                }

                return RedirectToPage("/Employees/Index");
            }
            
            return Page();
        }
    }
}
