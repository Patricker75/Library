using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

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
            if (_context.Member == null)
            {
                return RedirectToPage("../Error");
            }

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Password = string.Empty;
                ErrorMessage = "Invalid Username or Password";

                return LoginFail();
            }

            Member? member = _context.Member.Where(m => m.Username == Username && m.Password == m.Password).First();

            if (member == null)
            {
                return LoginFail();
            }

            HttpContext.Session.SetInt32("memberID", member.ID);

            return RedirectToPage("Profile");
        }
    }
}
