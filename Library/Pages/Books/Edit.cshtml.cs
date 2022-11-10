using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Books
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public int ID { get; set; }

        public string Title { get; set; } = string.Empty;

        public int RowCount { get; set; } = 0;

        public IList<Author> Authors { get; set; } = default!;

        public string Dewey { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public int Audience { get; set; } = -1;

        public string Publisher { get; set; } = string.Empty;

        public int Condition { get; set; } = -1;

        public string ErrorMessage { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public EditModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int bookID)
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
            {
                return RedirectToPage("/Index");
            }

            Book? b = _context.Book.Find(bookID);
            
            if (b == null)
            {
                return RedirectToPage("/Books/Index");
            }

            ID = b.ID;
            Title = b.Title;

            IQueryable<Writes> writes = _context.Writes.Where(w => w.BookID == bookID);
            Authors = (from a in _context.Author
                       where writes.Where(w => w.AuthorID == a.ID).Any()
                       select a).ToList();

            RowCount = Authors.Count;

            Dewey = b.DeweyNumber;
            Genre = b.Genre;
            Summary = b.Summary;

            Audience = (int)b.Audience;
            Condition = (int)b.Condition;

            IQueryable<Publishes> publishes = _context.Publishes.Where(p => p.BookID == bookID);
            Publisher = _context.Publisher.Where(p => p.ID == publishes.First().PublisherID).First().Name;

            return Page();
        }

        // Returns True if all Authors Added have a first and last name
        private bool ValidateAuthors()
        {
            foreach (var author in Authors)
            {
                if (!author.IsValid())
                {
                    return false;
                }
            }

            return true;
        }

        private void DeleteEmptyRows()
        {
            for (int i = 0; i < Authors.Count; i++)
            {
                if (Authors[i].IsEmpty())
                {
                    Authors.RemoveAt(i);
                    i--;
                }
            }

            if (Authors.Count > 0)
            {
                RowCount = Authors.Count;
            }
            else
            {
                RowCount = 0;
            }
        }

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

        private bool VerifyForm()
        {
            bool validForm = true;

            // Checks if Title, Publisher, Dewey, Summary, Genre is empty/null
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Publisher) || string.IsNullOrEmpty(Dewey) || string.IsNullOrEmpty(Summary) || string.IsNullOrEmpty(Genre))
            {
                validForm = false;
            }


            // Removes any that are blank from authors list
            DeleteEmptyRows();

            // Checks if all authors are valid
            if (!ValidateAuthors())
            {
                validForm = false;
            }

            // Checks if Condition or Audience is -1
            if (Condition == -1 || Audience == -1)
            {
                validForm = false;
            }

            return validForm;
        }

        public IActionResult OnPost(int bookID)
        {
            if (VerifyForm())
            {
                Book? b = _context.Book.Find(bookID);

                if (b == null)
                {
                    return Page();
                }

                b.Title = Title;
                b.Audience = (Audience)Audience;
                b.Condition = (Condition)Condition;
                b.Summary = Summary;
                b.Genre = Genre;
                b.DeweyNumber = Dewey;

                int? publisherID = _context.Publishes.Where(p => p.BookID == b.ID).First().PublisherID;

                if (publisherID == null)
                {
                    return Page();
                }

                int newPublisherID = GetPublisherID();

                if (publisherID != newPublisherID)
                {
                    _context.Publishes.Remove(_context.Publishes.Where(p => p.PublisherID == publisherID && p.BookID == b.ID).First());

                    _context.Publishes.Add(new Publishes()
                    {
                        BookID = b.ID,
                        PublisherID = newPublisherID
                    });
                }

                var newAuthorsID = GetAuthorIDs();

                var writes = _context.Writes.Where(w => w.BookID == b.ID).ToList();
                foreach (var w in writes)
                {
                    if (!newAuthorsID.Contains(w.AuthorID))
                    {
                        _context.Writes.Remove(w);
                    }
                }

                foreach (int id in newAuthorsID)
                {
                    if (!writes.Where(w => w.AuthorID == id).Any())
                    {
                        _context.Writes.Add(new Writes()
                        {
                            AuthorID = id,
                            BookID= b.ID,
                        });
                    }
                }

                _context.SaveChanges();
            }

            return Page();
        }
    }
}
