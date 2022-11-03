using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Employees
{
    [BindProperties]
    public class AddModel : PageModel
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

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
 
        private readonly LibraryContext _context;

        public AddModel(LibraryContext library)
        {
            _context = library;

            BirthDate = new DateTime(HireDate.Year - 18, HireDate.Month, HireDate.Day);
        }

        public void OnGet()
        {
            
        }

        private bool VerifyForm()
        {
            // Checks if username, first name, last name, job title, password, address, phone number is filled
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName)
                || string.IsNullOrEmpty(JobTitle) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Address)
                || string.IsNullOrEmpty(PhoneNumber))
            {
                return false;
            }

            // Check that values are a certain length
            if (Password.Length > 10)
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

            // Checks that Gender is chosen
            if (Gender < 0)
            {
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            if(VerifyForm())
            {
                Employee newEmployee = new Employee()
                {
                    Username = Username,
                    Password = Password,

                    FirstName = FirstName,
                    LastName = LastName,
                    
                    Address = Address,
                    PhoneNum = PhoneNumber,

                    JobTitle = JobTitle,
                    Salary = Salary,
                    
                    Gender = (Gender)Gender,

                    BirthDate = BirthDate,
                    HireDate = HireDate
                };

                if (!string.IsNullOrEmpty(MiddleInitial))
                {
                    newEmployee.MiddleName = MiddleInitial;
                }

                _context.Employee.Add(newEmployee);
                _context.SaveChanges();

                return RedirectToPage("Add");
            }
            else
            {
                ErrorMessage = "A Required Field has Been Left Blank";

                ModelState.Clear();

                return Page();
            }
        }
    }
}
