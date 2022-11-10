using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Data;
using Library.Models;

namespace Library.Pages.Services
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

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

        private bool VerifyForm()
        {
            // Check if name is filled
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            // Check if location is filled
            if (string.IsNullOrEmpty(Location))
            {
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            if (VerifyForm())
            {
                Service newService = new Service()
                {
                    Name = Name,
                    Location = Location,
                    IsAvailable = true
                };

                _context.Service.Add(newService);
                _context.SaveChanges();

                return RedirectToPage("Create");
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
