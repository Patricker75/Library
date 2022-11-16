using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Members
{
    [BindProperties]
    public class CheckoutHistoryModel : PageModel
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Start { get; set; } = DateTime.Today.AddDays(-1);

        [DataType(DataType.Date)]
        public DateTime End { get; set; } = DateTime.Today;

        public bool Books { get; set; }
        public bool Devices { get; set; }
        public bool Services { get; set; }

        private readonly LibraryContext _context;

        public CheckoutHistoryModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? memberID = -1)
        {
            if (memberID == -1)
            {
                if (HttpContext.Session.GetString("loginType") != "member")
                {
                    return RedirectToPage("/Index");
                }

                memberID = HttpContext.Session.GetInt32("loginID");
                if (memberID == null)
                {
                    return RedirectToPage("/Index");
                }

                ID = _context.Member.Find(memberID).ID;
            }
            else
            {
                Member? m = _context.Member.Find(memberID);

                if (m == null)
                {
                    return RedirectToPage("/Index");
                }

                ID = m.ID;
            }            

            return Page();
        }

        private bool VerifyForm()
        {
            // Check if Start occurs after End
            if (Start > End)
            {
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            if (VerifyForm())
            {
                return RedirectToPage("/Reports/COHistory", new { 
                    memberID = ID,
                    fetchBooks = Books,
                    fetchDevices = Devices,
<<<<<<< HEAD
=======
                    
>>>>>>> 7cdf424d32340358b0f97c2f3ee09a7acc2271e2
                    fetchServices = Services,
                    start = Start,
                    end = End
                });
            }
            else
            {
                return Page();
            }
        }
    }
}
