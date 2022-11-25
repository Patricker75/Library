using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult OnGet(int? deviceID)
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
            {
                return RedirectToPage("/Index");
            }

            Device? d = _context.Devices.Find(deviceID);

            if (d == null)
            {
                return RedirectToPage("/Devices/Index");
            }

            Device = d;

            return Page();
        }

        public IActionResult OnPost()
        {
            Device? device = _context.Devices.FirstOrDefault(d => d.ID == Device.ID);
            if (device == null)
            {
                return Page();
            }

            device.Type = Device.Type;
            device.Name = Device.Name;

            if (ModelState.IsValid)
            {
                _context.Attach(device).State = EntityState.Modified;                
                _context.SaveChanges();

                return RedirectToPage("/Devices/Index");
            }

            return Page();
        }

        public IActionResult OnPostDiscard()
        {
            return RedirectToPage("/Devices/Index");
        }
    }
}
