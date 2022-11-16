using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Data;
using Library.Models;

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

        public IActionResult OnGet(int id)
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
            {
                return RedirectToPage("/Index");
            }

            Service? s = _context.Service.Find(id);

            if (s == null)
            {
                return RedirectToPage("/Index/Services");
            }

            Service = s;
            
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Service.Update(Service);
                _context.SaveChanges();

                return RedirectToPage("/Services/Index");
            }

            return Page();
        }
    }
}
