using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Services
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Service Service { get; set; } = default!;

        private readonly LibraryContext _context;

        public EditModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? serviceID)
        {
            string? role = HttpContext.Session.GetString("employeeRole");
            if (role == null || (role != "Admin" && role != "Technician"))
            {
                return RedirectToPage("/Index");
            }

            if (serviceID == null)
            {
                return RedirectToPage("/Services/Index");
            }

            Service? s = _context.Services.FirstOrDefault(s => s.ID == serviceID);
            if (s == null)
            {
                return RedirectToPage("/Services/Index");
            }

            Service = s;

            return Page();
        }

        public IActionResult OnPost()
        {
            Service? service = _context.Services.FirstOrDefault(s => s.ID == Service.ID);
            if (service == null)
            {
                return Page();
            }

            service.Name = Service.Name;
            service.Location = Service.Location;

            if (ModelState.IsValid)
            {
                _context.Attach(service).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToPage("/Services/Index");
            }

            return Page();
        }

        public IActionResult OnPostDiscard()
        {
            return RedirectToPage("/Services/Index");
        }
    }
}
