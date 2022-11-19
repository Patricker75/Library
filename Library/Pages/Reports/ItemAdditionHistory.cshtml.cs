using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Reports
{
    public class ItemAdditionHistory : PageModel
    {
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public bool GenerateReport = false;

        public IList<Book> Books { get; set; } = default!;
        public IList<Device> Devices { get; set; } = default!;
        public IList<Resource> Resources { get; set; } = default!;
        public IList<Room> Rooms { get; set; } = default!;
        public IList<Service> Services { get; set; } = default!;

        private LibraryContext _context;

        public ItemAdditionHistory(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Start = DateTime.Today.AddDays(-30);
            End = DateTime.Today;
        }

        public IActionResult OnPost() 
        {
            End = End.AddDays(1).AddSeconds(-1);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Books = _context.Books.Where(b => Start <= b.DateAdded && b.DateAdded <= End).ToList();
            Devices = _context.Devices.Where(d => Start <= d.DateAdded && d.DateAdded <= End).ToList();
            Resources = _context.Resources.Where(r => Start <= r.DateAdded && r.DateAdded <= End).ToList();
            Rooms = _context.Rooms.Where(r => Start <= r.DateAdded && r.DateAdded <= End).ToList();
            Services = _context.Services.Where(s => Start <= s.DateAdded && s.DateAdded <= End).ToList();

            GenerateReport = true;
            return Page();
        }
    }
}
