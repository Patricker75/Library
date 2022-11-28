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

        public string Message { get; set; }

        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string message)
        {
            Message = message;
            if (_context.Rooms != null)
            {
                Rooms = _context.Rooms.ToList();
            }

            return Page();
        }

        public IActionResult OnPost(string location)
        {
            int? id = HttpContext.Session.GetInt32("loginID");
            if (id == null)
            {
                return RedirectToAction("Get");
            }

            Room? r = _context.Rooms.FirstOrDefault(r => r.Location == location);
            if (r == null)
            {
                return RedirectToAction("Get");
            }

            // Check if member has reserved another room
            if (_context.Rooms.FirstOrDefault(r => r.MemberID == id) != null) 
            {
                return RedirectToAction("Get", new { message = "You Have a Room Reserved Already"});
            }

            r.MemberID = id;
            r.IsAvailable = false;

            _context.Attach(r).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Get", new { message = "Room Reserved!"});
        }

        public IActionResult OnPostEdit(string location)
        {
            return RedirectToPage("/Rooms/Edit", new { location = location });
        }
    }
}
