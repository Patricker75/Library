using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Library.Pages.Devices
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Device> Devices { get; set; } = default!;

        public string Message { get; set; } = default!;

        private LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet(string message = "")
        {
            Message = message;

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
                return RedirectToAction("Get");
            }

            // Check if member has device type checked out
            var devices = (from d in _context.Devices
                           join co in _context.CheckOuts on d.ID equals co.ItemID
                           where co.Type == ItemType.Device && co.MemberID == id && !co.IsReturned
                           select d.Type).ToList();
            Device? device = _context.Devices.FirstOrDefault(d => d.ID == deviceID);
            if (device == null)
            {
                return RedirectToAction("Get");
            }

            // Member has device type checked out already
            if (devices.Contains(device.Type))
            {
                return RedirectToAction("Get", new { message = $"You Already Have a {device.Type} Checked Out"});
            }

            Member? m = _context.Members.FirstOrDefault(m => m.ID == id);
            if (m == null)
            {
                return RedirectToAction("Get");
            }

            if (m.Status != MemberStatus.Active)
            {
                return RedirectToAction("Get", new { message = "Your Account is Supsended" });
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

            _context.Database.ExecuteSqlRaw("INSERT INTO check_out (check_out_date, due_date, item_type, item_id, member_id, returned)\r\nVALUES ({0},{1},{2},{3},{4},{5})",
                DateTime.Now, DateTime.Now.AddDays(interval), ItemType.Device, device.ID, m.ID, 0);

            _context.SaveChanges();

            return RedirectToAction("Get", new { message = "Device Check Out Successful"});
        }
    
        public IActionResult OnPostEdit(int deviceID)
        {
            return RedirectToPage("/Devices/Edit", new { deviceID = deviceID });
        }
    }
}
