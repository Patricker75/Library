using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Books
{
    public class ViewBookModel : PageModel
    {
        [BindProperty]
        public Book Book { get; set; } = default!;

        [BindProperty]
        public Author Author { get; set; } = default!;

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        private readonly LibraryContext _context;

        public ViewBookModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? bookID)
        {
            if (bookID == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Book? b = _context.Books.FirstOrDefault(b => b.ID == bookID);
            if (b == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Book = b;

            Author? a = _context.Authors.FirstOrDefault(a => a.ID == b.AuthorID);
            if (a == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Publisher? p = _context.Publishers.FirstOrDefault(p => p.ID == b.PublisherID);
            if (p == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Author = a;
            Publisher = p;

            return Page();
        }
        
        public IActionResult OnPostCheckOut()
        {
            return RedirectToPage("/Books/Checkout", new { bookID = Book.ID });
        }

        public IActionResult OnPostEdit()
        {
            return Page();
        }
    }
}
