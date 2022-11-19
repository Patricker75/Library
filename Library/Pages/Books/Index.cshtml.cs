using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Books
{
    public enum SearchOption
    {
        Title = 0,
        Author = 1,
        Publisher = 2,
        Genre = 3
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
                        return Page();
                    case SearchOption.Genre:
                        Books = Context.Books.Where(b => b.Genre.ToString().Contains(SearchString)).ToList();
                        return Page();
                }

                return Page();
            }

            return Page();
        }
    }
}
