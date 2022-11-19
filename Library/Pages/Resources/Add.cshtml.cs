using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Resources
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Resource Resource { get; set; } = default!;

        public void OnGet()
        {
        }
    }
}
