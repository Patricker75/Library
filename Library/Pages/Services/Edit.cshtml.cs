using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Data;
using Library.Models;

namespace Library.Pages.Services
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

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

            Service? s = _context.Service.Find(id);

            if (s == null)
            {
                return RedirectToPage("/Index/Services");
            }

            ID = s.ID;
            Name = s.Name;
            Location = s.Location;
            IsAvailable = s.IsAvailable;
            
            return Page();
        }

        private bool VerifyForm()
        {
            // Check if name is filled
            if (string.IsNullOrEmpty(Name) || Name.Length > 50)
            {
                return false;
            }

            // Check if location is filled
            if (string.IsNullOrEmpty(Location) || Location.Length > 10)
            {
                return false;
            }

            return true;
        }

        public IActionResult OnPost(int serviceID)
        {
            Service? s = _context.Service.Find(serviceID);

            if (s == null)
            {
                return Page();
            }

            if (VerifyForm())
            {
                s.Location = Location;
                s.Name = Name;
                s.IsAvailable = IsAvailable;

                _context.SaveChanges();

                return RedirectToPage("/Services/Index");
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
