using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Employees
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string LoginType { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public LoginModel(LibraryContext context)
        {
            _context = context;
        }

        private IActionResult LoginFail()
        {
            Password = string.Empty;
            ErrorMessage = "Invalid Username or Password";

            return Page();
        }

        public IActionResult OnPost()
        {
            if (_context.Employee == null)
            {
                return RedirectToPage("../Error");
            }

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Password = string.Empty;
                ErrorMessage = "Invalid Username or Password";

                return LoginFail();
            }

            Employee? employee = _context.Employee.Where(e => e.Username == Username && e.Password == Password).FirstOrDefault();

            if (employee == null)
            {
                return LoginFail();
            }

            HttpContext.Session.SetString("loginType", "employee");
            HttpContext.Session.SetInt32("loginID", employee.ID);

            return RedirectToPage("Profile");
        }
    }
}
