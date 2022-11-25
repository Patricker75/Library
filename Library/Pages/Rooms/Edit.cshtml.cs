using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Rooms
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; } = default!;

        public EditModel(LibraryContext context)
        {
            _context = context;
        }

        private readonly LibraryContext _context;

        public IActionResult OnGet(string? location)
        {
            string? role = HttpContext.Session.GetString("employeeRole");
            if (role == null || (role != "Admin" && role != "Technician"))
            {
                return RedirectToPage("/Index");
            }

            if (location == null)
            {
                return RedirectToPage("/Rooms/Index");
            }

            Room? r = _context.Rooms.FirstOrDefault(r => r.Location == location);
            if (r == null)
            {
                return RedirectToPage("/Rooms/Index");
            }

            Room = r;

            return Page();
        }

        public IActionResult OnPost()
        {
            Room? room = _context.Rooms.FirstOrDefault(r => r.Location == Room.Location);
            if (room == null)
            {
                return Page();
            }

            room.Type = Room.Type;
            room.IsAvailable = Room.IsAvailable;

            if (ModelState.IsValid)
            {
                _context.Attach(room).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToPage("/Rooms/Index");
            }

            return Page();
        }

        public IActionResult OnPostDiscard()
        {
            return RedirectToPage("/Rooms/Index");
        }
    }
}
