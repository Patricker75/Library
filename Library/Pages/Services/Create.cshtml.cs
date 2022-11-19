using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Data;
using Library.Models;

namespace Library.Pages.Services
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Service Service { get; set; } = default!;

        private readonly LibraryContext _context;

        public CreateModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string? role = HttpContext.Session.GetString("employeeRole");
            if (role == null || (role != "Admin" && role != "Technician"))
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            Service.Availability = true;
            Service.DateAdded = DateTime.Now;

            if (ModelState.IsValid)
            {
               _context.Services.Add(Service);
               _context.SaveChanges();

                return RedirectToPage("Create");
            }
            
            return Page();
        }
    }
}
