using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginUser Login { get; set; } = default!;

        public string LoginType { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public LoginModel(LibraryContext context)
        {
            _context = context;
            Login = new LoginUser();
        }

        public void OnGet()
        {
            Login.Username = "admin";
            Login.Password = "empassboi";
        }

        private IActionResult MemberLogin(Int32 loginID)
        {
            Member? member = _context.Members.Where(m => m.LoginID == loginID).First();

            if (member == null)
            {
                return Page();
            }

            HttpContext.Session.SetString("loginType", "member");
            HttpContext.Session.SetInt32("loginID", member.ID);
            HttpContext.Session.SetString("userFullName", $"{member.FirstName} {member.LastName}");

            return RedirectToPage("/Members/Profile");
        }

        private IActionResult EmployeeLogin(Int32 loginID)
        {
            Employee t = _context.Employees.ToList().First();

            Employee? employee = _context.Employees.Where(e => e.LoginID == loginID).First();

            if (employee == null)
            {
                return Page();
            }

            HttpContext.Session.SetString("loginType", "employee");
            HttpContext.Session.SetInt32("loginID", employee.ID);
            HttpContext.Session.SetString("userFullName", $"{employee.FirstName} {employee.LastName}");
            HttpContext.Session.SetString("employeeRole", employee.JobRole.ToString());

            return RedirectToPage("/Employees/Profile");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            LoginUser? login = _context.Logins.Where(l => l.Username == Login.Username && l.Password == Login.Password).FirstOrDefault();
            if (login == null)
            {
                return Page();
            }

            int loginID = login.ID;
            // Member Login
            if(_context.Members.Where(m => m.LoginID == loginID).Any())
            {
                return MemberLogin(loginID);
            }
            else if (_context.Employees.Where(e => e.LoginID == loginID).Any())
            {
                return EmployeeLogin(loginID);
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
