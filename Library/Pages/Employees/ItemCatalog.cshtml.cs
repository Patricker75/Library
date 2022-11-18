using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Employees
{
    [BindProperties]
    public class ItemCatalogModel : PageModel
    {
        public bool IncludeBooks { get; set; }
        public bool IncludeDevices { get; set; }
        public bool IncludeRooms { get; set; }
        public bool IncludeServices { get; set; }

        public void OnGet()
        {
        }

        private bool VerifyForm()
        {
            if (!(IncludeBooks || IncludeDevices || IncludeRooms || IncludeServices))
            {
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            if (VerifyForm())
            {
                return RedirectToPage("/Reports/ItemCatalog", new {
                    book = IncludeBooks,
                    device = IncludeDevices,
                    room = IncludeRooms,
                    service = IncludeServices
                });
            }
            else
            {
                return Page();
            }
        }
    }
}
