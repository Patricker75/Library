using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Employees
{
    public class ProfileModel : PageModel
    {
        public Employee Employee { get; set; } = default!;

        private readonly LibraryContext _context;
        
        public ProfileModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("loginType") != "employee")
            {
                if (HttpContext.Session.GetString("loginType") == "member")
                {
                    return RedirectToPage("/Members/Profile");
                }
                else
                {
                    return RedirectToPage("/Login");
                }
            }

            int? employeeID = HttpContext.Session.GetInt32("loginID");
            if (employeeID == null)
            {
                return RedirectToPage("../Error");
            }

            Employee = _context.Employees.Where(e => e.ID == employeeID).First();

            return Page();
        }
    }
}
