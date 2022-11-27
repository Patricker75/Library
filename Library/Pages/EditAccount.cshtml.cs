using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class EditAccountModel : PageModel
    {
        public IActionResult OnGet()
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == "member")
            {
                return RedirectToPage("/Members/Edit");
            }
            else if (loginType == "employee")
            {
                return RedirectToPage("/Employees/Edit");
            }

            return RedirectToPage("/Index");
        }
    }
}
