using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Members
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; } = default!;

        [BindProperty]
        public LoginUser Login { get; set; } = default!;

        public int Error { get; set; }
 
        private readonly LibraryContext _context;

        public AddModel(LibraryContext library)
        {
            _context = library;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        private bool VerifyForm()
        {
            // Check if username exists in logins table
            if (_context.Logins.Where(l => l.Username == Login.Username).Any())
            {
                Error = 1;
                return false;
            }

            if (Member.BirthDate > DateTime.Today)
            {
                Error = 2;
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid && VerifyForm())
            {
                _context.Logins.Add(Login);
                _context.SaveChanges();

                Member.LoginID = Login.ID;
                Member.Balance = 0;
                Member.Status = MemberStatus.Active;
                Member.JoinDate = DateTime.Today;

                switch (Member.Type)
                {
                    case MemberType.Student:
                        Member.CheckOutLimit = 3;
                        break;
                    case MemberType.Professional:
                        Member.CheckOutLimit = 10;
                        break;
                }

                _context.Members.Add(Member);
                _context.SaveChanges();

                return RedirectToPage("/Login");
            }

            return Page();
        }
    }
}
