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
        public string Title { get; set; } = string.Empty;

        [BindProperty]
        public int RowCount { get; set; } = 0;

        [BindProperty]
        public IList<Author> Authors { get; set; } = default!;

        [BindProperty]
        public string Dewey { get; set; } = string.Empty;

        [BindProperty]
        public string Summary { get; set; } = string.Empty;

        [BindProperty]
        public string Genre { get; set; } = string.Empty;

        [BindProperty]
        public int Audience { get; set; } = -1;

        [BindProperty]
        public string Publisher { get; set; } = string.Empty;

        [BindProperty]
        public int Condition { get; set; } = -1;

        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public AddModel(LibraryContext context)
        {
            _context = context;
            Authors = new List<Author>() { new Author()};
        }

        public void OnGet()
        {
            RowCount++;
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

        // Returns True if all Authors Added have a first and last name
        private bool ValidateAuthors()
        {
            foreach(var author in Authors)
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

        public IActionResult OnPost()
        {
            if (VerifyForm())
            {
                AddToDatabase();

                return RedirectToPage("Add");
            }
            else
            {
                ErrorMessage = "A Required Field has Been Left Blank";

                ModelState.Clear();

                if (RowCount == 0)
                {
                    Authors = new List<Author>();
                    Authors.Add(new Author());
                }

                return Page();
            }
        }
    }
}
