using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Models.Relationships;

namespace Library.Pages.Books
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int SearchChoice { get; set; } = 0;

        public readonly Library.Data.LibraryContext _context;

        public IndexModel(Library.Data.LibraryContext context)
        {
            _context = context;
            
            if (_context.Writes != null)
            {
                Writes = _context.Writes.ToList();
            }
        }

        public IList<Book> Books { get;set; } = default!;

        public IList<Writes> Writes { get; set; } = default!;

        public void OnGet()
        {
            if (_context.Book != null)
            {
                if (!string.IsNullOrEmpty(SearchString))
                {
                    // Gets all books in DB
                    var books = from b in _context.Book
                                select b;

                    switch (SearchChoice)
                    {
                        // Filtering by Title
                        case 0:
                            books = books.Where(s => s.Title.Contains(SearchString));
                            break;
                        
                        // Filtering by Author
                        case 1:
                            var authorIDs = from a in _context.Author
                                            where (a.FirstName + " " + a.LastName).Contains(SearchString)
                                            select a.ID;
                            var bookIDs = from w in _context.Writes
                                         where authorIDs.Contains(w.AuthorID)
                                         select w.BookID;
                            books = books.Where(b => bookIDs.Contains(b.ID)).Distinct();
                            break;

                        // Filtering by Publisher
                        case 2:
                            var publisherIDs = from p in _context.Publisher
                                            where (p.Name).Contains(SearchString)
                                            select p.ID;
                            bookIDs = from p in _context.Publishes
                                          where publisherIDs.Contains(p.PublisherID)
                                          select p.BookID;
                            books = books.Where(b => bookIDs.Contains(b.ID)).Distinct();
                            break;

                        // Filtering by Genre
                        case 3:
                            books = books.Where(b => b.Genre.Contains(SearchString));
                            break;
                    }

                    Books = books.ToList();
                }
                else
                {
                    Books = _context.Book.ToList();
                }
            }
        }
    }
}
