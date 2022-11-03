using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _context;

        public IList<Member> Members = default!;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            if (_context.Member != null)
            {
                Members = _context.Member.ToList();
            }
        }
    }
}
