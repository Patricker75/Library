using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Models;
using Library.Models.Relationships;

namespace Library.Pages.Services
{
    public class IndexModel : PageModel
    {
        public List<Service> Services { get; set; } = default!;

        private readonly Library.Data.LibraryContext _context;

        public IndexModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            if (_context.Service != null)
            {
                Services = (from s in _context.Service
                           where s.IsAvailable
                           select s).ToList();
            }
        }

        public IActionResult OnPostUse(int memberID, int serviceID)
        {
            if (_context.Member.Find(memberID) == null)
            {
                return RedirectToAction("Get");
            }

            _context.Uses.Add(new Uses()
            {
                MemberID = memberID,
                ServiceID = serviceID,
                TimeStamp = DateTime.UtcNow
            });

            _context.SaveChanges();

            return RedirectToAction("Get");
        }

        public IActionResult OnPostEdit(int serviceID)
        {
            Service? s = _context.Service.Find(serviceID);

            if (s == null)
            {
                return RedirectToAction("Get");
            }

            return RedirectToPage("/Services/Edit", new { id = serviceID });
        }
    }
}
