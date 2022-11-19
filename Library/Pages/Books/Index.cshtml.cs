using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Books
{
    public enum SearchOption
    {
        Title = 0,
        Author = 1
    }

    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Book> Books { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public SearchOption SearchOption { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = default!;

        public readonly LibraryContext Context;

        public IndexModel(LibraryContext context)
        {
            Context = context;
        }

        public IActionResult OnGet()
        {
            if (Context.Books != null)
            {
                if (string.IsNullOrEmpty(SearchString))
                {
                    Books = Context.Books.ToList();
                    
                    return Page();
                }

                switch (SearchOption)
                {
                    case SearchOption.Title:
                        Books = Context.Books.Where(b => b.Title.Contains(SearchString)).ToList();
                        break;

                    case SearchOption.Author:
                        IQueryable<Author> authors = Context.Authors.Where(a => a.FirstName.Contains(SearchString) || a.LastName.Contains(SearchString));
                        Books = Context.Books.Where(b => authors.Where(a => a.ID == b.AuthorID).Any()).ToList();
                        break;
                }

                return Page();
            }

            return Page();
        }
    }
}
