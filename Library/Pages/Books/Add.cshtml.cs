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
        public int RowCount { get; set; } = 1;

        [BindProperty]
        public IList<Author> Authors { get; set; } = default!;

        [BindProperty]
        public string Dewey { get; set; } = string.Empty;

        [BindProperty]
        public string Summary { get; set; } = string.Empty;

        [BindProperty]
        public string Genre { get; set; } = string.Empty;

        [BindProperty]
        public int Audience { get; set; }

        [BindProperty]
        public string Publisher { get; set; } = string.Empty;

        public void OnGet()
        {
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
            RowCount = Authors.Count;
        }

        public IActionResult OnPost()
        {
            bool validForm = true;
            if (!ValidateAuthors())
            {
                DeleteEmptyRows(); 
                validForm = false;
            }

            if (validForm)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.Clear();

                return Page();
            }
        }
    }
}
