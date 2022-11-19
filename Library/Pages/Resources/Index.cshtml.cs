using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Resources
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Resource> Resources { get; set; } = default!;

        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (_context.Resources != null)
            {
                Resources = _context.Resources.ToList();
            }

            return Page();
        }

        public IActionResult OnPost(int resourceID)
        {            
            Resource? r = _context.Resources.FirstOrDefault(r => r.ID == resourceID);
            if (r == null)
            {
                return Page();
            }

            int? id = HttpContext.Session.GetInt32("loginID");
            string? loginType = HttpContext.Session.GetString("loginType");

            if (loginType != null && loginType == "member")
            {
                if (id != null)
                {
                    _context.Accesses.Add(new Models.Relationships.Access()
                    {
                        MemberID = (int)id,
                        ResourceID = resourceID,
                        TimeStamp = DateTime.Now
                    });

                    _context.SaveChanges();
                }
            }

            return Redirect(r.Url);
        }

        public IActionResult OnPostEdit(int resourceID)
        {
            return RedirectToPage("/Resources/Edit", new { resourceID = resourceID });
        }
    }
}
