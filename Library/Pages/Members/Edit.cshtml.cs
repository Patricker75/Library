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
            if (ModelState.IsValid && VerifyForm())
            {
                switch (Member.Type)
                {
                    case MemberType.Student:
                        Member.CheckOutLimit = 3;
                        break;
                    case MemberType.Professional:
                        Member.CheckOutLimit = 10;
                        break;
                }

                _context.Attach(Member).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToPage("/Login");
            }

            return Page();
        }
    }
}
