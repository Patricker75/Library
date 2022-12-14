using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Data;
using Library.Models;
using System.Xml.Linq;

namespace Library.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; } = default!;

        public CreateModel(LibraryContext context)
        {
            _context = context;
        }

        private readonly LibraryContext _context;

        public IActionResult OnGet()
        {
            string? role = HttpContext.Session.GetString("employeeRole");
            if (role == null || (role != "Admin" && role != "Technician"))
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
        
        public IActionResult OnPost()
        {
            Room.IsAvailable = true;
            Room.DateAdded = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Rooms.Add(Room);
                _context.SaveChanges();

                return RedirectToPage("/Rooms/Create");
            }

            return Page();
        }
    }
}
