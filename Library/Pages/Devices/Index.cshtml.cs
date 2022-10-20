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
    public class IndexModel : PageModel
    {
        private readonly Library.Data.LibraryContext _context;

        public IndexModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        public IList<Device> Device { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Device != null)
            {
                Device = await _context.Device.ToListAsync();
            }
        }
    }
}
