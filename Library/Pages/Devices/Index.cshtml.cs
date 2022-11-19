using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Devices
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Device> Devices { get; set; }

        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (_context.Devices != null)
            {
                Devices = _context.Devices.ToList();
            }

            return Page();
        }
    }
}
