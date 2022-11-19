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
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
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
