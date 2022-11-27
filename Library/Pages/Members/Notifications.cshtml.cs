using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    public class NotificationsModel : PageModel
    {
        public IList<Notification> Notifications { get; set; } = default!;

        private readonly LibraryContext _context;

        public NotificationsModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("loginType") != "member")
            {
                return RedirectToPage("/Index");
            }

            int? id = HttpContext.Session.GetInt32("loginID");
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            if (_context.Notifications != null)
            {
                Notifications = _context.Notifications.Where(n => n.MemberID == id && !n.Viewed).ToList();
            }

            return Page();
        }

        public IActionResult OnPost(int notifID)
        {
            Notification? notif = _context.Notifications.FirstOrDefault(n => n.ID == notifID);
            if (notif == null)
            {
                return Page();
            }

            notif.Viewed = true;

            _context.Attach(notif).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("/Members/Notifications");
        }
    }
}
