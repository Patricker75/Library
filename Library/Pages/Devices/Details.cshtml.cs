using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;

namespace Library.Pages.Devices
{
    public class DetailsModel : PageModel
    {
        private readonly Library.Data.LibraryContext _context;

        public DetailsModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

      public Device Device { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Device == null)
            {
                return NotFound();
            }

            var device = await _context.Device.FirstOrDefaultAsync(m => m.ID == id);
            if (device == null)
            {
                return NotFound();
            }
            else 
            {
                Device = device;
            }
            return Page();
        }
    }
}
