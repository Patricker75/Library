using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Reports
{
    [BindProperties]
    public class COHistory : PageModel
    {
        public IList<CheckOuts> CheckOuts { get; set; } = default!;
        public IList<Borrows> Borrows { get; set; } = default!;
        public IList<Uses> Uses { get; set; } = default!;

        public readonly LibraryContext Context;

        public COHistory(LibraryContext context) 
        {
            Context = context;
        }

        //public void OnGet() { }

        public void OnGet(int memberID, bool fetchBooks, bool fetchDevices, bool fetchServices, DateTime start, DateTime end)
        {
            if (fetchBooks && Context.Book != null)
            {
                CheckOuts = Context.CheckOuts.Where(co => co.ReturnDate >= start && co.ReturnDate <= end && co.MemberID == memberID).ToList();
            }

            if (fetchDevices && Context.Device != null)
            {
                Borrows = Context.Borrows.Where(b => b.ReturnDate >= start && b.ReturnDate <= end && b.MemberID == memberID).ToList();
            }

            if (fetchServices && Context.Device != null)
            {
                Uses = Context.Uses.Where(u => u.TimeStamp >= start && u.TimeStamp <= end && u.MemberID == memberID).ToList();
            }
        }
    }
}
