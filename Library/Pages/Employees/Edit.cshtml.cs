using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Library.Pages.Employees
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleInitial { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public int Gender { get; set; } = -1;

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; } = DateTime.Today;

        public string JobTitle { get; set; } = string.Empty;

        public decimal Salary { get; set; } = decimal.Zero;

        public string ErrorMessage { get; set; } = string.Empty;

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

            ID = e.ID;

            FirstName = e.FirstName;
            MiddleInitial = e.MiddleName ?? string.Empty;
            LastName = e.LastName;

            Address = e.Address ?? string.Empty;
            PhoneNumber = e.PhoneNum ?? string.Empty;

            Gender = (int)e.Gender;

            BirthDate = e.BirthDate;
            HireDate = e.HireDate;

            JobTitle = e.JobTitle;
            Salary = e.Salary;

            return Page();
        }

        private bool VerifyForm()
        {
            if (string.IsNullOrEmpty(FirstName) || FirstName.Length > 20)
            {
                return false;
            }
            if (string.IsNullOrEmpty(LastName) || LastName.Length > 20)
            {
                return false;
            }
            if (string.IsNullOrEmpty(JobTitle) || JobTitle.Length > 50)
            {
                return false;
            }
            if (string.IsNullOrEmpty(Address) || Address.Length > 150)
            {
                return false;
            }
            if (string.IsNullOrEmpty(PhoneNumber) || PhoneNumber.Length > 10)
            {
                return false;
            }

            // Check that Hire Date occurs after Birth Date
            if (BirthDate >= HireDate)
            {
                return false;
            }

            // Checks if Hire Date occurs 18 years after Birth Date
            TimeSpan diffenence = HireDate - BirthDate;
            if (diffenence.TotalDays < 18 * 365) // Comparing days between hiring and birthday to number of days in 18 years
            {
                return false;
            }

            // Checks that Salary is valid
            if (Salary <= 0)
            {
                return false;
            }

            return true;
        }

        public IActionResult OnPost(int employeeID)
        {
            Employee? e = _context.Employee.Find(employeeID);
            
            if (e == null)
            {
                return Page();
            }
            
            if (VerifyForm())
            {
                e.FirstName = FirstName;
                e.MiddleName = MiddleInitial.Substring(0,1);
                e.LastName = LastName;

                e.Address = Address;
                e.PhoneNum = PhoneNumber;

                e.Gender = (Gender)Gender;

                e.BirthDate = BirthDate;
                e.HireDate = HireDate;

                e.JobTitle = JobTitle;
                e.Salary = Salary;

                _context.SaveChanges();

                int? loginID = HttpContext.Session.GetInt32("loginID");
                if (loginID != null && e.ID == loginID)
                {
                    HttpContext.Session.SetString("userFullName", $"{e.FirstName} {e.LastName}");
                }

                return RedirectToPage("/Employees/Index");
            }
            else
            {
                ErrorMessage = "A Required Field has Been Left Blank or Invalid Length/Value";

                return Page();
            }
        }
    }
}
