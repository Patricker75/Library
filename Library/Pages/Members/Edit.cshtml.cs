using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Members
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; } = default!;

        public int Error { get; set; }

        private readonly LibraryContext _context;

        public EditModel(LibraryContext library)
        {
            _context = library;
        }

        public IActionResult OnGet()
        {
            string? type = HttpContext.Session.GetString("loginType");
            int? id = HttpContext.Session.GetInt32("loginID");

            if (type != "member")
            {
                return RedirectToPage("/Index");
            }

            Member? m  = _context.Members.FirstOrDefault(m => m.ID == id);
            if (m == null)
            {
                return RedirectToPage("/Index");
            }

            Member = m;

            return Page();
        }

        private bool VerifyForm()
        {
            if (Member.BirthDate > DateTime.Today)
            {
                Error = 2;
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            Member? member = _context.Members.FirstOrDefault(m => m.ID == Member.ID);
            if (member == null) 
            {
                return RedirectToPage("/Index");
            }

            if (ModelState.IsValid && VerifyForm())
            {
                member.FirstName = Member.FirstName;
                member.MiddleName = Member.MiddleName;
                member.LastName = Member.LastName;

                member.PhoneNumber = Member.PhoneNumber;
                member.Address = Member.Address;

                member.Gender = Member.Gender;
                member.BirthDate = Member.BirthDate;

                switch (Member.Type)
                {
                    case MemberType.Student:
                        member.CheckOutLimit = 3;
                        break;
                    case MemberType.Professional:
                        member.CheckOutLimit = 10;
                        break;
                }

                _context.Attach(member).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToPage("/Members/Profile");
            }

            return Page();
        }
    }
}
