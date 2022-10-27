using Library.Data;
using Library.Models;
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
        public int? Audience { get; set; }

        [BindProperty]
        public string Publisher { get; set; } = string.Empty;

        [BindProperty]
        public int? Condition { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public AddModel(LibraryContext context)
        {
            _context = context;
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

            // Checks if Condition or Audience is null
            if (Condition == null || Audience == null)
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

        private void AddToDatabase()
        {
            Book newBook = new Book()
            {
                Title = Title,
                //Audience = (Data.Audience)Audience,
                Condition = (Data.Condition)Condition,
                DeweyNumber = Dewey,
                Summary = Summary,
                Genre = Genre
            };

            Publisher publisher = new Publisher()
            {
                Name = Publisher
            };

            _context.Book.Add(newBook);
            _context.SaveChanges();

            int id = newBook.ID;

            ErrorMessage = "New Book ID: " + id;
        }

        public IActionResult OnPost()
        {
            if (VerifyForm())
            {
                AddToDatabase();

                return Page();
                //return RedirectToPage("Add");
                //return RedirectToPage("ViewBook", "ByObject", b);
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
