using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Devices
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Condition { get; set; } = -1;

        public int Type { get; set; } = -1;

        public string ErrorMessage { get; set; } = string.Empty;

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

            ID = d.ID;
            Name = d.Name;
            Type = (int)d.ItemType;
            Condition = (int)d.Condition;

            return Page();
        }

        private bool VerifyForm()
        {
            // Check that there is an Name
            if (string.IsNullOrEmpty(Name) || Name.Length > 100)
            {
                return false;
            }

            // Check that Condition is Chosen
            if (Condition < 0)
            {
                return false;
            }

            // Check that Item Type is Chosen
            if (Type < 0)
            {
                return false;
            }

            return true;
        }

        public IActionResult OnPost(int deviceID)
        {
            Device? d = _context.Device.Find(deviceID);
            
            if (d == null)
            {
                return Page();
            }

            if (VerifyForm())
            {
                d.Name = Name;
                d.Condition = (Condition)Condition;
                d.ItemType = (ItemType)Type;
                
                _context.SaveChanges();

                return RedirectToPage("/Devices/Index");
            }
            else
            {
                ErrorMessage = "A Required Field has Been Left Blank";

                ModelState.Clear();

                return Page();
            }
        }
    }
}
