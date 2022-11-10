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
    [BindProperties]
    public class CreateModel : PageModel
    {
        public int Type { get; set; } = -1;

        public string Location { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;
        
        public CreateModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        private readonly Library.Data.LibraryContext _context;

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
            if (Type < 0)
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
                Room newRoom = new Room()
                {
                    RoomType = (RoomType)Type,
                    Location = Location,
                    IsAvailable = true
                };

                _context.Room.Add(newRoom);
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
