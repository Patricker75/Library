using Library.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Employees
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string MiddleInitial { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        public string Address { get; set; } = string.Empty;

        [BindProperty]
        public int Gender { get; set; } = -1;

        [BindProperty]
        public DateTime BirthDate { get; set; }

        [BindProperty]
        public DateTime HireDate { get; set; } = DateTime.Now;

        [BindProperty]
        public string JobTitle { get; set; }

        [BindProperty]
        public decimal Salary { get; set; }
 
        private readonly LibraryContext _context;

        public AddModel(LibraryContext library)
        {
            _context = library;
        }

        public void OnGet()
        {
            BirthDate = new DateTime(HireDate.Year - 18, HireDate.Month, HireDate.Day);
        }
    }
}
