using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Employees
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public int Error { get; set; }

        private readonly LibraryContext _context;

        public EditModel(LibraryContext library)
        {
            _context = library;
        }

        public IActionResult OnGet(int? employeeID)
        {
            string? role = HttpContext.Session.GetString("employeeRole");
            int? id = HttpContext.Session.GetInt32("loginID");

            if (id != employeeID && role != "Admin")
            {
                return RedirectToPage("/Employees/Index");
            }

            Employee? employee = _context.Employees.FirstOrDefault(e => e.ID == employeeID);
            if (employee == null)
            {
                return RedirectToPage("/Employees/Index");
            }

            Employee = employee;

            return Page();
        }

        private bool VerifyForm()
        {
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
            Employee? employee = _context.Employees.FirstOrDefault(e => e.ID == Employee.ID);
            if (employee == null)
            {
                return Page();
            }

            employee.FirstName = Employee.FirstName;
            employee.MiddleName = Employee.MiddleName;
            employee.LastName = Employee.LastName;

            employee.PhoneNumber = Employee.PhoneNumber;
            employee.Address = Employee.Address;

            employee.Gender = Employee.Gender;
            employee.BirthDate = Employee.BirthDate;

            employee.HireDate = Employee.HireDate;
            employee.JobRole = Employee.JobRole;
            employee.Salary = Employee.Salary;

            if (ModelState.IsValid && VerifyForm())
            {
                _context.Attach(employee).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToPage("/Employees/Index");
            }

            return Page();
        }

        public IActionResult OnPostDiscard()
        {
            return RedirectToPage("/Employees/Index");
        }
    }
}
