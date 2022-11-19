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
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
        
        public IActionResult OnPost()
        {
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
