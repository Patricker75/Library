using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Room> Rooms { get; set; } = default!;

        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (_context.Rooms != null)
            {
                Rooms = _context.Rooms.ToList();
            }

            return Page();
        }
    }
}
