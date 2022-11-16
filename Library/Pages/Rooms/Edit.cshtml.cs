using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Rooms
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; } = default!;

        private readonly LibraryContext _context;

        public EditModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
            {
                return RedirectToPage("/Index");
            }

            Room? r = _context.Room.Find(id);

            if (r == null)
            {
                return RedirectToPage("/Rooms/Index");
            }

            Room = r;

            return Page();
        }

        public IActionResult OnPost()
        { 
            if (ModelState.IsValid)
            {
                _context.Room.Update(Room);
                _context.SaveChanges();

                return RedirectToPage("/Rooms");
            }

            return Page();
        }
    }
}
