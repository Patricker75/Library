using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Resources
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Resource> Resources { get; set; } = default!;

        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (_context.Resources != null)
            {
                Resources = _context.Resources.ToList();
            }

            return Page();
        }
    }
}
