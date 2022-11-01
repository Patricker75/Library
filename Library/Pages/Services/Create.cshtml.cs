using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Data;
using Library.Models;

namespace Library.Pages.Services
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        public string Location { get; set; } = string.Empty;

        [BindProperty]
        public bool Availability { get; set; } = true;

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

        private bool VerifyForm()
        {
            // Check if name is filled
            if (string.IsNullOrEmpty(Name))
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
                Service newService = new Service()
                {
                    Name = Name,
                    Location = Location,
                    Availability = Availability
                };

                _context.Service.Add(newService);
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
