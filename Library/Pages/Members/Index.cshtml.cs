using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    public class IndexModel : PageModel
    {
        public IList<Member> Members { get; set; } = default!;

        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            if (_context.Members != null)
            {
                Members = _context.Members.Where(m => m.Status == MemberStatus.Active).ToList();
            }
        }
    }
}
