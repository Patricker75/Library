using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    public class NotificationsModel : PageModel
    {
        public IList<Notification> Notifications { get; set; } = default!;

        public readonly LibraryContext Context;

        public NotificationsModel(LibraryContext context)
        {
            Context = context;
        }
        public IActionResult OnPostDismiss(bool noticationViewed)
        {
            return RedirectToPage("Members/Notifications", new { noticationViewed = noticationViewed });
        }
        public void OnGet()

        {

            if (Context.Notification != null)
            {
                Notifications = Context.Notification.ToList();
            }
        }
    }
}
