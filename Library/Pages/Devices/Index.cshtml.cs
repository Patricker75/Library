using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;
using Library.Models.Relationships;

namespace Library.Pages.Devices
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; } = string.Empty;

        private readonly Library.Data.LibraryContext _context;

        public IndexModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        public IList<Device> Device { get;set; } = default!;

        public void OnGet(string message = "")
        {
            Message = message;
            if (_context.Device != null)
            {
                List<int> borrowedIDs = (from d in _context.Device
                                         join b in _context.Borrows on d.ID equals b.DeviceID
                                         where !b.Returned
                                         select d.ID).ToList();
                
                Device = (from d in _context.Device
                          where !borrowedIDs.Contains(d.ID)
                          select d).ToList();
            }
        }

        public IActionResult OnPostBorrow(int memberID, int deviceID)
        {
            List<int> borrowedIDs = (from b in _context.Borrows
                                     where b.MemberID == memberID && !b.Returned
                                     select b.DeviceID).ToList();
            
            Device? device = _context.Device.Find(deviceID);
            if (device == null)
            {
                return RedirectToAction("Get", new { message = "No Device with the ID"});
            }

            if (borrowedIDs.Count != 0) 
            {
                List<ItemType> types = (from d in _context.Device
                                        where borrowedIDs.Contains(d.ID)
                                        select d.ItemType).ToList();

                if (types.Contains(device.ItemType))
                {
                    return RedirectToAction("Get", new { message = "Device of that category has been checked out" });
                }
            }

            _context.Borrows.Add(new Borrows()
            {
                MemberID = memberID,
                DeviceID = deviceID,
                ReturnDate = DateTime.Today.AddMonths(1),
                Returned = false
            });

            _context.SaveChanges();

            return RedirectToAction("Get");
        }
    }
}
