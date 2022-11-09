using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    public class ProfileModel : PageModel
    {
        public Member Member { get; set; } = default!;

        private readonly LibraryContext _context;

        public ProfileModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("loginType") != "member")
            {
                return RedirectToPage("../Error");
            }

            int? memberID = HttpContext.Session.GetInt32("loginID");
            if (memberID == null)
            {
                return RedirectToPage("../Error");
            }

            Member = _context.Member.Where(m => m.ID == memberID).First();

            return Page();
        }
    }
}
