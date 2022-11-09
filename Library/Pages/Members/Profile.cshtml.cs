using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Members
{
    public class ProfileModel : PageModel
    {
        public Member Member { get; set; } = default!;

        public List<CheckOuts> CurrentCheckOuts { get; set; } = default!;
        public List<Holds> Holds { get; set; } = default!;

        public readonly LibraryContext Context;

        public ProfileModel(LibraryContext context)
        {
            Context = context;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("loginType") != "member")
            {
                return RedirectToPage("../Error");
            }

            int? memberID = HttpContext.Session.GetInt32("loginID");
            if (memberID == null)
            {
                return RedirectToPage("../Error");
            }

            Member = Context.Member.Where(m => m.ID == memberID).First();

            CurrentCheckOuts = (from co in Context.CheckOuts
                                where co.MemberID == memberID && !co.Returned
                                select co).ToList();

            Holds = (from h in Context.Holds
                     where h.MemberID == memberID && h.Waiting
                     select h).ToList();

            return Page();
        }

        // id = BookID
        public IActionResult OnPostReturn(int bookID, int memberID)
        {
            // Returns book given the ID
            var checkoutTransaction = (from co in Context.CheckOuts
                                        where co.MemberID == memberID && co.BookID == bookID && !co.Returned
                                        select co).First();
            checkoutTransaction.Returned = true;

            Context.Attach(checkoutTransaction).State = EntityState.Modified;

            Context.SaveChanges();

            return RedirectToAction("Get");
        }
    }
}
