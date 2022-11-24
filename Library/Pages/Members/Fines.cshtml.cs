using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Members
{
    public class FinesModel : PageModel
    {
        public IList<Fine> Fines { get; set; } = default!;

        private readonly LibraryContext _context;

        public FinesModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Fines = _context.Fines.Where(f => f.MemberID == 2 && f.PaidDate == null).ToList();

            return Page();

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
                Fines = _context.Fines.Where(f => f.MemberID == id).ToList();
            }

            return Page();
        }

        public string GetItem(ItemType type, int id)
        {
            switch (type)
            {
                case ItemType.Book:
                    return _context.Books.First(b => b.ID == id).Title;
                case ItemType.Device:
                    return _context.Devices.First(d => d.ID == id).Name;
                default:
                    return "";
            }
        }

        public decimal CalculateFine(decimal amount, DateTime fineDate, DateTime? paidDate) 
        {
            TimeSpan diff;
            if (paidDate == null)
            {
                diff = (DateTime.Today - fineDate);
            }
            else
            {
                diff = (DateTime)paidDate - fineDate;
            }

            return amount * (int)diff.TotalDays;
        }

        public IActionResult OnPost(int fineID)
        {
            int? id = HttpContext.Session.GetInt32("loginID");
            if (id == null)
            {
                return RedirectToPage("/Members/Fines");
            }

            Member? m = _context.Members.FirstOrDefault(m => m.ID == fineID);
            if (m == null)
            {
                return RedirectToPage("/Members/Fines");
            }

            Fine paidFine = _context.Fines.First(f => f.ID == fineID);
            paidFine.PaidDate = DateTime.Now;

            m.Balance -= CalculateFine(paidFine.Amount, paidFine.FineDate, paidFine.PaidDate);

            _context.Attach(paidFine).State = EntityState.Modified;
            _context.Attach(m).State = EntityState.Modified;

            _context.SaveChanges();

            return RedirectToPage("/Members/Fines");
        }
    }
}
