using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; } = default!;

        public IList<int> CheckedOutBookID { get; set; } = default!;
        public IList<int> CheckedOutDeviceID { get; set; } = default!;
        public IList<Hold> Holds { get; set; } = default!;
        public Room? ReservedRoom { get; set; } = default!;

        public bool IsAccountSuspended { get; set; }
        public bool NewNotifications { get; set; }

        public LibraryContext Context;

        public ProfileModel(LibraryContext context)
        {
            Context = context;
        }

        public IActionResult OnGet()
        {
            int? id = HttpContext.Session.GetInt32("loginID");
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            Member? m = Context.Members.FirstOrDefault(m => m.ID == id);
            if (m == null)
            {
                return RedirectToPage("/Index");
            }

            Member = m;

            CheckedOutBookID = (from b in Context.Books
                                   join co in Context.CheckOuts on b.ID equals co.ItemID
                                   where co.Type == ItemType.Book && !co.IsReturned && co.MemberID == m.ID
                                   select co.ID).ToList();

            CheckedOutDeviceID = (from d in Context.Devices
                                  join co in Context.CheckOuts on d.ID equals co.ItemID
                                  where co.Type == ItemType.Device && !co.IsReturned && co.MemberID == m.ID
                                  select co.ID).ToList();

            Holds = (from h in Context.Holds
                      where h.MemberID == Member.ID && h.IsWaiting
                      select h).ToList();

            ReservedRoom = (from r in Context.Rooms
                           where r.MemberID == Member.ID
                           select r).FirstOrDefault();

            IsAccountSuspended = Member.Status == MemberStatus.Suspended;
            NewNotifications = Context.Notifications.Any(n => n.MemberID == Member.ID && !n.Viewed);

            return Page();
        }

        public IActionResult OnPostReturn(int checkOutID)
        {
            CheckOut? co = Context.CheckOuts.FirstOrDefault(co => co.ID == checkOutID);
            if (co == null)
            {
                return Page();
            }

            co.ReturnDate = DateTime.Now;
            co.IsReturned = true;

            Context.Attach(co).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();

            return RedirectToPage("/Members/Profile");
        }

        public IActionResult OnPostRelinquish(string location)
        {
            Room? r = Context.Rooms.FirstOrDefault(r => r.Location == location);
            if (r == null)
            {
                return Page();
            }

            r.MemberID = null;
            r.IsAvailable = true;

            Context.Attach(r).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();

            return RedirectToPage("/Members/Profile");
        }
    }
}
