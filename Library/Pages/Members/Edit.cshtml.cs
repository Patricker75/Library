using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Members
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; } = default!;

        public int Error { get; set; }
        
        private readonly LibraryContext _context;

        public EditModel(LibraryContext context)
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

            Member? m = _context.Member.Find(memberID);

            if (m == null)
            {
                return RedirectToPage("/Index");
            }

            Member = m;

            return Page();
        }

        bool VerifyForm()
        {
            if (Member.BirthDate > Member.JoinDate)
            {
                Error = 1;
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            Member? m = _context.Member.Find(Member.ID);

            if (m == null)
            {
                return Page();
            }

            m.FirstName = Member.FirstName;
            m.MiddleName = Member.MiddleName;
            m.LastName = Member.LastName;

            m.PhoneNumber = Member.PhoneNumber;
            m.Address = Member.Address;

            m.Gender = Member.Gender;
            m.BirthDate = Member.BirthDate;

            if (VerifyForm())
            {
                _context.Member.Update(m);

                _context.SaveChanges();

                return RedirectToPage("/Members/Profile");
            }
            
            return Page();
        }
    }
}
