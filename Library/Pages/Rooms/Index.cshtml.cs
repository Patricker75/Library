using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;

namespace Library.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; } = string.Empty;

        private readonly Library.Data.LibraryContext _context;

        public IndexModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; } = default!;

        public void OnGet(string message = "")
        {
            Message = message;

            if (_context.Room != null)
            {
                Room = _context.Room.ToList();
            }
        }

        public IActionResult OnPostReserve(int memberID, int roomID)
        {
            if (_context.Room.Where(r => r.ReserverID == memberID).Any())
            {
                return RedirectToAction("Get", new { message = "You Already Have a Room Reserved" });
            }

            Room room = _context.Room.Where(r => r.ID == roomID).First();
            room.ReserverID = memberID;
            room.IsAvailable = false;

            _context.SaveChanges();
            return RedirectToAction("Get", new { message = "Room Reserved" });

        }
    }
}
