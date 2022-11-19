using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Resources
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Resource Resource { get; set; } = default!;

        private readonly LibraryContext _context;

        public EditModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? resourceID)
        {
            string? role = HttpContext.Session.GetString("employeeRole");
            if (role == null || (role != "Admin" && role != "Librarian"))
            {
                return RedirectToPage("/Index");
            }

            if (resourceID == null)
            {
                return RedirectToPage("/Resources/Index");
            }

            Resource? resource = _context.Resources.FirstOrDefault(r => r.ID == resourceID);
            if (resource == null)
            {
                return RedirectToPage("/Resources/Index");
            }

            Resource = resource;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Attach(Resource).State = EntityState.Modified;

                return RedirectToPage("/Resources/Index");
            }

            return Page();
        }

        public IActionResult OnPostDiscard()
        {
            return RedirectToPage("/Resources/Index");
        }
    }
}
