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
    [BindProperties]
    public class CreateModel : PageModel
    {
        public string Name { get; set; } = string.Empty;
        
        public int Condition { get; set; } = -1;

        public int Type { get; set; } = -1;

        public string ErrorMessage { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public CreateModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            if (loginType == null || loginType != "employee")
            {
                return RedirectToPage("/Index");
            }

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
