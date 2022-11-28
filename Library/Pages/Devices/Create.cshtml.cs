using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Data;
using Library.Models;

namespace Library.Pages.Devices
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Device Device { get; set; } = default!;

        private readonly LibraryContext _context;

        public CreateModel(LibraryContext context)
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
            Device.DateAdded = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Devices.Add(Device);
                _context.SaveChanges();

                return RedirectToPage("Create");
            }

            return Page();
        }
    }
}
