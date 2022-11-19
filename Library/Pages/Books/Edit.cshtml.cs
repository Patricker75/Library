using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Books
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Book Book { get; set; } = default!;

        [BindProperty]
        public Author Author { get; set; } = default!;

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public int Error { get; set; }

        private readonly LibraryContext _context;

        public EditModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? bookID)
        {
            string? role = HttpContext.Session.GetString("employeeRole");
            if (role == null || (role != "Admin" && role != "Librarian"))
            {
                return RedirectToPage("/Index");
            }

            if (bookID == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Book? book = _context.Books.FirstOrDefault(b => b.ID == bookID);
            if (book == null)
            {
                return RedirectToPage("/Books/Index");
            }
            Book = book;

            Publisher? publisher = _context.Publishers.FirstOrDefault(p => p.ID == Book.PublisherID);
            if (publisher == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Author? author = _context.Authors.FirstOrDefault(a => a.ID == book.AuthorID);
            if (author == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Author = author;
            Publisher = publisher;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Attach(Author).State = EntityState.Modified;
                _context.Attach(Publisher).State = EntityState.Modified;
                _context.Attach(Book).State = EntityState.Modified;

                _context.SaveChanges();

                return RedirectToPage("/Books/Index");
            }

            return Page();
        }

        public IActionResult OnPostDiscard()
        {
            return RedirectToPage("/Books/Index");
        }
    }
}
