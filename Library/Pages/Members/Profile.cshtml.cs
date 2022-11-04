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
            int? memberID = HttpContext.Session.GetInt32("memberID") ?? null;

            if (memberID == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
