using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Books
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Book Book { get; set; } = default!;

        [BindProperty]
        public Author Author { get; set; } = default!;

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public int Error { get; set; }

        private readonly LibraryContext _context;

        public AddModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string? role = HttpContext.Session.GetString("employeeRole");
            if (role == null || (role != "Admin" && role != "Librarian"))
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
        
        private int SearchAuthor()
        {
            Author? author = _context.Authors.First(a => a.FirstName == Author.FirstName && a.LastName == Author.LastName);
            if (author == null)
            {
                _context.Authors.Add(Author);
                _context.SaveChanges();

                return Author.ID;
            }

            return author.ID;
        }

        private int SearchPublisher()
        {
            Publisher? publisher = _context.Publishers.First(p => p.Name == Publisher.Name);
            if (publisher == null)
            {
                _context.Publishers.Add(Publisher);
                _context.SaveChanges();

                return Publisher.ID;
            }

            return publisher.ID;
        }

        public IActionResult OnPost()
        {
            Book.DateAdded = DateTime.Now;

            if (ModelState.IsValid)
            {
                int authorID = SearchAuthor();
                int publisherID = SearchPublisher();

                Book.AuthorID = authorID;
                Book.PublisherID = publisherID;

                _context.Books.Add(Book);
                _context.SaveChanges();

                return RedirectToPage("/Books/Add");
            }

            return Page();
        }
    }
}
