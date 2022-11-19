using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;

namespace Library.Pages.Devices
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Device> Devices { get; set; } = default!;

        private LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            if (_context.Devices != null)
            {
                List<int> checkedOutID = (from d in _context.Devices
                                          join co in _context.CheckOuts on d.ID equals co.ItemID
                                          where co.Type == ItemType.Device && !co.IsReturned
                                          select d.ID).ToList();
                Devices = (from d in _context.Devices
                           where !checkedOutID.Contains(d.ID)
                           select d).ToList();
            }
        }

        public IActionResult OnPost(int deviceID)
        {
            int? id = HttpContext.Session.GetInt32("loginID");
            if (id == null)
            {
                return Page();
            }

            // Check if member has device type checked out
            var devices = (from d in _context.Devices
                           join co in _context.CheckOuts on d.ID equals co.ItemID
                           where co.Type == ItemType.Device && co.MemberID == id && !co.IsReturned
                           select d.Type).ToList();
            Device? device = _context.Devices.FirstOrDefault(d => d.ID == deviceID);
            if (device == null)
            {
                return Page();
            }
            if (devices.Contains(device.Type))
            {
                return Page();
            }

            Member? m = _context.Members.FirstOrDefault(m => m.ID == m.ID);
            if (m == null)
            {
                return Page();
            }

            // Days after for due date
            int interval = 0;
            switch (m.Type)
            {
                case MemberType.Student:
                    interval = 14;
                    break;
                case MemberType.Professional:
                    interval = 28;
                    break;
            }

            _context.CheckOuts.Add(new CheckOut()
            {
                MemberID = (int)id,
                Type = ItemType.Device,
                ItemID = deviceID,
                CheckOutDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(interval),
                IsReturned = false
            });

            _context.SaveChanges();

            return RedirectToPage("/Devices/Index");
        }
    
        public IActionResult OnPostEdit(int deviceID)
        {
            return RedirectToPage("/Devices/Edit", new { deviceID = deviceID });
        }
    }
}
