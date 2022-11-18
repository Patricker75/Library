using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Books
{
    public class AddModel : PageModel
    {
        public int RowCount { get; set; } = 0;

        [BindProperty]
        public Book NewBook { get; set; }

        [BindProperty]
        public IList<Author> Authors { get; set; } = default!;

        [BindProperty]
        public string Publisher { get; set; } = string.Empty;

        public int Error { get; set; }

        private readonly LibraryContext _context;

        public AddModel(LibraryContext context)
        {
            _context = context;
            Authors = new List<Author>() { new Author()};
        }

        public IActionResult OnGet()
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
            {
                return RedirectToPage("/Index");
            }

            RowCount++;

            return Page();
        }
        /*
        private int GetPublisherID()
        {
            int? publisherID = null;
            if (_context.Publisher.Any())
            {
                var queryResult = _context.Publisher.Where(p => p.Name == Publisher);
                if (queryResult.Any())
                {
                    publisherID = queryResult.First().ID;
                }
            }

            if (publisherID == null)
            {
                Models.Publisher newPublisher = new Models.Publisher()
                {
                    Name = Publisher
                };

                _context.Publisher.Add(newPublisher);
                _context.SaveChanges();

                publisherID = newPublisher.ID;
            }

            return (int)publisherID;
        }

        private IList<int> GetAuthorIDs()
        {
            List<int> authorIDs = new List<int>();

            // Checks if author table is empty
            if (!_context.Author.Any())
            {
                _context.AddRange(Authors);

                _context.SaveChanges();

                foreach (Author author in _context.Author.ToList())
                {
                    authorIDs.Add(author.ID);
                }

                return authorIDs;
            }

            // Iterates through Authors (the list) for any existing authors
            foreach (Author author in Authors)
            {
                var queryResult = _context.Author.Where(a => a.FirstName == author.FirstName
                && a.MiddleName == author.MiddleName
                && a.LastName == author.LastName);

                int authorID;
                if (queryResult.Any())
                {
                    authorID = queryResult.First().ID;
                }
                else
                {
                    _context.Author.Add(author);
                    _context.SaveChanges();

                    authorID = author.ID;
                }

                authorIDs.Add(authorID);
            }

            return authorIDs;
        }

        private void RelateAuthorToBook(int bookID, IList<int> authorIDs)
        {
            foreach (int authorID in authorIDs)
            {
                _context.Writes.Add(new Writes()
                {
                    AuthorID = authorID,
                    BookID = bookID
                });
            }
        }

        private void AddToDatabase()
        {
            Book newBook = new Book()
            {
                Title = Title,
                Audience = (Audience)Audience,
                Condition = (Condition)Condition,
                DeweyNumber = Dewey,
                Summary = Summary,
                Genre = Genre
            };

            int publisherID = GetPublisherID();
            IList<int> authorIDs = GetAuthorIDs();

            // Add newBook to DB and save to get ID
            _context.Book.Add(newBook);
            _context.SaveChanges();

            // Relates newBook, publisher, and authors appropriately
            _context.Publishes.Add(new Publishes()
            {
                BookID = newBook.ID,
                PublisherID = publisherID,
            });
            RelateAuthorToBook(newBook.ID, authorIDs);

            _context.SaveChanges();
        }
        */
        public IActionResult OnPost()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                _context.SaveChanges();

                return RedirectToPage("/Books/Create");
            }

            return Page();
        }
    }
}
