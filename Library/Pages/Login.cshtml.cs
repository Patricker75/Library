using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
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

        public void OnGet(string message = "")
        {
            ErrorMessage = message;

            Username = "TestEmploy";
            Password = "passtest";
        }

        private bool VerifyForm()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return false;
            }
            if (Username.Length > 20 || Password.Length > 10)
            {
                return false;
            }

            return true;
        }

        private IActionResult MemberLogin()
        {
            Member? member = _context.Member.Where(m => m.Username == Username && m.Password == Password).FirstOrDefault();

            if (member == null)
            {
                return RedirectToAction("Get", new { message = "Invalid Username/Password" });
            }

            HttpContext.Session.SetString("loginType", "member");
            HttpContext.Session.SetInt32("loginID", member.ID);
            HttpContext.Session.SetString("userFullName", $"{member.FirstName} {member.LastName}");

            return RedirectToPage("/Members/Profile");
        }

        private IActionResult EmployeeLogin()
        {
            Employee? employee = _context.Employee.Where(e => e.Username == Username && e.Password == Password).FirstOrDefault();

            if (employee == null)
            {
                return RedirectToAction("Get", new { message = "Invalid Username/Password" });
            }

            HttpContext.Session.SetString("loginType", "employee");
            HttpContext.Session.SetInt32("loginID", employee.ID);
            HttpContext.Session.SetString("userFullName", $"{employee.FirstName} {employee.LastName}");

            return RedirectToPage("/Employees/Profile");
        }

        public IActionResult OnPost()
        {
            if (!VerifyForm())
            {
                return RedirectToAction("Get", new { message = "Username & Password Required" });
            }

            if (_context.Member.Where(m => m.Username == Username).Any())
            {
                return MemberLogin();
            }
            else if (_context.Employee.Where(e => e.Username == Username).Any())
            {
                return EmployeeLogin();
            }
            else
            {
                return RedirectToAction("Get", new { message = "Invalid Username/Password" });
            }         
        }
    }
}
