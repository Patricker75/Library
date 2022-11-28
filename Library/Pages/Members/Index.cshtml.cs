using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public MemberStatus FilteredStatus { get; set; }

        public IList<Member> Members { get; set; } = default!;

        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;

            FilteredStatus = MemberStatus.Active;
        }

        public IActionResult OnGet()
        {
            if (_context.Members != null)
            {
                Members = _context.Members.Where(m => m.Status == FilteredStatus).ToList();
            }

            return Page();
        }

        public IActionResult OnPostReactivate(int memberID)
        {
            Member? member = _context.Members.FirstOrDefault(m => m.ID == memberID);
            if (member == null)
            {
                return RedirectToAction("Get");
            }

            member.Status = MemberStatus.Active;
            
            _context.Attach(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Get");
        }
    }
}
