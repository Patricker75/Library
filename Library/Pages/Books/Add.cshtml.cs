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
        public string TestStr { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        // Returns True if all Authors Added have a first and last name
        private bool ValidateAuthors()
        {
            foreach(var author in Authors)
            {
                if (string.IsNullOrEmpty(author.FirstName) || string.IsNullOrEmpty(author.LastName))
                {
                    return false;
                }
            }

            return true;
        }

        public IActionResult OnPost()
        {
            bool validForm = true;
            if (!ValidateAuthors())
            {
                validForm = false;
            }


            if (validForm)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
