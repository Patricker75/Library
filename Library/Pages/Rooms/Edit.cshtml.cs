using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Rooms
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public int ID { get; set; }

        public int Type { get; set; } = -1;

        public string Location { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

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

            ID = r.ID;

            Location = r.Location;
            Type = (int)r.RoomType;
            IsAvailable = r.IsAvailable;

            return Page();
        }

        private bool VerifyForm()
        {
            // Check that there is an Name
            if (string.IsNullOrEmpty(Location) || Location.Length > 10)
            {
                return false;
            }

            // Check that Item Type is Chosen
            if (Type < 0)
            {
                return false;
            }

            return true;
        }

        public IActionResult OnPost(int roomID)
        {
            Room? r = _context.Room.Find(roomID);
            
            if (r == null)
            {
                return Page();
            }

            if (VerifyForm())
            {
                r.Location = Location;
                r.IsAvailable = IsAvailable;
                r.RoomType = (RoomType)Type;
                
                _context.SaveChanges();

                return RedirectToPage("/Rooms/Index");
            }
            else
            {
                ErrorMessage = "A Required Field has Been Left Blank";

                ModelState.Clear();

                return Page();
            }
        }
    }
}
