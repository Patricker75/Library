using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Data;
using Library.Models;

namespace Library.Pages.Devices
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; } = string.Empty;
        
        [BindProperty]
        public int Condition { get; set; } = -1;

        [BindProperty]
        public int Type { get; set; } = -1;

        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public CreateModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        bool VerifyForm()
        {
            // Check that there is an Name
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            // Check that Condition is Chosen
            if (Condition < 0)
            {
                return false;
            }

            // Check that Item Type is Chosen
            if (Type < 0)
            {
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            if (VerifyForm())
            {
                Device newDevice = new Device()
                {
                    Name = Name,
                    Condition = (Condition)Condition,
                    ItemType = (ItemType)Type
                };

                _context.Device.Add(newDevice);
                _context.SaveChanges();

                return RedirectToPage("Create");
            }
            else {
                ErrorMessage = "A Required Field has Been Left Blank";
                
                ModelState.Clear();

                return Page();
            }
        }
    }
}
