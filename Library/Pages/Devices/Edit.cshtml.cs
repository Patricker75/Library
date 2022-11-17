using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Devices
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Device Device { get; set; } = default!;

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

            Device? d = _context.Device.Find(id);

            if (d == null)
            {
                return RedirectToPage("/Devices/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Device.Update(Device);                
                _context.SaveChanges();

                return RedirectToPage("/Devices/Index");
            }

            return Page();
        }
    }
}
