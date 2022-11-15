using Library.Data;
using Library.Models;
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
           // HttpContext.Session.SetString("loginType", "member");
            //HttpContext.Session.SetInt32("loginID", 17);
            //HttpContext.Session.SetString("userFullName", "Viet Bui");

            return Page();
        }
    }
}