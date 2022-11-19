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

        public IActionResult OnPost(int serviceID) 
        {
            int? id = HttpContext.Session.GetInt32("loginID");
            if (id == null) 
            {
                return Page();
            }
            
            Service? s = _context.Services.FirstOrDefault(s => s.ID == serviceID);
            if (s == null)
            {
                return Page();
            }

            _context.Uses.Add(new Models.Relationships.Use()
            {
                MemberID = (int)id,
                ServiceID = serviceID,
                TimeStamp = DateTime.Now
            });

            _context.SaveChanges();

            return RedirectToPage("/Services/Index");
        }

        public IActionResult OnPostEdit(int serviceID)
        {
            return RedirectToPage("/Services/Edit", new { serviceID = serviceID });
        }
    }
}
