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
        public int RoomType { get; set; } = -1;

        [BindProperty]
        public string Location { get; set; } = string.Empty;

        [BindProperty]
        public bool IsAvailable { get; set; } = true;

        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;
        public CreateModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        private readonly Library.Data.LibraryContext _context;

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        
        private bool VerifyForm()
        {
            // Check if name is filled
            if (RoomType<0)
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
                    RoomType = (RoomType)RoomType,
                    Location = Location,
                    IsAvailable = IsAvailable
                };



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
