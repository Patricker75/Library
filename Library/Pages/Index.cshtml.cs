using Library.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly LibraryContext _context;

        public IndexModel(ILogger<IndexModel> logger, LibraryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();

            HttpContext.Session.SetString("loginType", "employee");
            HttpContext.Session.SetInt32("loginID", 12);
            HttpContext.Session.SetString("roles", "admin,librarian");
            HttpContext.Session.SetString("userFullName", "autoFilled");

            

            return RedirectToPage("/Books/ViewBook", new { id = 31 });
        }
    }
}