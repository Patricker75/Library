using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Library.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Members
{
    public class FinesModel : PageModel
    {
        public IList<UnpaidFine> Fines { get; set; } = default!;

        private readonly LibraryContext _context;

        public FinesModel(LibraryContext context)
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

            if (_context.Fines != null)
            {
                Fines = _context.UnpaidFines.Where(uf => uf.MemberID == id).ToList();
            }

            return Page();
        }

        public string GetItemType(int checkoutID)
        {
            CheckOut? co = _context.CheckOuts.FirstOrDefault(co => co.ID == checkoutID);
            if (co == null)
            {
                return "";
            }

            return co.Type.ToString();
        }

        public string GetItem(int checkoutID)
        {
            CheckOut? co = _context.CheckOuts.FirstOrDefault(co => co.ID == checkoutID);
            if (co == null)
            {
                return "";
            }

            switch (co.Type)
            {
                case ItemType.Book:
                    return _context.Books.First(b => b.ID == co.ItemID).Title;
                case ItemType.Device:
                    return _context.Devices.First(d => d.ID == co.ItemID).Name;
                default:
                    return "";
            }
        }

        public IActionResult OnPost(int checkoutID, decimal fineAmount)
        {
            int? id = HttpContext.Session.GetInt32("loginID");
            if (id == null)
            {
                return RedirectToPage("/Members/Fines");
            }

            Member? m = _context.Members.FirstOrDefault(m => m.ID == id);
            if (m == null)
            {
                return RedirectToPage("/Members/Fines");
            }

            IList<Fine> paidFines = _context.Fines.Where(f => f.CheckoutID == checkoutID && f.MemberID == m.ID).ToList();
            foreach (var f in paidFines)
            {
                f.PaidDate = DateTime.Now;
            }

            m.Balance -= fineAmount;

            if (m.Balance == 0 && m.Status == MemberStatus.Suspended)
            {
                m.Status = MemberStatus.Active;
            }

            _context.AttachRange(paidFines);
            _context.Attach(m).State = EntityState.Modified;

            _context.SaveChanges();

            return RedirectToPage("/Members/Fines");
        }
    }
}
