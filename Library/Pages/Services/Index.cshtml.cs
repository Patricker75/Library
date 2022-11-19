using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Services
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Service> Services { get; set; }

        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (_context.Devices != null)
            {
                Services = _context.Services.ToList();
            }

            return Page();
        }
    }
}
