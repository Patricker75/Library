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
        
        public IActionResult OnPost()
        {
            Book.DateAdded = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Books.Add(Book);
                _context.SaveChanges();

                return RedirectToPage("/Books/Create");
            }

            return Page();
        }
    }
}
