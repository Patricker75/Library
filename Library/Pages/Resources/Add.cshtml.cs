using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Resources
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Resource Resource { get; set; } = default!;

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
            if (ModelState.IsValid)
            {
                Resource.DateAdded = DateTime.Now;

                _context.Resources.Add(Resource);
                _context.SaveChanges();

                return RedirectToPage("/Resources/Add");
            }

            return Page();
        }
    }
}
