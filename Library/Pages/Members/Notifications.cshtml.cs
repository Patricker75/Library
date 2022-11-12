using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    public class NotificationsModel : PageModel
    {
        public IList<Notification> Notifications { get; set; } = default!;

        public readonly LibraryContext _context;

        public NotificationsModel(LibraryContext context)
        {
            _context = context;
        }
        
        public IActionResult OnGet()
        {
            string? loginType = HttpContext.Session.GetString("loginType");

            if (loginType == null || loginType != "member")
            {
                return RedirectToPage("/Index");
            }

            int? memberID = HttpContext.Session.GetInt32("loginID");
            if (memberID == null)
            {
                return RedirectToPage("/Index");
            }

            if (_context.Notification != null)
            {
                Notifications = _context.Notification.Where(n => n.MemberID == memberID && !n.Viewed).ToList();
            }

            return Page();
        }

        public IActionResult OnPostDismiss(int notifID)
        {
            Notification? n = _context.Notification.Find(notifID);

            if (n == null)
            {
                return RedirectToAction("Get");
            }

            n.Viewed = true;
            _context.SaveChanges();

            return RedirectToAction("Get");
        }
    }
}
